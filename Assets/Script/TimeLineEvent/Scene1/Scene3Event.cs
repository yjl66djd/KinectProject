using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Event : MonoBehaviour
{
    public GameObject Puzzle;
    public GameObject Drag;

    public void TimeLine1Event()
   {
        GameRoot.Instance.Push(new BluePrintPanel());
   }
   public void TimeLine2Event()
   {
        
        Puzzle.SetActive(true);
   }
   public void TimeLine3Event()
   {
        Drag.SetActive(true);
    }
     public void TimeLine4Event()
   {
        GameRoot.Instance.sceneSystem.SetScene(new Scene4());
    } 

}
