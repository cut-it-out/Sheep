using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] NavMeshSurface surface;
    [SerializeField] List<GameObject> levels = new List<GameObject>();

    Vector3 basePosition = new Vector3(0f, 0f, 0f);

    public GameObject CurrentLevelObject { get; private set; }
    public int CurrentLevelIndex { get; private set; }
    public bool LastLevel { get; private set; }
    public Transform PlayerStartTransform { get; private set; }

    void Start()
    {
        LastLevel = false;
    }

    public void LoadLevel(int levelIndex = -1)
    {
        if (levelIndex != -1) // if specific level needs to load update indicator
        {
            Debug.Log($"UpdateLevelIndexes({levelIndex})");
            if (!SetCurrentLevelIndex(levelIndex))
                return;
        }

        // load desired level
        UnloadLevel();
        CurrentLevelObject = InstantiateLevel(levels[CurrentLevelIndex]);

        Debug.Log("CurrentLevelIndex: " + CurrentLevelIndex);

        // update navmesh
        surface.BuildNavMesh();

        // update start pos
        UpdatePlayerStart();
    }
    
    private void UpdatePlayerStart()
    {
        if (CurrentLevelObject != null)
        {
            //Debug.Log("UpdatePlayerStart() -- CurrentLevelObject != null");
            PlayerStartTransform = CurrentLevelObject.GetComponentInChildren<PlayerStart>().Get();
        }
    }

    public void UnloadLevel()
    {
        if (CurrentLevelObject != null)
        {
            Debug.Log("Destroy(CurrentLevelObject);");
            Destroy(CurrentLevelObject);
        }
    }

    public bool SetCurrentLevelIndex(int levelIndex)
    {
        
        if (CurrentLevelIndex < levels.Count)
        {
            CurrentLevelIndex = levelIndex;
            LastLevel = CurrentLevelIndex + 1 >= levels.Count;
            Debug.Log($"CurrentLevelIndex:{CurrentLevelIndex} - LastLevel:{LastLevel}");
            return true;
        }
        else
        {
            Debug.LogWarning($"CurrentLevelIndex({levelIndex}) cannot be higher then level count ({levels.Count})");
            return false;
        }
    }

    private GameObject InstantiateLevel(GameObject levelPrefab)
    {
        GameObject go = Instantiate(levelPrefab, basePosition, Quaternion.identity);
        go.name = levelPrefab.name;
        return go;

    }

    public int LevelCount()
    {
        return levels.Count;
    }
}
