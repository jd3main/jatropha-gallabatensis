using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vote : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    /*
    [SerializeField] private int numVotes = 1;
    public int NumVotes
    {
        get { return numVotes; }
        set
        {
            numVotes = value;
        }
    }

    virtual public int TakeVotes()
    {
        return numVotes;
    }

    virtual public void PutVotes(int n)
    {
        numVotes += n;
    }*/
}
