using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务面板
/// </summary>
public class RenamePanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/RenamePanel";

    public RenamePanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();
        
        UITool.GetOrAddComponentInChildren<Button>("Girl").onClick.AddListener(() =>
        {
            Push(new GirlPanel());
            AudioManager.PlayerAudio(AudioNames.ClickUI); 
        });  
        UITool.GetOrAddComponentInChildren<Button>("Boy").onClick.AddListener(() =>
        {
            Push(new BoyPanel());
            AudioManager.PlayerAudio(AudioNames.ClickUI);
        });
    }
}
