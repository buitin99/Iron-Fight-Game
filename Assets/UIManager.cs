using UnityEngine;
using System.Collections;
using Cinemachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{  
    public CinemachineVirtualCamera cinemachine;
    public GameObject cameraPlay;
    public GameObject startUI;
    public GameObject playUI;
    public GameObject endUI;
    public GameObject shopUI;
    public GameObject heroUI;
    public GameObject delayLoading;

    public GameObject settingUI;

    private GameManager gameManager;

    //Player Camera
    private GameObject  player;

    private bool        isPlay;

    //Hit Combo
    public TMP_Text     hitTxt;
    private int         hitPoint;
    private float       currentTimeCombo = 3f;
    private bool        isAttack;
    private SpawnMap    spawnMap;
    private int         totalEnemyInfor;
    private bool        isStatus;

    //
    private SettingData settingData;

    //Event
    public UnityEvent<int> OnTotalEnemy = new UnityEvent<int>();

    //
    private AudioManager audioManager;

    //
    private EndUI endUiClass;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        spawnMap = FindObjectOfType<SpawnMap>();

        settingData = SettingData.LoadData();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnEnable() 
    {

        audioManager.MuteGame(settingData.mute);
        gameManager.OnStartGame.AddListener(StartGame);
        gameManager.OnHit.AddListener(DisplayHit);
        gameManager.OnEndGame.AddListener(AlertEndGame);
        spawnMap.OnInforWave.AddListener(InforEndGame);
    }

    private void Update() 
    {
        if (isAttack)
        {
            CountTimeCombo();
        }
    }


    public void StartGame()
    {
        cameraPlay.SetActive(true);
        startUI.SetActive(false);
        StartCoroutine(DeylayLoading());
        playUI.SetActive(true);
        isPlay = true;
        currentTimeCombo = 0f;
        hitTxt.text = "";

    }

    public void EndGame()
    {
        cameraPlay.SetActive(false);
        startUI.SetActive(true);
        endUI.SetActive(false);
        StartCoroutine(DeylayLoading());
        isPlay = false;
        Destroy(player);
    }

    public void ResetLevel()
    {
        StartCoroutine(DeylayLoading());
        gameManager.StartGame();
        endUI.SetActive(false);
        Destroy(player);
    }

    IEnumerator DeylayLoading()
    {
        delayLoading.SetActive(true);
        yield return new WaitForSeconds(2f);
        delayLoading.SetActive(false);

        //09-02 Null Exception
        if (isPlay)
        {
            player = FindObjectOfType<CharacterController>().gameObject;
            cinemachine.LookAt = player.transform;
            cinemachine.Follow = player.transform;
        }
    }

    //Call When Lose Or Win
    public void StatusGame()
    {
        endUI.SetActive(true);
        endUiClass = FindObjectOfType<EndUI>();
        endUiClass.GetInforEndGame(isStatus);
        playUI.SetActive(false);
        if (isStatus)
        {
            OnTotalEnemy?.Invoke(totalEnemyInfor);
        }
    }

    public void GoToSetting()
    {
        settingUI.SetActive(true);
    }

    public void CloseSetting()
    {
        settingUI.SetActive(false);
    }

    private void DisplayHit()
    {
        isAttack = true;
        currentTimeCombo = 3f;
        if (isAttack)
        {
            hitPoint++;
            hitTxt.text = hitPoint + " hit";
        }
    }

    private void CountTimeCombo()
    {
        currentTimeCombo -= Time.deltaTime;
        if (currentTimeCombo < 0)
        {
            isAttack = false;
            hitPoint = 0;
            hitTxt.text = "";
        }
    }

    private void InforEndGame(int totalEnemy, int wave)
    {
        totalEnemyInfor = totalEnemy;
    }

    private void AlertEndGame(bool isWin)
    {
        isStatus = isWin;
    }

    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);
        gameManager.OnHit.RemoveListener(DisplayHit);
        gameManager.OnEndGame.RemoveListener(AlertEndGame);
        spawnMap.OnInforWave.RemoveListener(InforEndGame);
    }
}
