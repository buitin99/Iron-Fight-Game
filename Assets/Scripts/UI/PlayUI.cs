using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Cinemachine;
using TMPro;

public class PlayUI : MonoBehaviour
{
    public GameObject endBtn;
    public GameObject settingBtn;
    public GameObject moneyTxt;
    public GameObject previousImg;
    public GameObject hitImg;
    public GameObject delayLoading;
    public TMP_Text    levelTxt;
    private GameDatas  gameDatas;


    //Animator
    private Animator    animator;
    private int         previousHash;
    //Manager
    private GameManager gameManager;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        animator    = GetComponent<Animator>();
        previousHash= Animator.StringToHash("isAactive");
    }

    private void OnEnable() 
    {   
        gameManager.OnStartGame.AddListener(StartGame);
        gameManager.OnHit.AddListener(NonPreviousAnimation);
        gameDatas = GameDatas.LoadData();
        levelTxt.text = "Level " + gameDatas.LastestLevel;
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
        // PreviousAnimation(false);
    }

    public void PreviousAnimation(bool isAactive)
    {
        animator.SetBool(previousHash, isAactive);
    }

    public void NonPreviousAnimation()
    {
        animator.SetBool(previousHash, false);
    }

    private void OnDisable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
        gameManager.OnHit.AddListener(NonPreviousAnimation);
        PreviousAnimation(false);
    }
}
