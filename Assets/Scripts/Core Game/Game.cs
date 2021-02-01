using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    public float LevelTimer { get; private set; }
    public bool IsPaused { get; private set; }

    Coroutine timer;
    CanvasManager canvasManager;
    LevelManager levelManager;
    Player player;

    private void Start()
    {
        canvasManager = CanvasManager.GetInstance();
        levelManager = GetComponent<LevelManager>();
        player = Player.GetInstance();
        IsPaused = true;
    }

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

    public void LevelFinished()
    {
        StopTimer();
        IsPaused = true;
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

    public void StartTimer()
    {
        timer = StartCoroutine(timerCoroutine());
    }

    public void StopTimer()
    {
        StopCoroutine(timer);
    }

    IEnumerator timerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            LevelTimer += 1f;
        }
    }

}
