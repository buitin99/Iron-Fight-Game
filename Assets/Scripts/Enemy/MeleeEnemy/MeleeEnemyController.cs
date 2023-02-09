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
        if(attackState == AttackState.ATTACK && !isAttack) {

            ComboAttack();
            // ResetComboState();
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
            if (!isAttack && !isDeadPlayer)
            {
                // ResetComboState();
                ComboAttack();
            }
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
