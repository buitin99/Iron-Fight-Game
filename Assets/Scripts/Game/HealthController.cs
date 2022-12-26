using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float health = 100f;
    private MeleeEnemyController            enemy;
    private PlayerController playerController;
    private bool             playerDead;
    private bool             isKnockDown;
    public bool              isPlayer;

    private void Awake() 
    {
        if (!isPlayer)
        {
            enemy  = GetComponent<MeleeEnemyController>();
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
                    // if (Random.Range(0, 2) > 0)
                    // {
                        enemy.EnemyKnockDown();
                        
                    // }
                }
                else
                {  
                    // if (Random.Range(0, 3) > 1)
                    // {
                        enemy.EnemyHited();
                    // }
                }
            }
    }
}
