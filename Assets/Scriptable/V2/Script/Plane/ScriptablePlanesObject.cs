using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables Plane", menuName = "Scriptable Plane Objects/New Scriptables")]
public class ScriptablePlanesObject : ScriptableObject
{
    public ScriptablePlane[] planes;
}
