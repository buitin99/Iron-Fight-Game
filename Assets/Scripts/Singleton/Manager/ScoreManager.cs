using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    public GameObject obWave1, obWave2;
    public UnityEvent OnWaveDone = new UnityEvent();
    private SpawnEnemy spawnEnemy;
    private GameManager gameManager;
    private int score = 1;
    private int totalEnemy;
    private int _wave = 1;
    private GameData gameData;
    protected override void Awake() 
    {
        base.Awake();
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        gameManager = GameManager.Instance;
        gameData = GameData.Load();
    }
    private void OnEnable() 
    {
        spawnEnemy.OnTotalEnemy.AddListener(TotalEnemy);
        gameManager.OnStartGame.AddListener(StartGame);
    }

    public void TotalEnemy(int wave, int total)
    {
        totalEnemy = total;
        WaveOb(_wave);
    }

    private void WaveOb(int wave)
    {
        switch(wave)
        {
            case 1:
                break;
            case 2:
                obWave1.SetActive(false);
                break;
            case 3:
                // obWave1.SetActive(true);
                obWave2.SetActive(false);
                break;
            default:
                break;
        }
    }


    public void CountEnemy()
    {
        totalEnemy -= score;
        if (totalEnemy <= 0 && _wave <= 2)
        {
            WaveDone();
        }
        
        if (_wave == 3 && totalEnemy == 0)
        {
            gameManager.EndGame(true);
        }
    }

    private void WaveDone()
    {   
        _wave++;
        OnWaveDone?.Invoke();
    }

    private void StartGame()
    {
        _wave = 1;
        obWave1.SetActive(true);
        obWave2.SetActive(true);
    }

    private void OnDisable() 
    {
        spawnEnemy.OnTotalEnemy.RemoveListener(TotalEnemy);
        gameManager.OnStartGame.RemoveListener(StartGame);
    }
}
