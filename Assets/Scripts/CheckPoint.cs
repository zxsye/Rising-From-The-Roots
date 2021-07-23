using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckpointSystem cs;
    SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cs = GameObject.FindGameObjectWithTag("CPS").GetComponent<CheckpointSystem>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cs.lastCheckPoint = transform.position;
            m_SpriteRenderer.color = Color.green;
        }
    }
}
