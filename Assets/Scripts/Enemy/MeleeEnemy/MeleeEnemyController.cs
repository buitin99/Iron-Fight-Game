using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : Enemy
{
 
    protected override void Update()
    {
        if (!enemyDamageable.isKnockDown && !enemyDamageable.isDead)
        {
            EnemyFollowPlayer();
            EnemyRotation();
            ResetComboState();
        }
        base.Update();
        if (isRangeZone && !isAttack)
        {
        }

    }

    protected virtual void OnTriggerStay(Collider other)
    { 
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            ComboAttack();
            ResetComboState();
            isRangeZone = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            isRangeZone = false;
        }
    }

}
