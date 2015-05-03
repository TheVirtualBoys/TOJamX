using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour {

//	private float m_elapsedSinceSpawn;

	bool m_swappingLevels = false;

	// Use this for initialization
	void Start () {

		//init players for this state
		for( int i = 0; i < Main.numPlayers; i++ )
		{
			Player p = Main.GetPlayer ( i );
			//listen to button pushes
			p.InputEnabled( true );
			//anchor them to level spots
			Transform anchor = GameObject.Find ("PlayerAnchor" + i).transform;
			//p.gameObject.transform.parent = anchor;
			p.gameObject.transform.position = anchor.position;

			p.m_healthBar = GameObject.Find ("HealthBar" + (int)p.playerIndex);
			p.m_powerBar = GameObject.Find ( "PowerBar" + (int)p.playerIndex);
			p.m_explosion = GameObject.Find ( "Power_Splosion" + (int)p.playerIndex);
		}

//		m_elapsedSinceSpawn = 0;
	}

	public void ChangeToResults()
	{
		if (!m_swappingLevels)
		{
			m_swappingLevels = true;
			Application.LoadLevel("Results");
		}
	}
	
	// Update is called once per frame
	void Update () {

		bool readyForFlush = true;

		for( int i = 0; i < Main.numPlayers; i++ )
		{
			Player p = Main.GetPlayer(i);

			if (!p.ReadyToFlush())
			{
				readyForFlush = false;
			}

			if (false)
			{
				int rand = Random.Range(0, 3);

				if (rand == 0)
				{
					p.ThrowRock();
				}

				if (rand == 1)
				{
					p.ThrowPaper();
				}

				if (rand == 2)
				{
					p.ThrowScissors();
				}
			}
		}

		if (readyForFlush)
		{
			for( int i = 0; i < Main.numPlayers; i++)
			{
				Player p = Main.GetPlayer(i);

				p.FlushThrows();
			}
		}

/*		m_elapsedSinceSpawn += Time.deltaTime;
		while( m_elapsedSinceSpawn >= dbgSpawnWaitDuration )
		{
			m_elapsedSinceSpawn -= dbgSpawnWaitDuration;
			SpawnPrefab();
		}
*/
	}
}
