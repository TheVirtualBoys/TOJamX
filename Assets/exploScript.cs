using UnityEngine;
using System.Collections;

public class exploScript : MonoBehaviour {

	private float m_elapsed;
	public float m_duration;
	// Use this for initialization
	void Start () {
		m_elapsed = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_elapsed += Time.deltaTime;
		if ( m_elapsed >= m_duration )
		{
			Destroy( gameObject );
		}
	}
}
