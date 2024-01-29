using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景状态管理
/// </summary>
public class SceneSystem 
{
    /// <summary>
    /// 场景状态类
    /// </summary>
    SceneState sceneState;


    /// <summary>
    /// 设置当前场景进入当前场景
    /// </summary>
    /// <param name="state"></param>
    public void SetScene(SceneState state)
    {
        if(sceneState != null)
        {
            sceneState.OnExit();
        } 
        sceneState = state;
        if(sceneState != null)
        {
            sceneState.OnEnter();
        }
    }

}
