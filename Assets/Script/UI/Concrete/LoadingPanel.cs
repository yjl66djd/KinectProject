using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务面板
/// </summary>
public class LoadingPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/LoadingPanel";

    public LoadingPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();
        UITool.GetOrAddComponentInChildren<Button>("Go").onClick.AddListener(() =>
        {
            //GameRoot.Instance.Pop(null);
            GameRoot.Instance.sceneSystem.SetScene(new Scene1());
            //点击事件处理
            AudioManager.PlayerAudio(AudioNames.ClickUI);
        });

    }
}
