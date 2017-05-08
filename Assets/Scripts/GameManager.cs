using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject enemyPrefab;
    private GameObject playerPrefab;
	private GameObject idleDogPrefab;
	private GameObject capturedDogPrefab;

    public int enemyCount;
    private GameObject[] enemies;
    private Vector2 spawnXLocation;
    private Vector2 spawnYLocation;
	
	private Vector2 flagLocation;
	private Vector2 flagOffset;
	private GameObject playerFlag;
	private GameObject enemyFlag;
	private GameObject capturedDog;
	
	private GameObject player;
    private Vector2 playerSpawn;

	private int level;
	public int numDeaths;

    // Use this for initialization
    void Start () {
        Init();
    }

	void Init()
    {
        //Load prefabs
        playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
		idleDogPrefab = Resources.Load("Prefabs/idle_flag") as GameObject;
		capturedDogPrefab = Resources.Load("Prefabs/captured_dog") as GameObject;

        //Spawn player & set it at starting location
        player = Instantiate(playerPrefab);
		player.name = "Player";
        playerSpawn = new Vector2(-16.0f, -2.0f);
        player.transform.position = playerSpawn;

		//Spawn flags
		flagOffset = new Vector2(.2f,0.0f);

		/*
        playerFlag = Instantiate(idleDogPrefab);
		playerFlag.name = "PlayerFlag";
		playerFlag.transform.position = flagLocation * -1;
		//Changed to just the player capturing flag
		*/

		flagLocation = new Vector2(12.9f, 2f);
        enemyFlag = Instantiate(idleDogPrefab);
		enemyFlag.name = "EnemyFlag";
		enemyFlag.transform.position = flagLocation;

		//Limit enemy spawn locations
        spawnXLocation = new Vector2(6, 17);
        spawnYLocation = new Vector2(0, 3);
        enemies = new GameObject[enemyCount];

        //spawn enemies around their base
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
			enemy.name = "Enemy" + i;
            enemy.transform.position = new Vector2(Random.Range(spawnXLocation.x, spawnXLocation.y + 1), Random.Range(spawnYLocation.x, spawnYLocation.y + 1));
            enemies[i] = enemy;
        }
		
		capturedDog = Instantiate(capturedDogPrefab);
		capturedDog.transform.parent = player.transform;
		capturedDog.name = "CapturedFlag";
		capturedDog.transform.localPosition = new Vector2(0,1);
		capturedDog.SetActive(false);

		level = 0;
		numDeaths = 0;
    }


    void reset()
    {
        //Reset player's spawn position
        player.transform.position = playerSpawn;

		int lowestHeuristic = 999999;
		int lowestIter = 0;

		//Evolve current generation
        for (int i = 0; i < enemyCount; i++)
        {
			if(lowestHeuristic > enemies[i].GetComponent<AIController>().getHeuristic(){
				lowestHeuristic = enemies[i].GetComponent<AIController>().getHeuristic();
				lowestIter = i;
			}
        }


        //Reset enemies position
        for (int i = 0; i < enemyCount; i++)
        {
			enemies[i].GetComponent<CharacterController>().isDead = false;
			enemies[i].SetActive(true);
            enemies[i].transform.position = new Vector2(Random.Range(spawnXLocation.x, spawnXLocation.y + 1), Random.Range(spawnYLocation.x, spawnYLocation.y + 1));
        }

		//Reset flags
		capturedDog.SetActive(false);
		enemyFlag.SetActive(true);
		enemyFlag.transform.position = flagLocation;
		//playerFlag.transform.position = playerSpawn;
    }
	
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reset();
        }
		if(player.GetComponent<CharacterController>().isDead){
			StartCoroutine(RespawnPlayer());
		}
		if(player.GetComponent<CharacterController>().beatLevel){
			level++;			

		}
	}
	IEnumerator RespawnPlayer(){
		numDeaths++;
		if(player.GetComponent<CharacterController>().isHoldingFlag){
			enemyFlag.transform.position = player.transform.position;
		}
		else{
			enemyFlag.transform.position = flagLocation;		
		}
		capturedDog.SetActive(false);
		enemyFlag.SetActive(true);
		yield return new WaitForSeconds(1);
        player.transform.position = playerSpawn;
		player.GetComponent<CharacterController>().Init();
	}
}
