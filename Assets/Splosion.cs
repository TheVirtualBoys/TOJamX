using UnityEngine;
using System.Collections;

public class Splosion : MonoBehaviour {

	public bool enabled;
	private bool on;

	// Use this for initialization
	void Start () {
		on = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool state = false;
		if ( enabled )
		{
			on = !on;
			state = on;
		}
		gameObject.GetComponent<SpriteRenderer>().enabled = state;
	}
}
