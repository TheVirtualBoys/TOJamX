using UnityEngine;
using System.Collections;

public class ClearGameData : MonoBehaviour {

	public bool IsResultScreen = false;

	// Use this for initialization
	void OnDestroy () 
	{
		Main.Reset();

		OptionsInputHandler.grid = null;

		for (int i = 0; i < Main.MAX_PLAYERS; ++i)
		{
			GameObject go = GameObject.Find("Player" + i);
			if (go != null)
			{
				GameObject.Destroy(go);
			}
		}
	}

	void Start()
	{
		if (IsResultScreen)
		{
			//init players for this state
			for( int i = 0; i < Main.numPlayers; i++ )
			{
				Player p = Main.GetPlayer ( i );

				Transform anchor = GameObject.Find ("Player" + i + "Anchor").transform;
				//p.gameObject.transform.parent = anchor;
				p.gameObject.transform.position = anchor.position;
			}
		}
	}
}
