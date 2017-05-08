using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    
	private Rigidbody2D rb;
	private float dashSpeed = 20.0f;
    private float speed = 70.0f;
	private float chaseSpeed = 30.0f;

	public float dashCoolDown = .5f;
	public float dashIter;
	public bool canDash;

	public float attackCooldown = .3f;
	public bool isAttacking;
	public bool isDead;

	public bool isHoldingFlag;
	public bool beatLevel;
	// Use this for initialization
	void Start () {
		Init();
	}

	public float getDashSpeed(){ return dashSpeed;}
	public float getSpeed(){ return speed;}
	public float getChaseSpeed(){ return chaseSpeed;}

	//Init shouldnt really be public?? but theres a deadline
	public void Init(){
		rb = GetComponent<Rigidbody2D>();
		dashIter = 0;
		canDash = false;
		isAttacking = false;
		isDead = false;
		this.gameObject.SetActive(true);
		isHoldingFlag = false;
		beatLevel = false;
	}	

	// Update is called once per frame
	void Update () {
		

	}
	void OnCollisionEnter2D(Collision2D col)
    {
		if(transform.tag == "Player"){
			if(col.gameObject.transform.tag == "Enemy")
			{
				if(isAttacking){
					if(col.gameObject.GetComponent<CharacterController>().isAttacking){
						//they should bump off against each other
						col.gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - col.transform.position).normalized * getDashSpeed(), ForceMode2D.Impulse);
						rb.AddForce((col.transform.position - transform.position).normalized * getDashSpeed(), ForceMode2D.Impulse);
					}			
					else{
						//Enemy has been killed
						col.gameObject.GetComponent<CharacterController>().isDead = true;
						col.gameObject.SetActive(false);
	//					col.gameObject.GetComponent<Renderer>().material.color = new Color(col.gameObject.GetComponent<Renderer>().material.color.r, col.gameObject.GetComponent<Renderer>().material.color.g, col.gameObject.GetComponent<Renderer>().material.color.b);
						//col.gameObject.GetComponent<Renderer>().material.color = Color.gray;
					}
				}
				//Player has been killed. Whoops
				else if(col.gameObject.GetComponent<CharacterController>().isAttacking){
					col.gameObject.GetComponent<AIController>().killedPlayerCount++;
					isDead = true;
					isHoldingFlag = false;				
					this.gameObject.SetActive(false);
				}
			}
			//Capture flag	
			if(col.gameObject.transform.tag == "Flag"){
				col.gameObject.SetActive(false);
				isHoldingFlag = true;
				this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			}

			if(col.gameObject.transform.tag == "Home"){
				if(isHoldingFlag){
					beatLevel = true;
				}
			}
		}

/*
        else if (transform.tag == "Enemy" && col.gameObject.transform.tag == "Player")
        {

        }
*/
    }

	public void dash(){
		canDash = false;
		isAttacking = true;
		dashIter = 0.0f;
	}
}
