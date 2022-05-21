using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameController), true)]
[CanEditMultipleObjects]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("deck"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("CardPrefab"));
        }

        EditorGUILayout.Space();

        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Wagers");
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

            EditorGUILayout.LabelField("Game Specific Variables");
            // somehow put derived class properties here

            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }








    /*
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();

        inspector.Add(new Label("This is a custom inspector"));
        

        Box stagesBox = new Box();

        Toggle stagesToggle = new Toggle("Stages");
        SerializedProperty stagesProperty = serializedObject.FindProperty("useStages");
        stagesToggle.BindProperty(stagesProperty);
        stagesBox.Add(stagesToggle);

        if (stagesProperty.boolValue)
        {
            IntegerField currentStage = new IntegerField("Current Stage");
            currentStage.bindingPath = "currentStage";
            currentStage.BindProperty(serializedObject);
            stagesBox.Add(currentStage);

            IntegerField maxStage = new IntegerField("Max Stage");
            maxStage.bindingPath = "maxStage";
            maxStage.BindProperty(serializedObject);
            stagesBox.Add(maxStage);
        }

        inspector.Add(stagesBox);
        


        return inspector;
    }
    */
}
