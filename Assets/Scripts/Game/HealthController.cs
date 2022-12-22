using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float health = 100f;
    private EnemyController enemyController;
    private PlayerController playerController;
    private bool             playerDead;
    public bool              isPlayer;

    private void Awake() 
    {
        if (!isPlayer)
        {
            enemyController  = GetComponent<EnemyController>();
        }
        else
        {
            playerController = GetComponent<PlayerController>();    
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage, bool knockDown)
    {
        if (playerDead)
            return;
        
        health -= damage;

        // if (health <= 0f)
        // {
        //     playerController.PlayerDead();
        //     playerDead = true;


        //     if (isPlayer)
        //     {

        //     }
        //     return;
        // }

        if (!isPlayer)
        {
            if (knockDown)
            {
                if (Random.Range(0, 2) > 0)
                {
                    enemyController.EnemyKnockDown();
                }
            }
            else
            {  
                // if (Random.Range(0, 3) > 1)
                // {
                    enemyController.EnemyHit();
                // }
            }
        }
    }
}
