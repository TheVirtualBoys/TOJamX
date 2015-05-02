using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	public static short numPlayers = 2;
	//public Player players[];

	// Use this for initialization
	void Start()
	{
		Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update()
	{
		float dt = Time.deltaTime;
		Utils.Update(dt);
	}
}
