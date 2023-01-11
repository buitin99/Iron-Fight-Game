using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{

protected enum ComboState
{
    NONE,
    ATTACK,
    ATTACKA,
    KICK
}
    private int             velocityHash;
    protected int           attackHash;
    protected NavMeshAgent  agent;
    private bool            activeTimerToReset;
    private float           default_Combo_Timer = 0.4f;
    private float           current_Combo_Timer;
    private ComboState      current_Combo_State;
    protected Animator      animator;
    private Camera          cam;
    protected EnemyDamageable enemyDamageable;
    private SoundManager soundManager;
    public LayerMask     playerLayer;
    public GameObject    playerRotation;
    private bool turnRight, turnLeft, isCanMove;
    private bool isStartGame;
    public GameObject    leftHand, rightHand, rightLeg;
    public AudioClip    knockoutAudioClip;
    private GameManager gameManager;
    [Range(0,1)] public float volumeScale;
    protected virtual void Awake()
    {
        agent         = GetComponent<NavMeshAgent>();
        animator      = GetComponent<Animator>();
        velocityHash  = Animator.StringToHash("Velocity");
        attackHash    = Animator.StringToHash("Attack");  
        enemyDamageable = GetComponentInChildren<EnemyDamageable>();

        cam = Camera.main;
        enemyDamageable.setInit(100, 0);
        enemyDamageable.setInitKnockDown(0);

        soundManager = SoundManager.Instance;
        agent.updateRotation =  false;

        //prefabs 
        playerRotation = FindObjectOfType<CharacterController>().gameObject;
        gameManager = GameManager.Instance;
        isCanMove = true;
    }

    private void OnEnable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
    }

    protected virtual void Update()
    {
        // if (!isStartGame)
        //     return;

        HandleAnimation();
    }

    protected virtual void EnemyRotation()
    {
        if(agent.velocity.x > 0) {
            turnRight = true;
            turnLeft  =  false;
        } else if( agent.velocity.x < 0) {
            turnLeft  = true;
            turnRight = false;
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
        if (!isCanMove)
        {
            agent.ResetPath();
        }
        else
        {
            if(agent.remainingDistance <= agent.stoppingDistance) {
                agent.SetDestination(playerRotation.transform.position);
            }
        }
    }


    //Animator
    protected virtual void HandleAnimation()
    {   
        if (isCanMove)
        {
            Vector3 horizontalVelocity = new Vector3(agent.velocity.x, 0, agent.velocity.z);
            float Velocity = horizontalVelocity.magnitude/agent.speed;
            if(Velocity > 0) {
                animator.SetFloat(velocityHash, Velocity);
            } else {
                float v = animator.GetFloat(velocityHash);
                v = Mathf.Lerp(v, -0.1f, 20f * Time.deltaTime);
                animator.SetFloat(velocityHash, v);
            }
        }
         
    }


    public virtual void CameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
    }

    protected virtual void ComboAttack()
    {

        //3-1-2023 k co code nay
        // if (current_Combo_State == ComboState.KICK)
        //     return;

        current_Combo_State++;
        activeTimerToReset = true;
        current_Combo_Timer = default_Combo_Timer;


        if (current_Combo_State == ComboState.ATTACK)
        {
            animator.SetTrigger(attackHash);
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

    //Layer
    protected virtual void SetLayerEnemy()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    protected virtual void SetLayerDefault()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    // leftHand, rightHand, rightLeg
    public void LeftHandAttackTrue()
    {
        leftHand.SetActive(true);
    }
    public void LeftHandAttackFalse()
    {
        leftHand.SetActive(false);
    }

    public void RightHandAttackTrue()
    {
        rightHand.SetActive(true);
    }
    public void RightHandAttackFalse()
    {
        rightHand.SetActive(false);
    }

    public void RightLegAttackTrue()
    {
        rightLeg.SetActive(true);
    }
    public void RightLegAttackFalse()
    {
        rightLeg.SetActive(false);
    }

    public void EnemyCanMove()
    {
        isCanMove = true;
    }

    public void EnemyCanNotMove()
    {
        isCanMove = false;
    }

    public void PlaySoundKnockDown()
    {
        soundManager.PlayOneShot(knockoutAudioClip, volumeScale);
    }

    private void StartGame()
    {
        isStartGame = true;
    }

    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);
    }
}
