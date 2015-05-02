using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerIndex
{
	One,
	Two,
	Three,
	Four,
	Max
};

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
			if ( timers[i].Update(dt) )
			{	//timer expired
				timers.RemoveAt( i-- ); //counter increment, after i++ it will mean a new timer
			}
		}
	}

	public static void RemoveTimer(Timer t)
	{
		timers.Remove(t);
	}

	public static Timer AddTimer(float time, EmptyCallback callback)
	{
		Timer timer = new Timer(time, callback);
		timers.Add(timer);
		return timer;
	}

	public static Vector2 SplineLerp( Vector2 start, Vector2 middle, Vector2 end, float t )
	{
		return Vector2.Lerp( Vector2.Lerp( start, middle, t ), Vector2.Lerp( middle, end, t ), t );
	}
}
