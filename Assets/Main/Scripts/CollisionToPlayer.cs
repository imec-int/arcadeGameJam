using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionToPlayer : MonoBehaviour {

	public Player player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.CompareTag ("Book")) {
			player.CollisionFromChild (coll);
		}
	}
}
