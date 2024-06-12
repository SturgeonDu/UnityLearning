//==========================
// FileName: BaseUI.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected string m_kUIPath = string.Empty;
    protected string m_kName = string.Empty;
    ///初始化调用
    public virtual void OnInit(string kPath,string kName)
    {
        m_kUIPath = kPath;
        m_kName = kName;
    }

    ///显示时调用
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    ///隐藏时调用
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Close()
    {
        UIManager.Instance.CloseUI(m_kUIPath);
    }

    ///回收时调用
    public virtual void OnUnInit()
    {
        
    }
}
