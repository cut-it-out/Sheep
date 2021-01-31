using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    public float LevelTimer { get; private set; }

    [SerializeField] List<GameObject> levels = new List<GameObject>();

    Coroutine timer;

    private void Start()
    {
        // TODO load first level
        StartTimer();
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
