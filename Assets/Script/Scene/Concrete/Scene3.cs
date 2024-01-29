using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene3 : SceneState
{
    readonly string sceneName = "Scene3";
    PanelManager panelManager;

    public override void OnEnter()
    {
        panelManager = new PanelManager();
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            GameRoot.Instance.LoadSceneAsy(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            
            GameRoot.Instance.SetAction(panelManager.Push, panelManager.Pop);
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
    private void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        panelManager.Push(new MainPanel());
        panelManager.Push(new Scene3FlipPanel());
        Debug.Log($"{sceneName}场景加载完毕");

        GameRoot.Instance.SetAction(panelManager.Push, panelManager.Pop);
    }

}
