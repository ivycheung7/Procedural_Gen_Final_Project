using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController {

    private Rigidbody2D rb;
    //private Vector2 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        inputDetection();
//        Debug.Log("VELOCITY: " + rb.velocity);
    }

    void inputDetection()
    {
        Vector3 moveDirection = new Vector3(0,0,0);

        //How to use switch with getkey
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += -transform.right;
			rb.AddForce(moveDirection * getSpeed());
		}
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += transform.right;
        	rb.AddForce(moveDirection * getSpeed());
		}

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += transform.up;
        	rb.AddForce(moveDirection * getSpeed());
		}
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += -transform.up;
        	rb.AddForce(moveDirection * getSpeed());
		}

		//Dash
		if(canDash){
			if (Input.GetKeyDown(KeyCode.Space)){
				dash();
				rb.AddForce(moveDirection.normalized * getDashSpeed(), ForceMode2D.Impulse);
			}
		}
		else{
			dashIter += Time.deltaTime;
			
			isAttacking = (dashIter > attackCooldown) ? false : true;

			if(dashCoolDown <= dashIter){
				canDash = true;
				//You can dash again
			}
		}
    }
}
