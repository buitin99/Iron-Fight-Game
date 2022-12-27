using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerEnemyController : Enemy
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
}
