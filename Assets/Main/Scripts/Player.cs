using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Prime31;

public class Player : MonoBehaviour
{
	public enum Button
	{
		UP = 0, DOWN, LEFT, RIGHT, A, B
	}

	public int playerNumber;
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	public Throwable pickedUpObject = null;
	public Boolean isHolding = false;
	public bool isRunning = false;
	public bool jump = false;

	public Vector3 throwForce = new Vector3(5,12,0);

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	Dictionary<Button,string> _buttons;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
		Throwable.onPlayerPickupEnter += onPlayerPickupEnter;
		Throwable.onPlayerPickupExit += onPlayerPickupExit;
	}

	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}

	void onPlayerPickupEnter( int playerNumber,Throwable throwable)
	{
		// getting in the vicinity of a throwable
		pickedUpObject = throwable;
	}

	void onPlayerPickupExit( int playerNumber, Throwable throwable) {
		pickedUpObject = null;
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
		if(col.gameObject.CompareTag("Throwable")){
			
		}
			
	}

	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion

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
		if( _controller.isGrounded )
			_velocity.y = 0;

		if( Input.GetButton( _buttons[Button.RIGHT] ) )
		{
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if (_controller.isGrounded) {
				_animator.Play (Animator.StringToHash ("Run"));
				isRunning = true;

			}
			
		}
		else if( Input.GetButton( _buttons[Button.LEFT] ) )
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if (_controller.isGrounded) {
				_animator.Play (Animator.StringToHash ("Run"));
				isRunning = true;

			}
		}
		else
		{
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Idle" ) );
				isRunning = false;
				jump = false;
		}

		// move pickupObject 
		if(isHolding){
			pickedUpObject.transform.position = new Vector3(transform.position.x+(transform.localScale.x/2),transform.position.y+1,transform.position.z); ;
		}

		// we can only jump whilst grounded
		if( _controller.isGrounded && Input.GetButtonDown( _buttons[Button.A] ) )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			_animator.Play( Animator.StringToHash( "Jump" ) );
			jump = true;
		}


		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if( _controller.isGrounded && Input.GetButton( _buttons[Button.DOWN] ) )
		{
			_velocity.y *= 3f;
			_controller.ignoreOneWayPlatformsThisFrame = true;
		}

		_controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if( Input.GetButtonDown( _buttons[Button.B] ) )
		{
			//TODO: b-button stuff!
			Debug.Log(_buttons[Button.B]);

			if(isHolding){
				// if holding Throwable => Throw
				// calc force
				Vector2 force = new Vector2(transform.localScale.x*throwForce.x, throwForce.y);
				isHolding = false;
				pickedUpObject.Throw(force);
				pickedUpObject = null;
			}else{
				// if near Throwable => Pick up
				if(pickedUpObject != null){
					pickedUpObject.StartHolding();
					isHolding = true;
				// else => Punch
				}else{
					// punch
				}
					
			}
		}
	}

	public void Score()
	{
		pickedUpObject = null;
		isHolding = false;

		Debug.Log ("SCORE!");
	}
}