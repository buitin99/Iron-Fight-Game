using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int spawnMaps;
    public int totalEnemyinWave;
    public List<int> levels;
    public List<int> spawnPoints;

    public List<float> positionPointsSpawn;
    public List<float> positionPointsMaps;

    public int LastestLevel {
        get {
            return levels.Count > 0 ? levels[levels.Count - 1] : 0;
        }
    }
    public GameData()
    {
        spawnMaps = 0;
        totalEnemyinWave = 0;
        spawnPoints = new List<int>();
        levels = new List<int>();
        positionPointsSpawn = new List<float>();
        positionPointsMaps = new List<float>();
        spawnPoints.Add(0);
        positionPointsSpawn.Add(0);
        positionPointsMaps.Add(0);
    }

    public void Save() {
        GameDataManager<GameData>.SaveData(this);
    }
    
    public static GameData Load() {
        return GameDataManager<GameData>.LoadData();
    }
}
