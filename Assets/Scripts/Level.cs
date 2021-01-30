using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float LevelTimer { get; private set; }
    Game game;

    private void Start()
    {
        game = FindObjectOfType<Game>();
    }

    public void TargetIsMet()
    {
        game.StopTimer();
        LevelTimer = game.LevelTimer;
    }
}
