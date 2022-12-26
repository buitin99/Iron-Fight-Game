using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    private float _coinBonus;
    private float _health;
    [SerializeField]
    private HealthBarRennder healthBarRennder = new HealthBarRennder();
    private bool isDead;

    public AudioClip audioClip, deathAudioClip;
    [Range(0,1)]
    public float volumeScale;
    public UnityEvent<Vector3> OnTakeDamge;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamge(Vector3 hitPoint, Vector3 force, float damage)
    {
        _health -= damage;
        healthBarRennder.UpdateHealthBarValue(_health);
        OnTakeDamge?.Invoke(force);
        if (_health <= 0 && !isDead)
        {
            isDead = true;
        }
    }

}
