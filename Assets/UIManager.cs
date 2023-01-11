using UnityEngine;
using Cinemachine;
using TMPro;

public class UIManager : MonoBehaviour
{  
    private Animator animator;
    private int alert;
    private int non;
    private ScoreManager scoreManager;
    private GameManager gameManager;

    public CinemachineVirtualCamera vtcmr;
    public GameObject playBtn;
    public GameObject endBtn;
    public GameObject playerGO;
    public GameObject ava;
    private SpawnEnemy spawnEnemy;
    private SpawnMaps spawnMaps;
    public TMP_Text hitText;
    private int hitPoint;
    private void Awake() 
    {
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        alert = Animator.StringToHash("Next");
        non = Animator.StringToHash("Non");
        gameManager = GameManager.Instance;
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        spawnMaps = FindObjectOfType<SpawnMaps>();
    }

    private void OnEnable() 
    {
        scoreManager.OnWaveDone.AddListener(AlertPlayer);
        gameManager.OnEndGame.AddListener(EndGame);
        gameManager.OnNextLevels.AddListener(NewGame);
        gameManager.OnUpdateHitCombo.AddListener(DisplayHit);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NonAlert()
    {   
        animator.SetTrigger(non);
    }

    private void AlertPlayer()
    {
        animator.SetTrigger(alert);
    }

    public void ClickEndGame()
    {
        endBtn.SetActive(false);
        ava.SetActive(false);
        spawnEnemy.EndGame();
        spawnMaps.EndGame();
        gameManager.NextLevel();
    }

    public void StartGame()
    {
        ava.SetActive(true);
        gameManager.StartGame();
        playBtn.SetActive(false);
        playerGO.SetActive(false);
    }

    private void EndGame(bool isWin)
    {
        ava.SetActive(false);
        endBtn.SetActive(true);
    }

    private void NewGame()
    {
        playBtn.SetActive(true);
        playerGO.SetActive(true);
        vtcmr.Follow = playerGO.transform;
        vtcmr.LookAt = playerGO.transform;
    }

    private void DisplayHit()
    {
        hitPoint++;
        hitText.text = hitPoint + "hit";
    }

    private void OnDisable() 
    {
        scoreManager.OnWaveDone.RemoveListener(AlertPlayer);
        gameManager.OnEndGame.RemoveListener(EndGame);
        gameManager.OnNextLevels.RemoveListener(NewGame);
        gameManager.OnUpdateHitCombo.RemoveListener(DisplayHit);
    }
}
