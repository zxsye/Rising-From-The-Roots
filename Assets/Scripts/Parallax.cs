using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float starpos;
    public GameObject camera;
    public float parallax;

    // Start is called before the first frame update
    void Start()
    {
        starpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (camera.transform.position.x * parallax);
        transform.position = new Vector3(starpos + dist, transform.position.y, transform.position.z);
    }
}
