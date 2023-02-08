using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlMap : MonoBehaviour
{
    private EnemyDamageable[] enemyDamageables;
    private PlanePrefab[]     planes;
    private GameManager       gameManager;
    private SpawnMap          spawnMap;
    private int               waves;
    private int               totalWave;
    private int               enemies;
    private int               enemyInLevel;
    private int               indexPlanes;
    private int               indexEnemies;

    private List<EnemyDamageable>    enemyList = new List<EnemyDamageable>();
    private List<PlanePrefab>    planeList = new List<PlanePrefab>();


    //Game
    private UIManager ui;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
    }

    private void OnEnable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
        spawnMap = FindObjectOfType<SpawnMap>();
        ui       = FindObjectOfType<UIManager>();
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
        spawnMap.OnSpawnMapDone.AddListener(CotrolEnemy);
        spawnMap.OnInforWave.AddListener(MapInfo);
        StartCoroutine(WaitForStart());
        waves           = 1;
        indexPlanes     = 0;
        indexEnemies    = 0;
    }

    private void CotrolEnemy()
    {
        enemyDamageables = FindObjectsOfType<EnemyDamageable>();

        foreach (var item in enemyDamageables)
        {
            item.GetComponent<NavMeshAgent>().isStopped = true;
            enemyList.Add(item);
        }

        enemyList.Reverse();
        ControlPlane();
    }

    private void ControlPlane()
    {
        planes = FindObjectsOfType<PlanePrefab>();

        foreach (var item in planes)
        {
            planeList.Add(item);
        }

        planeList.Reverse();
    }

    private void MapInfo(int enemy, int wave)
    {
        enemies         = enemy/wave;
        enemyInLevel    = enemy/wave;
        totalWave       = wave;
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(2f);
        SetActiveEnemy(enemyInLevel);
    }

    public void EnemyCount()
    {
        enemies -= 1;
        if (enemies == 0 && waves < totalWave)
        {
            WaveDone();
        }

        if (enemies == 0 && waves == totalWave)
        {
            gameManager.EndGame(true);
            ui.playUI.SetActive(false);
            ui.endUI.SetActive(true);
        }
    }

    private void SetActiveEnemy(int number)
    {
        for (int j = indexEnemies; j < enemyInLevel+indexEnemies; j++)
        {
            enemyList[j].GetComponent<NavMeshAgent>().isStopped = false;
        }
        indexEnemies += enemyInLevel;
    }

    private void WaveDone()
    {
        waves++;
        enemies = enemyInLevel;
        planeList[indexPlanes].gameObject.SetActive(false);
        indexPlanes++;
        SetActiveEnemy(indexEnemies);
    }

    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);
        spawnMap.OnSpawnMapDone.RemoveListener(CotrolEnemy);
        spawnMap.OnInforWave.RemoveListener(MapInfo);
    }
}
