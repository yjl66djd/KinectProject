using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 开始场景管理
/// </summary>
public class StartScene : SceneState
{

    readonly string sceneName = "Start";
    PanelManager panelManager;

    public override void OnEnter()
    {
        panelManager = new PanelManager();
        if(SceneManager.GetActiveScene().name != sceneName)
        {
            GameRoot.Instance.LoadSceneAsy(sceneName); 
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            panelManager.Push(new StartPanel()); 
            GameRoot.Instance.SetAction(panelManager.Push , panelManager.Pop);
        }
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        panelManager.PopAll();
    }

    /// <summary>
    /// 场景加载完毕后的方法
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="load"></param>
    private void SceneLoaded (Scene scene , LoadSceneMode load)
    {
        panelManager.Push(new RenamePanel());
        GameRoot.Instance.SetAction(panelManager.Push , panelManager.Pop);
        Debug.Log($"{sceneName}场景加载完毕");
    }

}
