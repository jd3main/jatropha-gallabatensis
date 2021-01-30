using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHint : MonoBehaviour
{
    public GameObject hintObject;

    private void Start()
    {
        hintObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            hintObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            hintObject.SetActive(false);
        }
    }
}
