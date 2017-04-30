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
    }
    void inputDetection()
    {
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(transform.right * -speed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.right * -dashSpeed, ForceMode2D.Impulse);
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(transform.right * speed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.right * dashSpeed, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * speed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * dashSpeed, ForceMode2D.Impulse);
            }

        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(transform.up * -speed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * -dashSpeed, ForceMode2D.Impulse);
            }
        }

    }
}
