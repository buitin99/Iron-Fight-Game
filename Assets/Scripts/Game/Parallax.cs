using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{

    public Camera cam;
    public Transform subject;

    Vector3 startPos;
    // float startZ;
    // float distanceFromSubject => transform.position.z - subject.position.z; //2
    // float clipingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane)); // 1
    // float parallaxFactor => Mathf.Abs(distanceFromSubject) / clipingPlane;
    // Vector3 travel => (Vector3)cam.transform.position - startPos;
    // Vector2 newPos;
    // Start is called before the first frame update
    void Start()
    {
        // startPos = transform.position; //3
        // startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = startPos; //4
        // newPos = transform.position = startPos + travel*parallaxFactor;
        // transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
