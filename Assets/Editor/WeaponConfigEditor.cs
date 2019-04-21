using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(WeaponSettings))]
public class WeaponConfigEditor : Editor
{
    private WeaponSettings t;
    private SerializedObject GetTarget;
    private SerializedProperty ThisList;
    private int listSize;
    private List<bool> foldouts = new List<bool>();


    private void OnEnable()
    {
        t = (WeaponSettings)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("weapons");
    }

    public override void OnInspectorGUI()
    {
        GetTarget.Update();
        Repaint();
        listSize = ThisList.arraySize;
        listSize = EditorGUILayout.IntField("List Size", listSize);

        if (listSize != ThisList.arraySize)
        {
            while (listSize > ThisList.arraySize)
                ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
            while (listSize < ThisList.arraySize)
                ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
        }

        for (int i = 0; i < ThisList.arraySize; i++)
            foldouts.Add(false);

        EditorGUI.indentLevel++;
        for (int i = 0; i < listSize; i++)
        {
            foldouts[i] = EditorGUILayout.Foldout(foldouts[i], "Weapon " + (i + 1));
            if (!foldouts[i])
                continue;

            EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            var itemOfList = ThisList.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(itemOfList.FindPropertyRelative("Type"));
            //EditorGUILayout.PropertyField(itemOfList.FindPropertyRelative("ShootStats"), true);
            EditorGUILayout.LabelField("Stats");
            EditorGUI.indentLevel++;
            var stats = itemOfList.FindPropertyRelative("ShootStats");
            EditorGUILayout.PropertyField(stats.FindPropertyRelative("Damage"));
            EditorGUILayout.PropertyField(stats.FindPropertyRelative("Speed"));
            if (itemOfList.FindPropertyRelative("Type").enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(stats.FindPropertyRelative("ExplosionRadius"));
                EditorGUILayout.PropertyField(stats.FindPropertyRelative("EnemyMask"));
                EditorGUILayout.PropertyField(stats.FindPropertyRelative("ExplosionForce"));
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.PropertyField(itemOfList.FindPropertyRelative("Color"));
            EditorGUILayout.PropertyField(itemOfList.FindPropertyRelative("DelayBetweenShoot"));
            EditorGUI.indentLevel--;
        }
        EditorGUI.indentLevel--;

        GetTarget.ApplyModifiedProperties();
    }
}
