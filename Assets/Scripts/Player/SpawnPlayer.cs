// using UnityEngine;
// using Cinemachine;

// public class SpawnPlayer : MonoBehaviour
// {
//     public PlayerSO playerSO;
//     private GameManager gameManager;
//     private PlayerController playerController;
//     public CinemachineVirtualCamera vtcmr;
//     private Transform playerTransform;
//     private GameObject playerSpawn;
//     private GameObject player;
//     private void Awake() 
//     {
//         gameManager = GameManager.Instance;
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     private void OnEnable() 
//     {
//         gameManager.OnStartGame.AddListener(StartGame);
//         gameManager.OnNextLevels.AddListener(DestroyPlayerGO);
//     }

//     private void StartGame()
//     {
//         player = playerSO.player;
//         playerSpawn = Instantiate(player, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
//         playerTransform = playerSpawn.transform;
//         vtcmr.Follow = playerTransform;
//         vtcmr.LookAt = playerTransform;
//     }

//     private void DestroyPlayerGO()
//     {
//         Destroy(playerSpawn);
//     }

//     private void OnDisable() 
//     {
//         gameManager.OnStartGame.RemoveListener(StartGame);
//         gameManager.OnNextLevels.RemoveListener(DestroyPlayerGO);
//     }
// }
