using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountEnemy : MonoBehaviour
{
    private EnemyDamageable enemyDamageable;
    private int i = 0;

    // public UnityEvent 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake() 
    {
        enemyDamageable = GetComponent<EnemyDamageable>();
        enemyDamageable.OnEnemyDead.AddListener(CountEnemyDead);
    }

    private void CountEnemyDead()
    {
        i++;
        if (i == 1)
        {
            // stage1.SetActive(false);
        }

    }


}
