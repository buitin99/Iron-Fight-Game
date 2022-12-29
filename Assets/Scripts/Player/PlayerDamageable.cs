using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{

    private float _health = 1000;

    private SoundManager soundManager;
    public AudioClip audioClip, deathAudioClip;
    [Range(0,1)]
    public float volumeScale;
    private PlayerController playerController;
    private bool isDead;

    private void Awake() 
    {
        soundManager = SoundManager.Instance;
        playerController = GetComponent<PlayerController>();
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
        soundManager.PlayOneShot(audioClip);
        _health -= damage;
        // healthBarRennder.UpdateHealthBarValue(_health);

        if (_health > 0)
        {
            if (Random.Range(0, 4) >= 2)
            {
                playerController.KnockDown();
            }
            else
            {
                playerController.Hited();
            }
            Debug.Log(_health);
        }

        if (_health <= 0 && !isDead)
        {
            soundManager.PlayOneShot(deathAudioClip);
            isDead = true;
            playerController.PlayerDead();
        }
    }
}
