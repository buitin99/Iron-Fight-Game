using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyBonus : MonoBehaviour
{
    public bool useMagnet = true, addForceOnAwake = true;
    public LayerMask layerMask;
    [SerializeField] private int point;
    public AudioClip audioClip;
    public float volumeScale = 1;
    private Rigidbody rb;
    private GameManager gameManager;
    private SoundManager soundManager;
    // private ObjectPooler objectPooler;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
        // objectPooler = ObjectPooler.Instance;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() 
    {
        if (addForceOnAwake)
        {
            Vector3 dir = Random.insideUnitSphere.normalized;
            rb.AddForce(dir*8f, ForceMode.Impulse);
        }    
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((layerMask & (1 << other.gameObject.layer)) != 0)
        {
            // objectPooler.InactiveObject("Money", gameObject);
            soundManager.PlayOneShot(audioClip, volumeScale);
        }
    }

    private void OnDisable() 
    {
        gameManager.UpdateCurrency(point);
    }
}
