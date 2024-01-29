using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务面板
/// </summary>
public class Scene4FlipPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/Scene4FlipPanel";

    public Scene4FlipPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter(); 

        UITool.GetOrAddComponent<Book>().OnFlip.AddListener(() =>
        { 
             if(UITool.GetOrAddComponent<Book>().currentPage == 2)
            { 
                GameRoot.Instance.Push(new Scene4Map());
            }
            //点击事件处理
        });
    }
}
