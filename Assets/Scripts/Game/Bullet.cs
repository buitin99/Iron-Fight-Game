using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    // public GameObject impactEffect;
    private Rigidbody bulletRigidbody;
    private Vector3 dir;
    private float speed;
    private bool triggered;
    private float damage;
    private float force;
    private LayerMask layerMask;
    private ObjectPooler objectPooler;
    private void Awake() {
        objectPooler = ObjectPooler.Instance;
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(triggered) {
            FireBullet();
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        objectPooler.InactiveObject("Bullet", gameObject);
        ContactPoint contact = other.GetContact(0);
        if((layerMask & (1 << other.gameObject.layer)) != 0) {
            IDamageable damageable =  other.transform.GetComponentInParent<IDamageable>();
            if(damageable != null) {
                damageable.TakeDamge(damage);
            }
        } else {
            // GameObject obj = Instantiate(impactEffect, contact.point, Quaternion.LookRotation(contact.normal));
        }

        // if(other.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
        //     IDamageable damageable = other.transform.GetComponentInParent<ObstacleDamageable>();
        //     damageable.TakeDamge(contact.point, dir * 15);
        // }
    }

    private void FireBullet() {
        bulletRigidbody.velocity = dir * speed;
    }

    public void TriggerFireBullet(Vector3 _dir, float _speed, float _damage, float _force, LayerMask _layerMask) {
        dir = _dir;
        speed = _speed;
        damage = _damage;
        force = _force;
        layerMask = _layerMask;
        triggered = true;
        StartCoroutine(StartInactive());
    }

    IEnumerator StartInactive() {
        yield return new WaitForSeconds(10f);
        objectPooler.InactiveObject("Bullet",gameObject);
    }
 }
