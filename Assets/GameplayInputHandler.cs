using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameplayInputHandler : MonoBehaviour 
{
	// public so you can select what player this script is attached to.
	public PlayerIndex playerIndex = PlayerIndex.Max;

	// Active deadzone is the minimum amount of joystick movement needed to consider any input.
	public float joystickActiveDeadzone = 0.5f;

	// Minimum deadzone is the amount of movement needed to be considered for input after either Horiz or Vert pass the active deadzone.
	public float joystickMinimumDeadzone = 0.3f;

	// 2 deadzones are used to determine we want to move the joystick AND we know if it's going in a diagonal direction on the first check.


	// x and y joystick directions. x and y will either be 0 or +- 1
	private Vector2 m_joystickDirection = new Vector2 (0.0f, 0.0f);

	private List<SimpleButtonPress> m_buttonHandlers = new List<SimpleButtonPress>();

	// Use this for initialization
	public virtual void Start () 
	{
		InputEnabled ( true );
	}

	public void InputEnabled( bool on )
	{
		m_buttonHandlers.Clear();
		if ( on )
		{
			// we create simple button presses so we don't spam the functions when the button is pressed (only called once until the button is released completely)A
			m_buttonHandlers.Add (new SimpleButtonPress ("Player" + playerIndex.ToString () + "FireRock", ThrowRock));
			m_buttonHandlers.Add (new SimpleButtonPress ("Player" + playerIndex.ToString () + "FirePaper", ThrowPaper));
			m_buttonHandlers.Add (new SimpleButtonPress ("Player" + playerIndex.ToString () + "FireScissors", ThrowScissors));
		}
	}
		
	// Update is called once per frame
	public virtual void Update () 
	{
		foreach (SimpleButtonPress btn in m_buttonHandlers)
		{
			btn.Update();
		}

		// check for joystick handling.

		m_joystickDirection.x = 0.0f;
		m_joystickDirection.y = 0.0f;

		float horizontalInput = Input.GetAxis ("Horiz" + playerIndex.ToString());
		float verticalInput = Input.GetAxis ("Vert" + playerIndex.ToString ());

		if (horizontalInput > joystickActiveDeadzone || verticalInput > joystickActiveDeadzone) {

			// we have active joystick input.

			if (Mathf.Abs(horizontalInput) > joystickMinimumDeadzone)
			{
				if (horizontalInput > 0.0f)
				{
					m_joystickDirection.x = 1.0f;
				}
				else
				{
					m_joystickDirection.x = -1.0f;
				}
			}

			if (Mathf.Abs(verticalInput) > joystickMinimumDeadzone)
			{
				if (horizontalInput > 0.0f)
				{
					m_joystickDirection.y = 1.0f;
				}
				else
				{
					m_joystickDirection.y = -1.0f;
				}
			}
		}
	}

	public Vector2 GetJoystickDirection()
	{
		return m_joystickDirection;
	}

	public virtual void ThrowRock()
	{
		Debug.Log ("Threw a rock");
	}

	public virtual void ThrowPaper()
	{
		Debug.Log ("Threw a Paper");
	}

	public virtual void ThrowScissors()
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

