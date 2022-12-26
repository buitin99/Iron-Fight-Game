using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    public float speed = 5f;
    public float rotateSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Vector3 direction = (Vector3)target.position - rb.position;
        // direction.Normalize();
        // float rotateAmount =  Vector3.Cross(direction, transform.up).z; 
        // rb.angularVelocity = -rotateAmount * rotateSpeed;
        // rb.velocity = transform.up*speed;
    }
}
