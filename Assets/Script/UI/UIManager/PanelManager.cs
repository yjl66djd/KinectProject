using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 面板管理器
/// </summary>
public class PanelManager 
{

    /// <summary>
    /// 存储UI 面板的栈
    /// </summary>
    private Stack<BasePanel> stackPanel;


    /// <summary>
    /// ui管理器
    /// </summary>
    private UIManager uIManager;
    private BasePanel Panel;


    public PanelManager()
    {
        stackPanel =   new Stack<BasePanel>();
        uIManager = new UIManager();    
    }


    /// <summary>
    /// UI入栈操作，显示一个面板
    /// </summary>
    /// <param name="nextPanel">要显示的面板</param>
    public void Push(BasePanel nextPanel)
    {
        if(stackPanel.Count > 0)
        {
            Panel = stackPanel.Peek();
            Panel.OnPause();
        
        }
        stackPanel.Push(nextPanel);
        GameObject panelGo = uIManager.GetSingleUI(nextPanel.UIType);
        nextPanel.Initialize(new UITool(panelGo));
        nextPanel.Initialize(uIManager);
        nextPanel.Initialize(this);
        nextPanel.OnEnter();
    }

    public void Pop(Action arg = null)
    {
        if(stackPanel.Count > 0)
        {
            stackPanel.Pop().OnExit();
        }
        if(stackPanel.Count > 0)
        {
            stackPanel.Peek().OnResume();
        }

    }

    /// <summary>
    /// 所有面板弹出
    /// </summary>
    public void PopAll()
    {
        while (stackPanel.Count>0)
        {
            stackPanel.Pop().OnExit();
        }
    }

}
