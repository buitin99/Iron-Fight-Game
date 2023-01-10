using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{

    public UnityEvent<int> OnNextStep = new UnityEvent<int>();
    public UnityEvent OnNextLevel = new UnityEvent();
    public UnityEvent<int> OnStartGame = new UnityEvent<int>();
    public UnityEvent<bool> OnEndGame = new UnityEvent<bool>();

    private GameData gameData;
    private bool _isWin;
    private int currentLevel;

    protected override void Awake() 
    {
        base.Awake();
        gameData = GameData.Load();
    }
    
    private void OnEnable() 
    {
        currentLevel = gameData.LastestLevel;
    }


    private void Start() 
    {
        Application.targetFrameRate = 60;
        gameData.Save();
    }

    public void InitGame ()
    {
        // gameData = GameData.LoadData();
        // gameData.SaveData();
        //         levelsData.Save();

        //         Debug.Log(gameData);


        // spawnEnemy.InitSpawnEnemy(gameData.levels);

        // GameObject go1 = new GameObject();
        
        // for (int i = 0; i < gameData.positionPointsSpawn.Count; i++)
        // {
        //     go1.transform.position =  new Vector3 (gameData.positionPointsSpawn[0],gameData.positionPointsSpawn[1],gameData.positionPointsSpawn[2]);
        //     Debug.Log(gameData.positionPointsSpawn.Count);
        // }

        // Debug.Log(go1.transform.position);
        // int i  = 0;
        // int k  = 0;
        // while (i < gameData.spawnPoints[gameData.LastestLevel])
        // {
        //     GameObject go = new GameObject("go"+ i);
        //     int t = 0;
        //     while (t < 3)
        //     {
        //         go.transform.position = new Vector3 (gameData.positionPointsSpawn[k],gameData.positionPointsSpawn[k+1],gameData.positionPointsSpawn[k+2]);
        //         Debug.Log(t);
        //         t++;
        //     }
        //         k += 3;
        //     Debug.Log(go);
        //     i++;
        // }
    }

    //Update Lastest Level
    // public void NextLevel()
    // {
    //     OnNextLevel?.Invoke();
    // }

    public void EndGame(bool isWin)
    {
        // NextLevel();
        _isWin = isWin;
        OnEndGame?.Invoke(_isWin);
        currentLevel++;
        gameData.levels.Add(currentLevel);
        gameData.Save();
    }

    public void StartGame()
    {
        OnStartGame?.Invoke(gameData.LastestLevel);
    }
}
