using UnityEngine;
using System.Collections;

public class SwitchScreenOnInput : MonoBehaviour {

	public string [] acceptedInputs;
	public string levelToLoad;

	public AudioClip sound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (string input in acceptedInputs)
		{
			if (Input.GetKeyDown(input))
			{
				if (sound != null)
				{
					AudioHandler.PlaySoundEffect(sound);
				}

				Application.LoadLevel(levelToLoad);
			}
		}
	}
}
