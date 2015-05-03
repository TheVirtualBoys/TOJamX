using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Player : GameplayInputHandler
{
	public CharacterFactory.Characters playerClass;
	public static int sm_arcs = 3;
	private static List<int> sm_arcIndices = new List<int>();
	private GameObject m_childPlayerPrefab = null;
	private List<GameObject> m_throws;
	private List<GameObject> m_cancels;
	public Player m_targetPlayer = null;

	public int m_health;
	private bool m_startedDeathEnd;

	public Text m_healthText;

	// Use this for initialization
	public override void Start () {
		base.Start();
		m_health = 20;
		m_startedDeathEnd = false;
		SetCharacter( playerClass );
		if ( sm_arcIndices.Count == 0 ) { for( int i = 0; i < sm_arcs; i++ ) { sm_arcIndices.Add ( i ); } }
		m_throws = new List<GameObject>();
		m_cancels = new List<GameObject>();
	}

	static private void returnArcIndex( int index )
	{
		//return only if unique
		for(int i = 0; i < sm_arcIndices.Count; i++ )
		{
			if ( sm_arcIndices[i] == index ) return;
		}
		sm_arcIndices.Add ( index );
	}

	public void DoDamage()
	{
		if (m_health > 0)
		{
			m_health -= 1;
		}
	}

	public bool IsDead()
	{
		return m_health <= 0;
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();

		if (m_healthText == null)
		{
			string handle = "Player" + playerIndex.ToString() + "Health";
			if (GameObject.Find(handle) != null)
			{
				m_healthText = GameObject.Find(handle).GetComponent<Text>();
			}
		}

		//HACKJEFFGIFFEN //should be dynamic on stick direction per frame
		int targetIndex = 1 - (int)playerIndex; //binary invert :-D
		m_targetPlayer = Main.GetPlayer ( targetIndex );



		string counts = "throws-";
		//eliminate those who are ready
		while( m_throws.Count > 0 && m_throws[0].GetComponent<ProjectileHandler>().m_seppuku )
		{
			ProjectileHandler myPH = m_throws[0].GetComponent<ProjectileHandler>();

			if (Mathf.Approximately(myPH.m_currentT, 1.0f))
			{
				AudioHandler.PlaySoundEffect("Hurt" + Random.Range(1, 3)); // second number is exclusive...

				m_targetPlayer.DoDamage();
			}
			else
			{
				AudioHandler.PlaySoundEffect("SmallExplosionTest"); // second number is exclusive...
			}

			returnArcIndex( myPH.m_arc );
			Destroy ( m_throws[0] );
			m_throws.RemoveAt( 0 );
		}
		counts += m_throws.Count;

		counts += " -cancels-";
		//eliminate those who are ready
		while( m_cancels.Count > 0 && m_cancels[0].GetComponent<ProjectileHandler>().m_seppuku )
		{
			returnArcIndex( m_cancels[0].GetComponent<ProjectileHandler>().m_arc );
			Destroy ( m_cancels[0] );
			m_cancels.RemoveAt( 0 );
		}
		counts += m_cancels.Count;

		counts += " Indices = ";

		foreach (var num in sm_arcIndices)
		{
			counts += num + ", ";
		}

		Debug.Log ( counts );

		if (m_healthText != null)
		{
			m_healthText.text = m_health.ToString();
		}


		if (!m_startedDeathEnd && IsDead())
		{
			m_startedDeathEnd = true;
			Utils.AddTimer(5.0f, OnDeathComplete);
		}
	}
		               	
    public void OnDeathComplete()
    {
		Application.LoadLevel("Results");
	}

	public void SetCharacter( CharacterFactory.Characters which )
	{
		if ( playerClass != which || m_childPlayerPrefab == null )
		{
			if ( m_childPlayerPrefab != null ) { Destroy ( m_childPlayerPrefab ); }

			m_childPlayerPrefab = CharacterFactory.GetInst().Create( which );
			m_childPlayerPrefab.gameObject.transform.parent = transform;
			m_childPlayerPrefab.gameObject.transform.position = transform.position;

			playerClass = which;
		}
	}

	private void cancelWinningThrow()
	{
		m_cancels.Add( m_throws[0] ); //move
		m_throws.RemoveAt (0); //clean from throw list
	}

	private void cancelLosingThrow(float killingT)
	{
		GameObject go = m_throws[0];
		ProjectileHandler ph = go.GetComponent<ProjectileHandler>();

		cancelWinningThrow(); //moves it for us

		//set rps to die at time of collide
		//winner & loser share the arc's T=1 travel.  thus 1 >= the sum of their T's, and 1 - their sum gives you remaining flight T
		ph.m_endT = ph.m_currentT + ( 1.0f - killingT - ph.m_currentT ) / 2.0f; //opposing object will hit us at halfway from current to m_endT
	}

	private void createProjectile( RPSFactory.Type type )
	{
		GameObject myGO = RPSFactory.GetInst().Create( type );
		myGO.transform.parent = transform;
		ProjectileHandler myPH = myGO.GetComponent<ProjectileHandler>();
		RPSLogic myRPS = myGO.GetComponent<RPSLogic>();
		//myPH.duration = 20;
		//Decide how to throw based on opponent's airborne throws

		//configure place throw
		m_throws.Add( myGO );

		//with throw placed, as best as possible
		int whichArc;
		if ( m_targetPlayer.m_throws.Count > 0 )
		{
			GameObject theirGO = m_targetPlayer.m_throws[0];
			ProjectileHandler theirPH = theirGO.GetComponent<ProjectileHandler>();
			whichArc = theirPH.m_arc;

			RPSLogic theirRPS = theirGO.GetComponent<RPSLogic>();
			if ( myRPS.beats( theirRPS ) )
			{
				cancelWinningThrow();
				m_targetPlayer.cancelLosingThrow( myPH.m_currentT );
			}
			else if ( myRPS.equals( theirRPS ) )
			{
				cancelLosingThrow( theirPH.m_currentT );
				m_targetPlayer.cancelLosingThrow( myPH.m_currentT );
			}
			else
			{ 
				cancelLosingThrow( theirPH.m_currentT );
				m_targetPlayer.cancelWinningThrow();
			}
		}
		else
		{
			//when there's nothing to aim for, fresh arcs
			if ( sm_arcIndices.Count == 0 ) { sm_arcIndices.Add (sm_arcs++); } //let arc collections grow
			int arcIndex = Random.Range(0, sm_arcIndices.Count);
			whichArc = sm_arcIndices[arcIndex];
			sm_arcIndices.RemoveAt( arcIndex );
		}

		//finally, configure visual path
		myPH.SetArc( whichArc, transform.position, m_targetPlayer.transform.position );
	}

	public override void ThrowRock()
	{
		if (!IsDead() && !m_targetPlayer.IsDead())
		{
			createProjectile( RPSFactory.Type.Rock );
		}
	}
	
	public override void ThrowPaper()
	{
		if (!IsDead() && !m_targetPlayer.IsDead())
		{
			createProjectile( RPSFactory.Type.Paper );
		}
	}
	
	public override void ThrowScissors()
	{
		if (!IsDead() && !m_targetPlayer.IsDead())
		{
			createProjectile( RPSFactory.Type.Scissors );
		}
	}

}