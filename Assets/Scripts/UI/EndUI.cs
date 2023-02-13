using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour
{
    private GameDatas   gameDatas;
    private UIManager   uiManager;
    private GameManager gameManager;

    //Bonus Money
    public TMP_Text     moneyBonusTxt;
    private int         money;
    private int         totalMoney;

    //
    public GameObject  resetBtn;
    public GameObject  menuBtn;

    public GameObject  pannelWin;
    public GameObject  pannelLose;


    private void Awake() 
    {
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable() 
    {
        uiManager.OnTotalEnemy.AddListener(BonusEndGame);
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

    public void GetInforEndGame(bool isWin)
    {
        if (isWin)
        {
            pannelWin.SetActive(true);
        }
        else
        {
            pannelLose.SetActive(true);
        }
    }

    private void BonusEndGame(int totalEnemy)
    {
        int coefficient = totalEnemy*10;
        money           = Random.Range(coefficient, coefficient*3);
        SaveMoneyEndGame();
    }

    private void SaveMoneyEndGame()
    {
        gameDatas = GameDatas.LoadData();
        totalMoney = gameDatas.gold;
        moneyBonusTxt.text = "+ " + money + " G";
        totalMoney += money;
        gameDatas.gold = totalMoney;
        gameDatas.SaveData();
    }

    private void OnDisable() 
    {
        uiManager.OnTotalEnemy.RemoveListener(BonusEndGame);
    }
}
