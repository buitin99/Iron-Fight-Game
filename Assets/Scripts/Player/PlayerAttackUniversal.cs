using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackUniversal : CharacterAttackUniversal
{
    private float radius = 1f;
    private float damage = 15f;
    private HealthController healthController;

    private void Awake() 
    {
        healthController = GetComponentInParent<HealthController>();
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
            Debug.Log(hit[0].gameObject.name);
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

            if (gameObject.CompareTag("LeftArm") || gameObject.CompareTag("LeftLeg"))
            {
                hit[0].GetComponent<HealthController>().ApplyDamage(damage, true);
                // healthController.ApplyDamage(damage, true);
            }
            else
            {
                hit[0].GetComponent<HealthController>().ApplyDamage(damage, false);
                // healthController.ApplyDamage(damage, false);
            }

            gameObject.SetActive(false);
        }
    }   
}
