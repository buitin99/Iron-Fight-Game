using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent OnStartGame = new UnityEvent();
    public UnityEvent<bool> OnEndGame = new UnityEvent<bool>();
    public UnityEvent OnNextLevels = new UnityEvent();
    public UnityEvent OnHit = new UnityEvent();
    public UnityEvent<float> OnUpdateHealPlayerUI = new UnityEvent<float>();
    public UnityEvent<int, int> OnUpdateMoney = new UnityEvent<int, int>();
    private GameData gameData;
    private bool _isWin;
    public int moneyCollected {get; private set;}

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

    public void HitedInUI()
    {
        OnHit?.Invoke();
    }

    public void UpdateHealPlayerUI(float health)
    {
        OnUpdateHealPlayerUI?.Invoke(health);
    }

    public void UpdateCurrency(int point, bool save = false)
    {
        moneyCollected += point;
        OnUpdateMoney?.Invoke(moneyCollected, moneyCollected);
    }
}
