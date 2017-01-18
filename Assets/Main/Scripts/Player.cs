using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public enum Button
	{
		UP = 0, DOWN, LEFT, RIGHT, A, B
	}

	public int playerNumber;

	Dictionary<Button,string> _buttons;

	void Start()
	{
		SetupButtonNames();
	}

	void SetupButtonNames()
	{
		_buttons = new Dictionary<Button, string>();
		Button[] enumValues = (Button[])Enum.GetValues(typeof(Button));
		for (int i = 0; i < enumValues.Length; i++)
		{
			_buttons.Add(enumValues[i], "P"+ playerNumber +"_"+enumValues[i]);
		}
	}

	void Update ()
	{
		//Joystick directions
		if( Input.GetButtonDown( _buttons[Button.UP] ) )
		{
			Debug.Log(_buttons[Button.UP]);
		}
		if( Input.GetButtonDown( _buttons[Button.DOWN] ) )
		{
			Debug.Log(_buttons[Button.DOWN]);
		}
		if( Input.GetButtonDown( _buttons[Button.LEFT] ) )
		{
			Debug.Log(_buttons[Button.LEFT]);
		}
		if( Input.GetButtonDown( _buttons[Button.RIGHT] ) )
		{
			Debug.Log(_buttons[Button.RIGHT]);
		}

		//Action buttons
		if( Input.GetButtonDown( _buttons[Button.A] ) )
		{
			Debug.Log(_buttons[Button.A]);
		}
		if( Input.GetButtonDown( _buttons[Button.B] ) )
		{
			Debug.Log(_buttons[Button.B]);
		}
	}
}