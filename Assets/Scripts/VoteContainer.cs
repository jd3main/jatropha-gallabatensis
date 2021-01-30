using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VoteContainer : MonoBehaviour
{
    [SerializeField] int capacity = 100;
    [SerializeField] int votes = 0;


    public int Votes
    {
        get { return votes; }
        private set { votes = value; }
    }

    public virtual void Place(CharacterController character)
    {
        int n = Mathf.Min(character.votes, capacity-Votes);
        Votes += n;
        character.votes -= n;
        return;
    }

    public virtual void Take(CharacterController character)
    {
        int n = Mathf.Min(character.votes, capacity - Votes);
        Votes += n;
        character.votes -= n;
        return;
    }
}
