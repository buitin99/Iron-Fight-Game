using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent OnStartGame = new UnityEvent();
    public UnityEvent<bool> OnEndGame = new UnityEvent<bool>();
    public UnityEvent OnNextLevels = new UnityEvent();

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
        // gameData.Save();
    }

    public void NextLevel()
    {
        OnNextLevels?.Invoke();
    }

    public void EndGame(bool isWin)
    {
        _isWin = isWin;
        OnEndGame?.Invoke(_isWin);
        currentLevel++;
        gameData.levels.Add(currentLevel);
        gameData.Save();
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }
}
