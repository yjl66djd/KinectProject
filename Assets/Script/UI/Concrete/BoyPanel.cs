using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始面板
/// </summary>
public class BoyPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/BoyPanel";

    public BoyPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter(); 
        UITool.GetOrAddComponentInChildren<Button>("Continue").onClick.AddListener(() =>
        {
            GameRoot.Instance.Sexual = "Boy";
            GameRoot.Instance.sceneSystem.SetScene(new Scene1()); 
            AudioManager.PlayerAudio(AudioNames.ClickUI);
        });
        UITool.GetOrAddComponentInChildren<Button>("Back").onClick.AddListener(() =>
        {
            Pop();
            Push(new RenamePanel());
            AudioManager.PlayerAudio(AudioNames.ClickUI);
        });
    }
}
