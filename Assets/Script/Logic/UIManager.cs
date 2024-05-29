//==========================
// FileName: UIManager.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================
using System.Collections;
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
    
    public HeroUI m_HeroUI;
    public HeroCard m_CardAsset;

}
