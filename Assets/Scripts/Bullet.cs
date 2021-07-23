using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Player player;
    public GameManager gm;

    public Transform target;
    private bool stop;
    private bool start;
    public Vector3 startPos = new Vector3(10, -1, 0);
    public Vector3 endPos = new Vector3(-7, -7, 0);


    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        start = false;
        target = player.transform;
        gameObject.SetActive(true);
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameLost == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (!stop)
            {
                Quaternion rotation = Quaternion.LookRotation
                 (target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation
                 (transform.position - endPos, transform.TransformDirection(Vector3.up));
                transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(2);
        start = true;
        yield return new WaitForSecondsRealtime(10);
        stop = true;
        yield return new WaitForSecondsRealtime(5);
        gameObject.SetActive(false);
    }
}
