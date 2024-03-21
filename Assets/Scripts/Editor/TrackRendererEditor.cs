using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrackRenderer))]
public class TrackRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TrackRenderer trackRenderer = (TrackRenderer)target;
        if (GUILayout.Button("Éú³É¹ìµÀ"))
        {
            trackRenderer.GenerateMesh();
        }
        DrawDefaultInspector();
    }
    
}
