using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonController : MonoBehaviour
{
    public Sprite lockSprite;

    private TMP_Text levelText;
    private int level = 0;
    private Button button;
    private Image image;
    private StarManager starManager;

    LevelSelectionUIManager levelSelectionManager;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        image = button.GetComponent<Image>();
        levelText = GetComponentInChildren<TMP_Text>();
        levelSelectionManager = GetComponentInParent<LevelSelectionUIManager>();
        starManager = GetComponentInChildren<StarManager>();
    }

    public void Setup(int _level, bool _isUnlocked, int _stars = 0)
    {
        level = _level;
        levelText.text = level.ToString();

        if (_isUnlocked)
        {
            image.sprite = null;
            button.enabled = true;
            levelText.color = Color.black;
            starManager.Setup(_stars);
            //levelText.gameObject.SetActive(true);
        }
        else
        {
            //image.sprite = lockSprite;
            button.enabled = false;
            levelText.color = new Color(0.75f, 0.75f, 0.75f, 1);
            starManager.Setup(_stars);
            //levelText.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        //Debug.Log("level click: " + level);
        levelSelectionManager.StartLevel(level);
    }
}
