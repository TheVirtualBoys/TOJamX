using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	public GameObject spritePrefab;

	// Use this for initialization
	void Start () {
		Utils.AddTimer( 1, SpawnPrefab ); //HACKJEFFGIFFEN dummy projectile spawns
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//HACKJEFFGIFFEN dummy projectile spawns
	void SpawnPrefab()
	{
		GameObject clone = (GameObject)Instantiate( spritePrefab );
		clone.transform.parent = transform;
		
		Utils.AddTimer( 1, SpawnPrefab);
	}
}
