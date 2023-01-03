using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayer;
    private void OnTriggerEnter(Collider other) 
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null && ((enemyLayer & (1 << other.gameObject.layer)) != 0))
        {
            damageable.TakeDamge(30);
        }
    }
}
