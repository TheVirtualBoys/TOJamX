using UnityEngine;
using System.Collections;

public class RPSLogic : MonoBehaviour {

	public RPSFactory.Type m_type;
	
	public bool equals( RPSLogic other ){
		return m_type == other.m_type;
	}
	//aka >
	public bool beats( RPSLogic other ){
		if ( m_type == RPSFactory.Type.Rock && other.m_type == RPSFactory.Type.Scissors ) return true;
		if ( m_type == RPSFactory.Type.Scissors && other.m_type == RPSFactory.Type.Rock ) return false;
		return m_type > other.m_type;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
