using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务面板
/// </summary>
public class Scene4Map : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/Scene4Map";

    public Scene4Map() : base(new UIType(path))
    {
         
    }
    public override void OnEnter()
    {
        base.OnEnter(); 

        UITool.GetOrAddComponentInChildren<Button>("MapPoint").onClick.AddListener(() =>
        {
            PanelManager.PopAll();
            Push(new MainPanel());
            GameRoot.Instance.Scene3Animation1EnAble();
            AudioManager.PlayerAudio(AudioNames.ClickUI);
            //点击事件处理  
        });
    }
}
