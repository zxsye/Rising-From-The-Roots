using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public Animator[] attackAnimators;
    public int animatorIndex = 0;

    public Health health;
    public SceneChanger sceneChanger;

    public string trigger = "PreviousDone";

    public GameManager gm;

    public int phase;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        attackAnimators[animatorIndex].SetTrigger(trigger);
        animatorIndex++;
    }


    // Update is called once per frame
    void Update()
    {
        if(gm.gameLost == true)
        {
            gameObject.SetActive(false);
        }
        if (animatorIndex < attackAnimators.Length)
        {
            if (attackAnimators[animatorIndex - 1].GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                attackAnimators[animatorIndex].SetTrigger(trigger);
                animatorIndex++;

            }
        }
        else
        {
            if (attackAnimators[animatorIndex - 1].GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if (gm.gameLost == false)
                {
                    StartCoroutine(waiter());
                }
            }
        }
    }
    IEnumerator waiter()
    {
        //Wait for 2 seconds
        yield return new WaitForSecondsRealtime(2);
        sceneChanger.LoadCutScene(phase);
    }
}
