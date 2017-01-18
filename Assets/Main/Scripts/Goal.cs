using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goal : MonoBehaviour {

	public static event Action<int> onPlayerScore = delegate{};
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log (col.ToString());
		if (col.CompareTag ("Player")) {
			Player player = col.transform.parent.gameObject.GetComponent<Player> ();
			if (player.isHolding) {
				onPlayerScore (player.playerNumber);

				player.pickedUpObject.transform.position = new Vector3(UnityEngine.Random.Range(-9.0f,9.0f),20.0f,0);
				player.pickedUpObject.Drop ();
				player.Score();
			}

		}
	}
}
