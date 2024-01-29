using CustomFrame;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 管理全局
/// </summary>
public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance;
    /// <summary>
    /// 场景管理器
    /// </summary>
    public SceneSystem sceneSystem { get; private set; }

    /// <summary>
    /// 显示一个面板,用法GameRoot.Instance.Push();
    /// </summary>
    public UnityAction<BasePanel>Push { get; private set; }

    public UnityAction<Action> Pop { get; private set; }

    public LoadSceneAsync LoadingPanel;

    public String Sexual;
    public bool ChangeClick;
    public InteractionManager interactionManager;
    AsyncOperation operation;

    private void Awake()
    {
        if (Instance != null && Instance != this) // 防止Editor下的Instance已经存在，并且是自身
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; 
        DontDestroyOnLoad(gameObject);
        Init(); 
    }

    private void Init()
    {
        MonoSystem.Init();
    }

    private void Start()
    {
        sceneSystem = new SceneSystem();
        
    }

    /// <summary>
    /// 设置push
    /// </summary>
    /// <param name="push"></param>
    public void SetAction(UnityAction<BasePanel> push , UnityAction<Action> pop)
    {
        Push = push;
        Pop = pop;
    }
    public void LoadSceneAsy(string SceneName)
    {
        Push(new SceneFaderPanel());

        operation = SceneManager.LoadSceneAsync(SceneName);
        operation.allowSceneActivation = false; 

        //StartCoroutine(Loading(SceneName)); 
    }

    public void AllowChangeScene()
    {
        operation.allowSceneActivation = true;
    }

    private IEnumerator Loading(string SceneName )
    {
        Push(new LoadingPanel()); 
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (LoadingPanel == null)
            {
                LoadingPanel = FindObjectOfType<LoadSceneAsync>();
            }
            else
            {
                Slider slider = LoadingPanel.slider;
                slider.value = operation.progress;
                Text text = LoadingPanel.text;
                text.text = operation.progress * 100 + "%";
                if (operation.progress >= 0.9f)
                {
                    slider.value = 1;
                    text.text = "加载完毕";
                    if (Input.anyKeyDown || ChangeClick)
                    {
                        operation.allowSceneActivation = true;
                        Pop(null);
                    }
                }
            }
            
            yield return null;
        }

    }


    #region 动画事件

    public void Scene1Animation1DisAble()
    {
        GameObject obj = GameObject.Find("TimeLine1");
        GameObject obj2 = GameObject.Find("TimeLinetwo"); 
        obj2 = obj2.transform.Find("TimeLine2").gameObject;
        obj?.SetActive(false);
        obj2?.SetActive(true);
    }

    public void Scene1Animation3DisAble()
    { 
        GameObject obj = GameObject.Find("TimeLineFour");
        obj = obj.transform.Find("TimeLine4").gameObject; 
        obj?.SetActive(true);
    }


    public void Scene2Animation1EnAble()
    {
        GameObject obj = GameObject.Find("TimeLineOne");
        obj = obj.transform.Find("TimeLine1").gameObject;
        obj?.SetActive(true);
    }
     public void Scene2AnimationLast2EnAble()
    {
        Pop(null);
        GameObject obj = GameObject.Find("TimeLineThree");
        obj = obj.transform.Find("TimeLine3").gameObject;
        obj?.SetActive(true);
    }

    public void Scene3Animation1EnAble()
    {
        GameObject obj = GameObject.Find("TimeLineOne");
        obj = obj.transform.Find("TimeLine1").gameObject;
        obj?.SetActive(true);
    }

    public void Scene4Animation2EnAble()
    {
        GameObject obj = GameObject.Find("TimeLineTwo");
        obj = obj.transform.Find("TimeLine2").gameObject;
        obj?.SetActive(true);
    }
    public void Scene4Animation3EnAble()
    {
        GameObject obj = GameObject.Find("TimeLineTree");
        obj = obj.transform.Find("TimeLine3").gameObject;
        obj?.SetActive(true);
    }


    #endregion

}
