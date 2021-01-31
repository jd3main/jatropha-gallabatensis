using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootStepPlayer : MonoBehaviour
{
    public AudioSource[] sounds;
    public float timeInterval = 0.25f;

    float lastPlayTime = 0;
    int index = 0;

    public void Play()
    {
        if(Time.fixedTime - lastPlayTime > timeInterval)
        {
            sounds[index].Play();
            lastPlayTime = Time.fixedTime;
        }
        
        index = (index + 1) % sounds.Length;
    }

    public void Stop()
    {
        
    }
}
