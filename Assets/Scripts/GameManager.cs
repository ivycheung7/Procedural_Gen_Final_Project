using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject enemyPrefab;
    private GameObject playerPrefab;

    private GameObject enemy1;
    private GameObject player;

    // Use this for initialization
    void Start () {
        Init();
    }

	void Init()
    {
        playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;

        player = Instantiate(playerPrefab);
        player.transform.position = new Vector2(0.0f, 0.0f);

        enemy1 = Instantiate(enemyPrefab);
        enemy1.transform.position = new Vector2(6.0f, 0f);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
