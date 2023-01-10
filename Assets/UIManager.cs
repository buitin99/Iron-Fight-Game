using UnityEngine;

public class UIManager : MonoBehaviour
{  
    private Animator animator;
    private int alert;
    private int non;
    private ScoreManager scoreManager;
    private GameManager gameManager;

    public GameObject playBtn;
    public GameObject endBtn;
    public GameObject playerGO;
    private void Awake() 
    {
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        alert = Animator.StringToHash("Next");
        non = Animator.StringToHash("Non");
        gameManager = GameManager.Instance;
    }

    private void OnEnable() 
    {
        scoreManager.OnWaveDone.AddListener(AlertPlayer);
        gameManager.OnEndGame.AddListener(EndGame);
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
        // gameManager
    }

    public void StartGame()
    {
        gameManager.StartGame();
        playBtn.SetActive(false);
        playerGO.SetActive(false);
    }

    private void EndGame(bool isWin)
    {
        endBtn.SetActive(true);
    }

    private void OnDisable() 
    {
        scoreManager.OnWaveDone.RemoveListener(AlertPlayer);
        gameManager.OnEndGame.RemoveListener(EndGame);
    }
}
