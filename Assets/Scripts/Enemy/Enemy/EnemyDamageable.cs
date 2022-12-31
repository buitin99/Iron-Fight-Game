using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.AI;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    private float _coinBonus;
    private float _health = 1000;
    [SerializeField]
    private HealthBarRennder healthBarRennder = new HealthBarRennder();
    private SoundManager soundManager;
    public AudioClip audioClip, deathAudioClip;
    [Range(0,1)]
    public float volumeScale;

    public bool        isDead = false;
    public bool        isKnockDown = false; 
    public UnityEvent OnEnemyDead;

    private Animator animator;
    private int          knockDownHash;
    private int          hitHash;
    private int          laserHitHash;
    private int          deadHash;
    private float        standUpTimer = 2f;
    private bool knockBack;
    private NavMeshAgent agent;

    private void Awake() 
    {
        soundManager = SoundManager.Instance;
        animator = GetComponent<Animator>();
        deadHash      = Animator.StringToHash("Dead");
        knockDownHash = Animator.StringToHash("KnockDown");
        hitHash       = Animator.StringToHash("Hit");
        laserHitHash  = Animator.StringToHash("LaserHit");
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(knockBack) {
            agent.Move(-transform.forward * 10f * Time.deltaTime);
        }
    }

    private void LateUpdate() 
    {
        healthBarRennder.UpdateHealthBarRotation();
    }

    public void TakeDamge(float damage)
    {
        _health -= damage;
        healthBarRennder.UpdateHealthBarValue(_health);
        soundManager.PlayOneShot(audioClip);
        if (_health > 0)
        {
            if (Random.Range(0, 4) >= 3)
            {
                KnockDown();
            }
            else
            {
                Hited();
            }
        }

        if (_health <= 0 && !isDead )
        {
            soundManager.PlayOneShot(deathAudioClip);
            Dead();
            isDead = true;
        }
    }

    public void KnockDown()
    {
        agent.ResetPath();
        isKnockDown = true;
        knockBack = true;
        Invoke("CancelKnockBack", 0.3f);
        animator.SetTrigger(knockDownHash);
    }

    private void CancelKnockBack() {
        knockBack = false;
    }

    public void Hited()
    {
        animator.SetTrigger(hitHash);
    }

    public void Dead()
    {
        OnEnemyDead?.Invoke();
        animator.SetTrigger(deadHash);
    }
    
    protected virtual void StandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standUpTimer);
        isKnockDown = false;
    }

    public void setInit(float health, float coinBonus) {
        _health = health;
        _coinBonus = coinBonus;
        healthBarRennder.CreateHealthBar(transform, health);
    }

}
