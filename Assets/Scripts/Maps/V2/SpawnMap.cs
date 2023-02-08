using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpawnMap : MonoBehaviour
{
    //Scriptable
    [SerializeField]
    private ScriptableEnemiesObject enemy;
    [SerializeField]
    private ScriptableSpritesObject sprite;
    [SerializeField]
    private ScriptablePlanesObject plane;
    public ScriptableLevel levelSO;

    //Random
    private int randomEnemy;

    //List
    private List<GameObject> goSpawnSprites = new List<GameObject>();
    private List<GameObject> goSpawnPlanes  = new List<GameObject>();
    //Point Spawn Position Enemy
    private List<GameObject> goSpawnPoints  = new List<GameObject>();
    private List<GameObject> goSpawnEnemy   = new List<GameObject>();

    //Game
    private GameDatas gameData;
    private GameManager gameManager;
    // private ScoreManager scoreManager;

    //Event
    //int totalEnemy/ int Wave
    public UnityEvent<int, int> OnInforWave = new UnityEvent<int, int>();
    public UnityEvent           OnSpawnMapDone = new UnityEvent();

    //
    private EnemyDamageable[]   enemyDamageables;

    private int level;
    private int turn = 1;

    private void Awake() 
    {
        gameData     = GameDatas.LoadData();
        gameManager  = GameManager.Instance;
        // scoreManager = ScoreManager.Instance;
    }

    private void OnEnable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
        // scoreManager.OnWaveDone.AddListener(SpawnEnemy);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnInitSprite(int level)
    {
        int sprites = levelSO.levels[level].totalSprites;

        for (int i = 0; i < sprites; i++)
        {
            GameObject goSprite = new GameObject("goSprite");
            goSpawnSprites.Add(goSprite);
        }
    }

    //Plane
    private void SpawnInitPlane(int level)
    {
        int planes  = levelSO.levels[level].positionSpawnPlaneList.Length;

        for (int j = 0; j < planes; j++)
        {
            GameObject goPlane = new GameObject("goPlane");
            goSpawnPlanes.Add(goPlane);
        }
        SetPosiotionPlane(level);
    }

    private void SetPosiotionPlane(int level)
    {
        for (int m = 0; m < goSpawnPlanes.Count; m++)
        {
            goSpawnPlanes[m].transform.position = levelSO.levels[level].positionSpawnPlaneList[m];
        }
        SpawnPlane(level);
    }

    private void SpawnPlane(int level)
    {
        for (int n = 0; n < goSpawnPlanes.Count; n++)
        {
            Instantiate(plane.planes[0].plane, goSpawnPlanes[n].transform.position, plane.planes[0].plane.transform.rotation);
        }
    }

    //Spawn Enemy
    private void SpawnInitPositionSpawnPoint(int level)
    {
        int point = levelSO.levels[level].positionSpawnEnemiesList.Length;

        for (int k = 0; k < point; k++)
        {
            GameObject goPoint = new GameObject("goSpawnPoint");
            goSpawnPoints.Add(goPoint);
        }
        SetPositionEnemies(level);
    }

    private void SetPositionEnemies(int level)
    {
        for (int l = 0 ; l < goSpawnPoints.Count; l++)
        {
            goSpawnPoints[l].transform.position = levelSO.levels[level].positionSpawnEnemiesList[l];
        }

        SpawnEnemy(level);
    }

    private void SpawnEnemy(int level)
    {
        int totalEnemy = levelSO.levels[level].totalEnemies;
        int temp = 0;
        for (int l = 0; l < totalEnemy; l++)
        {
            randomEnemy = Random.Range(0,enemy.enemies.Length-1);
            Instantiate(enemy.enemies[randomEnemy].enemy, goSpawnPoints[temp].transform.position, enemy.enemies[randomEnemy].enemy.transform.rotation);
            temp++;
        }
        OnSpawnMapDone?.Invoke();
    }
    //Sprite

    private void SetPositionSprite()
    {
        int m = 0;
        while(m < goSpawnSprites.Count-1)
        {
            Instantiate(sprite.sprites[0].sprite, goSpawnSprites[m].transform.position, Quaternion.identity);
            m++;
        }
    }

    //Game
    private void StartGame()
    {
        gameData = GameDatas.LoadData();
        int level = gameData.LastestLevel;
        SpawnInitPlane(level);
        SpawnInitPositionSpawnPoint(level);
        OnInforWave?.Invoke(levelSO.levels[level].totalEnemies, levelSO.levels[level].totalWaves);
        enemyDamageables = FindObjectsOfType<EnemyDamageable>();
        foreach (var item in enemyDamageables)
        {
            // Debug.Log(item.transform.position);
        } 
    }

    private void EndGame(int level)
    {
        gameData = GameDatas.LoadData();
        gameData.totalLevel.Add(level);
        gameData.SaveData();
    }


    private void OnDisable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
        // scoreManager.OnWaveDone.AddListener(SpawnEnemy);
    }
}
