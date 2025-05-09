using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("LevelBGM");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, (s) => s.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void StopMusicAudio()
    {
        musicSource.Stop();
    }

    public void StopSfxAudio()
    {
        sfxSource.Stop();
    }

    public void PlaySfx(string name)
    {
        Sound sound = Array.Find(sfxSounds, (s) => s.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.clip = sound.clip; 
            sfxSource.Play();
        }
    }
}
