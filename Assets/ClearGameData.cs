using UnityEngine;
using System.Collections;

public class ClearGameData : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Main.Reset();

		OptionsInputHandler.grid = null;
	}

}
