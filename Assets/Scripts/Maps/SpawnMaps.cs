using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaps : MonoBehaviour
{
    [SerializeField]
    private ScriptableSpriteInfo spriteSO;
    private List<GameObject> goSpawnSpriteList = new List<GameObject>();
    private GameManager gameManager;
    private GameData gameData;
    private bool isStartGame;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        gameData = GameData.Load();
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
        isStartGame = true;
        gameData = GameData.Load();

        goSpawnSpriteList.Clear();
        InitSpawnSprite();
        SetPositionWhenStart();
    }

    private void InitSpawnSprite()
    {
        int i = 0;
        int k = 1;

        if (gameData.LastestLevel != 1)
        {
            k = gameData.totalPositionSpawnedMaps[gameData.LastestLevel-1] + 2;
        }

        Debug.Log(k);
        
        while (i < gameData.pointSpawnMaps[gameData.LastestLevel])
        {
            int t = 0;
            GameObject go = new GameObject("go" + i);
            while (t < 3)
            {
                go.transform.position = new Vector3 (gameData.positionPointsMaps[k], gameData.positionPointsMaps[k+1], gameData.positionPointsMaps[k+2]);
                t++;
            }
            k += 3;
            goSpawnSpriteList.Add(go);   
            i++;
        }
    }

    private void SetPositionWhenStart()
    {
        int l = 0;
        while(l < gameData.pointSpawnMaps[gameData.LastestLevel])
        {
            Instantiate(spriteSO.sprites[0].sprite2D,goSpawnSpriteList[l].transform.position, Quaternion.identity);
            l++;
        }
    }

    public void EndGame()
    {
        for (int n = 0; n < goSpawnSpriteList.Count; n++)
        {
            Destroy(goSpawnSpriteList[n].gameObject);
        }

        SpriteRenderer[] goSpriteRender = FindObjectsOfType<SpriteRenderer>();
        for (int m = 0; m < goSpriteRender.Length; m++)
        {
            Destroy(goSpriteRender[m].gameObject);
        }
    }

    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);   
    }
}
