using UnityEngine;
using System.Collections;

public class Timer
{
	float triggerTime = 0.0f;
	float elapsedTime = 0.0f;

	// set the callback via
	event Utils.EmptyCallback callback;

	public Timer(float time, Utils.EmptyCallback func)
	{
		triggerTime = time;
		RegisterCallback(func);
	}

	public void RegisterCallback(Utils.EmptyCallback func)
	{
		callback += func;
	}

	public void RemoveCallback(Utils.EmptyCallback func)
	{
		callback -= func;
	}

	// Update is called once per frame
	public void Update(float dt)
	{
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= triggerTime) {
			callback();
		}
	}
}
