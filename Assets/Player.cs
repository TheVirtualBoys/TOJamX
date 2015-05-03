using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Player : GameplayInputHandler
{
	public CharacterFactory.Characters playerClass;
	public const int arcs = 3;
	private GameObject m_childPlayerPrefab = null;
	private List<GameObject>[] m_throws = new List<GameObject>[arcs]; //one list per arc
	public Player m_targetPlayer = null;


	// Use this for initialization
	public override void Start () {
		base.Start();
		SetCharacter( playerClass );
		for( int i = 0; i < arcs; i++ )
		{
			m_throws[i] = new List<GameObject>();
		}
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();

		//HACKJEFFGIFFEN //should be dynamic on stick direction per frame
		int targetIndex = 1 - (int)playerIndex; //binary invert :-D
		m_targetPlayer = Main.GetPlayer ( targetIndex );



		string counts = "- ";
		for( int i = 0; i < arcs; i++ )
		{
			//eliminate those who are ready
			List<GameObject> throwArc = m_throws[i];
			while( throwArc.Count > 0 && throwArc[0].GetComponent<ProjectileHandler>().m_seppuku )
			{
				Destroy ( throwArc[0] );
				throwArc.RemoveAt( 0 );
			}

			counts += m_throws[i].Count;
			counts += ", ";
		}
		Debug.Log ( counts );

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

	private void createProjectile( RPSFactory.Type type )
	{
		GameObject myGO = RPSFactory.GetInst().Create( type );
		myGO.transform.parent = transform;
		ProjectileHandler myPH = myGO.GetComponent<ProjectileHandler>();
/*		RPSLogic myRPS = myGO.GetComponent<RPSLogic>();

		//figure out if a cancel is possible
		//search for arc with a possible cancel
		int arcToCancel = -1;
		for ( int i = 0; i < arcs; i++ )
		{
			List<GameObject> arc = m_targetPlayer.m_throws[i];
			if ( arc.Count > 0 ) 
			{
				GameObject theirGO = arc[arc.Count - 1];
				RPSLogic theirRPS = theirGO.GetComponent<RPSLogic>();

				if ( myRPS.
*/


		//set proj arc endpoints.  It tells us which arc it went on
		int whichArc = myPH.SetArc( transform.position, m_targetPlayer.transform.position );
		m_throws[ whichArc ].Add ( myGO );
	}

	public override void ThrowRock()
	{
		createProjectile( RPSFactory.Type.Rock );
	}
	
	public override void ThrowPaper()
	{
		createProjectile( RPSFactory.Type.Paper );
	}
	
	public override void ThrowScissors()
	{
		createProjectile( RPSFactory.Type.Scissors );
	}

}