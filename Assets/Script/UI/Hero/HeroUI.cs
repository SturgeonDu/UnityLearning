//==========================
// FileName: HeroUI.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : BaseUI
{
    public Button m_kCloseButton;
    public ScrollRect m_kScrollRect;
    
    private List<HeroConfig> m_kAllHeroList = new List<HeroConfig>();
    private GameObject m_kHeroCardAsset = null;
    private Dictionary<int, BaseUI> kHeroCardMap = new Dictionary<int, BaseUI>();

    public void OnBtn_Close()
    {
        Close();
    }
    
    public override void Show()
    {
        base.Show();
        m_kCloseButton.onClick.AddListener(OnBtn_Close);
        ConfigManager.Instance.GetAllHeroConfig(ref m_kAllHeroList);
        
        UIManager.Instance.LoadAsset("UI/Prefab/HeroCard.prefab",(kAssetName,asset) => {
            if(asset == null)
                return;
            m_kHeroCardAsset = asset;
            _createHeroCard();
        });
    }

    private void _createHeroCard()
    {
        for (int iIndex = 0; iIndex < Mathf.Min(50,m_kAllHeroList.Count); iIndex++)
        {
            HeroConfig kConfig = m_kAllHeroList[iIndex];
            var kInstance = Instantiate(m_kHeroCardAsset, m_kScrollRect.content);
            kInstance.name = "HeroCard|";
            HeroCard kCard = kInstance.GetComponent<HeroCard>();
            kCard.OnInit("UI/Prefab/HeroCard.prefab", "HeroCard");
            kCard.SetHeroData(kConfig);
            
            kHeroCardMap.Add(kConfig.id,kCard);
        }
    }
}