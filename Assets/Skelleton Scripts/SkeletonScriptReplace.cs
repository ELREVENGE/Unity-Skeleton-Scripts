using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkeletonScripts
{
    [CreateAssetMenu(fileName = "SkeletonScriptReplaceDefinition", menuName = "Skeleton Script/Find And Replace Settings")]
    public class SkeletonScriptReplace : ScriptableObject
    {
        public Core.ReplaceSettings settings = new Core.ReplaceSettings();
    }
}