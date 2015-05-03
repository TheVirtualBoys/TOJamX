using UnityEngine;
using System.Collections;

public class PlayMusicOnSceneStart : MonoBehaviour {

	public AudioClip clipToPlay;

	public float fadeInSeconds = 1.0f;
	public float fadeOutSeconds = 1.0f;

	// Use this for initialization
	void Start () 
	{
		AudioHandler.PlayMusic(clipToPlay, fadeInSeconds, fadeOutSeconds);
	}
}
