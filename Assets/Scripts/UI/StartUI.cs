using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartUI : MonoBehaviour
{
    public GameObject playBtn;
    public GameObject settingBtn;
    public GameObject shopBtn;
    public GameObject heroBtn;
    public GameObject newPlayBtn;
    public TMP_Text   moneyTxt;
    public GameObject delayLoading;
    //Manager
    private GameManager gameManager;
    private GameDatas   gameDatas;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameDatas   = GameDatas.LoadData();

        if (!gameDatas.isFlag)
        {
            newPlayBtn.SetActive(true);
        }
    }

    private void OnEnable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
        gameDatas = GameDatas.LoadData();
        moneyTxt.text = "" + gameDatas.gold + "G";
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame()
    {
        StartCoroutine(DeylayLoading());
    }

    IEnumerator DeylayLoading()
    {
        delayLoading.SetActive(true);
        yield return new WaitForSeconds(1);
        delayLoading.SetActive(false);
    }

    public void CreateJson()
    {
        gameDatas.isFlag = true;
        gameDatas.totalLevel.Add(1);
        gameDatas.SaveData();
    }

    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);
    }
}
