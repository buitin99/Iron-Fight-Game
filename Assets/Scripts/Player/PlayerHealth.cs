using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{   
    public float playerHealth = 100f;
    private PlayerController playerController;

    private void Awake() 
    {
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

    public override void ApplyDamage(float damage, bool knockDown)
    {
        if (playerDead)
            return;

        playerHealth -= damage;

        if (playerHealth <= 0f)
        {
            playerController.PlayerDead();
            playerDead = true;
        }
    }
}
