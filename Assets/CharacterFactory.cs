using UnityEngine;
using System.Collections;

public class CharacterFactory : MonoBehaviour {
	private static GameObject sm_instance = null;
	public static CharacterFactory GetInst() { return sm_instance.GetComponent<CharacterFactory>(); }

	public enum Characters
	{
		Construction,
		Caveman,
		Rocker,
		Paper1,
		Origami,
		Librarian,
		Barber,
		Crabapple,
		Seamstress,
		Max
	};

	public enum CharacterAnim
	{
		Idle,
		Powerup,
		Attack,
		Hit,
		Portrait,
		Max
	}

	public GameObject[] characters = new GameObject[(int)Characters.Max];
	
	void Start()
	{
		//singleton init (and mark to not delete on scene loads)
		if ( sm_instance == null )
		{
			sm_instance = this.gameObject;
			Object.DontDestroyOnLoad( this.gameObject );
			Object.DontDestroyOnLoad( this );
		}
		else //every scene load after the first will make a dupe of the GameObject, so self delete if we're a dupe
		{
			Destroy( this.gameObject );
		}
	}

	public GameObject Create( Characters type ) {
		return (GameObject)Instantiate( characters[ (int)type ] );
	}

	// Update is called once per frame
	void Update () {
	
	}
}
