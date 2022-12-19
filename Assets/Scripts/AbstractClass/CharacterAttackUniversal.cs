using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAttackUniversal : MonoBehaviour
{
    public LayerMask collisionLayer;
    public GameObject hitFx;
    public abstract void DetectCollision();
}
