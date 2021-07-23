using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // This is very important if we want to restart the level
public class EnemyCollision : MonoBehaviour
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
    // This function is called every time another collider overlaps the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Checking if the overlapped collider is an enemy
        if (other.CompareTag("Enemy"))
        {
            deathAudio.Play();
            anim = GameObject.FindWithTag("Canvas").GetComponent<Animator>();
            anim.SetTrigger("FadeOut");
            StartCoroutine(Restart("PlatformerScene"));
        }
        if (other.CompareTag("Cave"))
        {
            Destroy(GameObject.FindWithTag("CPS"));
            anim = GameObject.FindWithTag("Canvas").GetComponent<Animator>();
            anim.SetTrigger("FadeOut");
            StartCoroutine(LevelLoad("PlatformerTwoScene"));
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

        float delay = 0.2f;
        float elapsedTime = 0;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(name);
    }
}