using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public bool isMusic;
    public AudioClip audClip;

    [Range(0, 1)]
    public float clipVolume;
    [Range(0.1f, 3)]
    public float clipPitch;
    public bool canLoop;

    [Range(-1, 1)]
    public float panStereo;
    [Range(0, 1)]
    public float spacialBlend;
    public int minSoundDistance;
    public int maxSoundDistance;

    [HideInInspector]
    public AudioSource source;
}
