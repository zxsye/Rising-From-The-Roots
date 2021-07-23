using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollisionTwo : MonoBehaviour
{
    public Animator anim;
    public AudioSource deathAudio;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Checking if the overlapped collider is an enemy
        if (other.CompareTag("Enemy"))
        {
            deathAudio.Play();
            anim = GameObject.FindWithTag("Canvas").GetComponent<Animator>();
            anim.SetTrigger("FadeOut");
            StartCoroutine(Restart("PlatformerTwoScene"));
        }
        // Checks if collider is end of level
        if (other.CompareTag("Cave"))
        {

            Destroy(GameObject.FindWithTag("CPS"));
            anim = GameObject.FindWithTag("Canvas").GetComponent<Animator>();
            anim.SetTrigger("FadeOut");
            StartCoroutine(LevelLoad("Platformer3Scene"));
        }
    }

    IEnumerator LevelLoad(string name)
    {

        float delay = 1f;
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
        Destroy(GameObject.FindWithTag("Music"));
        SceneManager.LoadScene(name);
    }

    IEnumerator Restart(string name)
    {

        float delay = 0.5f;
        float elapsedTime = 0;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(name);
    }
}