using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(SpawnMap))]
public class EditPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property);
    }
}

