//==========================
// FileName: ResourcesManager.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================

using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

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
    
    //同步加载(Sync Load)
    public T LoadAsset<T>(string kPath) where T : Object
    {
        return Resources.Load<T>(kPath);
    }

    ///异步加载(Async Load),协程(Coroutine),委托（Delegate）,C# Lambda表达式
    public void LoadAssetAsync<T>(string kPath,Action<Object> kCallback) where T : Object
    {
        StartCoroutine(_asyncLoad<T>(kPath, kCallback));
    }

    public void Unload()
    {
        Resources.UnloadUnusedAssets();
    }

    private IEnumerator _asyncLoad<T>(string kPath,Action<Object> kCallback) where T : Object
    {
        ResourceRequest kRequest = Resources.LoadAsync<T>(kPath);
        yield return kRequest;
    
        if (kRequest.isDone)
        {
            kCallback.Invoke(kRequest.asset);
            yield break;
        }
    }
}
