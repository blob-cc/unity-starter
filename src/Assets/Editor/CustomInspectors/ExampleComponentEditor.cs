using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExampleComponent))]
public class ExampleComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ExampleComponent example = (ExampleComponent)target;

        example.intValue = EditorGUILayout.IntField("Int Value", example.intValue);
        example.floatValue = EditorGUILayout.FloatField("Float Value", example.floatValue);

        if (GUILayout.Button("Randomize Values"))
        {
            example.intValue = Random.Range(0, 100);
            example.floatValue = Random.Range(0f, 10f);
        }

        DrawDefaultInspector();  // Draw the default inspector for the rest of the properties.
    }
}