using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
	//singleton
	static GameObject originalInstance = null;

	//fun datas of funtimes
	public static short numPlayers      = 2;
	public static short numConfirmed 	= 0;
	public const short QUEUED_THROW_COUNT = 5;
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

	public static void Reset()
	{
		numPlayers   = 2;
		numConfirmed = 0;

		for (int i = 0; i < MAX_PLAYERS; ++i)
		{
			childPlayers[i] = null;
		}
	}

	public static Player GetPlayer(int which)
	{
		//if non existant, instantiate players, then enable player input
		for( int i = 0; i <= which; i++ )
		{
			if (Main.childPlayers[i] == null)
			{
				GameObject go = new GameObject("Player" + i);
				childPlayers[i] = go.AddComponent<Player>();
				childPlayers[i].playerIndex = (PlayerIndex)i;
			}
		}
		return childPlayers[which];
	}

		
	public static void SetPlayerCharacter(PlayerIndex player, CharacterFactory.Characters character)
	{
		childPlayers[(int)player].SetCharacter( character );
	}
}
