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
	private float initialJumpSpeed = .3f;

	//## FALLING ##//
	private float terminalVelocity = -.8f;
	private bool isFalling = false;
	private float gravityFactor = .02f;	

	//## SNOW COLLECTION ##//
	private float scale = 12f;
	private float scaleDownFactor = .1f;
	private float scaleUpFactor = .015f;
	private float maxScale = 60f;
	private float minScale = 6f;

	bool onDirt;
	bool onSnow;

	private bool inputDisabled = false;

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

	private void Move()
	{
		/*Rigidbody2D rgbd = this.GetComponent <Rigidbody2D>();

		float hInput = Input.GetAxisRaw ("Horizontal");
		if (hInput > 0) {

			rgbd.velocity += new Vector2(.01f * collisionNormal.x, -.01f * collisionNormal.y);

		} else if (hInput < 0) {
			float speedx = rgbd.velocity.x - .01f * collisionNormal.x;
			float speedy = rgbd.velocity.y + .01f * collisionNormal.y;
			rgbd.velocity -= new Vector2((speedx > 0) ? speedx : 0, (speedy < 0) ? speedy : 0);

			Debug.Log (speedx + " " + speedy);

		}
		Debug.Log (rgbd.velocity);*/
	}

	Vector2 jumpVector;
	Vector2 jumpNormal;

	private void Jump()
	{
		bool jumpButtonDown = Input.GetKey ("space");
		bool jumpButtonUp = Input.GetKeyUp ("space");
	
		jumpNormal = collisionNormal;
		
		if (jumpButtonDown && (onDirt || onSnow)) {
			if (!jumping) {
				jumpVector.x = initialJumpSpeed * collisionNormal.x;
				jumpVector.y = initialJumpSpeed * 1.5f * collisionNormal.y;
				jumping = true;
			}
		}
		
		if (jumping) {
			jumpVector.y -= gravityFactor;
			if (jumpVector.y < 0) {
				jumping = false;
				jumpVector.y = 0;
			}
			currentSpeedVector.y = jumpVector.y;
			currentSpeedVector.x = jumpVector.x;
		}
	}



	Vector3 collisionNormal;
	void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		collisionNormal = collisionInfo.contacts[0].normal;
	}
	

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
		jumpVector = new Vector2 ();
		scale = 5f;
	}
}