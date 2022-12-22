using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public ScriptableInfo enemyScriptalbe;
    public Transform     spawnPosition;
    private GameObject    enemy;


    private void Start() 
    {
        enemy = enemyScriptalbe.enemies[0].enemy;
        Instantiate(enemy, spawnPosition.position, Quaternion.identity);
    }
}
