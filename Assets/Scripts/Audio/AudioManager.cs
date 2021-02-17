using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource sheepAudioSource;
    [SerializeField] AudioSource ambientAudioSource;
    [SerializeField] AudioSource effectAudioSource;
    [SerializeField] AudioSource walkingAudioSource;

    [SerializeField] List<AudioClip> sheepSounds;
    [SerializeField] float sheepSoundMinVol = 0.1f;
    [SerializeField] float sheepSoundMaxVol = 0.6f;

    [SerializeField] AudioClip dingSound;
    [SerializeField] float dingVolume = 0.5f;

    public bool AmbientEnabled { get; private set; }
    public bool SoundEnabled { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        AmbientEnabled = true;
        SoundEnabled = true;
    }

    public void PlaySheepSound()
    {
        if (SoundEnabled)
        {
            if (!sheepAudioSource.isPlaying)
            {
                sheepAudioSource.PlayOneShot(sheepSounds[Random.Range(0, sheepSounds.Count)], Random.Range(sheepSoundMinVol, sheepSoundMaxVol));
            }
        }        
    }

    public void PlayWalkingSound(bool isWalking)
    {
        if (SoundEnabled)
        {
            if (isWalking)
            {
                if (!walkingAudioSource.isPlaying)
                {
                    walkingAudioSource.Play();
                }
            }
            else
            {
                if (walkingAudioSource.isPlaying) 
                {
                    walkingAudioSource.Stop();
                }
            }
        }
    }

    public void PlayDingSound()
    {
        if (SoundEnabled)
        {
            effectAudioSource.PlayOneShot(dingSound, dingVolume);
        }
    }


    public void SetAmbient(bool isEnabled)
    {
        AmbientEnabled = isEnabled;
        if (AmbientEnabled)
        {
            ambientAudioSource.Play();
        }
        else
        {
            ambientAudioSource.Stop();
        }
    }

    public void SetSound(bool isEnabled)
    {
        SoundEnabled = isEnabled;
        SetAmbient(isEnabled);
    }

}
