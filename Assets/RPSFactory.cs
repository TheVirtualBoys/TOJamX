using UnityEngine;
using System.Collections;

public class RPSFactory : MonoBehaviour {

	private static GameObject sm_instance;
	public static RPSFactory GetInst() { return sm_instance.GetComponent<RPSFactory>(); }

	public GameObject rockPrefab;
	public GameObject paperPrefab;
	public GameObject scissorsPrefab;
	public enum Type
	{
		Rock,
		Paper,
		Scissors,
		TypeSize
	};

	// Use this for initialization
	void Start () {
		sm_instance = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public GameObject Create( Type type ) {
		GameObject prefab = null;
		switch( type )
		{
		case Type.Rock: prefab = rockPrefab; break;
		case Type.Paper: prefab = paperPrefab; break;
		case Type.Scissors: prefab = scissorsPrefab; break;
		};
		
		GameObject creation = Instantiate( prefab );
		return (GameObject)creation;
	}
}		