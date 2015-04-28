using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		this.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce( new Vector2( 0.707f, 0.707f ) * 40, ForceMode2D.Impulse );
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.gameObject.transform.Translate(Input.GetAxis("Horizontal") * 1.0f, 0.0f, 0.0f);
		this.gameObject.transform.Translate(0.0f, Input.GetAxis("Vertical") * 1.0f, 0.0f);
	}
}
