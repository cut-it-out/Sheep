using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    public float LevelTimer { get; private set; }
    public bool IsPaused { get; private set; }
    public GameData Data { get; private set; }
    public int HighestLevelFinished { get; private set; }
    public int NextLevelToLoad { get; private set; }

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
        // check for highest level achieved
        HandleWhichLevelComesNext();
        
        

        //        DataManager.SaveData(Data);
    }

    private void HandleWhichLevelComesNext()
    {
        int i = 0;
        while (Data.levelStars[i] != 0 && i < Data.levelStars.Length)
        {
            Debug.Log($"Data.levelStars[{i}]='{Data.levelStars[i]}'");
            i++;
        }

        HighestLevelFinished = i - 1;
        Debug.Log("HighestLevelFinished ELSE: " + HighestLevelFinished);
        UpdateNextLevelToLoad();
    }

    private void UpdateNextLevelToLoad()
    {
        NextLevelToLoad = HighestLevelFinished + 1;
        Debug.Log("NextLevelToLoad: " + NextLevelToLoad);
        levelManager.SetCurrentLevelIndex(NextLevelToLoad);
        Debug.Log("levelManager.CurrentLevelIndex: " + levelManager.CurrentLevelIndex);
    }

    #region Game State Related Functions

    public void LoadNextLevel(int level = -1)
    {
        ResumeGame(); // to make sure we don't stuck in pause
        Debug.Log($"levelManager.LoadLevel({level})");
        levelManager.LoadLevel(level);
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
        ResumeGame(); // to make sure we don't stuck in pause
    }

    public void LevelFinished(Level level)
    {
        StopTimer();
        IsPaused = true;

        //save game data
        Debug.Log("saving data for level " + levelManager.CurrentLevelIndex);
        Data.levelNames[levelManager.CurrentLevelIndex] = levelManager.CurrentLevelObject.name;
        Data.levelTimes[levelManager.CurrentLevelIndex] = LevelTimer;
        Data.levelStars[levelManager.CurrentLevelIndex] = level.HowManyStars(LevelTimer);
        DataManager.SaveData(Data);
        Debug.Log("HighestLevelFinished: " + HighestLevelFinished);
        HandleWhichLevelComesNext();

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

    public void UnloadLevel()
    {
        StopTimer();
        IsPaused = true;
        levelManager.UnloadLevel();
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
        if (timer != null) // check to only have one timer running
        {
            StopCoroutine(timer);
        }
        timer = StartCoroutine(timerCoroutine());
    }

    private void StopTimer()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }
        timer = null;
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
