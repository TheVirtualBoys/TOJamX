using UnityEngine;
using System.Collections;

public class TimedSplash : MonoBehaviour
{
	public float secondsToWait = 5.0f;
	public string transitionLevel;
	private Timer timer;

	void Start()
	{
		timer = Utils.AddTimer(secondsToWait, ChangeScene);
	}

	void Update()
	{
		if (Input.anyKeyDown)
		{
			Utils.RemoveTimer(timer);
			Application.LoadLevel(transitionLevel);
		}
	}
	
	void ChangeScene()
	{
		Application.LoadLevel(transitionLevel);
	}
}
