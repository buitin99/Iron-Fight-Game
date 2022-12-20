using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public static bool playerDead;
    public abstract void ApplyDamage(float damage, bool knockDown);
}
