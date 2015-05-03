using UnityEngine;
using System.Collections;

public class ClearGameData : MonoBehaviour {

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

}
