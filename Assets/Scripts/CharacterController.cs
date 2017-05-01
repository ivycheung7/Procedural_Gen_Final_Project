using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Collision2D(Collider2D col)
    {
        if(transform.tag == "Player" && col.transform.tag == "Enemy")
        {
            //if(GetComponent<Rigidbody2D>().velocity < col.GetComponent<Rigidbody2D>().velocity)
            //{

            //}
        }
        else if (transform.tag == "Enemy" && col.transform.tag == "Player")
        {

        }

    }
}
