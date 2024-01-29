using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务面板
/// </summary>
public class Scene1HuiBenPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/Scene1HuiBenPanel";

    public Scene1HuiBenPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();

        UITool.GetOrAddComponentInChildren<Button>("Start").onClick.AddListener (() =>
        {
            PanelManager.Pop();
            Push(new MainPanel());
            GameRoot.Instance.Scene1Animation1DisAble();
            AudioManager.PlayerAudio(AudioNames.ClickUI); 
        });
        UITool.GetOrAddComponentInChildren<Button>("Start").gameObject.SetActive(false); 
         
    }
}
