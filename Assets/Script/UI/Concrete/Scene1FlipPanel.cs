using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务面板
/// </summary>
public class Scene1FlipPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/Scene1FlipPanel";

    public Scene1FlipPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();
        
        UITool.GetOrAddComponent<Book>().OnFlip.AddListener(() =>
        { 
             if(UITool.GetOrAddComponent<Book>().currentPage == 2)
            { 
                GameRoot.Instance.Push(new Scene1Map());
            }
            //点击事件处理
        });
        
    }
}
