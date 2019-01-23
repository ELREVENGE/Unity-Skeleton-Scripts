using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
class CreateSkeletonScriptWindow : EditorWindow
{
    [MenuItem("Assets/Create/Skeleton Script/Create Skeleton Script", priority = 0)]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateSkeletonScriptWindow));
    }
    public static SkeletonScripts.SkeletonScript skeletonScript;
    public static SkeletonScripts.SkeletonScriptReplace skeletonScriptReplace;
    public static string path;
    public static string nameOfFile;
    public static bool displayPreview;
    public static string previewContent = "";
    public static string previewPath = "";
    public static bool error = false;
    void OnGUI()
    {
        // The actual window code goes here
        skeletonScript = EditorGUILayout.ObjectField("Skeleton Script", skeletonScript, typeof(SkeletonScripts.SkeletonScript), false) as SkeletonScripts.SkeletonScript;
        skeletonScriptReplace = EditorGUILayout.ObjectField("Skeleton Script Replace", skeletonScriptReplace, typeof(SkeletonScripts.SkeletonScriptReplace), false) as SkeletonScripts.SkeletonScriptReplace;
        if (skeletonScript != null)
        {
            GUI.enabled = false;
            path = SkeletonScripts.Core.GetProjectDirectory();
            if (path == null) path = "";
            EditorGUILayout.TextField(new GUIContent("Directory", "Select a file in the \"Project\" window to set Directory"), path);
            GUI.enabled = true;
            nameOfFile = EditorGUILayout.TextField("Name", nameOfFile);
            //if (path != null && path[path.Length - 1] == '\\') path.Remove(path.Length - 1);
            //path += "\\";
            previewPath = path + "/" + nameOfFile + skeletonScript.extention;
            if (path != null && nameOfFile != null && !File.Exists(previewPath))
            {
                displayPreview = EditorGUILayout.ToggleLeft("Display Preview", displayPreview);
                error = !SkeletonScripts.Core.PathValidCheck(previewPath);
                if (!error)
                {
                    if (GUILayout.Button("Create Skeleton Script"))
                    {
                        SkeletonScripts.Core.CreateFile(nameOfFile, skeletonScript.extention, skeletonScript.lines, skeletonScriptReplace.settings);
                        AssetDatabase.Refresh();
                    }
                }
                else
                {
                    GUI.enabled = false;
                    GUILayout.Button("Can't save because of invalid path or name");
                    GUI.enabled = true;
                }

                if (displayPreview)
                {
                    if (GUILayout.Button("Refresh Preview"))
                    {
                        previewContent = SkeletonScripts.Core.GetPreview(skeletonScript.lines, skeletonScriptReplace.settings, nameOfFile);
                        previewPath = path + "\\" + nameOfFile + skeletonScript.extention;
                    }
                    GUI.enabled = false;
                    EditorGUILayout.LabelField(new GUIContent(previewPath, "Where the Script will save to"));
                    EditorGUILayout.TextArea(previewContent, GUILayout.Height(position.height - 30));
                    GUI.enabled = true;
                }
            }
        }
        
    }
}
