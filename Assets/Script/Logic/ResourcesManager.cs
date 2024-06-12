//==========================
// FileName: ResourcesManager.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// 资源加载容器：
/// 1.Editor 模式下的资源加载
/// 2.Runtime 模式下的资源加载
///     2.1 Resources.Load
///     2.2 AssetBundle.LoadAsset
/// </summary>
public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager _instance;
    public static ResourcesManager Instance 
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<ResourcesManager>();
            return _instance;
        }
    }

    private Dictionary<string, SpriteAtlas> m_kSpriteAtlasMap = new Dictionary<string, SpriteAtlas>();

    public void Init()
    {
        StartCoroutine(_asyncInit("MainAtlas"));
    }

    public Sprite LoadSprite(string kAtlasName, string kSpriteName)
    {
        if (!m_kSpriteAtlasMap.ContainsKey(kAtlasName))
            return null;
        return m_kSpriteAtlasMap[kAtlasName].GetSprite(kSpriteName);
    }

    private IEnumerator _asyncInit(string kAtlasName)
    {
        while (!AssetBundleManager.Instance.IsAssetBundleLoaded(kAtlasName.ToLower()))
        {
            yield return new WaitForEndOfFrame();
        }
        SpriteAtlas kAtlas = AssetBundleManager.Instance.LoadAtlas(kAtlasName);
        if(kAtlas != null)
            m_kSpriteAtlasMap.Add(kAtlasName,kAtlas);
    }
    
    public void UnInit()
    {
        
    }
}
