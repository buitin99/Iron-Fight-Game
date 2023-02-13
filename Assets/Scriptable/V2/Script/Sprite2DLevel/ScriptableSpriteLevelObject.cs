using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables Sprite Level", menuName = "Scriptable Sprite Level Objects/New Scriptables")]
public class ScriptableSpriteLevelObject : ScriptableObject
{
    public ScriptableSpriteLevel[] spriteLevels;
}
