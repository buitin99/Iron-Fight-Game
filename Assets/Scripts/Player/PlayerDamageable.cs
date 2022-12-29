using UnityEngine;
using System.Collections;

public class PlayerDamageable : MonoBehaviour, IDamageable
{

    private float _health = 100;

    private SoundManager soundManager;
    public AudioClip audioClip, deathAudioClip;
    [Range(0,1)]
    public float volumeScale;
    private PlayerController playerController;
    public bool isDead = false;
    public bool isKnockDown = false;

    private int                         deadHash;
    private int                         hitHash;
    private int                         knockDownHash;
    private int                         standUpHash;
    private Animator                    animator;
    private float                       standUpTimer = 2f;

    private void Awake() 
    {
        soundManager = SoundManager.Instance;
        playerController = GetComponent<PlayerController>();
        deadHash            = Animator.StringToHash("Dead");
        hitHash             = Animator.StringToHash("Hit");
        knockDownHash       = Animator.StringToHash("KnockDown");
        standUpHash       = Animator.StringToHash("StandUp");
        animator        = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamge(float damage)
    {
        _health -= damage;
        // healthBarRennder.UpdateHealthBarValue(_health);
        soundManager.PlayOneShot(audioClip);
        if (_health > 0)
        {
            Hited();
        }

        if (_health <= 0 && !isDead)
        {
            soundManager.PlayOneShot(deathAudioClip);
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        isDead = true;
        animator.SetTrigger(deadHash);
    }

    public void Hited()
    {
        animator.SetTrigger(hitHash);
    }

    // public void KnockDown()
    // {
    //     isKnockDown = true;
    //     animator.SetTrigger(knockDownHash);
    // }

    // IEnumerator StandUpAfterTime()
    // {
    //     yield return new WaitForSeconds(standUpTimer);
    //     isKnockDown = false;
    //     animator.SetTrigger(standUpHash);

    // }

    // private void StandUp()
    // {
    //     StartCoroutine(StandUpAfterTime());
    // }
}
