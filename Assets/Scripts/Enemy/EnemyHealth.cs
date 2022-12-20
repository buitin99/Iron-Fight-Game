using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public float enemyHealth = 100f;
    private EnemyController enemyController;

    private void Awake() 
    {
        enemyController = GetComponent<EnemyController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ApplyDamage(float damage, bool knockDown)
    {
        // if (playerDead)
        //     return;

        enemyHealth -= damage;

        if (knockDown)
        {
            if (Random.Range(0, 2) > 0)
            {
                enemyController.EnemyKnockDown();
            }
        }
        else
        {
            if (Random.Range(0, 3) > 1)
            {
                enemyController.EnemyHit();
            }
        }

        

    }
}
