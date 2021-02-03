using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScoreUpdater : MonoBehaviour
{
    TMP_Text timerText;
    Game game;

    private void Start()
    {
        timerText = GetComponent<TMP_Text>();
        game = Game.GetInstance();
    }

    private void Update()
    {
        timerText.text = GetFormattedTimer();
    }

    // TODO move this to a util function
    private string GetFormattedTimer()
    {
        float timer = game.LevelTimer;
        string minutes = Mathf.Floor(timer / 60).ToString("0");
        string seconds = (timer % 60).ToString("00");

        return string.Format("{0}:{1}", minutes, seconds);
    }
}
