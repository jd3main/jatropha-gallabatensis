using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    [Range(0.0f, 1.0f)]
    public float volume;
    public bool loop = false;
    [HideInInspector]
    public AudioSource source;
}
