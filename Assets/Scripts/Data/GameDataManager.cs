using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager<T> where T : class, new ()
{
    public static T LoadData ()
    {
        string _saveFile = Application.persistentDataPath + "/gamedata.json";
        // Does the file exist?
        if (File.Exists(_saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(_saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            T data = JsonUtility.FromJson<T>(fileContents);
            return data;
        }
        else
        {
            T newData = new T();
            Debug.Log("Init Data");
            return newData;
        }
    }

    public static void SaveData (T data)
    {
        string _saveFile = Application.persistentDataPath + "/gamedata.json";
        Debug.Log(_saveFile);
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(data);

        // Write JSON to file.
        File.WriteAllText(_saveFile, jsonString);
    }
}

public class LevelsData
{
    public int spawnPoints;
    public int spawnMaps;
    public int totalEnemyinWave;
    public List<int> levels;
    public List<float> positionPointsSpawn;
    public List<float> positionPointsMaps;

    public int LastestLevel {
        get {
            return levels.Count > 0 ? levels[levels.Count - 1] : 0;
        }
    }

    public LevelsData()
    {
        spawnPoints = 0;
        spawnMaps = 0;
        totalEnemyinWave = 0;
        levels = new List<int>();
        positionPointsSpawn = new List<float>();
        positionPointsMaps = new List<float>();
        positionPointsSpawn.Add(0);
        positionPointsMaps.Add(0);
    }

    public void Save() {
        GameDataManager<LevelsData>.SaveData(this);
    }
    
    public static LevelsData Load() {
        return GameDataManager<LevelsData>.LoadData();
    }
}