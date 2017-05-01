using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float dashSpeed;
    private float speed;
    private Rigidbody2D rb;
    //private Vector2 


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        dashSpeed = 30.0f;
        speed = 110.0f;
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
            rb.AddForce(moveDirection * speed);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += transform.right;
            rb.AddForce(moveDirection * speed);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += transform.up;
            rb.AddForce(moveDirection * speed);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += -transform.up;
            rb.AddForce(moveDirection * speed);
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(moveDirection * dashSpeed, ForceMode2D.Impulse);
        }
    }
}
