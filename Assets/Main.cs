using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	public short numPlayers = 2;
	//public Player players[];
	public GameObject spritePrefab;

	// Use this for initialization
	void Start()
	{
		Object.DontDestroyOnLoad(this);
		Utils.AddTimer( 1, SpawnPrefab );
	}
	
	// Update is called once per frame
	void Update()
	{
		float dt = Time.deltaTime;
		Utils.Update(dt);
	}

	void SpawnPrefab()
	{
		GameObject clone = (GameObject)Instantiate( spritePrefab );
		clone.transform.parent = transform;

		Utils.AddTimer( 1, SpawnPrefab);
	}


}
