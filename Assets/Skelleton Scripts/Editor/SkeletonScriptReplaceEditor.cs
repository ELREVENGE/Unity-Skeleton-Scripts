﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;
[CustomEditor(typeof(SkeletonScripts.SkeletonScriptReplace), true)]
public class SkeletonScriptReplaceEditor : Editor{
    public static bool expandFileName = true;
    public static bool expandReplaceWith = true;
    // Draw the property inside the given rect
    public override void OnInspectorGUI()
    {
        SkeletonScripts.SkeletonScriptReplace skeletonScriptReplace = (serializedObject.targetObject as SkeletonScripts.SkeletonScriptReplace);
        using (new GUILayout.VerticalScope("box"))
        {
            skeletonScriptReplace.settings.enableReplaceWithFileNameList = EditorGUILayout.ToggleLeft("Replace With File Name", skeletonScriptReplace.settings.enableReplaceWithFileNameList);
            if (!skeletonScriptReplace.settings.enableReplaceWithFileNameList)
            {
                GUI.enabled = false;
            }
            EditorGUI.indentLevel++;
            using (new GUILayout.VerticalScope("box"))
            {
                expandFileName = EditorGUILayout.Foldout(expandFileName, new GUIContent(expandFileName ? "Hide" : "Show"));
                if (expandFileName)
                {
                    int count = skeletonScriptReplace.settings.ReplaceWithFileNameList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        skeletonScriptReplace.settings.ReplaceWithFileNameList[i] = EditorGUILayout.DelayedTextField("Find", skeletonScriptReplace.settings.ReplaceWithFileNameList[i]);
                    }
                    if (GUILayout.Button("Add"))
                    {
                        skeletonScriptReplace.settings.ReplaceWithFileNameList.Add("");
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        if (count > 0) skeletonScriptReplace.settings.ReplaceWithFileNameList.RemoveAt(count - 1);
                    }
                    
                }
                EditorGUI.indentLevel--;
                GUI.enabled = true;
            }
        }
        using (new GUILayout.VerticalScope("box"))
        {
            skeletonScriptReplace.settings.enableReplaceList = EditorGUILayout.ToggleLeft("Replace With", skeletonScriptReplace.settings.enableReplaceList);
            if (!skeletonScriptReplace.settings.enableReplaceList)
            {
                GUI.enabled = false;
            }
            EditorGUI.indentLevel++;
            using (new GUILayout.VerticalScope("box"))
            {
                expandReplaceWith = EditorGUILayout.Foldout(expandReplaceWith, new GUIContent(expandReplaceWith ? "Hide" : "Show"));
                if (expandReplaceWith)
                {
                    int count = skeletonScriptReplace.settings.ReplaceList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        using (new GUILayout.VerticalScope("box"))
                        {
                            SkeletonScripts.Core.ReplaceSettings.FindAndReplace findAndReplace = skeletonScriptReplace.settings.ReplaceList[i];
                            findAndReplace.Find = EditorGUILayout.DelayedTextField("Find", findAndReplace.Find);
                            findAndReplace.Replace = EditorGUILayout.DelayedTextField("Replace With", findAndReplace.Replace);
                            skeletonScriptReplace.settings.ReplaceList[i] = findAndReplace;
                        }
                    }
                    //using (var verticalScope = new GUILayout.VerticalScope("box"))
                    {
                        if (GUILayout.Button("Add"))
                        {
                            skeletonScriptReplace.settings.ReplaceList.Add(new SkeletonScripts.Core.ReplaceSettings.FindAndReplace());
                        }
                        if (GUILayout.Button("Remove"))
                        {
                            if (count > 0) skeletonScriptReplace.settings.ReplaceList.RemoveAt(count - 1);
                        }
                    }
                }
                EditorGUI.indentLevel--;
                GUI.enabled = true;
            }
        }
            
        if (GUILayout.Button("Use in new Script"))
        {
            CreateSkeletonScriptWindow.skeletonScriptReplace = skeletonScriptReplace;
            CreateSkeletonScriptWindow.ShowWindow();
        }
}

    /*
    public Object rawFile;
    public string extention = ".";
    public List<string> lines;
    public void GetInfoFrom(string path, bool setDefaultFileName)
    {
        lines.Clear();
        if (File.Exists(path))
        {
            int counter = 0;
            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
                counter++;
            }
            file.Close();
        }
    }
    public void CreateFile(string path, SkeletonScriptReplace skeletonScriptReplace = null, string nameOfFile = null)
    {
        if (!File.Exists(path))
        {
            // Create a new file   
            StreamWriter sw = File.CreateText(path + "\\" + nameOfFile + extention);
            if (skeletonScriptReplace != null)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i].ToString();
                    sw.WriteLine(skeletonScriptReplace.Replace(line, Path.GetFileNameWithoutExtension(nameOfFile)));
                }
            }
            sw.Close();
        }
    }

    public string GetPreview(SkeletonScriptReplace skeletonScriptReplace = null, string nameOfFile = null)
    {
        string retVal = "";
        if (skeletonScriptReplace != null)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i].ToString();
                retVal += skeletonScriptReplace.Replace(line, Path.GetFileNameWithoutExtension(nameOfFile)) + "\n";
            }
        }
        return retVal;
    }
    [ContextMenu("Update info from file")]
    public void UpdateInfo()
    {
        if (rawFile != null)
        {
            string path = AssetDatabase.GetAssetPath(rawFile);
            extention = Path.GetExtension(path);
            System.IO.StreamReader file = new System.IO.StreamReader(Path.GetFullPath(path));
            string line;
            lines.Clear();
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();
        }
        else
        {

        }
    }*/
}

