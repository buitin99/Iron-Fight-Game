using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private  ScriptableInfo enemySO;
    public  GameObject spawn, spawn1, spawn2, spawn3, spawn4, spawn5;
    private GameObject _enemy;
    public UnityEvent<int, int> OnTotalEnemy = new UnityEvent<int, int>();
    private int turn = 1;
    private ScoreManager scoreManager;
    private void Awake() 
    {
        scoreManager = FindObjectOfType<ScoreManager>();
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

    public void Wave()
    {
        ChangePosSpawn();
        _enemy = enemySO.enemies[0].enemy;
        int   r  = Random.Range(2,3);
        switch(r)
        {
            case 1:
                    Instantiate(_enemy, new Vector3 (spawn.transform.position.x,spawn.transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                break;
            case 2:
                    Instantiate(_enemy, new Vector3 (spawn.transform.position.x,spawn.transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                    Instantiate(_enemy, new Vector3 (spawn1.transform.position.x,spawn1.transform.position.y, Random.Range(-6.1f, 7.1f)), Quaternion.identity);
                break;
            default:
                break;
        }
        turn++;
        OnTotalEnemy?.Invoke(turn, r);
    }

    private void ChangePosSpawn()
    {
        switch(turn)
        {
            case 1:
                break;
            case 2:
                    spawn.transform.position = spawn2.transform.position;
                    spawn1.transform.position = spawn3.transform.position;
                break;
            case 3:
                    spawn.transform.position = spawn4.transform.position;
                    spawn1.transform.position = spawn5.transform.position;
                break;
        }
    }

    private void OnDisable() 
    {
        scoreManager.OnWaveDone.RemoveListener(Wave);
    }

}
