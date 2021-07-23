using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatmap : MonoBehaviour
{
    public Metronome metronome;

    public string[] arrows;

    public int noteIndex;

    public float[] noteBeats;

    public int[] noteDirections;

    public bool songFinished;

    void Awake()
    {
        ArrayList noteBeatList = new ArrayList();
        ArrayList noteDirectionList = new ArrayList();

        string beatMap = Resources.Load<TextAsset>("RhythmGame").text;
        string[] beatMapArr = beatMap.Split('\n');

        foreach (string noteInfo in beatMapArr)
        {
            string[] infoArr = noteInfo.Split(' ');
            noteBeatList.Add(float.Parse(infoArr[0], CultureInfo.InvariantCulture.NumberFormat) 
                + metronome.audioStartOffset/metronome.secPerBeat);
            infoArr[1] = infoArr[1][0].ToString();
            if (infoArr[1].Equals("L"))
            {
                noteDirectionList.Add(0);
            }
            else if (infoArr[1].Equals("D"))
            {
                noteDirectionList.Add(1);
            }
            else if (infoArr[1].Equals("U"))
            {
                noteDirectionList.Add(2);
            }
            else if (infoArr[1].Equals("R"))
            {
                noteDirectionList.Add(3);
            } 
            else
            {
                // Debug.Log("Note Direction Parse Error");
                noteDirectionList.Add(0);
            }
        }

        noteBeats = (float[]) noteBeatList.ToArray(typeof(float));
        noteDirections = (int[]) noteDirectionList.ToArray(typeof(int));
    }

    void Start()
    {
        arrows = new string[4];
        arrows[0] = "Left";
        arrows[1] = "Down";
        arrows[2] = "Up";
        arrows[3] = "Right";
        metronome = GameObject.Find("Metronome").GetComponent<Metronome>();   
        noteIndex = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (noteIndex >= noteBeats.Length && metronome.songPositionInBeats > noteBeats[noteIndex - 1] + 3) 
        {
            songFinished = true;
        }

        if (!songFinished &&
                noteIndex < noteBeats.Length &&
                noteBeats[noteIndex] < metronome.songPositionInBeats + metronome.beatBuffer) 
        {
            GameObject arrow = Instantiate(Resources.Load<GameObject>(arrows[noteDirections[noteIndex]]));
            FallingArrow arrowScript = arrow.GetComponent<FallingArrow>();
            arrowScript.noteBeat = noteBeats[noteIndex];
            arrowScript.arrowDir = noteDirections[noteIndex];
            noteIndex++;
        }
    }

    /*
    void rollArrowIndex()
    {
        int nextIndex = Random.Range(0, 4);
        if (nextIndex == arrowIndex && noteIndex != 0) {
            if (noteBeats[(noteIndex - 1) % noteBeats.Length] - noteBeats[noteIndex % noteBeats.Length] < 0.0000001f)
            {
                while (nextIndex == arrowIndex)
                {
                    nextIndex = Random.Range(0, 4);
                }
            }
        }
        arrowIndex = nextIndex;
    }
    */
}
