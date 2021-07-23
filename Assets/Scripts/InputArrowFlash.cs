using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputArrowFlash : MonoBehaviour
{
    public Metronome metronome;

    public SpriteRenderer[] renderers;

    public int nRenderers;

    // Start is called before the first frame update
    void Start()
    {
        metronome = GameObject.Find("Metronome").GetComponent<Metronome>();  
        GameObject[] litArrows = GameObject.FindGameObjectsWithTag("LitArrowSprite");
        renderers = new SpriteRenderer[nRenderers];
        for (int i = 0; i < nRenderers; i++)
        {
            renderers[i] = litArrows[i].GetComponent<SpriteRenderer>();
            renderers[i].color = new Color(1, 1, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (metronome.isNextBeat)
       {
           StartCoroutine("Flash");
       }
    }

    IEnumerator Flash() {
        Color[] original = new Color[4];
        for (int i = 0; i < nRenderers; i++) 
        {
            original[i] = renderers[i].color;
        }

        for (int i = 0; i < metronome.songBpm / 5; i++) {
            for (int j = 0; j < nRenderers; j++) 
            {
                Color tmp = renderers[j].color;
                tmp.a += 5 / metronome.songBpm;
                renderers[j].color = tmp;
            }
            yield return null;
        }

        for (int i = 0; i < metronome.songBpm / 2; i++)
        {
            for (int j = 0; j < nRenderers; j++) 
            { 
                Color tmp = renderers[j].color;
                tmp.a -= 2 / metronome.songBpm;
                renderers[j].color = tmp;
            }
            yield return null;
        }

        for (int i = 0; i < nRenderers; i++) 
        {
            renderers[i].color = original[i];
        }
    }
}
