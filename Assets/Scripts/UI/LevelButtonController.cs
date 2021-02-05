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

    LevelSelectionUIManager levelSelectionManager;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        image = button.GetComponent<Image>();
        levelText = GetComponentInChildren<TMP_Text>();
        levelSelectionManager = GetComponentInParent<LevelSelectionUIManager>();
    }

    public void Setup(int _level, bool _isUnlocked)
    {
        level = _level;
        levelText.text = level.ToString();

        if (_isUnlocked)
        {
            image.sprite = null;
            button.enabled = true;
            levelText.gameObject.SetActive(true);
        }
        else
        {
            image.sprite = lockSprite;
            button.enabled = false;
            levelText.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        Debug.Log("level click: " + level);
        levelSelectionManager.StartLevel(level);
    }
}
