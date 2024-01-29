using SonicBloom.Koreo.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Event : MonoBehaviour
{
    public GameObject DragObj;


   public void TimeLine1Event()
   { 
        DragObj.SetActive(true); 
   }
   public void TimeLine2Event()
   {
        FindObjectOfType<SimpleMusicPlayer>().Play();
   }
   public void TimeLine3Event()
   {
        GameRoot.Instance.sceneSystem.SetScene(new Scene3());
    }

}
