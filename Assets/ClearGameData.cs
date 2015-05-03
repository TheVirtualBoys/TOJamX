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

	void OnStart()
	{
		if (IsResultScreen)
		{
			GameObject.Find ("Player0").transform.position = GameObject.Find("Player0Anchor").transform.position;
			GameObject.Find ("Player1").transform.position = GameObject.Find("Player1Anchor").transform.position;
		}
	}
}
