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
    public int NextLevelIndex { get; private set; }
    public bool LastLevel { get; private set; }
    public Transform PlayerStartTransform { get; private set; }

    void Start()
    {
        LastLevel = false;
    }

    public void LoadLevel(int levelIndex = -1)
    {
        // check and update indexes
        if (levelIndex != -1) // if specific level needs to load
        {
            UpdateLevelIndexes(levelIndex);
        }
        //else if (CurrentLevelObject == null) // if first level load in game
        //{
        //    UpdateLevelIndexes(0);
        //}
        else
        {
            UpdateLevelIndexes();
        }

        // load desired level
        UnloadLevel();
        CurrentLevelObject = InstantiateLevel(levels[CurrentLevelIndex]);

        Debug.Log("CurrentLevelIndex: " + CurrentLevelIndex);

        // update start pos
        UpdatePlayerStart();

        // update navmesh
        surface.BuildNavMesh();
    }
    
    private void UpdatePlayerStart()
    {
        Debug.Log("UpdatePlayerStart()");
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

    public void UpdateLevelIndexes(int levelIndex = -1)
    {
        if (levelIndex != -1 && levelIndex < levels.Count)
        {
            CurrentLevelIndex = levelIndex;
        }
        else
        {
            if (!LastLevel)
            {
                CurrentLevelIndex++;
            }
        }
        
        if (CurrentLevelIndex < levels.Count)
        {
            if (CurrentLevelIndex + 1 < levels.Count)
            {
                NextLevelIndex = CurrentLevelIndex + 1;
            }
            else
            {
                LastLevel = true;
            }

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
