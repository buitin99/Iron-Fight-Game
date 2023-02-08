using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDatas
{
    // private Maps maps;
    public  List<int> totalLevel;
    public int gold;
    public int diamond;
    public bool isFlag;
    // public List<Maps> mapInfor;

    public int LastestLevel {
        get {
            return totalLevel.Count > 0 ? totalLevel[totalLevel.Count-1] : 0;
        }
    }

    public GameDatas()
    {
        totalLevel = new List<int>();
        // mapInfor   = new List<Maps>();

        gold       = 0;
        diamond    = 0;
        totalLevel.Add(0);
        isFlag = false;
        // mapInfor.Add(this.maps);
    }

    public void SaveData()
    {
        GameDataManagers<GameDatas>.SaveData(this);
    }

    public static GameDatas LoadData()
    {
        return GameDataManagers<GameDatas>.LoadData();
    }
}

[System.Serializable]
public class Maps
{
    public int             level;
    public int             totalWaves;
    public int             EnemyInWave;
    public int             totalSprites;
    public List<Vector3>   positionSpawnEnemy;
    public List<Vector3>   positionSpawnSprite;
    public List<Vector3>   positionSpawnPlane;


    public Maps(int leves, int waves, int enemies, int sprites, List<Vector3> positionEnemies, List<Vector3> positionSprites, List<Vector3> positionPlane)
    {
        positionSpawnEnemy  = new List<Vector3>();
        positionSpawnSprite = new List<Vector3>();
        positionSpawnPlane  = new List<Vector3>();

        level               = leves;
        totalWaves          = waves;
        EnemyInWave         = enemies;
        totalSprites        = sprites;
        positionSpawnEnemy  = positionEnemies;
        positionSpawnSprite = positionSprites;
        positionSpawnPlane  = positionPlane;
    }
}
