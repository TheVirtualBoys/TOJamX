using UnityEngine;
using System.Collections;

public class RPSLogic : MonoBehaviour {

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
	
	public GameObject Create( Type type ) {
		GameObject prefab = null;
		switch( type )
		{
		case Type.Rock: prefab = rockPrefab; break;
		case Type.Paper: prefab = paperPrefab; break;
		case Type.Scissors: prefab = scissorsPrefab; break;
		};
		
		GameObject creation = Instantiate( prefab );
		RPSLogic scriptInst = creation.GetComponent<RPSLogic>();
		scriptInst.m_type = type;
		return (GameObject)creation;
	}
	
	private Type m_type;
	
	public bool equals( RPSLogic other ){
		return m_type == other.m_type;
	}
	//aka >
	public bool beats( RPSLogic other ){
		if ( m_type == Type.Rock && other.m_type == Type.Scissors ) return true;
		if ( m_type == Type.Scissors && other.m_type == Type.Rock ) return false;
		return m_type > other.m_type;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
