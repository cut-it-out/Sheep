using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionUIManager : MonoBehaviour
{
    public int totalLevel = 5;
    public int unlockedLevel = 1;

    private LevelButtonController[] levelButtons;

    private int totalPage;
    private int page = 0;
    private int pageItem = 15;

    public GameObject nextButton;
    public GameObject backButton;

    private void Start()
    {
        levelButtons = GetComponentsInChildren<LevelButtonController>();
        Refresh();
    }

    public void StartLevel(int level)
    {
        // TODO add here the level load stuff
    }

    public void ClickNext()
    {
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
        totalPage = totalLevel / pageItem;
        int index = page * pageItem;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = index + i + 1;
            if (level <= totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].Setup(level, level <= unlockedLevel);
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
