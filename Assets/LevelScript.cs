using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

//	private float m_elapsedSinceSpawn;

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
			p.gameObject.transform.parent = anchor;
			p.gameObject.transform.position = anchor.position;
		}

//		m_elapsedSinceSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
/*		m_elapsedSinceSpawn += Time.deltaTime;
		while( m_elapsedSinceSpawn >= dbgSpawnWaitDuration )
		{
			m_elapsedSinceSpawn -= dbgSpawnWaitDuration;
			SpawnPrefab();
		}
*/
	}
}
