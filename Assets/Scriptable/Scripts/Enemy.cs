using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Enemy")]
public class Enemy : ScriptableObject
{
    public float heath = 100;
    public float moneyBonus = 100;
    public float speed = 3;
    public float speedPatrol = 5;
    public float angularSpeed = 120;
    public float acceleration = 8;
    public float IdleTime = 2;
    public float alertTime = 10;

    public string enemyType;
    public GameObject enemy;

}
