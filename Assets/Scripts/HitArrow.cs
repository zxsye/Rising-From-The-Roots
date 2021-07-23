using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArrow : MonoBehaviour
{
    public Metronome metronome;

    public int expandSpeed;

    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 4;
        metronome = GameObject.Find("Metronome").GetComponent<Metronome>(); 
        expandSpeed = 2;
        StartCoroutine("Expand");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Expand() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        for (int i = 0; i < metronome.songBpm / expandSpeed; i++) {
            transform.localScale += new Vector3(expandSpeed/metronome.songBpm, expandSpeed/metronome.songBpm, expandSpeed/metronome.songBpm);
            Color tmp = sprite.color;
            tmp.a -= expandSpeed / metronome.songBpm;
            sprite.color = tmp;
            yield return null;
        }

        Destroy(gameObject);
    }
}
