//==========================
// FileName: AssetBundleManager.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class AssetBundleManager : MonoBehaviour
{
    public class Bundle
    {
        public string m_kName;
        public int m_iNum;
        public AssetBundle m_kBundle;

        public bool Unload()
        {
            m_iNum--;
            if (m_iNum <= 0)
            {
                m_kBundle.Unload(false);
                m_kBundle = null;
                m_iNum = 0;
            }

            return m_iNum <= 0;
        }

        public Object LoadAsset(string kAssetName)
        {
            m_iNum++;
            return m_kBundle.LoadAsset(kAssetName);
        }
    }
    //单例
    private static AssetBundleManager _instance = null;
    public static AssetBundleManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<AssetBundleManager>();
            return _instance;
        }
    }
    
    private Dictionary<string, Bundle> m_kBundleMap = null;

    public void Init()
    {
        m_kBundleMap = new Dictionary<string, Bundle>();
        //load asset bundle
        LoadAssetBundle("textures","ui/textures/textures.bd");
        LoadAssetBundle("mainatlas","ui/atlas/mainatlas.bd");
        // LoadAssetBundle("textures","ui/textures/textures.bd");
    }

    public bool IsAssetBundleLoaded(string kBundleName)
    {
        if (!m_kBundleMap.ContainsKey(kBundleName))
            return false;
        return m_kBundleMap[kBundleName].m_kBundle != null;
    }
    
    public SpriteAtlas LoadAtlas(string kAtlasName)
    {
        string kBundleName = kAtlasName.ToLower();
        Bundle kRes = null;
        if (!m_kBundleMap.TryGetValue(kBundleName, out kRes))
            return null;
        return kRes.LoadAsset(kAtlasName) as SpriteAtlas;
    }

    public Texture LoadTexture(string kTextureName)
    {
        string kBundleName = "textures";
        if (!IsAssetBundleLoaded(kBundleName))
            return null;
        Bundle kRes = null;
        if (!m_kBundleMap.TryGetValue(kBundleName, out kRes))
            return null;
        return kRes.LoadAsset(kTextureName) as Texture;
    }

    ///从bundle里加载prefab资源
    public void LoadAsset(string kAssetPath,string kAssetName, Action<GameObject> kCallback)
    {
        string kBundleName = kAssetName.ToLower();
        string kBundlePath = kAssetPath.Replace(".prefab", ".bd").ToLower();
        LoadAssetBundle(kBundleName,kBundlePath, () =>
        {
            if(!IsAssetBundleLoaded(kBundleName))
                return;
            GameObject kGO = m_kBundleMap[kBundleName].LoadAsset(kAssetName) as GameObject;
            kCallback.Invoke(kGO);
        });
    }

    public void UnloadAsset(GameObject kInstance)
    {
        string kAssetName = kInstance.name;
        kAssetName = kAssetName.Substring(0, kAssetName.Length - 1);
        string kBundleName = kAssetName.ToLower();
        if (IsAssetBundleLoaded(kBundleName))
        {
            if (m_kBundleMap[kBundleName].Unload())
                m_kBundleMap.Remove(kBundleName);
        }

        DestroyImmediate(kInstance);
    }

    public void LoadAssetBundle(string kBundleName,string kPath,Action kCallback = null)
    {
        if (m_kBundleMap.ContainsKey(kBundleName))
        {
            if(kCallback != null)
                kCallback.Invoke();
            return;
        }

        string kFilePath = Application.streamingAssetsPath + "/AssetBundle/Windows/" + kPath;
        StartCoroutine(_asyncLoadAssetBundle(kBundleName,kFilePath,kCallback));
    }

    public void Unload(string kName)
    {
        if(!m_kBundleMap.ContainsKey(kName))
            return;
        
        var kBundle = m_kBundleMap[kName];
        kBundle.Unload();
    }

    private IEnumerator<AssetBundleCreateRequest> _asyncLoadAssetBundle(string kBundleName,string kPath,Action kCallback)
    {
        AssetBundleCreateRequest kRequest = AssetBundle.LoadFromFileAsync(kPath);
        yield return kRequest;
        if (kRequest.isDone)
        {
            m_kBundleMap.Add(kBundleName,new Bundle(){m_kName = kBundleName,m_kBundle = kRequest.assetBundle,m_iNum = 0});
            if(kCallback != null)
                kCallback.Invoke();
        }
    }

    public void UnInit()
    {
        m_kBundleMap.Clear();
    }
}
