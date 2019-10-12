#if  UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class LevelDataCreate
{
    [MenuItem ("Create/CreateLevelDataList")]
    static void LevelDataList ()
    {
        Level_ParameterList asset = ScriptableObject.CreateInstance<Level_ParameterList> ();

        AssetDatabase.CreateAsset (asset, "Assets/LevelDataList.asset");
        AssetDatabase.Refresh ();
    }
}

#endif
