using UnityEngine;
using System.Collections;

public class OptionsInputHandler : MonoBehaviour
{
	public PlayerIndex playerID = PlayerIndex.Max;
	GameObject[] grid;
	float lastInputTime = 0.0f;
	float repeatTime = 0.3f;
	byte selection;

	void Start()
	{
		if (Main.numPlayers < (int)playerID + 1) Destroy(this);
		grid = new GameObject[9];
		for (int i = 0; i < 9; ++i)
		{
			grid[i] = GameObject.Find("Char" + i);
		}

		switch (playerID)
		{
			case PlayerIndex.One:
				selection = 0;
			break;
			case PlayerIndex.Two:
				selection   = 2;
			break;
			case PlayerIndex.Three:
				selection = 6;
			break;
			case PlayerIndex.Four:
				selection  = 8;
			break;
		}
		gameObject.transform.position = grid[selection].transform.position;
		lastInputTime = Time.timeSinceLevelLoad;
	}

	void Update()
	{
		if ((lastInputTime + repeatTime) > Time.timeSinceLevelLoad)
		{
			return;
		}
		float x            = Input.GetAxisRaw("Horiz" + playerID);
		float y            = Input.GetAxisRaw("Vert" + playerID);
		byte lastSelection = selection;

		Debug.Log(playerID + " " + x + "," + y);
		if (x > 0.3f)
		{
			// joystick right
			switch (selection)
			{
				case 0:
					selection = 1;
				break;
				case 1:
					selection = 2;
				break;
				case 2:
					selection = 0;
				break;
				case 3:
					selection = 4;
				break;
				case 4:
					selection = 5;
				break;
				case 5:
					selection = 3;
				break;
				case 6:
					selection = 7;
				break;
				case 7:
					selection = 8;
				break;
				case 8:
					selection = 6;
				break;
			}
		}
		else if (x < -0.3f)
		{
			// joystick left
			switch (selection)
			{
				case 0:
					selection = 2;
				break;
				case 1:
					selection = 0;
				break;
				case 2:
					selection = 1;
				break;
				case 3:
					selection = 5;
				break;
				case 4:
					selection = 3;
				break;
				case 5:
					selection = 4;
				break;
				case 6:
					selection = 8;
				break;
				case 7:
					selection = 6;
				break;
				case 8:
					selection = 7;
				break;
			}
		}
		if (y > 0.3f)
		{
			// joystick up
			switch (selection)
			{
				case 0:
					selection = 8;
				break;
				case 1:
					selection = 5;
				break;
				case 2:
					selection = 8;
				break;
				case 3:
					selection = 0;
				break;
				case 4:
					selection = 0;
				break;
				case 5:
					selection = 1;
				break;
				case 6:
					selection = 3;
				break;
				case 7:
					selection = 3;
				break;
				case 8:
					selection = 4;
				break;
			}
		}
		else if (y < -0.3f)
		{
			// joystick down
			switch (selection)
			{
				case 0:
					selection = 4;
				break;
				case 1:
					selection = 5;
				break;
				case 2:
					selection = 5;
				break;
				case 3:
					selection = 7;
				break;
				case 4:
					selection = 8;
				break;
				case 5:
					selection = 8;
				break;
				case 6:
					selection = 0;
				break;
				case 7:
					selection = 3;
				break;
				case 8:
					selection = 0;
				break;
			}
		}

		if (lastSelection != selection)
		{
			// play a sound and update graphics
			gameObject.transform.position = grid[selection].transform.position;
			lastInputTime = Time.timeSinceLevelLoad;
		}
	}
}
