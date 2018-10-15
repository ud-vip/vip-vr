using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureCreator))]
public class TextureCreatorInspector : Editor
{

    private TextureCreator creator;

    private void RefreshCreator()
    {
        if (Application.isPlaying)
        {
            creator.FillTexture();
        }
    }
    private void OnEnable()
    {
        creator = target as TextureCreator;
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
