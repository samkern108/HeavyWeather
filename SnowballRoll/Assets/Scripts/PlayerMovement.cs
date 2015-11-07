using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//## INPUT ##//
	private float vAxis = 0;
	private float hAxis = 0;
	private bool boosting = false;

	//## ROLLING ##//
	private Vector3 currentSpeedVector;
	private float boostSpeed = .4f;
	private float maxSpeed = .2f;
	private float acceleration = .02f;
	private float deceleration = .02f;

	//##  JUMPING  ##//
	private bool jumping = false;
	private float initialJumpSpeed = .4f;
	private float boostJumpSpeed = .6f;
	private float currentJumpSpeed;

	//## FALLING ##//
	private float terminalVelocity = -.8f;
	private bool isFalling = false;
	private float gravityFactor = .02f;	

	//## SNOW COLLECTION ##//
	private int snow = 0;
	private int scale = 1;

	bool onDirt;
	bool onSnow;
	
	private void Linecast()
	{
		Vector3 linecastDown = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
		onDirt = Physics2D.Linecast (transform.position, linecastDown, 1 << LayerMask.NameToLayer ("Dirt"));
		onSnow = Physics2D.Linecast (transform.position, linecastDown, 1 << LayerMask.NameToLayer ("Snow"));
	}


	void Update () 
	{
//		boosting = Input.GetKey ("z");
		vAxis = Input.GetAxis ("Vertical");
		hAxis = Input.GetAxis ("Horizontal");

		Move ();
		Jump ();
		Linecast ();
		transform.position += currentSpeedVector;
		CameraFollow.inst.UpdatePos (transform.position);
	}

	private void Move()
	{
		float hInput = Input.GetAxisRaw ("Horizontal");
		if (hInput > 0) {
			currentSpeedVector.x += Mathf.Sign (hInput) * acceleration;
			if (currentSpeedVector.x > maxSpeed || currentSpeedVector.x < -maxSpeed) {
				currentSpeedVector.x = Mathf.Sign (hInput) * maxSpeed;
			}
		} else if (hInput < 0) {
			currentSpeedVector.x -= deceleration;
			if(currentSpeedVector.x < 0) {
				currentSpeedVector.x = 0;
			}
		}
	}

	private void Jump()
	{
		bool jumpButtonDown = Input.GetKey ("space");
		bool jumpButtonUp = Input.GetKeyUp ("space");
		
		if (jumpButtonDown) {
			if (!jumping) {
				if (boosting) {
					currentJumpSpeed = boostJumpSpeed;
				} else {
					currentJumpSpeed = initialJumpSpeed;
				}
				jumping = true;
			}
		} else if (jumpButtonUp && currentJumpSpeed > 0) {
			currentJumpSpeed -= gravityFactor;
			if (currentJumpSpeed < 0) {
				jumping = false;
				currentJumpSpeed = 0;
			}
		}
		
		if (jumping) {
			currentJumpSpeed -= gravityFactor;
			if (currentJumpSpeed < 0) {
				jumping = false;
				currentJumpSpeed = 0;
			}
			currentSpeedVector.y = currentJumpSpeed;
		}
	}
}