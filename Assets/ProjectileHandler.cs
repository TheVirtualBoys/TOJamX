using UnityEngine;
using System.Collections;

public class ProjectileHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce( new Vector2( 0.707f, 0.707f ) * 40, ForceMode2D.Impulse );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}