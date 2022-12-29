using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    private float _coinBonus;
    private float _health = 1000;
    [SerializeField]
    private HealthBarRennder healthBarRennder = new HealthBarRennder();
    private bool isDead;
    private SoundManager soundManager;
    public AudioClip audioClip, deathAudioClip;
    [Range(0,1)]
    public float volumeScale;

    private Enemy enemy;
    private void Awake() {
        soundManager = SoundManager.Instance;
        enemy = GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() 
    {
        healthBarRennder.UpdateHealthBarRotation();
    }

    public void TakeDamge(float damage)
    {
        soundManager.PlayOneShot(audioClip);
        _health -= damage;
        healthBarRennder.UpdateHealthBarValue(_health);

        if (_health > 0)
        {
            if (Random.Range(0, 4) >= 2)
            {
                enemy.KnockDown();
            }
            else
            {
                enemy.Hited();
            }
            Debug.Log(_health);
        }

        if (_health <= 0 && !isDead)
        {
            soundManager.PlayOneShot(deathAudioClip);
            isDead = true;
            enemy.Dead();
            
        }
    }

    public void setInit(float health, float coinBonus) {
        _health = health;
        _coinBonus = coinBonus;
        healthBarRennder.CreateHealthBar(transform, health);
    }

}
