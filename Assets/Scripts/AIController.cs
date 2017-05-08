using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CharacterController {

    private Rigidbody2D rb;

	int genotypeSize;

	string[] directions;
	//north east south west northeast southeast southwest northwest and stationary
	public string[] setPath;
	public int startIter;

	double timeInterval;
	double timeIter;

	//Heuristic
	public int killedPlayerCount;
	float timeClosestToPlayer;

    // Use this for initialization
    void Start () {
		Init();    
    }

    void Init()
    {
	    rb = GetComponent<Rigidbody2D>();
		genotypeSize = 8;
		directions = new string[] { "0", "1", "2", "3", "4", "5" , "6", "7", "8"};
		setPath = new string[genotypeSize];

		for(int i = 0; i < genotypeSize; i++){
			setPath[i] = directions[Random.Range(0, directions.Length)];
		}

		startIter = 0;
		timeInterval = 0.5f;
		timeIter = 0.0f;

		killedPlayerCount = 0;
		timeClosestToPlayer = 0.0f;
		dashCoolDown = .75f; // To make it easier for player compared to .5f cooldown for player
	}

	//Ty AP Thomson for this function
	public void simpleShuffle(int[] list) {
		for (int i = 0; i < list.Length*10; i++) {
			int randIndex1 = Random.Range(0, list.Length);
			int randIndex2 = Random.Range(0, list.Length);

			int tempShuffleSpace = list[randIndex1];
			list[randIndex1] = list[randIndex2];
			list[randIndex2] = tempShuffleSpace;
		}
	}

	public void mutate(string[] p1, string[] p2){
		//First parent will have lets say 5-7 out of 8 directions that they will pass down
		int amtGenesPassed = Random.Range(5,8);
		int amtMutation = Random.Range(0,3);
		
		int[] genePosition = new int[] {0,1,2,3,4,5,6,7};
		string[] newGenes = p1;
		simpleShuffle(genePosition);
		
		//Parents genes merge
		for(int i = 0; i<genePosition.Length;i++){
			//These positions will be mutated
			if(i < genotypeSize - amtGenesPassed){
				newGenes[genePosition[i]] = p2[genePosition[i]];							
			}
		}

		//Shuffle again		
		simpleShuffle(genePosition);
		
		//For the slight chance of mutation
		for(int i = 0; i<genePosition.Length;i++){
			//These positions will be mutated
			if(i < amtMutation){
				newGenes[genePosition[i]] = directions[Random.Range(0, directions.Length)];							
			}
		}
		
		setPath = newGenes;
		Debug.Log(newGenes[0]+ "," + newGenes[1]+ "," + newGenes[2]+ "," + newGenes[3]+ "," + newGenes[4]+ "," + newGenes[5]+ "," + newGenes[6]+ "," + newGenes[7]+ "====");
		
	}

    void movement()
    {
		Vector3 moveDirection = new Vector3(0,0,0);

		switch(setPath[startIter]){
			case "0":
				moveDirection += transform.up;
				break;
			case "1":
				moveDirection += transform.right;
				break;
			case "2":
				moveDirection -= transform.up;
				break;
			case "3":
				moveDirection -= transform.right;
				break;

			case "4":
				moveDirection += transform.up + transform.right;
				break;
			case "5":
				moveDirection += transform.right - transform.up;
				break;
			case "6":
				moveDirection -= transform.right + transform.up;
				break;
			case "7":
				moveDirection -= (transform.right - transform.up);
				break;
			case "8":
			default:
				break;
		}

        rb.AddForce(moveDirection * getChaseSpeed());

		//Time for next step
		if(timeIter > timeInterval){
			//Read next step
			startIter = (setPath.Length-2 < startIter) ? 0 : startIter+1;
			timeIter = 0.0f;
		}
		timeIter += Time.deltaTime;
    }

    // Update is called once per frame
    void Update () {
        //Chase player
		if(!isDead){
			GameObject player = GameObject.Find("Player");
			if(Vector3.Distance(player.transform.position, transform.position) < 5.0f){
				//Debug.Log("FOUND ENEMY: " + Vector3.Distance(player.transform.position, transform.position));
				if(canDash){
					dash();
					rb.AddForce((player.transform.position - transform.position).normalized * getDashSpeed(), ForceMode2D.Impulse);
				}
				else{
					rb.AddForce((player.transform.position - transform.position) * getChaseSpeed());
					dashIter += Time.deltaTime;
					
					isAttacking = (dashIter > attackCooldown) ? false : true;

					if(dashCoolDown <= dashIter){
						canDash = true;
						//You can dash again
					}
				}				
				timeClosestToPlayer += Time.deltaTime;
			}
			else{
				movement();
			}		
		}
    }

	float getHeuristic(){
	/*
		being able to kill the player is important
	*/	
		return (isDead) ? (killedPlayerCount * 100) + timeClosestToPlayer - 10 : (killedPlayerCount * 100) + timeClosestToPlayer; 
	}



}
