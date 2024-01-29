using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI父类
public class BasePanel
{
    /// <summary>
    /// UI信息
    /// </summary>
    public UIType UIType { get; private set; }
    /// <summary>
    /// UI管理工具
    /// </summary>
    public UITool UITool { get; private set; }
    /// <summary>
    /// 面板管理器
    /// </summary>
    public PanelManager PanelManager { get; private set; }

    /// <summary>
    /// UI管理器
    /// </summary>
    public UIManager UIManager { get; private set; }


    public BasePanel (UIType uiType)
    {
        this.UIType = uiType;
    }
    /// <summary>
    /// 初始化UItool
    /// </summary>
    /// <param name="tool"></param>
    public void Initialize(UITool tool)
    {
        UITool = tool;
    }
    /// <summary>
    /// 初始化任务管理器
    /// </summary>
    /// <param name="panelManager"></param>
    public void Initialize(PanelManager panelManager)
    {
        PanelManager = panelManager;    
    }

    /// <summary>
    /// 初始化UI管理器
    /// </summary>
    /// <param name="uIManager"></param>
    public void Initialize(UIManager uIManager)
    {
        UIManager = uIManager;
    }



    /// <summary>
    /// UI进入时操作，只执行一次
    /// </summary>
    public virtual void OnEnter()
    {
    }
    /// <summary>
    /// UI暂停时执行操作
    /// </summary>
    public virtual void OnPause()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }
    /// <summary>
    /// UI继续时执行操作
    /// </summary>
    public virtual void OnResume()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }
    /// <summary>
    /// UI退出时执行操作
    /// </summary>
    public virtual void OnExit()
    {
        UIManager.DestroyUI(UIType);
    }


    public void Push(BasePanel panel)
    {
        PanelManager?.Push(panel);
    }

    public void Pop( )
    {
        PanelManager?.Pop();
    }
}
