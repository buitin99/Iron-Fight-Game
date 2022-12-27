using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{

protected enum State
{
    IDLE,
    PATROL,
    FIGHT
}

public enum TypePatrol
{
    STANDINPLACE,
    MOVEAROUND
}

protected enum ComboState
{
    NONE,
    ATTACK
}


    private int          knockDownHash;
    private int          hitHash;
    private int          standUpHash;
    private int          laserHitHash;
    private int          velocityHash;
    private int          attackHash;
    private int          deadHash;
    private float        standUpTimer = 2f;
    private NavMeshAgent agent;
    private bool         activeTimerToReset;
    private float        default_Combo_Timer = 0.4f;
    private float        current_Combo_Timer;
    private ComboState   current_Combo_State;
    private Vector3      direction;
    private Animator     animator;
    private bool         isFight;
    private Vector3      posWhenDectededPlayer;
    private Camera       cam;
    private EnemyDamageable enemyDamageable;

    protected float      speed = 3;
    protected float      speedPatrol = 5;
    protected float      angularSpeed = 120;
    protected float      acceleration = 8;
    protected float      idleTime = 2;
    protected float      alertTime = 10;
    protected float      speedRotation = 7;
    protected int        patrolIndex;
    protected float      IdleTime;
    protected Vector3    playerDirection;

    public Vector3       standPos;
    public Vector3[]     patrolList; 
    public LayerMask     attackLayerMask;
    public LayerMask     playerLayer;
    public GameObject    playerRotation;
    public TypePatrol    typePatrol;
    public bool          isMelee, isGunner, isBoss;

    protected virtual void Awake()
    {
        agent         = GetComponent<NavMeshAgent>();
        animator      = GetComponent<Animator>();
        velocityHash  = Animator.StringToHash("Velocity");
        knockDownHash = Animator.StringToHash("KnockDown");
        standUpHash   = Animator.StringToHash("StandUp");
        hitHash       = Animator.StringToHash("Hit");
        laserHitHash  = Animator.StringToHash("LaserHit");
        attackHash    = Animator.StringToHash("Attack");
        deadHash      = Animator.StringToHash("Dead");
        enemyDamageable = GetComponent<EnemyDamageable>();

        cam = Camera.main;
        enemyDamageable.setInit(100, 10);
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    private void LateUpdate() 
    {
        Vector3 dirCam = cam.transform.position - transform.position;
        dirCam.y = 0;
    }

    //State

    protected virtual void Patrol()
    {
        if (patrolList != null && patrolList.Length > 0)
        {
            Vector3 patrolPoint = patrolList[patrolIndex];
            switch(typePatrol)
            {
                case TypePatrol.STANDINPLACE:
                    agent.SetDestination(standPos);
                    if (agent.remainingDistance <= agent.stoppingDistance)
                        IdleTime += Time.deltaTime;
                        {
                            if (IdleTime > idleTime)
                            {
                                patrolIndex++;
                                if (patrolIndex >= patrolList.Length)
                                {
                                    patrolIndex = 0;
                                }
                                IdleTime = 0;
                            }  
                        }
                    if (isFight)
                        break;
                    break;
                case TypePatrol.MOVEAROUND:
                    if (agent.remainingDistance <= agent.stoppingDistance)
                        {   
                            IdleTime += Time.deltaTime;
                            if (IdleTime > idleTime)
                            {
                                patrolIndex++;
                                if (patrolIndex >= patrolList.Length)
                                {
                                    patrolIndex = 0;
                                }
                                agent.SetDestination(patrolPoint);
                                IdleTime = 0;
                            }
                            // if (isFight)
                            // {
                            //     agent.isStopped = true;
                            //     Fight(posWhenDectededPlayer);
                            //     break;
                            // }
                            break;
                        }
                    break;
                default:
                    break;
            }
        }
    }

    // protected virtual void Fight(Vector3 pos)
    // {
    //     agent.SetDestination(pos);
    //     //Logic
    // }

    //Controller
    protected void RotationLook(Vector3 direction)
    {
        if (gameObject.layer != LayerMask.NameToLayer("Default"))
        {
            Quaternion rotationLook = Quaternion.LookRotation(direction);
            transform.rotation      = Quaternion.Lerp(transform.rotation, rotationLook, 40f*Time.deltaTime);
        }
    }


    //Animator
    protected virtual void HandleAnimation()
    {
        Vector3 horizontalVelocity = new Vector3(agent.velocity.x, 0, agent.velocity.z);
        float Velocity = horizontalVelocity.magnitude/3;
        if(Velocity > 0) {
            animator.SetFloat(velocityHash, Velocity);
        } else {
            float v = animator.GetFloat(velocityHash);
            v = Mathf.Lerp(v, -0.1f, 20f * Time.deltaTime);
            animator.SetFloat(velocityHash, v);
        } 
    }

    protected virtual void KnockDown()
    {
        animator.SetTrigger(knockDownHash);
    }

    protected virtual void Hited()
    {
        animator.SetTrigger(hitHash);
    }

    protected virtual void Dead()
    {
        animator.SetTrigger(deadHash);
    }

    protected virtual void StandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    protected virtual void CameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
    }

    protected virtual void ComboAttack()
    {
        current_Combo_State++;
        activeTimerToReset = true;
        current_Combo_Timer = default_Combo_Timer;

        if (isMelee)
        {
            if (current_Combo_State == ComboState.ATTACK)
            {
                animator.SetTrigger(attackHash);
            }
        }
    }

    protected void ResetComboState()
    {
        if (activeTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;

            if (current_Combo_Timer <= 0f)
            {
                current_Combo_State = ComboState.NONE;

                activeTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }
    
    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standUpTimer);
        animator.SetTrigger(standUpHash);
    }

    IEnumerator WaitForAttackAfterTime(float timer)
    {
        yield return new WaitForSeconds(timer);
    }

    //Layer
    protected virtual void SetLayerEnemy()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    protected virtual void SetLayerDefault()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    //Trigger
    protected virtual void OnTriggerEnter(Collider other)
    {
        // if ((attackLayerMask & (1 << other.gameObject.layer)) != 0)
        // {
        //     isFight = true;
        //     posWhenDectededPlayer = other.gameObject.transform.position;
        // }

        if (gameObject.layer != LayerMask.NameToLayer("Default"))
        {
            if ((attackLayerMask & (1 << other.gameObject.layer)) != 0)
            {
                if (isMelee)
                {   
                    //Health > 0
                    if (Random.Range(0, 4) > 2)
                    {
                        KnockDown();
                    }
                    else
                    {
                        Hited();

                    }
                    //Health < 0
                    // Dead();
                }

                if (isGunner)
                {
                    //Health > 0 
                    Hited();
                    //Health < 0
                    // Dead();
                }

                if (isBoss)
                {
                    //Do Something
                }
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    { 
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            // ComboAttack(); 
        }

        Vector3 dir = playerRotation.transform.position - transform.position;
        playerDirection = dir;
        RotationLook(dir);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        RotationLook(playerDirection);
    }

    //Disable
    protected virtual void OnDisable()
    {

    }

}
