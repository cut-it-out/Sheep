using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager 
{
    const string FILE_NAME = "SheepData.save";

    public static void SaveData(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(json);
    }

    public static GameData LoadData()
    {
        GameData data = new GameData(LevelManager.GetInstance().LevelCount());
        string json = ReadFromFile();
        if (json != "")
        {
            JsonUtility.FromJsonOverwrite(json, data);
            return data;
        }
        else
        {
            return null;
        }
    }

    private static void WriteToFile(string json)
    {
        string path = GetFilePath();
        FileStream stream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(stream))
        {
            writer.Write(json);
        }
        //stream.Close();
    }

    private static string ReadFromFile()
    {
        string path = GetFilePath();
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        } 
        else
        {
            Debug.LogWarning("Save file does not exist on path: " + path);
            return "";
        }
    }

    private static string GetFilePath()
    {
        return Application.persistentDataPath + "/" + FILE_NAME;
    }
}
