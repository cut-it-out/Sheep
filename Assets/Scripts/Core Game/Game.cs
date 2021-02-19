using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResult
{
    public int levelIndex;
    public string levelName;
    public int levelStar;
    public float levelTime;
    public bool lastLevel;

    public LevelResult(int _levelIndex = 0, string _levelName = null, int _levelStar = 0, float _levelTime = 0f, bool _lastLevel = false)
    {
        levelIndex = _levelIndex;
        levelName = _levelName;
        levelStar = _levelStar;
        levelTime = _levelTime;
        lastLevel = _lastLevel;
    }
}

public class Game : Singleton<Game>
{
    public float LevelTimer { get; private set; }
    public bool IsPaused { get; private set; }
    public GameData Data { get; private set; }
    public int HighestLevelFinished { get; private set; }
    public int NextLevelToLoad { get; private set; }

    public LevelResult LevelResults { get; private set; }
    
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

        LevelResults = new LevelResult();

        //disable player to stop from dropping
        player.gameObject.SetActive(false);

        // load previous saves if they exist
        Data = DataManager.LoadData();
        if (Data == null)
        {
            Debug.Log("setup new GameData()");
            Data = new GameData(levelManager.LevelCount());
        }

        // check for highest level achieved
        UpdateHighestLevelFinished();
        UpdateNextLevelToLoad();

    }

    private void UpdateHighestLevelFinished()
    {
        int i = 0;
        while (i < Data.levelStars.Length)
        {
            if (Data.levelStars[i] != 0)
            {
                i++;
            }
            else
            {
                break;
            }
        }
        HighestLevelFinished = i < Data.levelStars.Length ? i - 1 : Data.levelStars.Length - 2;        
    }

    private void UpdateNextLevelToLoad()
    {
        if (LevelResults.levelName != null && !LevelResults.lastLevel)
        {
            NextLevelToLoad = LevelResults.levelIndex + 1;
        }
        else
        {
            NextLevelToLoad = HighestLevelFinished + 1;
        }
        Debug.Log("NextLevelToLoad: " + NextLevelToLoad);
        levelManager.SetCurrentLevelIndex(NextLevelToLoad);
    }

    #region Game State Related Functions

    public void LoadNextLevel(int level = -1)
    {
        SetLevelResults(true); // reset saved results
        ResumeGame(); // to make sure we don't stuck in pause
        Debug.Log($"levelManager.LoadLevel({level})");
        levelManager.LoadLevel(level);
        player.WarpToStartPosition(levelManager.PlayerStartTransform);
        IsPaused = false;
        StartTimer();
    }

    public void RestartLevel()
    {
        StopTimer();
        IsPaused = true;
        levelManager.LoadLevel(levelManager.CurrentLevelIndex);
        player.WarpToStartPosition(levelManager.PlayerStartTransform);
        IsPaused = false;
        StartTimer();
        ResumeGame(); // to make sure we don't stuck in pause
    }

    public void LevelFinished(Level level)
    {
        StopTimer();
        IsPaused = true;

        //save game data
        //Debug.Log("saving data for level " + levelManager.CurrentLevelIndex);
        Data.levelNames[levelManager.CurrentLevelIndex] = levelManager.CurrentLevelObject.name;
        Data.levelTimes[levelManager.CurrentLevelIndex] = LevelTimer;
        Data.levelStars[levelManager.CurrentLevelIndex] = level.HowManyStars(LevelTimer);
        DataManager.SaveData(Data);
        SetLevelResults();

        //Debug.Log("HighestLevelFinished: " + HighestLevelFinished);
        UpdateHighestLevelFinished();
        UpdateNextLevelToLoad();

        canvasManager.SwitchCanvas(CanvasType.LevelFinished);
    }

    public void SetLevelResults(bool reset = false)
    {
        if (reset)
        {
            LevelResults = new LevelResult();
        }
        else
        {
            LevelResults = new LevelResult(
                levelManager.CurrentLevelIndex,
                Data.levelNames[levelManager.CurrentLevelIndex],
                Data.levelStars[levelManager.CurrentLevelIndex],
                Data.levelTimes[levelManager.CurrentLevelIndex],
                levelManager.LastLevel
                );
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        player.gameObject.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
        player.WarpToStartPosition(player.transform); // to disregard menu clicks stuck in movement que
    }

    public void UnloadLevel()
    {
        StopTimer();
        IsPaused = true;
        levelManager.UnloadLevel();
        player.gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
