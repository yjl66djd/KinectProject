using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlandPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/BlandPanel";

    public BlandPanel() : base(new UIType(path))
    {


    }
    public override void OnEnter()
    {
        base.OnEnter();
      
    }



}
