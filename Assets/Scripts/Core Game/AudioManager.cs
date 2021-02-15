using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource sheepAudioSource;
    [SerializeField] AudioSource musicAudioSource;

    [SerializeField] AudioClip sheepSound;    

    public bool MusicEnabled { get; private set; }
    public bool SoundEnabled { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        MusicEnabled = true;
        SoundEnabled = true;
    }

    public void SheepSound(float volume = 0.8f)
    {
        if (SoundEnabled)
        {
            sheepAudioSource.PlayOneShot(sheepSound, volume);
        }
    }

    public void SetMusic(bool isEnabled)
    {
        MusicEnabled = isEnabled;
        if (MusicEnabled)
        {
            musicAudioSource.Play();
        }
        else
        {
            musicAudioSource.Stop();
        }
    }

    public void SetSound(bool isEnabled)
    {
        SoundEnabled = isEnabled;
    }

}
