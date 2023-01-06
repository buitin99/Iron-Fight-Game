using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerEnemy : Enemy
{
    private bool isRange, isReadyAttack;
    private int shootHash;
    private float distance;
    private Vector3 rot, gunRot;
    private Weapon enemyWeapon;
    public GameObject shootPoint;

    //Gun
    public float damage = 10f;
    public float range = 100f;

    protected override void Awake() 
    {
        base.Awake();
        shootHash = Animator.StringToHash("Shoot");
        enemyWeapon = GetComponentInChildren<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!enemyDamageable.isKnockDown && !enemyDamageable.isDead)
        {
            EnemyGunMovement();
            ResetComboState();
            if (isRange)
            {
                EnemyGunShoot();
            }
        }
        base.Update();
    }   

    private void EnemyGunMovement()
    {
        if (!isRange)
        {
            EnemyFollowPlayer();
        }
        distance = Vector3.Distance(playerRotation.transform.position, transform.position);
        rot = playerRotation.transform.position - transform.position;
        RotationLook(rot);
    }



    private void GunRotation()
    {
        gunRot = playerRotation.transform.position - shootPoint.transform.position;
        RotationLook(gunRot);
    }

    private void RotationLook(Vector3 dir)
    {
        Quaternion rotLook = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotLook, 40f * Time.deltaTime);
    }

    protected override void EnemyFollowPlayer()
    {
        base.EnemyFollowPlayer();
    }

    private void EnemyGunShoot()
    {
        animator.SetLayerWeight(1,1);
        Attack(playerRotation.transform.position);
    }

    private void Attack(Vector3 playerPos)
    {
        if ((animator.GetLayerWeight(1) == 1) && !isReadyAttack)
        {
            Invoke("WaitForReadyAttack", 0.1f);
        }

        if (isReadyAttack)
        {
            enemyWeapon.Shoot(playerRotation.transform, playerLayer, "FromEnemy");
        }
    }

    private void WaitForReadyAttack() {
        isReadyAttack = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            agent.SetDestination(transform.localPosition);
            isRange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0 && distance <= 1f)
        {
            ComboAttack();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            isRange = false;
        }
    }
}
