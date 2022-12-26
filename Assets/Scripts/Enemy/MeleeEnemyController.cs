using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Patrol();
        HandleAnimation();
        ResetComboState();
    }

    public void EnemyKnockDown()
    {
        KnockDown();
    }

    public void EnemyHited()
    {
        Hited();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
    }
}
