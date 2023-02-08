using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Levels", menuName = "Scriptable Levels/New Levels")]
public class ScriptableLevel : ScriptableObject
{
    public Level[] levels;
}

[System.Serializable]
public class Level
{
    [Space]
    public string key;
    [Space]
    public int totalWaves;
    [Space]
    public int totalEnemies;
    [Space]
    public int enemyInWave;
    [Space]
    public int totalSprites;
    [Space]
    public Vector3[] positionSpawnEnemiesList;
    [Space]
    public Vector3[] positionSpawnSpritesList;
    [Space]
    public Vector3[] positionSpawnPlaneList;
}

