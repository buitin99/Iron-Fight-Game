using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWindowQuest : MonoBehaviour
{
    public LayerMask mask;

    private void OnTriggerEnter(Collider other) 
    {
        if ((mask & (1 << other.gameObject.layer)) != 0)
        {
            
        }

    }
    
}
