//==========================
// FileName: UIManager.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //单例
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<UIManager>();
            return _instance;
        }
    }
    
    public Camera m_UICamera = null;
    private Dictionary<string, BaseUI> m_kUIMap = new Dictionary<string, BaseUI>();

    public void Init()
    {
        
    }

    public void OpenUI(string kAssetPath)
    {
        LoadAsset(kAssetPath, (kAssetName,asset) =>
        {
            if(asset == null)
                return;
            GameObject kInstance = Instantiate(asset, transform);
            kInstance.name = kAssetName + "|";
            var kBaseUI = kInstance.GetComponent<BaseUI>();
            kBaseUI.OnInit(kAssetPath,kAssetName);
            kBaseUI.Show();
            
            m_kUIMap.Add(kAssetPath,kBaseUI);
        });
    }

    public void LoadAsset(string kAssetPath, Action<string,GameObject> kCallback)
    {
        string kAssetName = Path.GetFileNameWithoutExtension(kAssetPath);
        AssetBundleManager.Instance.LoadAsset(kAssetPath, kAssetName, (asset)=>
        {
            if(asset == null)
                return;
            kCallback.Invoke(kAssetName,asset);
        });
    }

    public void CloseUI(string kAssetPath)
    {
        if(!m_kUIMap.ContainsKey(kAssetPath))
            return;
        var kUI = m_kUIMap[kAssetPath];
        kUI.Hide();
        m_kUIMap.Remove(kAssetPath);
        kUI.OnUnInit();
        AssetBundleManager.Instance.UnloadAsset(kUI.gameObject);
    }

    public void UnInit()
    {
        
    }
}
