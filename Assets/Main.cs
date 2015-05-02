﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
	//singleton
	static GameObject originalInstance = null;

	//fun datas of funtimes
	public static short numPlayers      = 2;
	public const short MAX_PLAYERS      = 4;
	public static Player[] childPlayers = new Player[Main.MAX_PLAYERS];

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
	}
	
	// Update is called once per frame
	void Update()
	{
		float dt = Time.deltaTime;
		Utils.Update(dt);
	}

	public static void SetPlayerCharacter(PlayerIndex player, CharacterFactory.Characters character)
	{
		childPlayers[(int)player].SetCharacter( character );
	}
}
