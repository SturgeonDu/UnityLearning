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
public class HeroCard : BaseUI
{
    public RawImage m_RawImage_HeroIcon;
    public RawImage m_RawImage_Rarity;
    public Text m_HeroName;
    public Image[] m_StarList;

    private string[] m_kRarity = new[] {"blue","blue","blue","purple","gold"};
    
    public void SetHeroData(HeroConfig kConfig)
    {
        m_HeroName.text = kConfig.name;

        int iStar = Random.Range(0, kConfig.rarity);
        for (int iIndex = 0; iIndex < m_StarList.Length; iIndex++)
        {
            m_StarList[iIndex].gameObject.SetActive(iIndex < kConfig.rarity);
            if (iIndex < kConfig.rarity)
            {
                string kSpriteName = iIndex < iStar ? "icon_rank_1" : "icon_rank_2";
                var kSprite = ResourcesManager.Instance.LoadSprite("MainAtlas",kSpriteName);
                if (kSprite != null)
                    m_StarList[iIndex].sprite = kSprite;
            }
        }

        m_RawImage_HeroIcon.texture = AssetBundleManager.Instance.LoadTexture(kConfig.portrait);
        m_RawImage_Rarity.texture = AssetBundleManager.Instance.LoadTexture(m_kRarity[kConfig.rarity - 1]);
    }
}
