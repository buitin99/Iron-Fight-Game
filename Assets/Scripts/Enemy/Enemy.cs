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
    MOVEAROUND,
    ATTACK
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
    private Animator     animator;
    private Camera       cam;
    private EnemyDamageable enemyDamageable;
    private SoundManager soundManager;
    private float       health = 100;

    private bool        isDead;
    protected Vector3    playerDirection;

    public Vector3       standPos;
    public Vector3[]     patrolList; 
    public LayerMask     attackLayerMask;
    public LayerMask     playerLayer;
    public GameObject    playerRotation;
    public TypePatrol    typePatrol;
    public bool          isMelee, isGunner, isBoss;
    private bool turnRight, turnLeft;
    public GameObject    leftHand, rightHand, rightLeg;

    public AudioClip    audioClip, knockoutAudioClip, deadAudioClip;
    [Range(0,1)] public float volumeScale;

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

        soundManager = SoundManager.Instance;
        agent.updateRotation =  false;
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            HandleAnimation();
            ResetComboState();
            EnemyFollowPlayer();
            EnemyRotation();
        }
        
    }

    
    // protected void RotationLook(Vector3 direction)
    // {
    //     if (gameObject.layer != LayerMask.NameToLayer("Default"))
    //     {
    //         Quaternion rotationLook = Quaternion.LookRotation(direction);
    //         transform.rotation      = Quaternion.Lerp(transform.rotation, rotationLook, 40f*Time.deltaTime);
    //     }
    // }

    protected virtual void EnemyRotation()
    {
        if(agent.velocity.x > 0) {
            turnRight = true;
            turnLeft =  false;
        } else if( agent.velocity.x < 0) {
            turnLeft = true;
            turnRight =  false;
        }

        if(turnRight) {
            Quaternion rot = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, rot, 15 * Time.deltaTime);
            if(Vector3.Angle(transform.forward, Vector3.right) <= 0) {
                turnRight = false;
            }
        }

        if(turnLeft) {
            Quaternion rot = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, rot, 15 * Time.deltaTime);
            if(Vector3.Angle(transform.forward, Vector3.left) <= 0) {
                turnLeft = false;
            }
        }

    }

    protected virtual void EnemyFollowPlayer()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) {
            agent.SetDestination(playerRotation.transform.position);
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
        PlaySoundDead();
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

    protected virtual void AttackWhenHandlePlayer(Vector3 pos)
    {
        agent.SetDestination(pos);
        typePatrol = TypePatrol.ATTACK;
    }

    protected virtual void BackToPatrol()
    {
        typePatrol = TypePatrol.MOVEAROUND;
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

    //Layer
    protected virtual void SetLayerEnemy()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    protected virtual void SetLayerDefault()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    protected virtual void SetLeftHandAttack()
    {
        leftHand.layer = LayerMask.NameToLayer("EnemyAttack");
    }

    protected virtual void SetLeftHandDefault()
    {
        leftHand.layer = LayerMask.NameToLayer("Default");
    }

    protected virtual void SetRightHandAttack()
    {
        rightHand.layer = LayerMask.NameToLayer("EnemyAttack");
    }

    protected virtual void SetRightHandDefault()
    {
        rightHand.layer = LayerMask.NameToLayer("Default");
    }

    protected virtual void SetRightLegAttack()
    {
        rightLeg.layer = LayerMask.NameToLayer("EnemyAttack");
    }

    protected virtual void SetRightLegDefault()
    {
        rightLeg.layer = LayerMask.NameToLayer("Default");
    }

    //Trigger
    protected virtual void OnTriggerEnter(Collider other)
    {
        // if ((attackLayerMask & (1 << other.gameObject.layer)) != 0)
        // {
        //     isFight = true;
        //     posWhenDectededPlayer = other.gameObject.transform.position;
        // }

        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            AttackWhenHandlePlayer(other.transform.position);
        }

        if (gameObject.layer != LayerMask.NameToLayer("Default"))
        {
            if ((attackLayerMask & (1 << other.gameObject.layer)) != 0)
            {
                // IDamageable damageable = other.GetComponentInParent<IDamageable>();
                // Collider closestPoint = other.ClosestPoint;
                if (isMelee)
                {   

                    if (health > 0)
                    {
                        if (Random.Range(0, 4) >= 2)
                        {
                            health -= 20;
                            KnockDown();
                            PlaySound();
                        }
                        else
                        {
                            health -= 10;
                            Hited();
                            PlaySound();
                        }
                        //Health < 0
                        // Dead();
                    }
                    else
                    {
                        Dead();
                        isDead = true;
                    }
                    
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
            ComboAttack(); 
        }

        Vector3 dir = playerRotation.transform.position - transform.position;
        playerDirection = dir;
        // RotationLook(dir);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        // RotationLook(playerDirection);
    }


    //Audio
    private void PlaySound()
    {
        soundManager.PlayOneShot(audioClip, volumeScale);
    }

    private void PlaySoundDead()
    {
        soundManager.PlayOneShot(deadAudioClip, volumeScale);
    }

    public void PlaySoundKnockDonw()
    {
        soundManager.PlayOneShot(knockoutAudioClip, volumeScale);
    }
}
