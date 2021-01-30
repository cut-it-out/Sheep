using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public float LevelTimer { get; private set; }

    [SerializeField] List<GameObject> levels = new List<GameObject>();

    Coroutine timer;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
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
