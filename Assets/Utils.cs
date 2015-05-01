using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utils
{
	public delegate void EmptyCallback();
	public delegate void CallbackEvent<T>(T value);
	public delegate T ResultEvent<out T>();
	private static List<Timer> timers = new List<Timer>();

	public static void Update(float dt)
	{
		for (int i = 0; i < timers.Count; ++i)
		{
			timers[i].Update(dt);
		}
	}

	public static Timer AddTimer(float time, EmptyCallback callback)
	{
		Timer timer = new Timer(time, callback);
		timers.Add(timer);
		return timer;
	}
}
