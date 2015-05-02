using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Characters
{
	Construction,
	Caveman,
	Rocker,
	Paper1,
	Origami,
	Librarian,
	Barber,
	Crabapple,
	Seamstress,
	Max
}

public class Main : MonoBehaviour
{
	//singleton
	static GameObject originalInstance = null;

	//fun datas of funtimes
	public static short numPlayers     = 2;
	static List<Player> childPlayers   = new List<Player>();

	void Start()
	{
		//singleton init (and mark to not delete on scene loads)
		if ( originalInstance == null )
		{
			originalInstance = this.gameObject;
			Object.DontDestroyOnLoad( this.gameObject );
			Object.DontDestroyOnLoad( this );
		}
		else //every scene load after the first will make a dupe of the GameObject, so self delete if we're a dupe
		{
			Destroy( this.gameObject );
		}
		gameObject.GetComponentsInChildren<Player>(true, childPlayers);
	}
	
	// Update is called once per frame
	void Update()
	{
		float dt = Time.deltaTime;
		Utils.Update(dt);
	}

	public static void SetPlayerCharacter(PlayerIndex player, Characters character)
	{
		for (int i = 0;i < childPlayers.Count; ++i)
		{
			if (childPlayers[i].playerIndex == player) {
				childPlayers[i].playerClass = character;
			}
		}
	}
}
