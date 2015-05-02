using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MovementHandler : MonoBehaviour 
{
	// public so you can select what player this script is attached to.
	public PlayerIndex playerIndex = PlayerIndex.One;


	private List<SimpleButtonPress> m_buttonHandlers = new List<SimpleButtonPress>();

	// Use this for initialization
	void Start () 
	{
			// we create simple button presses so we don't spam the functions when the button is pressed (only called once until the button is released completely)
		m_buttonHandlers.Add (new SimpleButtonPress ("Player" + playerIndex.ToString () + "FireRock", ThrowRock));
		m_buttonHandlers.Add (new SimpleButtonPress ("Player" + playerIndex.ToString () + "FirePaper", ThrowPaper));
		m_buttonHandlers.Add (new SimpleButtonPress ("Player" + playerIndex.ToString () + "FireScissors", ThrowScissors));
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (SimpleButtonPress btn in m_buttonHandlers)
		{
			btn.Update();
		}
	}

	void ThrowRock()
	{
		Debug.Log ("Threw a rock");
	}

	void ThrowPaper()
	{
		Debug.Log ("Threw a Paper");
	}

	void ThrowScissors()
	{
		Debug.Log ("Threw a Scissors");
	}
}

// not..... really sure where to put this >_>
public class SimpleButtonPress
{
	public delegate void CallbackFunction();
	
	private CallbackFunction 	m_function;
	private string 				m_inputAxis;
	private bool				m_isPressed;
	
	
	public bool IsPressed 		{ get { return m_isPressed; } }
	
	public SimpleButtonPress(string inputAxis, CallbackFunction func)
	{
		m_function = func;
		m_inputAxis = inputAxis;
	}
	
	public void Update()
	{
		float inputValue = Input.GetAxisRaw (m_inputAxis);
		
		if (!IsPressed) 
		{
			if (Mathf.Approximately(inputValue, 1.0f))
			{
				Fire();
			}
		}
		else
		{
			if (Mathf.Approximately(inputValue, 0.0f))
			{
				m_isPressed = false;
			}
		}
	}
	
	void Fire()
	{
		m_isPressed = true;
		
		if (m_function != null) 
		{
			m_function();
		}
	}
}

