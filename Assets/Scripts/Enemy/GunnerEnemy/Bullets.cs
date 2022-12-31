using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private float speed = 20f;

    [SerializeField] private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate() 
    {
        rb.velocity = Vector3.forward*speed;
    }

    private void OnCollisionEnter(Collision other) 
    {
        gameObject.SetActive(false);
    }
}
