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
    ATTACKA,
    ATTACKB,
    ATTACKC
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
    protected float      speed = 3;
    protected float      speedPatrol = 5;
    protected float      angularSpeed = 120;
    protected float      acceleration = 8;
    protected float      idleTime = 2;
    protected float      alertTime = 10;
    protected float      speedRotation = 7;
    protected int        patrolIndex;
    protected float      IdleTime;
    protected Vector3    pos;

    public Vector3       standPos;
    public Vector3[]     patrolList; 
    public LayerMask     layerMask;
    public LayerMask     playerLayer;
    public GameObject    playerRotation;
    public TypePatrol    typePatrol;

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
                        transform.rotation = LerpRotation(patrolPoint, transform.position, 10f); 
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
                        }
                    if (isFight)
                        break;
                    break;
                default:
                    break;
            }
        }
    }

    protected virtual void Fight()
    {
        isFight = true;
        //Logic
    }

    //Controller

    protected virtual Quaternion LerpRotation(Vector3 pos1, Vector3 pos2, float speed)
    {
        Vector3 dirLook = pos1 - pos2;
        Quaternion rotLook = Quaternion.LookRotation(dirLook.normalized);
        rotLook.x = 0;
        rotLook.z = 0;
        return Quaternion.Lerp(transform.rotation, rotLook, speed*Time.deltaTime);
    }

    protected void RotationLook(Vector3 direction)
    {
        Quaternion rotationLook = Quaternion.LookRotation(direction);
        transform.rotation      = Quaternion.Lerp(transform.rotation, rotationLook, 40f*Time.deltaTime);
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

    protected virtual void ComboAttack()
    {
        current_Combo_State++;
        activeTimerToReset = true;
        current_Combo_Timer = default_Combo_Timer;
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

    }

    protected virtual void OnTriggerStay(Collider other)
    {

    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            Hited();
        }
    }

    //Disable
    protected virtual void OnDisable()
    {

    }

}
