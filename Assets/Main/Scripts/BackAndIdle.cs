using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackAndIdle : MonoBehaviour
{
	static string BTN_BACK = "BACK";
	static string[] IDLE_BUTTONS = {	"P1_UP", "P1_DOWN", "P1_LEFT", "P1_RIGHT", "P1_A", "P1_B",
										"P2_UP", "P2_DOWN", "P2_LEFT", "P2_RIGHT", "P2_A", "P2_B" };

	public string backSceneName = "MainMenu";
	public float idleTimeout = 120.0f;

	float _idleTime;

	void Start()
	{
		_idleTime = 0.0f;
	}

	void Update ()
	{
		//back button returns to main menu
		if(Input.GetButtonDown(BTN_BACK))
		{
			Debug.Log("BackAndIdle > BACK Pressed: returning to main menu.");
			SceneManager.LoadScene(backSceneName);
		}

		//when idling for too long: return to main menu
		_idleTime += Time.unscaledDeltaTime;
		if(_idleTime > idleTimeout)
		{
			Debug.Log("BackAndIdle > game idle for "+ idleTimeout +" seconds: returning to main menu.");
			SceneManager.LoadScene(backSceneName);
		}

		for (int i = 0; i < IDLE_BUTTONS.Length; i++)
		{
			if( Input.GetButtonDown(IDLE_BUTTONS[i]) )
			{
				_idleTime = 0.0f;
			}
		}
	}
}
