using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionUIManager : MonoBehaviour
{

    private LevelButtonController[] levelButtons;

    private int totalLevel = 0;
    private int totalPage;
    private int page = 0;
    private int pageItem = 15;

    public GameObject nextButton;
    public GameObject backButton;

    Game game;

    private void OnEnable()
    {
        //Debug.Log("onEnable");
        if (game != null)
        {
            Refresh();
        }
    }

    private void Start()
    {
        //Debug.Log("Start");
        totalLevel = LevelManager.GetInstance().LevelCount();
        levelButtons = GetComponentsInChildren<LevelButtonController>();
        game = Game.GetInstance();
        
        // TODO: don't know why but if I didn't trigger a click
        // simple calling Refresh() does not do a proper refresh somehow...
        Invoke("ClickNext", 0f);
    }

    public void StartLevel(int level)
    {
        Debug.Log("StartLevel: " + (level-1));
        game.LoadNextLevel(level - 1);
    }

    public void ClickNext()
    {
        //Debug.Log("ClickNext triggered");
        page = Mathf.Clamp(page + 1, 0, totalPage);
        Refresh();
    }
    
    public void ClickBack()
    {
        page = Mathf.Clamp(page - 1,0,totalPage);
        Refresh();
    }

    public void Refresh()
    {
        //Debug.Log("Refresh triggered");
        totalPage = totalLevel / pageItem;
        int index = page * pageItem;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = index + i + 1;
            if (level <= totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].Setup(level, i <= game.NextLevelToLoad, game.Data.levelStars[i]);
            }
            else
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }

        CheckButton();
    }

    private void CheckButton()
    {
        backButton.SetActive(page > 0);
        nextButton.SetActive(page < totalPage);
    }
}
