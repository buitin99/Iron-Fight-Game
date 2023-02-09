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
        
    }

    public void PreviousAnimation(bool isAactive)
    {
        animator.SetBool(previousHash, isAactive);
    }
}
