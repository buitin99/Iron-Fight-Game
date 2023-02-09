using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables Players", menuName = "Scriptable Players Objects/New Scriptables")]
public class ScriptablePlayersObject : ScriptableObject
{
    public ScriptablePlayer[] players;
}
