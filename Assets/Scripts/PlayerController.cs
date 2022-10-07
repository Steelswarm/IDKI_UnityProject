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
	
	public AudioSource audioSource;
	public AudioClip goodbye;
	public AudioClip whatDoesTheFoxSay;
	public AudioClip jumpSFX;
	
	public AudioSource themeSFXSource;
	
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
	bool sitDown = false;
	
	
	void Start(){
		Cursor.lockState = CursorLockMode.Locked; //Hides Cursor
		gravity *= gravityMultiplier;
		audioSource = GetComponent<AudioSource>();
		themeSFXSource = GameObject.Find("Player").GetComponent<AudioSource>();
		
	}
	
	
    // Update is called once per frame
    void Update()
	{
		
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Makes a Sphere on bottom of player to check if GameObject is touching the ground
		
		if(isGrounded == false) //Jump animation will only trigger if player is on the ground
		{
			foxAnimator.SetBool("Jump_f", false);
		}
		
		
		if(isGrounded && velocity.y < 0){ //Resets Simulated Gravity
			velocity.y = -2f;
		}
		
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
		
		if(direction.magnitude >= 0.1f){ //Moves player using horizontal and vertical inputs
			
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			
			Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			
			foxAnimator.SetBool("playerIsMoving", true);
			controller.Move(moveDirection.normalized * speed * Time.deltaTime);
		} else {foxAnimator.SetBool("playerIsMoving", false);}
		
		
		if(Input.GetButtonDown("Jump") && isGrounded){ //Makes the player jump
			foxAnimator.SetBool("playerIsMoving", false);
			foxAnimator.SetBool("Jump_f", true);
			foxAnimator.SetBool("isGrounded", false);
			foxAnimator.SetTrigger("Jump_trigger");
			audioSource.PlayOneShot(jumpSFX, 1.0f);
			
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
			
		}
		
		if(Input.GetButtonUp("Jump")){
			foxAnimator.SetBool("isGrounded", true);
			
		}
		
		if(Input.GetKeyDown("m") && isGrounded){ //Easter Egg 1: Foxie dances
			partyMode = !partyMode;
			if(partyMode) audioSource.PlayOneShot(whatDoesTheFoxSay, 1.0f);
			foxAnimator.SetBool("SPIN!!!", partyMode);
			
			
		}
		
		if(Input.GetKeyDown("n") && isGrounded){ //Easter egg 2: Foxie says goodbye
			sitDown = !sitDown;;
			if(sitDown) audioSource.PlayOneShot(goodbye, 1.0f);
			foxAnimator.SetBool("sit", sitDown);
			
			
		}


		velocity.y += gravity * Time.deltaTime; 
		controller.Move(velocity * Time.deltaTime); //Simulates gravity as we are using a Player Controller that does not support Rigidbody
        
    }
}
