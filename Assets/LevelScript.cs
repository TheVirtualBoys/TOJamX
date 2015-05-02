using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	public GameObject spritePrefab;
	public float dbgSpawnWaitDuration;
	private float m_elapsedSinceSpawn;

	// Use this for initialization
	void Start () {
		SpawnPrefab(); //HACKJEFFGIFFEN dummy projectile spawns
		m_elapsedSinceSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		m_elapsedSinceSpawn += Time.deltaTime;
		while( m_elapsedSinceSpawn >= dbgSpawnWaitDuration )
		{
			m_elapsedSinceSpawn -= dbgSpawnWaitDuration;
			SpawnPrefab();
		}

	}

	//HACKJEFFGIFFEN dummy projectile spawns
	void SpawnPrefab()
	{
		GameObject clone = (GameObject)Instantiate( spritePrefab );
		clone.transform.parent = transform;
	}
}
