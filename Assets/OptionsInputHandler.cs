using UnityEngine;
using System.Collections;

public class OptionsInputHandler : MonoBehaviour
{
	public PlayerIndex playerID = PlayerIndex.Max;
	float lastInputTime         = 0.0f;
	float repeatTime            = 0.3f;
	static GameObject[] grid;
	Player player;
	byte selection;

	void Start()
	{
		// destroy Players 3 and 4 "full body" sprites
		if (Main.numPlayers < (int)playerID + 1)
		{
			Destroy(GameObject.Find("ActiveChar" + playerID));
			Destroy(this);
			return;
		}

		player = Main.GetPlayer((int)playerID); //ensures exists etc

		Transform parentTransform = GameObject.Find("ActiveChar" + playerID).transform;
		player.transform.parent = parentTransform;
		player.transform.position = parentTransform.position;

		//just setup the grid once (this script runs char picker)
		if ( grid == null )
		{
			grid = new GameObject[(int)CharacterFactory.Characters.Max];
			for (int i = 0; i < (int)CharacterFactory.Characters.Max; ++i)
			{
				// populate the grid with Char0, Char1... for positioning
				grid[i] = GameObject.Find("Char" + i);
				GameObject character = CharacterFactory.GetInst().Create( (CharacterFactory.Characters)i );
				character.gameObject.transform.parent = grid[i].gameObject.transform;
				character.gameObject.transform.position = grid[i].gameObject.transform.position;
			}
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
		SetPlayerSprite(selection);
	}

	void SetPlayerSprite(byte index)
	{
		if (index >= 0 && index < (int)CharacterFactory.Characters.Max)
		{
			// todo change fullPlayer line.
			//player.sprite                 = grid[(int)index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
			Main.SetPlayerCharacter(playerID, (CharacterFactory.Characters)selection);
			// move the select box to the selected character.
			gameObject.transform.position = grid[selection].transform.position + new Vector3(0.0f, 0.0f, -1.0f);
			return;
		}
		Debug.LogWarning("Tried setting index out of bounds");
	}

	void Update()
	{
		player.InputEnabled( false );

		if (Input.GetKeyDown((KeyCode)((int)KeyCode.Joystick1Button0 + (Utils.JOYSTICK_BUTTON_OFFSET * ((int)playerID))))) // Dear god the casts!
		{
			Main.SetPlayerCharacter(playerID, (CharacterFactory.Characters)selection);
			gameObject.GetComponent<FlashOnSelect>().stopFlashing = true;
			Main.numConfirmed++;
			this.enabled = false;

			AudioHandler.PlaySoundEffect("Select" + Random.Range(2, 4)); // second number is exclusive

			return;
		}
		if ((lastInputTime + repeatTime) > Time.timeSinceLevelLoad)
		{
			return;
		}
		float x            = Input.GetAxisRaw("Horiz" + playerID);
		float y            = Input.GetAxisRaw("Vert" + playerID);
		byte lastSelection = selection;
		#region gross
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
		#endregion // gross
		if (lastSelection != selection)
		{
			// play a sound and update graphics
			lastInputTime = Time.timeSinceLevelLoad;
			SetPlayerSprite(selection);
		}
	}
}

