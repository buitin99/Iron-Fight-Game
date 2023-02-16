using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : Enemy
{
    public enum AttackState {
        IDLE,
        ATTACK
    }
    public AttackState attackState;
    private GameManager gameManager;
    protected override void Awake() 
    {
        base.Awake();
        gameManager = GameManager.Instance;
    }

    protected override void Update()
    {
        if (!enemyDamageable.isKnockDown && !enemyDamageable.isDead)
        {
            EnemyFollowPlayer();
            EnemyRotation();
            ResetComboState();
        }
        base.Update();

        // if (isDeadPlayer)
        //     return;

        if (isDeadPlayer)
        {
            attackState = AttackState.IDLE;
        }
        
        if(attackState == AttackState.ATTACK && !isAttack) 
        {
            ComboAttack();
        }

    
    }
    private void OnTriggerEnter(Collider other) 
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            attackState = AttackState.ATTACK;
        }

    }

    protected virtual void OnTriggerStay(Collider other)
    { 
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            gameManager.DetectedPlayer(other.transform);
            if (Mathf.Abs(other.transform.rotation.y - transform.rotation.y) < 1.6f && Mathf.Abs(other.transform.rotation.y - transform.rotation.y) > 1.2f)
            {
                attackState = AttackState.ATTACK;
            }
            attackState = AttackState.ATTACK;
        } 
    }

    private void OnTriggerExit(Collider other) 
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            attackState = AttackState.IDLE;
        }
    }
}
