using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;
[CustomEditor(typeof(SkeletonScripts.SkeletonScript), true)]
public class SkeletonScriptEditor : Editor
{
    public static string lines;
    public static Vector2 scroll;
    // Draw the property inside the given rect
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SkeletonScripts.SkeletonScript skeletonScript = (serializedObject.targetObject as SkeletonScripts.SkeletonScript);
        Undo.RecordObject(skeletonScript, "Skeleton script change properties");
        using (new GUILayout.VerticalScope("box"))
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rawFile"), new GUIContent("Raw File", "this gets the extention and lines from a file"));
            if (skeletonScript.rawFile == null)
            {
                GUI.enabled = false;
            }
            if (GUILayout.Button(GUI.enabled ? "Update Info From File" : "Missing Raw File to Update From"))
            {
                skeletonScript.UpdateInfo();
                EditorUtility.SetDirty(skeletonScript);
            }
        }
        GUI.enabled = true;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("extention"), new GUIContent("Extention", "the extention the new files will be (like .cs or .cg)"));
        //
        scroll = EditorGUILayout.BeginScrollView(scroll);
        lines = EditorGUILayout.TextArea(skeletonScript.lines, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Use in New Script"))
        {
            CreateSkeletonScriptWindow.skeletonScript = skeletonScript;
            CreateSkeletonScriptWindow.ShowWindow();
        }
        serializedObject.ApplyModifiedProperties();
    }
}