using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public List<int>   pointSpawnMaps;
    public List<int>   totalEnemyinWave;
    public List<int>   levels;
    public List<int>   pointSpawnEnemys;
    public List<int>   totalPositionSpawnedEnemys;
    public List<int>   totalPositionSpawnedMaps;
    public List<float> positionPointsSpawn;
    public List<float> positionPointsMaps;
    public List<Vector3> pos;

    public int LastestLevel {
        get {
            return levels.Count > 0 ? levels[levels.Count - 1] : 0;
        }
    }
    public GameData()
    {
        totalPositionSpawnedEnemys = new List<int>();
        totalPositionSpawnedMaps   = new List<int>();

        pointSpawnMaps = new List<int>();
        totalEnemyinWave = new List<int>();
        pointSpawnEnemys = new List<int>();
        levels = new List<int>();
        positionPointsSpawn = new List<float>();
        positionPointsMaps = new List<float>();

        pointSpawnMaps.Add(0);
        totalEnemyinWave.Add(0);
        levels.Add(0);
        pointSpawnEnemys.Add(0);
        positionPointsSpawn.Add(0);
        positionPointsMaps.Add(0);
        totalPositionSpawnedEnemys.Add(0);
        totalPositionSpawnedMaps.Add(0);
    }

    public void Save() {
        GameDataManager<GameData>.SaveData(this);
    }
    
    public static GameData Load() {
        return GameDataManager<GameData>.LoadData();
    }
}
