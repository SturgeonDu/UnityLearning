//==========================
// FileName: ABBuilder.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ABBuilder
{
    [MenuItem("AssetBundle/Build")]
    public static void BuildAssetBundle()
    {
        string kOutputPath = Application.streamingAssetsPath + "/AssetBundle/" + _getPlatformName(EditorUserBuildSettings.activeBuildTarget);
        if (!Directory.Exists(kOutputPath))
            Directory.CreateDirectory(kOutputPath);
        
        List<AssetBundleBuild> kBuildList = new List<AssetBundleBuild>();
        _collect(Application.dataPath + "/UI/Prefab/","*.prefab","ui/prefab/",ref kBuildList);
        
        
        BuildAssetBundleOptions eOption = BuildAssetBundleOptions.None |
                                          BuildAssetBundleOptions.ForceRebuildAssetBundle;
        BuildPipeline.BuildAssetBundles(kOutputPath,kBuildList.ToArray(),eOption,EditorUserBuildSettings.activeBuildTarget);
    }

    /// <summary>
    /// 单独出包
    /// </summary>
    /// <param name="kRoot"></param>
    /// <param name="kSearchPattern"></param>
    /// <param name="kBundleDir"></param>
    /// <param name="kBuildList"></param>
    private static void _collect(string kRoot,string kSearchPattern,string kBundleDir,ref List<AssetBundleBuild> kBuildList)
    {
        string[] kPaths = Directory.GetFiles(kRoot, kSearchPattern, SearchOption.AllDirectories);
        if(kPaths == null || kPaths.Length <= 0)
            return;
        foreach (var kFilePath in kPaths)
        {
            string kAssetPath  = kFilePath.Substring(kFilePath.IndexOf("Assets"));
            string kAssetName  = Path.GetFileNameWithoutExtension(kFilePath);
            string kBundleName = kAssetName.ToLower() + ".bd";
            AssetBundleBuild kBuild = new AssetBundleBuild();
            kBuild.assetNames       = new string[1];
            kBuild.assetNames[0]    = kAssetPath;
            kBuild.assetBundleName  = kBundleDir + kBundleName;
            
            kBuildList.Add(kBuild);
        }
    }

    private static string _getPlatformName(BuildTarget kPlatform)
    {
        if (kPlatform == BuildTarget.Android)
            return "Android";
        else if (kPlatform == BuildTarget.iOS)
            return "iOS";
        else if (kPlatform == BuildTarget.StandaloneWindows || kPlatform == BuildTarget.StandaloneWindows64)
            return "Windows";
        
        return "Windows";
    }
}
