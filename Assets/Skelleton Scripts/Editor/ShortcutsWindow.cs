using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShortcutsWindow : EditorWindow
{
    [MenuItem("Assets/Create/Skeleton Script/Shortcut", priority = 0)]
    [MenuItem("Window/Skeleton Scripts/Shortcut Editor", priority = 0)]
    public static void ShowWindow()
    {
        GetWindow(typeof(ShortcutsWindow));
    }
    public static MonoScript SaveTo;
    private void OnGUI()
    {
        SaveTo = EditorGUILayout.ObjectField("Saves To", SaveTo, typeof(MonoScript), false) as MonoScript;
    }
}
