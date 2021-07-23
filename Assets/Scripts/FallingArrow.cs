using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingArrow : MonoBehaviour
{
    public Metronome metronome;

    public Vector3 spawnPos;

    public Vector3 targetPos;

    public Vector3 removePos;

    public float noteBeat;

    public string[] hitArrows;

    public int arrowDir;

    public FillBar fillBar;

    private double timeTolerance = 0.10f;

    // Start is called before the first frame update
    void Start()
    {
        metronome = GameObject.Find("Metronome").GetComponent<Metronome>();
        fillBar = GameObject.Find("Slider").GetComponent<FillBar>();
        hitArrows = new string[4];
        hitArrows[0] = "LeftHit";
        hitArrows[1] = "DownHit";
        hitArrows[2] = "UpHit";
        hitArrows[3] = "RightHit";
        string inputName;
        switch (arrowDir) 
        {
            case 0:
                inputName = "LeftArrow";
                break;
            case 1:
                inputName = "DownArrow";
                break;
            case 2:
                inputName = "UpArrow";
                break;
            case 3:
                inputName = "RightArrow";
                break;
            default:
                Debug.Log("arrowDir not set!");
                inputName = "error";
                break;
        }
        targetPos = GameObject.Find(inputName).GetComponent<Transform>().position;
        targetPos.z -= 2;
        spawnPos = targetPos + new Vector3(0, 10, 0);
        transform.position = spawnPos;
        removePos = targetPos + new Vector3(0, -10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > targetPos.y)
        {
            transform.position = Vector3.Lerp(
                spawnPos,
                targetPos,
                (metronome.beatBuffer - (noteBeat - metronome.songPositionInBeats)) / metronome.beatBuffer
            );
        }
        else if (transform.position.y <= targetPos.y && transform.position.y > -6)
        {
            transform.position = Vector3.Lerp(
                targetPos,
                removePos,
                -(noteBeat - metronome.songPositionInBeats) / metronome.beatBuffer
            );
        }
        else
        {
            reportMissed();
        }
    }

    public void processHit()
    {
        double timeToNote = (metronome.songPositionInBeats - noteBeat) * metronome.secPerBeat;
        metronome.totalDelay += (float)timeToNote;
        metronome.notesPressed++;
        // Debug.Log("Avg Delay: " + metronome.totalDelay/metronome.notesPressed + "s");
        if (Math.Abs(timeToNote) < timeTolerance)
        {
            reportHit();
        }
    }

    public void reportHit() {
        fillBar.noteHit();
        Instantiate(Resources.Load<GameObject>(hitArrows[arrowDir]), transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void reportMissed() {
        fillBar.noteMissed();
        Destroy(gameObject);
    }
}
