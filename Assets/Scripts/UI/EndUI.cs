using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour
{

    private GameDatas gameDatas;
    private UIManager uiManager;
    private GameManager gameManager;

    //Bonus Money
    public TMP_Text    moneyBonusTxt;
    private int         money;

    private void Awake() 
    {
        gameDatas = GameDatas.LoadData();
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable() 
    {
        gameManager.OnEndGame.AddListener(RandomMoneyEndGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void GoToMenu()
    {
        uiManager.EndGame();
    }

    private void RandomMoneyEndGame(bool isWin)
    {
        if (isWin)
        {
            money = Random.Range(70, 150);
            moneyBonusTxt.text = "" + money;
        }
    }

    private void OnDisable() 
    {
        gameManager.OnEndGame.RemoveListener(RandomMoneyEndGame);
    }
}
