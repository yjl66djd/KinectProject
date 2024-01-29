using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4Event : MonoBehaviour
{ 
    public void TimeLine1Event()
   {
        GameRoot.Instance.Push(new CarCatch());
   }
   public void TimeLine2Event()
   {
        GameRoot.Instance.Push(new BlandPanel()); 
   }
   public void TimeLine3Event()
   {
        
    }
     public void TimeLine4Event()
   {
        GameRoot.Instance.sceneSystem.SetScene(new Scene4());
    } 

}
