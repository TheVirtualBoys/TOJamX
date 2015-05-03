using UnityEngine;
using System.Collections;

public class WaitForCharSelect : MonoBehaviour
{
	void Update()
	{
		if (Main.numConfirmed >= Main.numPlayers)
		{
			Application.LoadLevel("Main");
		}
	}
}
