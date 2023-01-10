using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenes : MonoBehaviour
{
    private GameManager gameManager;
    private int lastestLevel;
    private void Awake() 
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLastestLevel()
    {
        lastestLevel = GameData.Load().LastestLevel; 
    }
}
