using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackUniversal : CharacterAttackUniversal
{
    private float radius = 1f;
    private float damage = 10f;
    private EnemyHealth enemyHealth;

    private void Awake() 
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }
    public override void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);

        if (hit.Length > 0)
        {
            // Debug.Log(hit[0].gameObject.name);
            Vector3 hitPos = hit[0].transform.position;
            hitPos.y += 1.3f;

            if (hit[0].transform.forward.x > 0)
            {
                hitPos.x += 0.3f;
            }
            else if (hit[0].transform.forward.x < 0)
            {
                hitPos.x -= 0.3f;
            }

            Instantiate(hitFx, hitPos, Quaternion.identity);

           

            gameObject.SetActive(false);
        }
    }

}
