using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.gameObject.transform.Translate(Input.GetAxis("Horizontal") * 1.0f, 0.0f, 0.0f);
		this.gameObject.transform.Translate(0.0f, Input.GetAxis("Vertical") * 1.0f, 0.0f);
	}
}
