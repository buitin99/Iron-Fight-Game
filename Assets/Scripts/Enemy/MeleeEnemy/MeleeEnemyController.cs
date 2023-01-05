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
    }

}
