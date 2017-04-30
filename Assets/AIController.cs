using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    private float dashSpeed;
    private float speed;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        
    }

    void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        dashSpeed = 30.0f;
        speed = 130.0f;
    }

    // Update is called once per frame
    void Update () {


        //            rb.AddForce(transform.right * -speed);
    }
}
