using UnityEngine;

public class DeactiveVFX : MonoBehaviour
{
    public float timer = 2f;

    private void Start() {
        Invoke("DeactiveAfterTime", timer);
    }


    void DeactiveAfterTime()
    {
        // gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
