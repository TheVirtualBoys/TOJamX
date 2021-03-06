﻿using UnityEngine;
using System.Collections;

public class ProjectileHandler : MonoBehaviour {

	public float duration; //Seconds of travel from start to end
	private Vector2 m_start;
	public Vector2 middle;
	private Vector2 m_end;
	public float variance;
	private float m_startTime;
	public float m_currentT;
	public float m_endT;
	public bool m_seppuku;
	public int m_arc;

	// Use this for initialization
	void Start () {
		//this.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce( new Vector2( 0.707f, 0.707f ) * 40, ForceMode2D.Impulse );
		m_startTime = Time.time;
		m_seppuku = false;
	}

	public void SetArc( int whichArc, Vector2 start, Vector2 end )
	{
		m_start = start; //avoid flicker from transform start pos to spline start
		transform.position = start;
		m_end = end;

		m_arc = whichArc;
		float variation = (float)whichArc / (float)(Player.sm_arcs - 1); //variation needs to be 0-1 inclusive
		
		//find perpendicular to path
		Vector2 line = end - start;
		line.Normalize();
		Vector2 perpendicular = new Vector2( -line.y, line.x );
		//to ensure perpendicular is the same whether arc is viewed a-b or b-a, prefer y positive perpendiculars.
		if ( perpendicular.y < 0.0f ) { perpendicular = -perpendicular; }

		//select which of the arc middles we want
		Vector2 lowVariant = middle - perpendicular * variance;
		Vector2 highVariant = middle + perpendicular * variance;
		middle = Vector2.Lerp( lowVariant, highVariant, variation );
	}

	// Update is called once per frame
	void Update () {
		float elapsed = Time.time - m_startTime;
		m_currentT = Mathf.Min( elapsed / duration, m_endT );
		Vector2 splinePos = Utils.SplineLerp( m_start, middle, m_end, m_currentT );

		transform.position = new Vector3( splinePos.x, splinePos.y, 1.0f );

		//if we're done set the flag were ready for honorable death
		if ( m_currentT >= m_endT )
		{
			m_seppuku = true;
		}
	}
}