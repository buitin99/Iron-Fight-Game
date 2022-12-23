using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunEnemyController : MonoBehaviour
{
    private Animator     animator;
    private int          patrolIndex = 0;
    private int          velocityHash;
    private float        idleTime;
    private float        speed = 5;
    private NavMeshAgent agent;
    private int          knockDownHash;
    private int          hitHash;
    private int          laserHitHash;
    private int          attackHash;
    private int          shootHash;
    public Vector3       standPos;
    public Vector3[]     patrolList;
    public GameObject    playerRotation;
    public LayerMask     playerMask;
    private Vector3      newPosEnemy;
    private bool         isShooting;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    private void Awake() 
    {
        agent         = GetComponent<NavMeshAgent>();
        animator      = GetComponent<Animator>();
        velocityHash  = Animator.StringToHash("Velocity");
        knockDownHash = Animator.StringToHash("KnockDown");
        hitHash       = Animator.StringToHash("Hit");
        laserHitHash  = Animator.StringToHash("LaserHit");
        attackHash    = Animator.StringToHash("Attack");
        shootHash     = Animator.StringToHash("Shoot");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateAlwaysPlayer();
        HandleAnimation();
        ShootPlayer();
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

    public void EnemyKnockDown()
    {
        animator.SetTrigger(knockDownHash);
    }

    public void EnemyHit()
    {
        animator.SetTrigger(hitHash);
    }

    private void OnTriggerStay(Collider other) 
    {
        if ((playerMask & (1 << other.gameObject.layer)) != 0)
        {

        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((playerMask & (1 << other.gameObject.layer)) != 0)
        {
            animator.SetTrigger(attackHash);
            StartCoroutine(WaitForSecondToMove(2f));
            Debug.Log(123123123123123123);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        
    }

    private void RotationLook(Vector3 direction)
    {
        Quaternion rotLook = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotLook, 40f * Time.deltaTime);
    }

    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        // randomDirection += transform.position;
        // NavMeshHit hit;
        // Vector3 finalPosition = Vector3.zero;
        // if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
        //     finalPosition = hit.position;            
        // }
        return randomDirection;
    }

    private void RotateAlwaysPlayer()
    {
        Vector3 dir = playerRotation.transform.position - transform.position;
        RotationLook(dir);
    }

    IEnumerator WaitForSecondToMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        agent.SetDestination(RandomNavmeshLocation(0.8f));
    }

    private void ShootPlayer()
    {
        if (isShooting) return;

        StartCoroutine(WaitForShoot());
    }

    private IEnumerator WaitForShoot()
    {
        if (isShooting) yield break;

        isShooting = true;
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger(shootHash);
        yield return new WaitForSeconds(5f);
        isShooting = false;
    }

    public void SpawnBullet()
    {
        var bul = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bul.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward*speed;
    }
}
