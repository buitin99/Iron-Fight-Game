using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Revival : MonoBehaviour
{
    //Scriptable
    [SerializeField]
    private ScriptablePlayersObject player;
    public GameObject   posPlayerDead;
    private GameManager  gameManager;
    private PlayerController playerController;
    public CinemachineVirtualCamera cmr;

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
        playerController = FindObjectOfType<PlayerController>();
    }

    private void GetPositionPlayerDead(Transform posPlayer)
    {
        Debug.Log(posPlayer.position);
        posPlayerDead.transform.position = posPlayer.transform.position;
        playerController.gameObject.SetActive(false);
    }

    public void RevivalPlayer()
    {
        GameObject pla =  Instantiate(player.players[0].player, posPlayerDead.transform.position,player.players[0].player.transform.rotation);
        cmr.LookAt = pla.transform;
        cmr.Follow = pla.transform;
        // player.players[0].player.GetComponentInChildren<PlayerDamageable>().setInit(200,0);
    }

    private void OnDisable() 
    {
        gameManager.OnPlayerDead.RemoveListener(GetPositionPlayerDead);
    }
}
