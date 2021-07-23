using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Metronome metronome;

    public GameObject[] retrievedNotes;

    private float inputArrowY;


    // Start is called before the first frame update
    void Start()
    {
        metronome = GameObject.Find("Metronome").GetComponent<Metronome>(); 
        inputArrowY = GameObject.Find("InputArrows").GetComponent<Transform>().position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            retrievedNotes = GameObject.FindGameObjectsWithTag("ArrowLeft");
            processNote(retrievedNotes);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            retrievedNotes = GameObject.FindGameObjectsWithTag("ArrowUp");
            processNote(retrievedNotes);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            retrievedNotes = GameObject.FindGameObjectsWithTag("ArrowDown");
            processNote(retrievedNotes);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            retrievedNotes = GameObject.FindGameObjectsWithTag("ArrowRight");
            processNote(retrievedNotes);
        }
    }


    void processNote(GameObject[] retrievedNotes) 
    {
        GameObject closestNote = null;
        double targetDist = 1000;
        foreach (GameObject note in retrievedNotes) 
        {
            if (closestNote == null) 
            {
                closestNote = note;
                targetDist = Math.Abs(note.GetComponent<Transform>().position.y - inputArrowY);
            }
            else
            {
                double noteDist = Math.Abs(note.GetComponent<Transform>().position.y - inputArrowY);
                if (noteDist < targetDist)
                {
                    closestNote = note;
                    targetDist = noteDist; 
                }
            }
        }

        if (closestNote != null) 
        {
            closestNote.GetComponent<FallingArrow>().processHit();
        }
    }
}
