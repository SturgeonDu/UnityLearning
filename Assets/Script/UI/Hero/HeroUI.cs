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

public class HeroData
{
    public string name;
    public int rarity;
    public int star;
    public string icon;

    public static HeroData RandomCreate(int iIndex)
    {
        return new HeroData()
        {
            name = "HeroName_"+ iIndex,
            rarity = Random.Range(1,3),
            star = Random.Range(3,5),
            icon = iIndex % 2 == 0 ? "H_Monkey01_portrait" : "H_Panda01_portrait",
        };
    }
}

public class HeroUI : MonoBehaviour
{
    public Button m_Button_Close;
    public ScrollRect m_kScrollRect;
    public GridLayoutGroup m_kContent;
    
    // Start is called before the first frame update
    void Start()
    {
        //随机生成20个英雄数据和卡牌
        for (int iIndex = 0; iIndex < 20; iIndex++)
        {
            var kData = HeroData.RandomCreate(iIndex);
            var kAsset = UIManager.Instance.m_CardAsset;
            var kInstance = Instantiate(kAsset);
            kInstance.transform.SetParent(m_kContent.transform,false);
            HeroCard kCard = kInstance.GetComponent<HeroCard>();
            kCard.SetHeroData(kData);
        }
        m_Button_Close.onClick.AddListener(OnBtn_Click);
    }
    
    void OnBtn_Click()
    {
        gameObject.SetActive(false);
    }
}
