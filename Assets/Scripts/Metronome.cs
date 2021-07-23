using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    // Song beats per minute
    public float songBpm;

    // number of seconds for each song beat
    public float secPerBeat;

    // current song position, seconds
    public float songPosition;

    // curernt song position, in beats
    public float songPositionInBeats;

    // how many seconds elapsed since song started
    public float dspSongTime;

    public bool songStarted;

    public bool isNextBeat;

    public int beatBuffer;

    public float firstBeatOffset;

    public float totalDelay;

    public int notesPressed;

    // AudioSource attached to conductor which plays music
    public AudioSource musicSource;

    public float audioStartOffset;

    private Vector3 scaleChange;


    void Awake() {
        musicSource = GetComponent<AudioSource>();
        musicSource.Stop();
        secPerBeat = 60f / songBpm;
        dspSongTime = (float) AudioSettings.dspTime;
        beatBuffer = 2;
        firstBeatOffset = 2;
        songStarted = false;
        totalDelay = 0.0F;
        notesPressed = 0;
        audioStartOffset = 0.05f;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        float prevPositionInBeats = songPositionInBeats;
        songPositionInBeats = songPosition / secPerBeat;
        isNextBeat = (int) songPositionInBeats - (int) prevPositionInBeats != 0;
        if (!songStarted && songPosition > 0)
        {
            musicSource.Play();
            songStarted = true;
            isNextBeat = true;
        }
    }
}
