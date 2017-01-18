using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Throwable : MonoBehaviour {

	public static event Action<int,Throwable> onPlayerPickupEnter;

	public Transform player1;
	public Transform player2;

	Collider2D _triggerCollider;
	Collider2D _throwableCollider;
	Rigidbody2D _rb2D;

	// Use this for initialization
	void Start () {
		Collider2D[] _collider2D = GetComponents<Collider2D> ();

		if (_collider2D [0].isTrigger) {
			_triggerCollider = _collider2D [0];
			_throwableCollider = _collider2D [1];
		} else {
			_throwableCollider = _collider2D [0];
			_triggerCollider = _collider2D [1];
		}
		_rb2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			//highlight
			//todo

			Player player = col.gameObject.GetComponent<Player> ();
			onPlayerPickupEnter (player.playerNumber,this);

		}
	}

	public void StartHolding()
	{
		_throwableCollider.enabled = false;
		_triggerCollider.enabled = false;
	}

	public void Throw(Vector2 force) 
	{
		_rb2D.AddForce (force, ForceMode2D.Impulse);

	}

	public void Drop()
	{
		
	}
}
