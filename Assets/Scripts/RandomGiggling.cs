using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGiggling : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TryToGiggle());
    }

    IEnumerator TryToGiggle()
    {
        while (true)
        {
            if (Random.Range(0.0f, 1.0f) < 0.1f) AudioManager.instance.Play(Random.Range(0.0f, 1.0f) > 0.5f ? "Giggle1" : "Giggle2");
            yield return new WaitForSeconds(1);
        }
    }
}
