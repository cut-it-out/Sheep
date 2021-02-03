using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishedLevelUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text stars;
    [SerializeField] GameObject nextLevelButton;

    private void OnEnable()
    {
        //TODO move cached vars out?
        LevelManager levelManager = LevelManager.GetInstance();
        Game game = Game.GetInstance();

        if (levelManager.CurrentLevelObject)
        {
            timerText.text = GetFormattedTimer(game.Data.levelTimes[levelManager.CurrentLevelIndex]);
            stars.text = game.Data.levelStars[levelManager.CurrentLevelIndex].ToString() + " Stars";
            if (levelManager.LastLevel)
            {
                nextLevelButton.SetActive(false);
            }
            else
            {
                nextLevelButton.SetActive(true);
            }
        }
    }

    // TODO move this to a util function
    private string GetFormattedTimer(float timer)
    {
        string minutes = Mathf.Floor(timer / 60).ToString("0");
        string seconds = (timer % 60).ToString("00");

        return string.Format("{0}:{1}", minutes, seconds);
    }
}
