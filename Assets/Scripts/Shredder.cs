using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : VoteContainer
{
    public float decreasePerSec = 2;
    public float beginTime = -1;
    public int donePieces = 0;

    void Update()
    {
        if (beginTime == -1 && Votes > 0)
        {
            beginTime = Time.time;
            donePieces = 0;
        }

        if (beginTime != -1)
        {
            while (Votes != 0 && donePieces < (Time.time - beginTime) * decreasePerSec)
            {
                donePieces++;
                Votes--;
            }
            if (Votes == 0)
            {
                ClearState();
            }
        }
    }

    void ClearState()
    {
        beginTime = -1;
        donePieces = 0;
    }
}
