using UnityEngine;
using System.Collections;

public class ProjectileHandler : MonoBehaviour {

	public float duration; //Seconds of travel from start to end
	public Vector2 start;
	public Vector2 middle;
	public Vector2 end;
	public int arcs;
	public float variance;
	private float m_startTime;

	// Use this for initialization
	void Start () {
		//this.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce( new Vector2( 0.707f, 0.707f ) * 40, ForceMode2D.Impulse );
		m_startTime = Time.time;

		transform.position = start; //avoid flicker from transform start pos to spline start

		float variation = (float)Random.Range( 0, arcs ) / (float)(arcs - 1); //variation needs to be 0-1 inclusive
		
		//find perpendicular to path
		Vector2 line = end - start;
		line.Normalize();
		Vector2 perpendicular = new Vector2( -line.y, line.x );

		//select which of the arc middles we want
		Vector2 lowVariant = middle - perpendicular * variance;
		Vector2 highVariant = middle + perpendicular * variance;
		middle = Vector2.Lerp( lowVariant, highVariant, variation );
	}
	
	// Update is called once per frame
	void Update () {
		float elapsed = Time.time - m_startTime;
		float arc = Mathf.Min( elapsed / duration, 1.0f );
		Vector2 splinePos = Utils.SplineLerp( start, middle, end, arc );

		transform.position = new Vector3( splinePos.x, splinePos.y, 1.0f );

		if ( arc == 1.0f ) 
		{
			//JEFF, if this projectile is colliding in the middle, use SmallExplosionTest instead

			AudioHandler.PlaySoundEffect("Hurt" + Random.Range(1, 3)); // second number is exclusive...

			Destroy( this.gameObject ); 
		}
	}
}