using UnityEngine;

public class RangeWeapon : Weapon
{
    public Transform shootPosition;
    // public ParticleSystem shootEffect;
    public float speedBullet;
    [Range(0,1)] public float volumeScale;

    private SoundManager soundManager;
    private ObjectPooler objectPooler;
    protected override void Awake() 
    {
        base.Awake();
        soundManager = SoundManager.Instance;
        objectPooler = ObjectPooler.Instance;
    }

    
    public override void Shoot(Transform target, LayerMask targets, string namelayerMask)
    {
        if (Time.time >= timeNextShoot)
        {
            OnShoot?.Invoke();
            // shootEffect.Play();
            GameObject c_bullet = objectPooler.SpawnObject("Bullet", shootPosition.position, Quaternion.identity);
            c_bullet.layer = LayerMask.NameToLayer(namelayerMask);
            soundManager.PlayOneShot(audioClip, volumeScale);
            c_bullet.GetComponent<Bullet>().TriggerFireBullet(shootPosition.forward.normalized, speedBullet, damage, force, targets);
            timeNextShoot = Time.time + delayShoot;
        }
    }

}
