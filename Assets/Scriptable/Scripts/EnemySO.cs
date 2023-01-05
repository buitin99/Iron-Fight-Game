using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Enemy")]
public class EnemySO : ScriptableObject
{
    public float heath;
    public float moneyBonus;
    public float speed = 3;
    public float speedPatrol = 5;
    public float angularSpeed = 120;
    public float acceleration = 8;
    public float IdleTime = 2;
    public float alertTime = 10;

    public string enemyType;
    public GameObject enemy;

    public float Health
    {
        get => heath;
        set => heath = value;
    }

    public float Money
    {
        get => moneyBonus;
        set => moneyBonus = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    
}
