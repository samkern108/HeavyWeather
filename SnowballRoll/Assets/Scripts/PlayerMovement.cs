using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public static PlayerMovement inst;

	//## INPUT ##//
	private float vAxis = 0;
	private float hAxis = 0;

	//## ROLLING ##//
	private Vector3 currentSpeedVector;
	private float maxSpeed = .2f;
	private float acceleration = .02f;
	private float deceleration = .02f;

	//##  JUMPING  ##//
	private bool jumping = false;
	private float initialJumpSpeed = .4f;
	private float currentJumpSpeed;

	//## FALLING ##//
	private float terminalVelocity = -.8f;
	private bool isFalling = false;
	private float gravityFactor = .02f;	

	//## SNOW COLLECTION ##//
	private float scale = 5;
	private float scaleDownFactor = .03f;
	private float scaleUpFactor = .015f;
	private float maxScale = 30f;
	private float minScale = .3f;

	bool onDirt;
	bool onSnow;

	private bool inputDisabled = false;
	
	private void Linecast()
	{
		Vector3 linecastDown = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
		onDirt = Physics2D.Linecast (transform.position, linecastDown, 1 << LayerMask.NameToLayer ("Dirt"));
		onSnow = Physics2D.Linecast (transform.position, linecastDown, 1 << LayerMask.NameToLayer ("Snow"));

		if (onSnow) {
			scale += scaleUpFactor;
			if(scale > maxScale) {
				scale = maxScale;
			}
		} else if (onDirt) {
			scale -= scaleDownFactor;
			if(scale < minScale) {
				GameManager.inst.GameOver();
			}
		}
	}

	public void SetInstance()
	{
		inst = this;
	}

	void Update () 
	{
		if (!inputDisabled) {
			vAxis = Input.GetAxis ("Vertical");
			hAxis = Input.GetAxis ("Horizontal");

			Move ();
			Jump ();
			Linecast ();
			transform.position += currentSpeedVector;
			CameraFollow.inst.UpdatePos (transform.position);
            SnowFollow.instance.UpdatePos(transform.position);

			transform.localScale = new Vector3 (scale, scale, 1);
		}
	}

	public void FreezeMovement()
	{
		inputDisabled = true;
		this.GetComponent<Rigidbody2D>().gravityScale = 0;
		this.GetComponent<Rigidbody2D>().velocity = new Vector2();
	}

	public void StartOver()
	{
		inputDisabled = false;
		this.GetComponent<Rigidbody2D>().gravityScale = 1f;
		transform.position = new Vector3 (-9.76f, 7.04f, 0f); //hacky hacky hack
		currentSpeedVector = new Vector3 ();
		jumping = false;
		currentJumpSpeed = 0f;
		scale = 5f;
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
		
		if (jumpButtonDown && (onDirt || onSnow)) {
			if (!jumping) {
				currentJumpSpeed = initialJumpSpeed;
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