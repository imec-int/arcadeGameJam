using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public string nextSceneName;
	public Text txtReadyP1;
	public Text txtReadyP2;

	bool _p1Ready;
	bool _p2Ready;

	void Start ()
	{
		_p1Ready = false;
		_p2Ready = false;
		StartCoroutine("BlinkCR");
	}

	IEnumerator BlinkCR()
	{
		while (true)
		{
			yield return new WaitForSeconds(1.0f);
			if(!_p1Ready) txtReadyP1.enabled = !txtReadyP1.enabled;
			if(!_p2Ready) txtReadyP2.enabled = !txtReadyP2.enabled;
		}
	}

	void Update ()
	{
		//back button quits the game
		if(Input.GetButtonDown("BACK"))
		{
			Debug.Log("> MainMenu Back Pressed: Quitting Application!");
			Application.Quit();
		}

		//players press button to get "READY"
		if(Input.GetButtonDown("P1_A") || Input.GetButtonDown("P1_B"))
		{
			_p1Ready = true;
			txtReadyP1.enabled = true;
			txtReadyP1.text = "- READY -";
		}

		if(Input.GetButtonDown("P2_A") || Input.GetButtonDown("P2_B"))
		{
			_p2Ready = true;
			txtReadyP2.enabled = true;
			txtReadyP2.text = "- READY -";
		}

		if(_p1Ready && _p2Ready)
		{
			this.enabled = false;
			StopCoroutine("BlinkCR");
			StartCoroutine("StartGameCR");
		}
	}

	IEnumerator StartGameCR()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		SceneManager.LoadScene(nextSceneName);
	}
}
