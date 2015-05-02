using UnityEngine;
using System.Collections;



public class Player : GameplayInputHandler {



	public Player m_targetPlayer = null;

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	
	}

	public override void ThrowRock()
	{
		GameObject rpsFactory = GameObject.Find("RPS");
		GameObject rock = rpsFactory.GetComponent<RPSLogic>().Create( RPSLogic.Type.Rock );
		rock.transform.parent = transform;

	}
	
	public override void ThrowPaper()
	{
		Debug.Log ("Threw a Paper");
	}
	
	public override void ThrowScissors()
	{
		Debug.Log ("Threw a Scissors");
	}

/*	//HACKJEFFGIFFEN dummy projectile spawns
	void SpawnPrefab(RPS which)
	{
		GameObject prefab;
		switch( which.m_type )
		{
		case RPS.Type.Rock: 
		GameObject clone = (GameObject)Instantiate( spritePrefab );
		clone.transform.parent = transform;
	}
	*/
}