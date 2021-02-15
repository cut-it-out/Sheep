using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource sheepAudioSource;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource ambientAudioSource;
    [SerializeField] AudioSource effectAudioSource;

    [SerializeField] List<AudioClip> sheepSounds;
    [SerializeField] float sheepSoundMinVol = 0.1f;
    [SerializeField] float sheepSoundMaxVol = 0.6f;

    [SerializeField] AudioClip dingSound;
    [SerializeField] float dingVolume = 0.5f;



    public bool MusicEnabled { get; private set; }
    public bool AmbientEnabled { get; private set; }
    public bool SoundEnabled { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        MusicEnabled = true;
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

    public void PlayDingSound()
    {
        if (SoundEnabled)
        {
            effectAudioSource.PlayOneShot(dingSound, dingVolume);
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
