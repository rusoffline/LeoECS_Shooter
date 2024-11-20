﻿using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadFieldAttribute))]
public class ReadFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
