using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bullets;
    public Player player;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBullets()
    {
        GameObject enemyHard = Instantiate(bullets, new Vector3(10, -1, 0), Quaternion.identity);
        enemyHard.gameObject.transform.position = new Vector3(10, -1, 0);
        Bullet b = enemyHard.GetComponent<Bullet>();
        b.endPos = new Vector3(-7, -7, 0);
        b.player = player;
        b.gm = gm;
    }
}
