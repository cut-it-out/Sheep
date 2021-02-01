using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float LevelTimer { get; private set; }
    Game game;

    private void Start()
    {
        game = Game.GetInstance();
    }

    public void TargetIsMet()
    {
        game.LevelFinished();
        LevelTimer = game.LevelTimer;
    }
}
