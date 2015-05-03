using UnityEngine;
using System.Collections;



public class Player : GameplayInputHandler
{
	public CharacterFactory.Characters playerClass;
	private GameObject m_childPlayerPrefab = null;

	public Player m_targetPlayer = null;

	// Use this for initialization
	public override void Start () {
		base.Start();
		SetCharacter( playerClass );
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();

		//HACKJEFFGIFFEN //should be dynamic on stick direction per frame
		int targetIndex = 1 - (int)playerIndex; //binary invert :-D
		m_targetPlayer = Main.GetPlayer ( targetIndex );
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
		GameObject thing = RPSFactory.GetInst().Create( type );
		thing.transform.parent = transform;
		ProjectileHandler ph = thing.GetComponent<ProjectileHandler>();

		//set proj arc endpoints
		ph.start = transform.position;
		ph.end = m_targetPlayer.transform.position;
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