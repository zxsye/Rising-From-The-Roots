using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public FillBar progressBar;

    public Beatmap beatmap;

    public bool gameSuccessful;
    public bool GameSuccessful 
    {
        get
        {
            return gameSuccessful;
        }
    }

    public float successThreshold;

    private bool gameEnding;

    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        progressBar = GameObject.Find("Slider").GetComponent<FillBar>(); 
        beatmap = GameObject.Find("Beatmap").GetComponent<Beatmap>(); 
        gameSuccessful = false;
        gameEnding = false;
        successThreshold = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnding && beatmap.songFinished)
        {
            StartCoroutine("EndGame");
        } 

        if (progressBar.CurrentValue > successThreshold)
        {
            progressBar.setSuccess();
        }
        else 
        {
            progressBar.setFail();
        }
    }

    IEnumerator EndGame()
    {
        gameEnding = true;
        if (progressBar.CurrentValue > successThreshold)
        {
            gameSuccessful = true;
        }
        yield return new WaitForSeconds(2);
        Debug.Log("Game Successful: " + gameSuccessful);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}