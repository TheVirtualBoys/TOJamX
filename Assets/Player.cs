using UnityEngine;
using System.Collections;



public class Player : GameplayInputHandler
{
	public Characters playerClass = Characters.Max;
	public Player m_targetPlayer = null;

	// Use this for initialization
	public override void Start () {
		base.Start();
		playerClass = Characters.Max;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public override void ThrowRock()
	{
		GameObject thing = RPSFactory.GetInst().Create( RPSFactory.Type.Rock );
		thing.transform.parent = transform;

	}
	
	public override void ThrowPaper()
	{
		GameObject thing = RPSFactory.GetInst().Create( RPSFactory.Type.Paper );
		thing.transform.parent = transform;
	}
	
	public override void ThrowScissors()
	{
		GameObject thing = RPSFactory.GetInst().Create( RPSFactory.Type.Scissors );
		thing.transform.parent = transform;
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