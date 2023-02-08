using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    private int score = 1;
    private int wave = 1;
    private int totalEnemies;
    private int totalWaves;

    //Game
    private GameManager gameManager;
    private SpawnMap    spawnMap;

    protected override void Awake() 
    {
        base.Awake();
        gameManager = GameManager.Instance;
        spawnMap    = FindObjectOfType<SpawnMap>();
    }
    private void OnEnable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
    }

    private void StartGame()
    {
        spawnMap.OnInforWave.AddListener(InforMap);
    }

    public void InforMap(int enemy, int wave)
    {
        totalEnemies = enemy;
        totalWaves   = wave;
    }

    public void CountEnemy()
    {
        totalEnemies -= score;
        if (totalEnemies <= 0 && wave <= totalWaves)
        {
            WaveDone();
        }
        
        if (wave == 3 && totalWaves == 0)
        {
            gameManager.EndGame(true);
        }
    }

    private void WaveDone()
    {   
        wave++;
    }

    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);
        spawnMap.OnInforWave.RemoveListener(InforMap);
    }
}
