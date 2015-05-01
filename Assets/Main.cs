using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	public GameObject spritePrefab;

	// Use this for initialization
	void Start()
	{
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
