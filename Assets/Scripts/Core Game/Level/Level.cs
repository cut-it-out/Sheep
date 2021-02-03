using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] float twoStarTime = 60f;

    Game game;

    private void Start()
    {
        game = Game.GetInstance();
    }

    public int HowManyStars(float levelTime)
    {
        if (twoStarTime / 2 >= levelTime)
        {
            return 3;
        }
        if (twoStarTime >= levelTime)
        {
            return 2;
        }
        else
        {
            return 1;
        }

    }

    public void TargetIsMet()
    {
        game.LevelFinished(this);
    }

}
