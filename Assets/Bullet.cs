using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;

    public LayerMask playerMask;

    private void Awake() 
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if ((playerMask & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }

    }
}
