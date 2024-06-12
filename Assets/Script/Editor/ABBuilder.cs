//==========================
// FileName: ABBuilder.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================

using System;
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
        if (Directory.Exists(kOutputPath))
            DeleteDirectory(kOutputPath);
        if (!Directory.Exists(kOutputPath))
            Directory.CreateDirectory(kOutputPath);
        
        List<AssetBundleBuild> kBuildList = new List<AssetBundleBuild>();
        _collect(Application.dataPath + "/UI/Textures/","*.png","ui/textures/","textures.bd",ref kBuildList);
        _collect(Application.dataPath + "/UI/Atlas/","*.spriteatlas","ui/atlas/",ref kBuildList);
        _collect(Application.dataPath + "/UI/Prefab/","*.prefab","ui/prefab/",ref kBuildList);
        BuildAssetBundleOptions eOption = BuildAssetBundleOptions.None |
                                          BuildAssetBundleOptions.ForceRebuildAssetBundle;
        BuildPipeline.BuildAssetBundles(kOutputPath,kBuildList.ToArray(),eOption,EditorUserBuildSettings.activeBuildTarget);
    }
    
    ///删除文件夹下的所以文件
    public static void DeleteDirectory(string kPath)
    {
        try
        {
            DirectoryInfo kDirectory    = new DirectoryInfo(kPath);
            FileSystemInfo[] kFileInfo  = kDirectory.GetFileSystemInfos();
            foreach (FileSystemInfo kInfo in kFileInfo)
            {
                if (kInfo is DirectoryInfo)
                {
                    DirectoryInfo kDirInfo = new DirectoryInfo(kInfo.FullName);
                    kDirInfo.Delete(true);
                }
                else
                {
                    File.Delete(kInfo.FullName);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Clear directory error:" + e.ToString());
        }
    }
    
    /// <summary>
    /// 单独出包
    /// </summary>
    /// <param name="kRoot">资源所在的根目录</param>
    /// <param name="kSearchPattern">资源搜索格式</param>
    /// <param name="kBundleDir">AssetBundle输出路径</param>
    /// <param name="kBuildList"></param>
    /// <param name="bPackSeparately">true:独立打包,false:整体打包</param>
    private static void _collect(string kRoot,string kSearchPattern,string kBundleDir,ref List<AssetBundleBuild> kBuildList)
    {
        string[] kPaths = Directory.GetFiles(kRoot, kSearchPattern, SearchOption.AllDirectories);
        if(kPaths == null || kPaths.Length <= 0)
            return;
        foreach (var kFilePath in kPaths)
        {
            string kAssetPath       = kFilePath.Substring(kFilePath.IndexOf("Assets"));
            string kAssetName       = Path.GetFileNameWithoutExtension(kFilePath);
            string kBundleName      = kAssetName.ToLower() + ".bd";
            AssetBundleBuild kBuild = new AssetBundleBuild();
            kBuild.assetNames       = new string[1];
            kBuild.assetNames[0]    = kAssetPath;
            kBuild.assetBundleName  = kBundleDir + kBundleName;
            kBuildList.Add(kBuild);
        }
    }

    private static void _collect(string kRoot,string kSearchPattern,string kBundleDir,string kBundleName,ref List<AssetBundleBuild> kBuildList)
    {
        string[] kPaths = Directory.GetFiles(kRoot, kSearchPattern, SearchOption.AllDirectories);
        if(kPaths == null || kPaths.Length <= 0)
            return;
        AssetBundleBuild kBuild     = new AssetBundleBuild();
        kBuild.assetBundleName      = kBundleDir + kBundleName;
        List<string> kAssetPathList = new List<string>();
        List<string> kNameList = new List<string>();
        foreach (var kFilePath in kPaths)
        {
            string kAssetPath       = kFilePath.Substring(kFilePath.IndexOf("Assets"));
            string kAssetName       = Path.GetFileNameWithoutExtension(kFilePath);
            kAssetPathList.Add(kAssetPath);
            kNameList.Add(kAssetName);
        }

        kBuild.assetNames = kAssetPathList.ToArray();
        kBuild.addressableNames = kNameList.ToArray();
        kBuildList.Add(kBuild);
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
