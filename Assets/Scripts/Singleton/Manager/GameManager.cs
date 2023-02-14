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
    public UnityEvent OnPlayerRevival = new UnityEvent();
    private GameData gameData;
    private bool _isWin;
    public int moneyCollected {get; private set;}
    // private int currentLevel;

    //V2
    private GameDatas gameDatas;

    private PlayerController playerController;

    //Event Enemy
    public UnityEvent<Transform> OnDetectedPlayer = new UnityEvent<Transform>();
    public UnityEvent<Transform> OnPlayerDead     = new UnityEvent<Transform>();
    public UnityEvent            OnPasueGame      = new UnityEvent();
    public UnityEvent            OnResumeGame     = new UnityEvent();

    protected override void Awake() 
    {
        base.Awake();
        gameData = GameData.Load();
        gameDatas = GameDatas.LoadData();
    }
    
    private void OnEnable() 
    {
        // currentLevel = gameData.LastestLevel;
    }

    private void Start() 
    {
        Application.targetFrameRate = 60;
        // gameData.Save();
    }

    public void EndGame(bool isWin)
    {
        _isWin = isWin;
        OnEndGame?.Invoke(_isWin);
        gameDatas = GameDatas.LoadData();
        if (isWin)
        {
            int currentLevel = gameDatas.LastestLevel;
            currentLevel++;
            gameDatas.totalLevel.Add(currentLevel);
            gameDatas.SaveData();
        }
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
        playerController = FindObjectOfType<PlayerController>();
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

    public void DetectedPlayer(Transform player)
    {
        OnDetectedPlayer?.Invoke(player);
    }

    public void PlayerDead()
    {
        OnPlayerDead?.Invoke(playerController.transform);
    }

    public void PlayerRevival()
    {
        OnPlayerRevival?.Invoke();
    }

    public void PauseGame()
    {
        OnPasueGame?.Invoke();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        OnResumeGame?.Invoke();
        Time.timeScale = 1;
    }

    public void GameHello()
    {
        Debug.Log("ABCasdasd");
    }
}
