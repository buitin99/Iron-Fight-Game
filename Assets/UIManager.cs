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

    private GameManager gameManager;

    //Player Camera
    private GameObject  player;

    private bool        isPlay;

    //Hit Combo
    public TMP_Text     hitTxt;
    private int         hitPoint;
    private float       currentTimeCombo = 3f;
    private bool        isAttack;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
    }

    private void OnEnable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
        gameManager.OnHit.AddListener(DisplayHit);
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
        playUI.SetActive(false);
        endUI.SetActive(true);
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

    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);
        gameManager.OnHit.RemoveListener(DisplayHit);
    }

    






























    //07-02-2022
    // private Animator animator;
    // private int alert;
    // private int non;
    // private ScoreManager scoreManager;
    // private GameManager gameManager;

    // public GameObject playBtn;
    // public GameObject endBtn;
    // public GameObject playerGO;
    // public GameObject ava;
    // private SpawnEnemy spawnEnemy;
    // // private SpawnMaps spawnMaps;
    // public TMP_Text hitText;
    // private int hitPoint;
    // public GameObject hitGO;
    // private EnemyDamageable enemyDamageable;
    // private float currentTimeCombo = 3f;
    // private bool isAttack;
    // public TMP_Text levelTxt;
    // public GameObject levelGO;
    // private GameData gameData;

    // // Settings
    // public List<GameObject> settingBtnLists;
    // public List<GameObject> backgroundList;

    // // Video Player
    // private UnityEngine.Video.VideoPlayer videoPlayer;

    // public List<GameObject> preListLevelImg;
    // public List<GameObject> ListLevelImg;
    // public List<GameObject> nxtListLevelImg;


    // private void Awake() 
    // {
    //     animator = GetComponent<Animator>();
    //     scoreManager = FindObjectOfType<ScoreManager>();
    //     alert = Animator.StringToHash("Next");
    //     non = Animator.StringToHash("Non");
    //     gameManager = GameManager.Instance;
    //     spawnEnemy = FindObjectOfType<SpawnEnemy>();
    //     // spawnMaps = FindObjectOfType<SpawnMaps>();
    //     videoPlayer = FindObjectOfType<UnityEngine.Video.VideoPlayer>();
    // }

    // private void OnEnable() 
    // {
    //     scoreManager.OnWaveDone.AddListener(AlertPlayer);
    //     gameManager.OnEndGame.AddListener(EndGame);
    //     gameManager.OnNextLevels.AddListener(NewGame);
    //     gameManager.OnStartGame.AddListener(StartGameUI);
    // }

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     if (isAttack)
    //     {
    //         CountTimeCombo();
    //     }
    // }

    // public void NonAlert()
    // {   
    //     animator.SetTrigger(non);
    // }

    // private void AlertPlayer()
    // {
    //     animator.SetTrigger(alert);
    // }

    // public void ClickEndGame()
    // {
        // endBtn.SetActive(false);
        // ava.SetActive(false);
        // spawnEnemy.EndGame();
        // // spawnMaps.EndGame();
        // gameManager.NextLevel();
        // levelGO.SetActive(false);
    // }

    // public void StartGame()
    // {
        // gameData = GameData.Load();
        // levelTxt.text = "Level " + gameData.LastestLevel;
        // levelGO.SetActive(true);
        // ava.SetActive(true);
        // gameManager.StartGame();
        // playBtn.SetActive(false);
        // playerGO.SetActive(false);
        // cameraStart.
    // }

    // private void EndGame(bool isWin)
    // {
    //     NonAlert();
    //     ava.SetActive(false);
    //     endBtn.SetActive(true);
    //     hitGO.SetActive(false);
    // }

    // private void NewGame()
    // {
    //     playBtn.SetActive(true);
    //     playerGO.SetActive(true);
    //     hitPoint = 0;
    //     currentTimeCombo = 0f;
    //     hitText.text = hitPoint + "hit";
    // }

    // private void DisplayHit()
    // {
    //     isAttack = true;
    //     hitGO.SetActive(true);
    //     currentTimeCombo = 3f;
    //     if (isAttack)
    //     {
    //         hitPoint++;
    //         hitText.text = hitPoint + "hit";
    //     }
    // }

    // private void CountTimeCombo()
    // {
    //     currentTimeCombo -= Time.deltaTime;
    //     if (currentTimeCombo < 0)
    //     {   
    //         isAttack = false;
    //         hitGO.SetActive(false);
    //         hitPoint = 0;
    //         hitText.text = hitPoint + "";
    //     }
    // }


    // private void StartGameUI()
    // {
    //     gameManager.OnHit.AddListener(DisplayHit);
    //     gameManager.OnUpdateHealPlayerUI.AddListener(UpdateHealthUI);
    // }

    // private void UpdateHealthUI(float damage)
    // {
        
    // }

    // public void ClickButtonInSettings(int id)
    // {
    //     for (int i = 0; i < settingBtnLists.Count; i++)
    //     {
    //         backgroundList[i].gameObject.SetActive(false);
    //     }
    //     backgroundList[id].gameObject.SetActive(true);
    // }

    // public void ClickButtonSettings(int id)
    // {
    //     backgroundList[id].gameObject.SetActive(true);
    //     ClickButtonInSettings(0);
    // }

    // public void ClickBackButton(int id)
    // {
    //     backgroundList[id].gameObject.SetActive(false);
    // }

    // public void ShowVideoSkill(int id)
    // {
    //     string url = "C:/Users/OS/Tin_Project/Iron Fight Game/Assets/Video/" + id + ".mp4";
    //     videoPlayer.url = url;
    // }

    // public void ChangeLevelInUI()
    // {
    //     //if current level = 1 hide 
    //     // current level
    //     preListLevelImg[0].SetActive(true);
    //     ListLevelImg[1].SetActive(true);
    //     nxtListLevelImg[2].SetActive(true);
    // }



    // private void OnDisable() 
    // {
    //     scoreManager.OnWaveDone.RemoveListener(AlertPlayer);
    //     gameManager.OnEndGame.RemoveListener(EndGame);
    //     gameManager.OnNextLevels.RemoveListener(NewGame);
    //     gameManager.OnStartGame.RemoveListener(StartGameUI);
    //     gameManager.OnHit.RemoveListener(DisplayHit);
    //     gameManager.OnUpdateHealPlayerUI.RemoveListener(UpdateHealthUI);
    // }
}
