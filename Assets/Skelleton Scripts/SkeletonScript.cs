using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;
namespace SkeletonScripts
{
    public static class Core
    {
        public static bool CreateFile(string nameOfFile, string extention, string lines, ReplaceSettings replaceSettings)
        {
            bool retVal = false;
            string directory = GetProjectDirectory();
            if (directory != null)
            {
                string path = directory + "\\" + nameOfFile + extention;
                if (PathValidCheck(path))
                {
                    // Create a new file   
                    StreamWriter sw = File.CreateText(path);
                    if (lines != null && lines != "")
                    {
                        string aLine = null;
                        StringReader strReader = new StringReader(lines);
                        if (replaceSettings != null)
                        {
                            while ((aLine = strReader.ReadLine()) != null)
                            {
                                sw.WriteLine(replaceSettings.Replace(aLine, nameOfFile));
                                retVal = true;
                            }
                        }
                        else
                        {
                            while ((aLine = strReader.ReadLine()) != null)
                            {
                                sw.WriteLine(aLine);
                                retVal = true;
                            }
                        }
                        
                    }
                    sw.Close();
                }
            }
            return retVal;
        }
        public static string GetPreview(string lines, ReplaceSettings replace = null, string nameOfFile = null)
        {
            if (replace != null)
            {
                string retVal = "";
                string aLine = null;
                StringReader strReader = new StringReader(lines);
                while ((aLine = strReader.ReadLine()) != null)
                {
                    retVal += replace.Replace(aLine, nameOfFile) + "\n";
                }
                return retVal;
            }
            else
            {
                return lines;
            }
        }
        public static string GetProjectDirectory()
        {
            string path;
            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    return path;
                }
            }
            return null;
        }
        public static bool PathValidCheck(string path)
        {
            string file = path + ".tmp";
            try
            {
                if (File.Exists(path)) return false;
                string fileName = Path.GetFileNameWithoutExtension(path);
                if (fileName != null && fileName.Length > 0)
                {
                    for (int i = 0; i < fileName.Length; i++)
                    {
                        char c = fileName[i];
                        if ((c == '_' || ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))) == false)
                        {
                            return false;
                        }
                    }
                    using (File.Create(file)) { }
                    File.Delete(file);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        //
        [System.Serializable]
        public class ReplaceSettings
        {
            public bool enableReplaceList = true;
            public List<FindAndReplace> ReplaceList = new List<FindAndReplace>();
            public bool enableReplaceWithFileNameList = true;
            public List<string> ReplaceWithFileNameList = new List<string>() { "#SCRIPTNAME#" };
            [System.Serializable]
            public struct FindAndReplace
            {
                public string Find;
                public string Replace;
            }
            private string Replace(string SourceLine, string Find, string Replace)
            {
                if (Find != Replace && Find != "" && Find != null)
                {
                    string result = SourceLine;
                    while (result.Contains(Find))
                    {
                        int Place = result.IndexOf(Find);
                        result = result.Remove(Place, Find.Length).Insert(Place, Replace);
                    }
                    return result;
                }
                return SourceLine;

            }
            public string Replace(string Source, string fileName = null)
            {
                if (Source != null && Source != "")
                {
                    foreach (FindAndReplace findAndReplace in ReplaceList)
                    {
                        Source = Replace(Source, findAndReplace.Find, findAndReplace.Replace);
                    }
                    if (enableReplaceWithFileNameList && fileName != null && fileName != "")
                    {
                        foreach (string find in ReplaceWithFileNameList)
                        {
                            Source = Replace(Source, find, fileName);
                        }
                    }
                }
                return Source;
            }
        }

    }
    [CreateAssetMenu(fileName = "SkeletonScriptDefinition", menuName = "Skeleton Script/Definition")]
    public class SkeletonScript : ScriptableObject
    {
        public Object rawFile;
        public string extention = ".";
        [TextArea(0,150)]
        public string lines;
        public void GetInfoFrom(string path)
        {
            lines = "";
            extention = "";
            if (File.Exists(path))
            {
                int counter = 0;
                string line;
                System.IO.StreamReader file =
                    new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    lines += line + "\n";
                    counter++;
                }
                file.Close();
            }
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
                lines = "";
                while ((line = file.ReadLine()) != null)
                {
                    lines += line + "\n";
                }
                file.Close();
            }
            else
            {

            }
        }
        //OLD
        /*
        public void CreateFile(string path, SkeletonScriptReplace skeletonScriptReplace = null, string nameOfFile = null)
        {
            if (!File.Exists(path))
            {
                // Create a new file   
                StreamWriter sw = File.CreateText(path + "\\" + nameOfFile + extention);
                if (skeletonScriptReplace != null)
                {
                    string aLine = null;
                    StringReader strReader = new StringReader(lines);
                    while ((aLine = strReader.ReadLine()) != null)
                    {
                        sw.WriteLine(skeletonScriptReplace.Replace(aLine, Path.GetFileNameWithoutExtension(nameOfFile)));
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
                string aLine = null;
                StringReader strReader = new StringReader(lines);
                while ((aLine = strReader.ReadLine()) != null)
                {
                    retVal += skeletonScriptReplace.Replace(aLine, Path.GetFileNameWithoutExtension(nameOfFile)) + "\n";
                }
            }
            return retVal;
        }
        */
    }
}