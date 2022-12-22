using UnityEngine.Events;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    public UnityEvent<Vector3> OnTakeDamge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamge(Vector3 hitPoint, Vector3 force, float damage = 0)
    {
        OnTakeDamge?.Invoke(force);
    }
}
