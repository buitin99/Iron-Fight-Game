using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : MonoBehaviour
{

    private GameDatas gameDatas;
    private UIManager uiManager;

    private void Awake() 
    {
        gameDatas = GameDatas.LoadData();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnEnable() 
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu()
    {
        uiManager.EndGame();
    }   
}
