using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始面板
/// </summary>
public class Gesture1Panel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/Gesture1Panel";

    public Gesture1Panel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter(); 
       
    }
}
