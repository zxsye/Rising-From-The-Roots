using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gameLost = false;
    public bool gameWon = false;
    public Player player;
    public Health health;
    public EnemyAttacks enemyAttacks;
    public GameOver gameOver;
    public SceneChanger sc;

    void Start()
    {
        Time.timeScale = 1;
        health = player.health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health.health == 0)
        {
            gameLost = true;
            gameOver.Setup();
        }
    }
}
