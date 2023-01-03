using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    public float force;
    public AudioClip audioClip;
    [SerializeField] protected float damage, delayShoot;
    protected float timeNextShoot;
    private Animator _animator;
    private int shootHash;
    public UnityEvent OnShoot;
    public abstract void Shoot(Transform target, LayerMask targets, String namelayerMask);
    protected virtual void Awake() 
    {
        _animator = GetComponent<Animator>();
        shootHash = Animator.StringToHash("Shoot");
    }

    protected virtual void OnEnable() 
    {
        OnShoot.AddListener(WeaponPlayAnimation);
    }

    private void WeaponPlayAnimation()
    {
        // _animator.SetTrigger(shootHash);
    }
    
    protected virtual void OnDisable() 
    {
        OnShoot.RemoveListener(WeaponPlayAnimation);
    }
}
