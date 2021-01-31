using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    public float LevelTimer { get; private set; }
    public bool IsPaused { get; private set; }

    [SerializeField] List<GameObject> levels = new List<GameObject>();

    Coroutine timer;
    CanvasManager canvasManager;
    GameObject currentLevelObject;


    private void Start()
    {
        canvasManager = CanvasManager.GetInstance();
        IsPaused = true;
    }

    public void LoadNextLevel()
    {
        // TODO: LOAD LEVEL LOGIC COMES HERE 
        // load next level which is not yet finished (take into account saved achievements)

        IsPaused = false;
        StartTimer();
    }

    public void RestartLevel()
    {
        // TODO handle restart
        LoadNextLevel();
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
