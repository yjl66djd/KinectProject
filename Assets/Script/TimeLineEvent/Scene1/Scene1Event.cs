using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Event : MonoBehaviour
{
    public int FindCount = 0;
    public GameObject TimeLine3;
    private bool t3A;

    private void Update()
    {
        if (FindCount >= 3 && !t3A)
        {
            t3A = true;
            TimeLine3.SetActive(true);

        }
    }


    public void BookAwake()
    {
        GameRoot.Instance.Push(new TipsPanel());
        GameRoot.Instance.Push(new Scene1FlipPanel());
    }

    public void Scene1HAndS()
    {
        
    }

    public void Scene1Last()
    {
       
        GameRoot.Instance.Push(new ScrachPanel()); 
    }

    public void Scene2Awake()
    {
        GameRoot.Instance.sceneSystem.SetScene(new Scene2()); 
    }

}
