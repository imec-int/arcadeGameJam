using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Throwable : MonoBehaviour {

	public static event Action<int,Throwable> onPlayerPickupEnter = delegate{};
	public static event Action<int, Throwable> onPlayerPickupExit = delegate {};

	Collider2D _triggerCollider;
	Collider2D _throwableCollider;
	Rigidbody2D _rb2D;
	Color _originalColor;
	SpriteRenderer _renderer;

	// Use this for initialization
	void Start () {
		_renderer = GetComponent<SpriteRenderer> ();
		_originalColor = _renderer.color;
		Collider2D[] _collider2D = GetComponents<Collider2D> ();

		if (_collider2D [0].isTrigger) {
			_triggerCollider = _collider2D [0];
			_throwableCollider = _collider2D [1];
		} else {
			_throwableCollider = _collider2D [0];
			_triggerCollider = _collider2D [1];
		}
		_rb2D = GetComponent<Rigidbody2D> ();

		Debug.Log ("test");
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log (col.ToString());
		if (col.CompareTag ("Player")) {
			highlight ();
			Player player = col.transform.parent.gameObject.GetComponent<Player> ();
			onPlayerPickupEnter (player.playerNumber,this);

		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			unhighlight ();
			Player player = col.transform.parent.gameObject.GetComponent<Player> ();
			onPlayerPickupExit (player.playerNumber,this);

		}
	}

	public void StartHolding()
	{
		unhighlight ();
		_throwableCollider.enabled = false;
		_triggerCollider.enabled = false;
	}

	public void Throw(Vector2 force) 
	{
		_throwableCollider.enabled = true;
		_rb2D.AddForce (force, ForceMode2D.Impulse);

	}

	private void highlight() {
		_renderer.color = Color.white;
	}

	private void unhighlight() {
		_renderer.color = _originalColor;
	}

	public void Drop()
	{
		
	}
}
