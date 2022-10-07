using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
	
	public float groundDistance = .4f;
	public float speed = 6f;
	public float gravityMultiplier = 1f;
	public float turnSmoothTime = .1f;
	
	public Animator bunnyAnimator;
	
	float turnSmoothVelocity;
	Vector3 velocity;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	private void moveRelaxed() 
	{
		

		
	}
    
}
