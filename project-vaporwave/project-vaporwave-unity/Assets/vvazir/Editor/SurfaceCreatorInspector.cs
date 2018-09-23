using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SurfaceCreator))]
public class SurfaceCreatorInspector : Editor
{

    private SurfaceCreator creator;

    private void RefreshCreator()
    {
        if (Application.isPlaying)
        {
            creator.Refresh();
        }
    }
    private void OnEnable()
    {
        creator = target as SurfaceCreator;
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
