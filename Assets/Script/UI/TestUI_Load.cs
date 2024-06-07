//==========================
// FileName: TestUI_Load.cs
// Copyright (C) 2021-2024 Chengdu WhyNot Games Technology Co., Ltd. All Right Reserved.
// Author: DoWell
// CreateTime: #CreateTime#
// Description:
//==========================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TestUI_Load : MonoBehaviour
{
    public RawImage m_kImage_Rarity;
    public RawImage m_kRawImage_Avatar;
    public Image[] m_kStar;
    
    // Start is called before the first frame update
    void Start()
    {
        //var obj = Resources.Load("Atlas/MainAtlas/icon_rank_1");
        // SpriteAtlas sprite2d = Resources.Load<SpriteAtlas>("Atlas/MainAtlas");
        // Sprite kSprite = sprite2d.GetSprite("icon_rank_2");
        //
        // Texture blue = ResourcesManager.Instance.LoadAsset<Texture>("Textures/bg/blue");
        // m_kImage_Rarity.texture = blue;
        // Texture avatar = ResourcesManager.Instance.LoadAsset<Texture>("Textures/characters/H_Monkey01_portrait");
        // m_kRawImage_Avatar.texture = avatar;
    }
}
