using UnityEngine;
using System.Collections;

public class GroundCharacter : MonoBehaviour {

	//motion parameters;
	public float MovementSpeed;
	public float JumpStrength;

	//ground check parameters;
	public bool UseLineCast;
	public LayerMask WhatIsGround;
	public Collider2D GroundCheck;

	public Rigidbody2D Body;
	public Collider2D shape;
	public Animator animator;

	//input variables;
	private float inputXAxis;
	private bool inputJump;


	void Start () {
		inputJump = false;
		inputXAxis = 0;
		//animator = GetComponent<Animator> ();
	}

	void FixedUpdate(){
		//horizontal input force;
		Body.AddForce (new Vector2 (inputXAxis* MovementSpeed - Body.velocity.x, 0)/ Time.fixedDeltaTime);

		//vertical input force (speed is set);s
		bool onGround = CanJump ();
		
		if (onGround && inputJump) {
			Body.velocity = new Vector2 (Body.velocity.x, JumpStrength);
			//Body.AddForce(new Vector2(0, JumpStrength / Time.fixedDeltaTime));
		}
		inputJump = false;

		//animation parameters;
		if (animator != null) {
			animator.SetFloat("SpeedX",Mathf.Abs(Body.velocity.x));
			animator.SetFloat("SpeedY",Mathf.Abs(Body.velocity.y));
			animator.SetBool("OnGround", onGround);
		}
	}

	public void Move(float intensity){
		inputXAxis = intensity;
	}
	
	public void Jump(){
		inputJump = true;
	}

	private bool CanJump(){
		//use raycast instead;
		Bounds bond = shape.bounds;
		if (Physics2D.Raycast (transform.position, Vector2.down,1.1f * bond.extents.y, WhatIsGround).collider !=null) {
			return true;
		}
		return false;

		if (UseLineCast) {
			Bounds groundbox = GroundCheck.bounds;
			Vector2 topleft = groundbox.min + new Vector3(0, 2*groundbox.extents.y, 0);
			Vector2 lowright = groundbox.max - new Vector3(0, 2*groundbox.extents.y, 0);
			if (Physics2D.Linecast(topleft,groundbox.min,WhatIsGround)
			    || Physics2D.Linecast(groundbox.max,lowright,WhatIsGround)){
				return true;
			} else
				return false;
		}

		if (Physics2D.IsTouchingLayers (GroundCheck)) {
			return true;
		} else
			return false;
	}


}
