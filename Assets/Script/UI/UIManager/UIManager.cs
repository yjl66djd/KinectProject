using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理ui创建与销毁
public class UIManager
{
    //存储所有UI信息
    private Dictionary<UIType, GameObject> dicUI;

    public UIManager()
    {
        dicUI = new Dictionary<UIType, GameObject>();
    }

    /// <summary>
    /// 获取一个UI对象
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("Canvas");

        if (!parent)
        {
            Debug.LogError("Canvas不存在");
            return null;
        }
        if (dicUI.ContainsKey(type))
        {
            return dicUI[type];
        }

        GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path),parent.transform);
        ui.name = type.Name;
        dicUI.Add(type, ui);
        return ui;  
    }

    /// <summary>
    /// 销毁UI对象
    /// </summary>
    /// <param name="type">UI信息</param>
    public void DestroyUI(UIType type)
    {
        if (dicUI.ContainsKey(type))
        {
            GameObject.Destroy(dicUI[type]);
            dicUI.Remove(type);
        }
    }
}
