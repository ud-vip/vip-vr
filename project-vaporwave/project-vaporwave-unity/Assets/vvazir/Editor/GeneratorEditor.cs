using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generator))]
public class GeneratorInspector : Editor
{

    private Generator creator;

    private void RefreshCreator()
    {
        if (Application.isPlaying)
        {
            creator.CreateAsteroids();
        }
    }
    private void OnEnable()
    {
        creator = target as Generator;
        Undo.undoRedoPerformed += RefreshCreator;
    }
    private void OnDisable()
    {
        Undo.undoRedoPerformed -= RefreshCreator;
    }


    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck() && Application.isPlaying)
        {
            RefreshCreator();
        }
    }
}
