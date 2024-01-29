using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始面板
/// </summary>
public class GesturePanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/GesturePanel";

    public GesturePanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter(); 
       
    }
}
