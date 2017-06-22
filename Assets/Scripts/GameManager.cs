using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text levelText;
    public Text deathsText;

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

        //Text
        levelText.text = "Dogs Sheltered: " + level;
        deathsText.text = "Deaths: " + numDeaths;
    }


    void reset()
    {
        //Reset player's spawn position
        player.transform.position = playerSpawn;
        int[] indices = new int[enemyCount];
        List<float> heuristics = new List<float>();
        //Evolve current generation
        //CS Gods pls forgive me for what I have to do since this is pretty much last minute
        for (int i = 0; i < enemyCount; i++)
        {
            heuristics.Add(enemies[i].GetComponent<AIController>().getHeuristic());
        }

        heuristics.Sort();

        for(int i = 0; i < enemyCount; i++)
        {
            for (int j = 0; j < enemyCount; j++)
            {
                if(enemies[i].GetComponent<AIController>().getHeuristic() == heuristics[j])
                {
                    indices[0] = j;
                    break;
                }
            }
        }
        //Indices should be in order now

        int[] validParents = new int[enemyCount];
		//For now, the best performing enemy's genes will be mixed with the rest of the enemies. Aka it gets around.
        for (int i = 0; i < enemyCount; i++) {
			//Last element (worst performing) will be randomly generated.
            if (i == enemyCount - 1)
            {
				enemies[i].GetComponent<AIController>().Init();
				//The last element that needs to be eliminated and replaced with a child frm 1st and 2nd.
                //enemies[i].GetComponent<AIController>().mutate(enemies[indices[0]].GetComponent<AIController>().setPath, enemies[indices[1]].GetComponent<AIController>().setPath);
            }
            else
            {
                if(i != 0)
                {
                    enemies[i].GetComponent<AIController>().mutate(enemies[i].GetComponent<AIController>().setPath, enemies[indices[1]].GetComponent<AIController>().setPath);
                }
                else
                {
                    enemies[i].GetComponent<AIController>().mutate(enemies[i].GetComponent<AIController>().setPath, enemies[indices[0]].GetComponent<AIController>().setPath);
                }
            }
        }

        //One dies, the rest mate with the best performer. However if you change the enemyCount... 

        //Reset enemies position & mutate genetic code
        for (int i = 0; i < enemyCount; i++){
			enemies[i].GetComponent<CharacterController>().isDead = false;
			enemies[i].SetActive(true);
            enemies[i].transform.position = new Vector2(Random.Range(spawnXLocation.x, spawnXLocation.y + 1), Random.Range(spawnYLocation.x, spawnYLocation.y + 1));
        }

		//Reset flags
		capturedDog.SetActive(false);
		enemyFlag.SetActive(true);
		enemyFlag.transform.position = flagLocation;
        //playerFlag.transform.position = playerSpawn;

        //Text
        level++;
        levelText.text = "Dogs Sheltered: " + level;
        deathsText.text = "Deaths: " + numDeaths;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reset();
        }
        if (player.GetComponent<CharacterController>().isDead)
        {
			addDeath();
            StartCoroutine(RespawnPlayer());
        }
        if (player.GetComponent<CharacterController>().beatLevel)
        {
            reset();
            player.GetComponent<CharacterController>().beatLevel = false;
        }
        levelText.text = "Dogs Sheltered: " + level;
        deathsText.text = "Deaths: " + numDeaths;
    }

    public void addDeath()
    {
        numDeaths++;
    }

    IEnumerator RespawnPlayer(){
		player.GetComponent<CharacterController>().isDead = false;

        if (player.GetComponent<CharacterController>().isHoldingFlag){
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
