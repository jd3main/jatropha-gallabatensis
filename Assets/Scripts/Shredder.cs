using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shredder : VoteContainer
{
    public float decreasePerSec = 2;
    public float beginTime = -1;
    public int donePieces = 0;
    public AudioSource sfx;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (beginTime == -1 && Votes > 0)
        {
            beginTime = Time.time;
            donePieces = 0;
        }

        if (beginTime != -1)
        {
            animator.SetBool("ON", true);
            sfx.mute = false;
            while (Votes != 0 && donePieces < (Time.time - beginTime) * decreasePerSec)
            {
                donePieces++;
                Votes--;
            }
            if (Votes == 0)
            {
                ClearState();
                animator.SetBool("ON", false);
                sfx.mute = true;
            }
        }
    }

    void ClearState()
    {
        beginTime = -1;
        donePieces = 0;
    }
}
