using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public LayerMask playerLayer;
    private void OnTriggerEnter(Collider other) 
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null && ((playerLayer & (1 << other.gameObject.layer)) != 0))
        {
            damageable.TakeDamge(other.transform.position,5);
        }
    }
}
