using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables Enemies", menuName = "Scriptable Enemies Objects/New Scriptables")]
public class ScriptableEnemiesObject : ScriptableObject
{
    public SriptableEnemy[] enemies;
}
