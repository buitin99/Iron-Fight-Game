using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    IDLE,
    PATROL
}

public enum EnemyComboState
{
    NONE,
    ATTACKA,
    ATTACKB,
    ATTACKC
}
public class EnemyController : MonoBehaviour
{
    private Animator     animator;
    private int          patrolIndex = 0;
    private Transform    player;
    private int          velocityHash;
    private float        idleTime;
    private float        speed = 5;
    private NavMeshAgent agent;
    public Scanner       scanner = new Scanner();
    private GameObject   fieldOfView;

    private int          knockDownHash;
    private int          hitHash;
    private int          standUpHash;
    private int          laserHitHash;
    private int          attackA;
    private int          attackB;
    private int          attackC;
    private int          right90;

    private float        standUpTimer = 2f;

    private bool                        activeTimerToReset;
    private float                       default_Combo_Timer = 0.4f;
    private float                       current_Combo_Timer;
    private EnemyComboState             current_Combo_State;
    private Vector3      direction;
    public Vector3       standPos;
    public Vector3[]     patrolList; 
    public State         state;
    public Transform     rootScanner;
    [Range(0, 360)]
    public float         detectionAngle;
    public float         viewDistance;
    public LayerMask     layerMask;
    public LayerMask     playerLayer;
    public GameObject    playerRotation;
    private void Awake() 
    {
        agent         = GetComponent<NavMeshAgent>();
        animator      = GetComponent<Animator>();
        velocityHash  = Animator.StringToHash("Velocity");
        knockDownHash = Animator.StringToHash("KnockDown");
        hitHash       = Animator.StringToHash("Hit");
        standUpHash   = Animator.StringToHash("StandUp");
        laserHitHash  = Animator.StringToHash("LaserHit");
        attackA       = Animator.StringToHash("AttackA");
        attackB       = Animator.StringToHash("AttackB");
        attackC       = Animator.StringToHash("AttackC");
    }

    private void OnEnable() 
    {
        scanner.OnDetectedTarget.AddListener(HandleWhenDetected);
    }

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = scanner.CreataFieldOfView(rootScanner, rootScanner.position, detectionAngle, viewDistance);
    }

    // Update is called once per frame
    void Update()
    {
        // Patrol();
        // HandleAnimation();
        ResetComboState();  
        // direction = playerRotation.GetComponent<PlayerController>().direction;
    }

    private void Patrol()
    {
        if (patrolList != null && patrolList.Length > 0)
        {
            Vector3 patrolPoint = patrolList[patrolIndex];
            switch(state)
            {
                case State.IDLE:

                    break;
                case State.PATROL:
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        // animator.SetLayerWeight(2, 1);
                        idleTime += Time.deltaTime;
                        if (idleTime > 2)
                        {   
                            animator.SetLayerWeight(1, 1);
                            patrolIndex++;
                            if (patrolIndex >= patrolList.Length)
                            {
                                patrolIndex = 0;
                            }
                            agent.SetDestination(patrolPoint);
                            idleTime = 0;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void HandleAnimation() 
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

    public void HandleWhenDetected(List<RaycastHit> hitList)
    {
        // animator.SetTrigger(attackA);
        // Debug.Log(12312312);
    }

    public void EnemyKnockDown()
    {
        animator.SetTrigger(knockDownHash);
    }

    public void EnemyHit()
    {
        animator.SetTrigger(hitHash);
    }

    void EnemyStandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standUpTimer);
        animator.SetTrigger(standUpHash);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((layerMask & (1 << other.gameObject.layer)) != 0)
        {
            animator.SetTrigger(laserHitHash);
        }
       
    }

    private void OnTriggerStay(Collider other) 
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            PunchAnimation(); 
        }

        Vector3 dir = playerRotation.transform.position - transform.position;

        RotationLook(dir);
    }
    private void PunchAnimation()
    {
        // if (current_Combo_State == EnemyComboState.ATTACKC)
        //         return;

            current_Combo_State++;
            activeTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;

            if (current_Combo_State == EnemyComboState.ATTACKA)
            {
                animator.SetTrigger(attackA);
            }

            // if (current_Combo_State == EnemyComboState.ATTACKB)
            // {
            //     animator.SetTrigger(attackB);
            // }

            // if (current_Combo_State == EnemyComboState.ATTACKC)
            // {
            //     animator.SetTrigger(attackC);
            // }   
    }

    private void ResetComboState()
    {
        if (activeTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;

            if (current_Combo_Timer <= 0f)
            {
                current_Combo_State = EnemyComboState.NONE;

                activeTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }

    private void RotationLook(Vector3 direction)
    {
        Quaternion rotLook = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotLook, 40f * Time.deltaTime);
    }

    private void OnDisable() 
    {
        scanner.OnDetectedTarget.RemoveListener(HandleWhenDetected);
    }
}
