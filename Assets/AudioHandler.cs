using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioHandler : MonoBehaviour {

	static GameObject originalInstance = null;

	static GameObject s_musicEmitterObject = null;
	static GameObject s_sfxEmitterObject = null;

	static float s_fadeOutTimeSeconds = 0.0f;
	static float s_fadeInTimeSeconds = 0.0f;
	static float s_currentTimeSeconds = 0.0f;
	static bool s_fadingOut = false;
	static AudioClip s_nextMusicToPlay = null;

	public AudioClip[] musicClips;
	public AudioClip[] sfxClips;


	const bool disableAudio = false;

	// Use this for initialization
	void Start()
	{
		//singleton init (and mark to not delete on scene loads)
		if ( originalInstance == null )
		{
			originalInstance = this.gameObject;
			Object.DontDestroyOnLoad( this.gameObject );
			Object.DontDestroyOnLoad( this );

			// load up all bg musics and sfx's. Screw RAM usage, LAOD IT ALL.

			s_musicEmitterObject = new GameObject("BackgroundMusicEmitter");
			Object.DontDestroyOnLoad(s_musicEmitterObject.AddComponent<AudioSource>());
			Object.DontDestroyOnLoad(s_musicEmitterObject);
			s_musicEmitterObject.transform.parent = this.gameObject.transform;

			s_sfxEmitterObject = new GameObject("SFXEmitter");
			Object.DontDestroyOnLoad(s_sfxEmitterObject.AddComponent<AudioSource>());
			Object.DontDestroyOnLoad(s_sfxEmitterObject);
			s_sfxEmitterObject.transform.parent = this.gameObject.transform;
		}
		else //every scene load after the first will make a dupe of the GameObject, so self delete if we're a dupe
		{
			Destroy( this.gameObject );
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (s_nextMusicToPlay == null)
		{
			return;
		}

		if (s_fadingOut)
		{
			if (s_currentTimeSeconds >= s_fadeOutTimeSeconds)
			{
				s_fadingOut = false;
				s_currentTimeSeconds = 0.0f;

				AudioSource source = s_musicEmitterObject.GetComponent<AudioSource>();
				source.loop = true;
				source.clip = s_nextMusicToPlay;
				source.volume = 0.0f;
				source.Play();

				return;
			}
			else
			{
				float ratio = 1.0f - (s_currentTimeSeconds / s_fadeOutTimeSeconds);

				AudioSource source = s_musicEmitterObject.GetComponent<AudioSource>();
				source.volume = ratio;
			}
		}
		else
		{
			if (s_currentTimeSeconds >= s_fadeInTimeSeconds)
			{
				s_nextMusicToPlay = null;

				AudioSource source = s_musicEmitterObject.GetComponent<AudioSource>();
				source.volume = 1.0f;

				s_currentTimeSeconds = 0.0f;

				return;
			}
			else
			{
				float ratio = s_currentTimeSeconds / s_fadeInTimeSeconds;
				
				AudioSource source = s_musicEmitterObject.GetComponent<AudioSource>();
				source.volume = ratio;
			}
		}

		s_currentTimeSeconds += Time.deltaTime;
	}

	public static void PlayMusic(string nameOfAudio)
	{
		PlayMusic (nameOfAudio, 0.0f);
	}

	public static void PlayMusic(string nameOfAudio, float fadeInTimeSeconds)
	{
		PlayMusic(nameOfAudio, fadeInTimeSeconds, 0.0f);
	}

	public static void PlayMusic(string nameOfAudio, float fadeInTimeSeconds, float fadeOutTimeSeconds)
	{
		foreach (var iter in originalInstance.GetComponent<AudioHandler>().musicClips)
		{
			if (iter.name == nameOfAudio)
			{
				PlayMusic(iter, fadeInTimeSeconds, fadeOutTimeSeconds);
			}
		}
	}

	public static void PlayMusic(AudioClip audio)
	{
		PlayMusic (audio, 0.0f);
	}
	
	public static void PlayMusic(AudioClip audio, float fadeInTimeSeconds)
	{
		PlayMusic(audio, fadeInTimeSeconds, 0.0f);
	}
	
	public static void PlayMusic(AudioClip audio, float fadeInTimeSeconds, float fadeOutTimeSeconds)
	{
		if (disableAudio)
			return;

		s_nextMusicToPlay = audio;
		s_fadeInTimeSeconds = fadeInTimeSeconds;
		s_fadeOutTimeSeconds = fadeOutTimeSeconds;
		s_currentTimeSeconds = 0.0f;
		s_fadingOut = true;
	}


	public static void PlaySoundEffect(string nameOfEffect)
	{
		PlaySoundEffect(nameOfEffect, 1.0f);
	}

	public static void PlaySoundEffect(string nameOfEffect, float volume)
	{
		foreach (var iter in originalInstance.GetComponent<AudioHandler>().sfxClips)
		{
			PlaySoundEffect(iter, volume);
		}
	}

	public static void PlaySoundEffect(AudioClip audio)
	{
		PlaySoundEffect(audio, 1.0f);
	}
	
	public static void PlaySoundEffect(AudioClip audio, float volume)
	{
		if (disableAudio)
			return;

		AudioSource source = s_sfxEmitterObject.GetComponent<AudioSource>();
		source.clip = audio; 
		source.loop = false;
		source.PlayOneShot(audio, volume);
	}
}
