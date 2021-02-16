using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetController : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] Material targetActive;
    [SerializeField] Material targetDone;

    [Header("Target")]
    [SerializeField] int targetCount = 3;

    int currentCount = 0;
    Level level;
    MeshRenderer meshRenderer;
    TMP_Text sheepCountText;
    AudioManager audioManager;

    private void Start()
    {
        level = gameObject.GetComponentInParent<Level>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        sheepCountText = GetComponentInChildren<TMP_Text>();
        audioManager = AudioManager.GetInstance();

        sheepCountText.text = targetCount.ToString();
    }

    private void CheckCountAndHandleActions()
    {
        if (currentCount >= targetCount)
        {
            meshRenderer.material = targetDone;
            level.TargetIsMet();
        }
        else if (currentCount < targetCount)
        {
            meshRenderer.material = targetActive;
        }

        UpdateSheepCountText();
    }

    private void UpdateSheepCountText()
    {
        int displayCount = Mathf.Clamp(targetCount - currentCount, 0, targetCount);
        sheepCountText.text = displayCount != 0 ? displayCount.ToString() : "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sheep")
        {
            currentCount++;
            if (currentCount <= targetCount)
            {
                audioManager.PlayDingSound();
            }
            CheckCountAndHandleActions();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sheep")
        {
            currentCount--;
            CheckCountAndHandleActions();
        }
    }
}
