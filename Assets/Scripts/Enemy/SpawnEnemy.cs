using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private  ScriptableInfo enemySO;
    public  GameObject spawn, spawn1;

    private GameObject spawnPoint, spawnPoint1;

    private List<GameObject> goSpawnList = new List<GameObject>();
    private GameObject _enemy;
    public UnityEvent<int, int> OnTotalEnemy = new UnityEvent<int, int>();
    private int turn = 1;
    private int randomEnemy;
    private ScoreManager scoreManager;
    private GameManager gameManager;
    private GameData gameData;
    private void Awake() 
    {
        gameManager = GameManager.Instance;
        scoreManager = FindObjectOfType<ScoreManager>();
        gameData = GameData.Load();
    }

    private void OnEnable() 
    {
        scoreManager.OnWaveDone.AddListener(Wave);
    }

    // Start is called before the first frame update
    void Start()
    {
        Wave();
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void InitSpawnEnemy()
    {   
        int i  = 0;
        int k  = 0;
        while (i < gameData.spawnPoints[gameData.LastestLevel])
        {
            GameObject go = new GameObject("go"+ i);

            int t = 0;
            while (t < 3)
            {
                go.transform.position = new Vector3 (gameData.positionPointsSpawn[k],gameData.positionPointsSpawn[k+1],gameData.positionPointsSpawn[k+2]);
            
                Debug.Log(t);
                t++;
            }
            k += 3;
            goSpawnList.Add(go);
            // Debug.Log(go);
            Debug.Log(goSpawnList.Count); 
            i++;

        }
    }


    public void Wave()
    {
        InitSpawnEnemy();
        // ChangePosSpawn();


        // _enemy = enemySO.enemies[0].enemy;


        int   r  = Random.Range(1,2);
        switch(r)
        {
            case 1:
                    randomEnemy = Random.Range(0,3);
                    Instantiate(enemySO.enemies[randomEnemy].enemy, new Vector3 (goSpawnList[0].transform.position.x,goSpawnList[0].transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                break;
            case 2:
                    randomEnemy = Random.Range(0,3);
                    Instantiate(enemySO.enemies[randomEnemy].enemy, new Vector3 (spawn.transform.position.x,spawn.transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                    randomEnemy = Random.Range(0,3);
                    Instantiate(enemySO.enemies[randomEnemy].enemy, new Vector3 (spawn1.transform.position.x,spawn1.transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                break;
            default:
                break;
        }
        turn++;
        OnTotalEnemy?.Invoke(turn, r);
    }

    // private void ChangePosSpawn()
    // {
    //     switch(1)
    //     {
    //         case 1:
    //                 spawn.transform.position = goSpawnList[0].transform.position;
    //                 spawn1.transform.position = goSpawnList[1].transform.position;
    //             break;
    //         // case 2:
    //         //         spawn.transform.position = spawn2.transform.position;
    //         //         spawn1.transform.position = spawn3.transform.position;
    //         //     break;
    //         // case 3:
    //         //         spawn.transform.position = spawn4.transform.position;
    //         //         spawn1.transform.position = spawn5.transform.position;
    //         //     break;
    //     }
    // }

    private void OnDisable() 
    {
        scoreManager.OnWaveDone.RemoveListener(Wave);
    }

}
