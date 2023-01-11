using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private  ScriptableInfo enemySO;
    public GameObject spawnPoint;
    public GameObject spawnPoint1;
    private GameObject spawnPoint2;
    private List<GameObject> goSpawnList = new List<GameObject>();
    public UnityEvent<int, int> OnTotalEnemy = new UnityEvent<int, int>();
    private int turn = 1;
    private int randomEnemy;
    private ScoreManager scoreManager;
    private GameManager gameManager;
    private GameData gameData;
    private bool isStartGame;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        scoreManager = FindObjectOfType<ScoreManager>();
        gameData = GameData.Load();
    }

    private void OnEnable() 
    {
        scoreManager.OnWaveDone.AddListener(Wave);
        gameManager.OnStartGame.AddListener(StartGame);
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        // if (!isStartGame)
        //     return;    
    }

    private void StartGame()
    {
        gameData = GameData.Load();
        isStartGame = true;

        turn = 1;
        goSpawnList.Clear();
        InitSpawnEnemy();
        SetPositionWhenStart();
        Wave();

    }

    private void InitSpawnEnemy()
    {
        int i  = 0;
        int k  = 1;
        
        if (gameData.LastestLevel != 1)
        {
            k = gameData.totalPositionSpawnedEnemys[gameData.LastestLevel-1] + 1;
        }

        while (i < gameData.pointSpawnEnemys[gameData.LastestLevel])
        {
            GameObject go = new GameObject("go"+ i);
            int t = 0;
            while (t < 3)
            {
                go.transform.position = new Vector3 (gameData.positionPointsSpawn[k],gameData.positionPointsSpawn[k+1],gameData.positionPointsSpawn[k+2]);
                t++;
            }
            k += 3;
            goSpawnList.Add(go);
            i++;
        }
    }

    private void SetPositionWhenStart()
    {
        int temp = gameData.pointSpawnEnemys[gameData.LastestLevel] /gameData.totalEnemyinWave[gameData.LastestLevel];

        switch (temp)
        {
            case 1:
                    spawnPoint.transform.position  = goSpawnList[temp-1].transform.position;
                break;
            case 2:
                    spawnPoint.transform.position  = goSpawnList[temp-temp].transform.position;
                    spawnPoint1.transform.position  = goSpawnList[temp-1].transform.position;
                break;
            case 3:
                    spawnPoint.transform.position  = goSpawnList[0].transform.position;
                    spawnPoint1.transform.position = goSpawnList[1].transform.position;
                    spawnPoint2.transform.position = goSpawnList[2].transform.position;
                break;
        }
    }

    public void Wave()
    {
        ChangePosSpawn();
        int  r  = Random.Range(2,3);
        switch(r)
        {
            case 1:
                    randomEnemy = Random.Range(0,3);
                    Instantiate(enemySO.enemies[randomEnemy].enemy, new Vector3 (spawnPoint.transform.position.x,spawnPoint.transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                    Debug.Log(spawnPoint.transform.position);
                break;
            case 2:
                    randomEnemy = Random.Range(0,3);
                    Instantiate(enemySO.enemies[randomEnemy].enemy, new Vector3 (spawnPoint.transform.position.x,spawnPoint.transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                    randomEnemy = Random.Range(0,3);
                    Instantiate(enemySO.enemies[randomEnemy].enemy, new Vector3 (spawnPoint1.transform.position.x,spawnPoint1.transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                break;
            default:
                break;
        }
        turn++;
        OnTotalEnemy?.Invoke(turn, r);
    }

    private void ChangePosSpawn()
    {
        switch(turn)
        {
            case 1:
                break;
            case 2:
                    spawnPoint.transform.position = goSpawnList[turn].transform.position;
                    spawnPoint1.transform.position = goSpawnList[turn+1].transform.position;
                break;
            case 3:
                    spawnPoint.transform.position = goSpawnList[turn].transform.position;
                    spawnPoint1.transform.position = goSpawnList[turn+1].transform.position;
                break;
        }
    }

    public void EndGame()
    {
        for (int n = 0; n < goSpawnList.Count; n++)
        {
            Destroy(goSpawnList[n].gameObject);
        }

        NavMeshAgent[] goDestroy = FindObjectsOfType<NavMeshAgent>();
        for (int m = 0; m < goDestroy.Length; m++)
        {
            Destroy(goDestroy[m].gameObject);
        }
    }

    private void OnDisable() 
    {
        scoreManager.OnWaveDone.RemoveListener(Wave);
        gameManager.OnStartGame.RemoveListener(StartGame);
    }

}
