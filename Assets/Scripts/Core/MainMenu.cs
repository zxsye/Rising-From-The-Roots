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

    public void StartAfterPlatformer() {
        LoadScene("AfterPlatformer");
    }

    public void LoadScene(string title) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(title);
    }

    public void StartAfterRhythm() {
        LoadScene("AfterRhythm");
    }

    public void Start1AfterPhase() {
        LoadScene("1AfterPhase");
    }

    public void Start2AfterPhase() {
        LoadScene("2AfterPhase");
    }

    public void Start3AfterPhase() {
        LoadScene("3AfterPhase");
    }
}