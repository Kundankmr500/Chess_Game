using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveAssetBundle : EditorWindow {

    [MenuItem("Window/Save AssetBundles")]
    public static void Open()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        AssetDatabase.Refresh();
    }
}
