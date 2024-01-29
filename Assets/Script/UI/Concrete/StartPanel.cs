using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始面板
/// </summary>
public class StartPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/StartPanel";

    public StartPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter(); 
        UITool.GetOrAddComponentInChildren<Button>("Start").onClick.AddListener(() =>
        { 
            GameRoot.Instance.Push(new RenamePanel());
            //点击事件处理
            AudioManager.PlayerAudio(AudioNames.ClickUI);
        });
    }
}
