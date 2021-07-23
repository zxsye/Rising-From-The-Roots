using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedAnimation : MonoBehaviour
{
    public Animator animator;

    public AnimatorStateInfo animatorStateInfo;

    public int currentState;

    public Metronome metronome;

    public float animSpeedMultiplier;

    private float animStartBeat;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        currentState = animatorStateInfo.fullPathHash;
        metronome = GameObject.Find("Metronome").GetComponent<Metronome>();
        animStartBeat = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (metronome.songPositionInBeats - animStartBeat >= 1/animSpeedMultiplier)
        {
            animStartBeat = metronome.songPositionInBeats;
        }
        animator.Play(currentState, -1, (metronome.songPositionInBeats - animStartBeat) * animSpeedMultiplier);
        animator.speed = 0;
    }
}
