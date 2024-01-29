using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starts : MonoBehaviour
{
     public 
    void Start()
    {
        Invoke("StartScene", 0.5f);
        
    }

    void StartScene()
    {
        GameRoot.Instance.sceneSystem.SetScene(new StartScene());
        Debug.Log(GameRoot.Instance.interactionManager );
    }
}
