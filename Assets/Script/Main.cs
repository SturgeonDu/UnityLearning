//==========================
// FileName: Main.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssetBundleManager.Instance.Init();
        ResourcesManager.Instance.Init();
        ConfigManager.Instance.Init();
        UIManager.Instance.Init();

        StartCoroutine(_init());
    }

    private IEnumerator _init()
    {
        while (!AssetBundleManager.Instance.IsAssetBundleLoaded("textures") || !AssetBundleManager.Instance.IsAssetBundleLoaded("mainatlas"))
        {
            yield return new WaitForEndOfFrame();
        }
        //打开UI
        UIManager.Instance.OpenUI("UI/Prefab/HeroUI.prefab");
    }

    void OnDestroy()
    {
        UIManager.Instance.UnInit();
        ConfigManager.Instance.UnInit();
        ResourcesManager.Instance.UnInit();
        AssetBundleManager.Instance.UnInit();
    }
}
