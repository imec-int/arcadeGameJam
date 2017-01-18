using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
	public Text win;


	public Text p1;
	public Text p2;


	public GameObject player1;
	public GameObject player2;

	private int p1Score = 0;
	private int p2Score = 0;

	// Use this for initialization
	void Start () {
		win.enabled = false;
		Goal.onPlayerScore += Goal_onPlayerScore;
	}

	void Goal_onPlayerScore (int obj)
	{
		if (obj == 1) {
			p1Score += 10;
			p1.text = "Player 1: " + p1Score;
		} else {
			p2Score += 10;
			p2.text = "Player 2: " + p2Score;
		}

		if (p1Score >= 100) {
			EndGame ("Player 1");
		} else if (p2Score >= 100) {
			EndGame ("Player 2");
		}

	}

	void EndGame(string p) {
			player1.SetActive (false);
			player2.SetActive (false);

			win.text = p + " wins!!!";
			win.enabled = true;
			StartCoroutine (EndGameCR());
	}

	IEnumerator EndGameCR()
	{
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
