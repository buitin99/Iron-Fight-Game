using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    IDLE,
    PATROL
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
    private Scanner      scanner = new Scanner();
    private GameObject   fieldOfView;

    private int          knockDownHash;
    private int          hitHash;
    private int          standUpHash;

    private float        standUpTimer = 2f;


    public Vector3       standPos;
    public Vector3[]     patrolList; 
    public State         state;
    public Transform     rootScanner;
    [Range(0, 360)]
    public float         detectionAngle;
    public float         viewDistance;

    private void Awake() 
    {
        agent         = GetComponent<NavMeshAgent>();
        animator      = GetComponent<Animator>();
        velocityHash  = Animator.StringToHash("Velocity");
        knockDownHash = Animator.StringToHash("KnockDown");
        hitHash       = Animator.StringToHash("Hit");
        standUpHash   = Animator.StringToHash("StandUp");
    }

    private void OnEnable() 
    {
        // scanner.OnDetectedTarget.AddListener(HandleWhenDetected);
    }

    // Start is called before the first frame update
    void Start()
    {
        // fieldOfView = scanner.CreataFieldOfView(rootScanner, rootScanner.position, detectionAngle, viewDistance);
    }

    // Update is called once per frame
    void Update()
    {
        // Patrol();
        // HandleAnimation();
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
                        idleTime += Time.deltaTime;
                        if (idleTime > 2)
                        {
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
        Debug.Log(123);
   }
}
