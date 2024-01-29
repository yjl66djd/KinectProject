using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/TipsPanel";

    public TipsPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();
      
    }



}
