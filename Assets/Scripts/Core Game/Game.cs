using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    public float LevelTimer { get; private set; }
    public bool IsPaused { get; private set; }
    public GameData Data { get; private set; }

    Coroutine timer;
    CanvasManager canvasManager;
    LevelManager levelManager;
    Player player;
    
    private void Start()
    {
        canvasManager = CanvasManager.GetInstance();
        levelManager = LevelManager.GetInstance();
        player = Player.GetInstance();
        IsPaused = true;
        
        // load previous saves if they exist
        Data = DataManager.LoadData();
        if (Data == null)
        {
            Debug.Log("setup new GameData()");
            Data = new GameData(levelManager.LevelCount());
        }

//        DataManager.SaveData(Data);
    }

    #region Game State Related Functions

    public void LoadNextLevel()
    {
        Debug.Log("levelManager.LoadLevel();");
        levelManager.LoadLevel();
        Debug.Log("player.MoveToStartPosition(levelManager.PlayerStartTransform);");
        player.MoveToStartPosition(levelManager.PlayerStartTransform);
        IsPaused = false;
        StartTimer();
    }

    public void RestartLevel()
    {
        StopTimer();
        IsPaused = true;
        levelManager.LoadLevel(levelManager.CurrentLevelIndex);
        player.MoveToStartPosition(levelManager.PlayerStartTransform);
        IsPaused = false;
        StartTimer();
    }

    public void LevelFinished(Level level)
    {
        StopTimer();
        IsPaused = true;

        //save game data
        Data.levelNames[levelManager.CurrentLevelIndex] = levelManager.CurrentLevelObject.name;
        Data.levelTimes[levelManager.CurrentLevelIndex] = LevelTimer;
        Data.levelStars[levelManager.CurrentLevelIndex] = level.HowManyStars(LevelTimer);
        DataManager.SaveData(Data);

        canvasManager.SwitchCanvas(CanvasType.LevelFinished);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
    
    public void QuitGame()
    {
        // TODO handle saving if needed
        Application.Quit();
    }

    #endregion

    #region Timer Functions

    private void StartTimer()
    {
        timer = StartCoroutine(timerCoroutine());
    }

    private void StopTimer()
    {
        StopCoroutine(timer);
    }

    IEnumerator timerCoroutine()
    {
        LevelTimer = 0f;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            LevelTimer += 1f;
        }
    }

    #endregion

}
