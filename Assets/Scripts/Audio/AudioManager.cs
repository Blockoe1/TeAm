using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public float musicVolume;
    [SerializeField] AudioMixerGroup masterMixer;
    [SerializeField] private float timeBetweenFootsteps = .05f;
    [SerializeField] AudioClip c;

    private int footstepTrackCount;
    private int pickaxeTrackCount;

    private Coroutine footsteps;
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayMusic("s");
        }

        foreach (Sound sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();


            sound.source.clip = sound.audClip;
            sound.source.volume = sound.clipVolume;
            sound.source.pitch = sound.clipPitch;
            sound.source.loop = sound.canLoop;
            sound.source.panStereo = sound.panStereo;
            sound.source.spatialBlend = sound.spacialBlend;
            sound.source.minDistance = sound.minSoundDistance;
            sound.source.maxDistance = sound.maxSoundDistance;
            sound.source.rolloffMode = AudioRolloffMode.Linear;
            sound.source.playOnAwake = false;

            if(sound.name.Contains("Footstep"))
                footstepTrackCount++;
            if (sound.name.Contains("Pickaxe"))
                pickaxeTrackCount++;
        }


    }


    public void Play(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Play();
        }
    }

    public void Stop(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Stop();
        }

    }

    public void StopAllSounds()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source.Stop();
        }
    }

    public void PlayMusic(string s)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].isMusic)
                Sounds[i].source.Stop();
        }
        Play(s);

    }

    public void PlayFootsteps()
    {
        if(footsteps!=null)
        {
            return;
        }
        footsteps = StartCoroutine(Footsteps());
    }

    //???
    public void StopFootsteps()
    {
        if (footsteps != null)
        {
            StopCoroutine(footsteps);
            footsteps = null;
        }
    }

    private IEnumerator Footsteps()
    {
        while(true)
        {
            int footstepTrack = UnityEngine.Random.Range(1, footstepTrackCount);
            Play("Footstep" + footstepTrack);
            yield return new WaitForSeconds(timeBetweenFootsteps);
        }
    }

    public void PlayPickaxe()
    {
        int track = UnityEngine.Random.Range(1, pickaxeTrackCount);
        Play("Pickaxe" + track);
    }

    public void CanaryDeathMusic()
    {
        PlayMusic("Death");
    }

    public void CanaryLivingMusic()
    {
        PlayMusic("Alive");
    }

}
