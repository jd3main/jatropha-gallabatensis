using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootStepPlayer : MonoBehaviour
{
    public AudioSource[] sounds;
    public float timeInterval = 0.25f;
    public bool mutable = false;

    float lastPlayTime = 0;
    int index = 0;
    bool muted = false;

    public void Play()
    {
        if (muted) return;
        if(Time.fixedTime - lastPlayTime > timeInterval)
        {
            sounds[index].Play();
            lastPlayTime = Time.fixedTime;
            index = (index + 1) % sounds.Length;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && mutable) muted = !muted;
    }
}
