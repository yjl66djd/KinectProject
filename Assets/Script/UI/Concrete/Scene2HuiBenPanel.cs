using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务面板
/// </summary>
public class Scene2HuiBenPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/Scene2HuiBenPanel";

    public Scene2HuiBenPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();

        UITool.GetOrAddComponentInChildren<Button>("Start").onClick.AddListener (() =>
        {
            PanelManager.PopAll();
            Push(new MainPanel());
            GameRoot.Instance.Scene2Animation1EnAble();
            AudioManager.PlayerAudio(AudioNames.ClickUI);
        });
        UITool.GetOrAddComponentInChildren<Button>("Start").gameObject.SetActive(false); 
         
    }
}
