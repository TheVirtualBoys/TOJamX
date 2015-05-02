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

	// Update is called once per frame, returns true if expired
	public bool Update(float dt)
	{
		elapsedTime += dt;
		if (elapsedTime >= triggerTime) {
			callback();
			return true;
		}
		return false;
	}
}
