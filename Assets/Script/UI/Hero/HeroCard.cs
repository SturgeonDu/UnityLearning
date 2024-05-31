//==========================
// FileName: HeroCard.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///英雄卡牌 组件
public class HeroCard : MonoBehaviour
{
    public RawImage m_RawImage_HeroIcon;
    public RawImage m_RawImage_Rarity;
    public Text m_HeroName;
    public Image[] m_StarList;

    public void SetHeroData(HeroData kData)
    {
        m_HeroName.text = kData.name;
    }
}
