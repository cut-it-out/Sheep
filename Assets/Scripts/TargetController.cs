using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        level = gameObject.GetComponentInParent<Level>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void CheckCountAndHandleActions()
    {
        Debug.Log("currentCount: " + currentCount);
        if (currentCount >= targetCount)
        {
            meshRenderer.material = targetDone;
            Debug.Log("count is bigger");
            level.TargetIsMet();
        }
        else if (currentCount < targetCount)
        {
            meshRenderer.material = targetActive;
            Debug.Log("count is less");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sheep")
        {
            currentCount++;
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
