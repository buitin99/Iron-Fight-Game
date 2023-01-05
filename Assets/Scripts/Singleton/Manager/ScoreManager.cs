using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    public GameObject obWave1, obWave2;
    public UnityEvent OnWaveDone = new UnityEvent();
    private SpawnEnemy spawnEnemy;
    private int score = 1;
    private int totalEnemy;
    private int _wave = 1;
    protected override void Awake() 
    {
        base.Awake();
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
    }
    private void OnEnable() 
    {
        spawnEnemy.OnTotalEnemy.AddListener(TotalEnemy);
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
    }

    private void WaveDone()
    {   
        _wave++;
        OnWaveDone?.Invoke();
    }

    private void OnDisable() 
    {
        spawnEnemy.OnTotalEnemy.RemoveListener(TotalEnemy);
    }
}
