//==========================
// FileName: AssetBundleManager.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
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

    private Dictionary<string, AssetBundle> m_kBundleMap = null;

    public void Init()
    {
        m_kBundleMap = new Dictionary<string, AssetBundle>();
        //load asset bundle
    }
}
