using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] StarManager starManager;

    private Game game;
    private LevelManager levelManager;

    private int currentStarCount = 3;
    private int previousStarCount;

    private void OnEnable()
    {
        game = Game.GetInstance();
        levelManager = LevelManager.GetInstance();
    }

    private void Update()
    {
        previousStarCount = currentStarCount;

        // update timer
        timerText.text = GetFormattedTimer();

        // update starCount
        currentStarCount = levelManager.GetCurrentStarCount(game.LevelTimer);

        if (previousStarCount != currentStarCount)
        {
            starManager.Setup(currentStarCount);
        }
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
