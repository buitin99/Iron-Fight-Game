using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillBoard : MonoBehaviour
{
    [SerializeField] bool freeXZAxis = true;

    // Update is called once per frame
    void Update()
    {
        if (freeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
