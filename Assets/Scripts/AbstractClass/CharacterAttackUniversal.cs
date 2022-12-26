using UnityEngine;

public abstract class CharacterAttackUniversal : MonoBehaviour
{
    public LayerMask collisionLayer;
    // public GameObject hitFx;
    public abstract void DetectCollision();
}
