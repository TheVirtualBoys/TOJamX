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

	private List<RPSFactory.Type> m_queuedThrows = new List<RPSFactory.Type>();

	public int m_health;
	public const int m_maxHealth = 20;
	private bool m_startedDeathEnd;

	public Text m_healthText;
	public Text m_queueText;
	public GameObject m_healthBar;
	public GameObject m_powerBar;

	// Use this for initialization
	public override void Start () {
		base.Start();
		m_health = m_maxHealth;
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

	public bool ReadyToFlush()
	{
		return (Main.QUEUED_THROW_COUNT == m_queuedThrows.Count && m_throws.Count == 0 && m_cancels.Count == 0);
	}

	public void FlushThrows()
	{
		foreach(var thing in m_queuedThrows)
		{
			createProjectile(thing);
		}

		m_queuedThrows.Clear();
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();

		//HACKJEFFGIFFEN //should be dynamic on stick direction per frame
		int targetIndex = 1 - (int)playerIndex; //binary invert :-D
		m_targetPlayer = Main.GetPlayer ( targetIndex );



		string counts = "throws-";
		//eliminate those who are ready
		for ( int i = 0; i < m_throws.Count; i++ )
		{
			GameObject go = m_throws[i];
			ProjectileHandler goPH = go.GetComponent<ProjectileHandler>();
			if ( goPH.m_seppuku )
			{
				if (Mathf.Approximately(goPH.m_currentT, 1.0f))
				{
					AudioHandler.PlaySoundEffect("Hurt" + Random.Range(1, 3)); // second number is exclusive...

					m_targetPlayer.DoDamage();
				}
				else
				{
					AudioHandler.PlaySoundEffect("SmallExplosionTest"); // second number is exclusive...
				}

				returnArcIndex( goPH.m_arc );
				Destroy ( go );
				m_throws.RemoveAt( i );
			}

		}
		counts += m_throws.Count;

		counts += " -cancels-";
		//eliminate those who are ready
		for ( int i = 0; i < m_cancels.Count; i++ )
		{
			GameObject go = m_cancels[i];
			ProjectileHandler goPH = go.GetComponent<ProjectileHandler>();
			if ( goPH.m_seppuku )
			{
				if (Mathf.Approximately(goPH.m_currentT, 1.0f))
				{
					AudioHandler.PlaySoundEffect("Hurt" + Random.Range(1, 3)); // second number is exclusive...
					
					m_targetPlayer.DoDamage();
				}
				else
				{
					AudioHandler.PlaySoundEffect("SmallExplosionTest"); // second number is exclusive...
				}

				returnArcIndex( goPH.m_arc );
				Destroy ( go );
				m_cancels.RemoveAt( i );
			}
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

		//healthbar
		{
			float fullScale = 101.0f * (float)m_health / (float)m_maxHealth;
			Vector3 scale = m_healthBar.transform.localScale;
			if ( scale.x > 0 ) { scale.x = fullScale; }
			else { scale.x = -fullScale; }
			m_healthBar.transform.localScale = scale;
		}

		//powerbar
		{
			Vector3 scale = m_powerBar.transform.localScale;
			scale.y = Mathf.Max(1, 200.0f * (float)m_queuedThrows.Count / (float)Main.QUEUED_THROW_COUNT );
			m_powerBar.transform.localScale = scale;
		}

		if (m_queueText != null)
		{
			m_queueText.text = (Main.QUEUED_THROW_COUNT - m_queuedThrows.Count).ToString(); 
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
			if (m_queuedThrows.Count < Main.QUEUED_THROW_COUNT)
			{
				m_queuedThrows.Add( RPSFactory.Type.Rock );
			}
		}
	}
	
	public override void ThrowPaper()
	{
		if (!IsDead() && !m_targetPlayer.IsDead())
		{
			if (m_queuedThrows.Count < Main.QUEUED_THROW_COUNT)
			{
				m_queuedThrows.Add( RPSFactory.Type.Paper );
			}
		}
	}
	
	public override void ThrowScissors()
	{
		if (!IsDead() && !m_targetPlayer.IsDead())
		{
			if (m_queuedThrows.Count < Main.QUEUED_THROW_COUNT)
			{
				m_queuedThrows.Add( RPSFactory.Type.Scissors );
			}
		}
	}

}