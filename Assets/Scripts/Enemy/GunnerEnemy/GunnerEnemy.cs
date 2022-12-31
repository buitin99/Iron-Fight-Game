using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerEnemy : Enemy
{
    private bool isRange;
    private int shootHash;
    private float distance;
    private Vector3 rot;
    public Camera cmr;

    //Gun

    public float damage = 10f;
    public float range = 100f;

    protected override void Awake() 
    {
        base.Awake();
        shootHash = Animator.StringToHash("Shoot");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        EnemyGunMovement();
        ResetComboState();
        Shoot();
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
        animator.SetTrigger(shootHash);
    }

    public void Shoot()
    {
        RaycastHit hit;
       if (Physics.Raycast(cmr.transform.position, cmr.transform.forward, out hit, range))
       {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(cmr.transform.position, cmr.transform.forward, Color.blue, 10f);
       }  
    }



    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            EnemyGunShoot();
        }


        if ((playerLayer & (1 << other.gameObject.layer)) != 0 && distance <= 1f)
        {
            ComboAttack();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            agent.SetDestination(transform.localPosition);
            isRange = true;
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
