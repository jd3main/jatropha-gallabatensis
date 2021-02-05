using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioRoom : MonoBehaviour
{
    public string sound1;
    public string sound2;
    bool isPlaying = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlaying) return;
        AudioManager.instance.Play(Random.Range(0.0f, 1.0f) > 0.5f ? sound1 : sound2);
        isPlaying = true;
        StartCoroutine(checkPlaying());
    }

    IEnumerator checkPlaying()
    {
        // I am lazy. Just hard code it unless you want to change the audio clip.
        yield return new WaitForSeconds(6.824f);
        isPlaying = false;
    }
}
