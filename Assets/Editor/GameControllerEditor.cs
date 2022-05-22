using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Reflection;
using System;

[CustomEditor(typeof(GameController), true)]
[CanEditMultipleObjects]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MonoBehaviour selectedMono;
        if (target.GetType() == typeof(BlackjackController))
            selectedMono = (BlackjackController)target;
        if (target.GetType() == typeof(CardCounterController))
            selectedMono = (CardCounterController)target;
        if (target.GetType() == typeof(CardMatcherController))
            selectedMono = (CardMatcherController)target;
        if (target.GetType() == typeof(CardSorterController))
            selectedMono = (CardSorterController)target;
        else
            selectedMono = (GameController)target;

        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(selectedMono), target.GetType(), false);

        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("CardPrefab"));
        }

        EditorGUILayout.Space();

        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Wagers", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("minWager"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxWager"));

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        {
            bool stagesToggle = serializedObject.FindProperty("useStages").boolValue;
            stagesToggle = EditorGUILayout.BeginToggleGroup("Stages", stagesToggle);
            serializedObject.FindProperty("useStages").boolValue = stagesToggle;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("currentStage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxStage"));

            EditorGUILayout.EndToggleGroup();
        }

        EditorGUILayout.Space();
        
        {
            bool timerToggle = serializedObject.FindProperty("useTimer").boolValue;
            timerToggle = EditorGUILayout.BeginToggleGroup("Timer", timerToggle);
            serializedObject.FindProperty("useTimer").boolValue = timerToggle;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxTimeSeconds"));

            EditorGUILayout.EndToggleGroup();
        }

        EditorGUILayout.Space();

        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Game Specific Variables", EditorStyles.boldLabel);

            FieldInfo[] childFields = target.GetType().GetFields(BindingFlags.DeclaredOnly
                                                                | BindingFlags.Instance
                                                                | BindingFlags.Public
                                                                | BindingFlags.NonPublic);

            foreach (FieldInfo field in childFields)
            {
                if (field.IsPublic || field.GetCustomAttribute(typeof(SerializeField)) != null)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(field.Name));
                }
            }


            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
