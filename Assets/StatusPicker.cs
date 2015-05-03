using UnityEngine;
using System.Collections;

public class StatusPicker : MonoBehaviour {

	public PlayerIndex player;

	public Sprite winSprite;
	public Sprite loseSprite;

	// Use this for initialization
	void Start () {
		Sprite spr = winSprite;

		if (Main.GetPlayer((int)player).IsDead())
		{
			spr = loseSprite;
		}

		GetComponent<SpriteRenderer>().sprite = spr;
	}
}
