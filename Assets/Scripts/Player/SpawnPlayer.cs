using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameManager gameManager;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame(int level)
    {
        Instantiate(playerPrefab, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }

    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);
    }
}
