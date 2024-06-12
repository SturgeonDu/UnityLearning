
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

/// <summary>
/// 配置管理器，加载管理配置
/// </summary>
public class ConfigManager : MonoBehaviour
{
    private static ConfigManager _instance = null;
    public static ConfigManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<ConfigManager>();
            return _instance;
        }
    }

    private const string CONFIG_URL = "http://192.168.1.91:5984/rs_config/_design/{0}/_view/{1}";
    private Dictionary<int, HeroConfig> m_kHeroConfigMap;
    
    public void Init()
    {
        m_kHeroConfigMap = new Dictionary<int, HeroConfig>();
        Coroutine kCoroutine = StartCoroutine(_asyncLoadConfig("heroes", "heroes"));
    }

    public HeroConfig GetHeroConfig(int iHeroID)
    {
        HeroConfig kConfig = null;
        if (m_kHeroConfigMap.TryGetValue(iHeroID, out kConfig))
            return kConfig;
        
        return null;
    }

    public void GetAllHeroConfig(ref List<HeroConfig> kList)
    {
        kList.Clear();
        kList.AddRange(m_kHeroConfigMap.Values);
    }

    public void UnInit()
    {
        //StopCoroutine(kCoroutine);
        StopAllCoroutines();
    }

    ///HTTP下载英雄配置表
    private IEnumerator<UnityWebRequestAsyncOperation> _asyncLoadConfig(string kTable,string kView)
    {
        string url = string.Format(CONFIG_URL, kTable, kView);
        using (UnityWebRequest kRequest = UnityWebRequest.Get(url))
        {
            kRequest.SetRequestHeader("Accept-Language", "zh_CN");
            kRequest.timeout = 30;
            yield return kRequest.SendWebRequest();
            
            if (!string.IsNullOrEmpty(kRequest.error))
            {
                Debug.LogError("[ConfigManager._asyncLoadConfig] Download config error:"+url);
                yield break;
            }

            if (kRequest.isDone)
            {
                string json = kRequest.downloadHandler.text;
                if (string.IsNullOrEmpty(json))
                    yield break;
                try
                {
                    JsonData kJsonData = JsonMapper.ToObject(json);
                    foreach (JsonData kLine in kJsonData["rows"])
                    {
                        HeroConfig kHeroConfig = JsonMapper.ToObject<HeroConfig>(kLine["value"].ToJson());
                        if (m_kHeroConfigMap.ContainsKey(kHeroConfig.id))
                            continue;
                        m_kHeroConfigMap[kHeroConfig.id] = kHeroConfig;
                    }
                    Debug.Log("Load hero config done,config count:" + m_kHeroConfigMap.Count);
                }
                catch (Exception e)
                {
                    Debug.LogError(string.Format("Config:{0}/{1} to json object error:{2}",kTable,kView, e.Message));
                }
            }
            kRequest.Dispose();
        }
    }
}