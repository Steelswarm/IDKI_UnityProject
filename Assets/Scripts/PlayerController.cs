using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	
	public CharacterController controller;	
	public Transform cam;
	public Transform groundCheck;
	public LayerMask groundMask;
	public Animator foxAnimator;
	
	public float groundDistance = .4f;
	public float speed = 6f;
	public float gravityMultiplier = 1f;
	public float turnSmoothTime = .1f;
	public float jumpHeight = 3f;
	
	float gravity = -9.81f;
	float turnSmoothVelocity;
	Vector3 velocity;
	bool isGrounded;
	bool partyMode = false;
	
	
	void Start(){
		Cursor.lockState = CursorLockMode.Locked; //Hides Cursor
		gravity *= gravityMultiplier;
		
	}
	
	
    // Update is called once per frame
    void Update()
	{
		
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		
		if(isGrounded == false)
		{
			foxAnimator.SetBool("Jump_f", false);
		}
		
		if(isGrounded && velocity.y < 0){
			velocity.y = -2f;
		}
		
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
		
		if(direction.magnitude >= 0.1f){
			
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			
			Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			
			foxAnimator.SetBool("playerIsMoving", true);
			controller.Move(moveDirection.normalized * speed * Time.deltaTime);
		} else {foxAnimator.SetBool("playerIsMoving", false);}
		
		
		if(Input.GetButtonDown("Jump") && isGrounded){
			foxAnimator.SetBool("playerIsMoving", false);
			foxAnimator.SetBool("Jump_f", true);
			foxAnimator.SetTrigger("Jump_trigger");
			
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
			
		}
		
		if(Input.GetKeyDown("m") && isGrounded){
			partyMode = !partyMode;
			foxAnimator.SetBool("SPIN!!!", partyMode);
			
			
		}


		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
        
    }
}
