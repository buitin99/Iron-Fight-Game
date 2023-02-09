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
    [SerializeField]
    private HealthBarRennder healthBarRennder = new HealthBarRennder();
    private int                         deadHash;
    private int                         hitHash;
    private int                         knockDownHash;
    private int                         standUpHash;
    private int                         stateDeath;
    private Animator                    animator;
    private float                       standUpTimer = 2f;
    private GameManager gameManager;

    private void Awake() 
    {
        soundManager = SoundManager.Instance;
        playerController = GetComponent<PlayerController>();
        deadHash         = Animator.StringToHash("Dead");
        hitHash          = Animator.StringToHash("Hit");
        knockDownHash    = Animator.StringToHash("KnockDown");
        standUpHash      = Animator.StringToHash("StandUp");
        animator         = GetComponent<Animator>();
        stateDeath       = Animator.StringToHash("StateDeath");

        gameManager = GameManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate() 
    {
        healthBarRennder.UpdateHealthBarRotation();
    }
    public void TakeDamge(float damage)
    {
        gameManager.UpdateHealPlayerUI(damage);
        _health -= damage;
        healthBarRennder.UpdateHealthBarValue(_health);
        soundManager.PlayOneShot(audioClip);
        if (_health > 0)
        {
            Hited();
        }

        if (_health <= 0 && !isDead)
        {
            soundManager.PlayOneShot(deathAudioClip);
            gameManager.PlayerDead();
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        isDead = true;
        // float t = Random.Range(1, 4);
        animator.SetFloat(stateDeath,1);
    }

    public void Hited()
    {
        animator.SetTrigger(hitHash);
    }

    public void setInit(float health, float coinBonus) {
        _health = health;
        healthBarRennder.CreateHealthBar(transform, health);
    }
}
