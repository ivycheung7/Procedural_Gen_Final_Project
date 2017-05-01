using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject enemyPrefab;
    private GameObject playerPrefab;

    public int enemyCount;
    private GameObject[] enemies;
    private Vector2 spawnXLocation;
    private Vector2 spawnYLocation;

    private GameObject player;
    private Vector2 playerSpawn;

    // Use this for initialization
    void Start () {
        Init();
    }

	void Init()
    {
        //Load prefabs
        playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;

        //Spawn player & set it at starting location
        player = Instantiate(playerPrefab);
        playerSpawn = new Vector2(-16.0f, -2.0f);

        player.transform.position = playerSpawn;

        spawnXLocation = new Vector2(6, 17);
        spawnYLocation = new Vector2(0, 3);

        enemies = new GameObject[enemyCount];

        //Create & add enemy to list
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector2(Random.Range(spawnXLocation.x, spawnXLocation.y + 1), Random.Range(spawnYLocation.x, spawnYLocation.y + 1));
            enemies[i] = enemy;
        }

    }

    void reset()
    {
        //Reset player
        player.transform.position = playerSpawn;

        //Reset enemies
        for (int i = 0; i < enemyCount; i++)
        {
            enemies[i].transform.position = new Vector2(Random.Range(spawnXLocation.x, spawnXLocation.y + 1), Random.Range(spawnYLocation.x, spawnYLocation.y + 1));
        }


    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reset();
        }
	}
}
