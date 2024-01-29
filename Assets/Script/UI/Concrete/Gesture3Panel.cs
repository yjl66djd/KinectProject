using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始面板
/// </summary>
public class Gesture3Panel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/Gesture3Panel";

    public Gesture3Panel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
}
