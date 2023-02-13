using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Revival : MonoBehaviour
{
    //Scriptable
    [SerializeField]
    private ScriptablePlayersObject player;
    public GameObject   posPlayerDead;
    private GameManager  gameManager;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        gameManager.OnPlayerDead.AddListener(GetPositionPlayerDead);
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
        // Debug.Log(posPlayerDead.transform.position);
    }

    private void GetPositionPlayerDead(Transform posPlayer)
    {
        // Debug.Log(posPlayer.transform.position);
        posPlayerDead.transform.position = posPlayer.transform.position;
    }

    public void RevivalPlayer()
    {
        // Debug.Log(posPlayerDead.transform.position);
        Instantiate(player.players[0].player, posPlayerDead.transform.position,player.players[0].player.transform.rotation);
    }

    private void OnDisable() 
    {
        gameManager.OnPlayerDead.RemoveListener(GetPositionPlayerDead);
    }
}
