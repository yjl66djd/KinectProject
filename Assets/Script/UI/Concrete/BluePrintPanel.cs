using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrintPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/BluePrintPanel";

    public BluePrintPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();
      
    }



}
