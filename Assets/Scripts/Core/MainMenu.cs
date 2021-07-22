using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    
    // Start is called before the first frame update
    void Awake() {
        instance = this;
    }

    void Start()
    {

    }

    public void StartNewGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StoryBoard");
    }
}
