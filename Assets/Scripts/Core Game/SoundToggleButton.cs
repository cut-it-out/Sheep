using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundToggleButton : MonoBehaviour
{
    AudioManager audioManager;
    TMP_Text soundButtonText;

    private void Start()
    {
        soundButtonText = GetComponentInChildren<TMP_Text>();
        audioManager = AudioManager.GetInstance();

        SetButtonText(audioManager.SoundEnabled);
    }

    public void ToggleSound()
    {
        bool newSetting = !audioManager.SoundEnabled;
        audioManager.SetSound(newSetting);
        SetButtonText(newSetting);

    }

    private void SetButtonText(bool soundEnabled)
    {
        string t = soundEnabled ? "On" : "Off";
        soundButtonText.text = "Sound: " + t; 
    }

}
