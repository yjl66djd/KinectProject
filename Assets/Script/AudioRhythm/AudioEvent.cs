using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using UnityEngine;

public class AudioEvent : MonoBehaviour
{
       
    [SerializeField]
    ReflectTimer timer;
     
    [EventID]
    public string EventName;

    private void Awake()
    {
        Koreographer.Instance.RegisterForEvents(EventName, ViberatEvent);
    }

    //节奏调用事件，注册后每个节奏点触发
    void ViberatEvent(KoreographyEvent evt)
    { 
#if UNITY_EDITOR
            timer.Starting();//开始计时并识别
            Debug.Log(2); 

#elif UNITY_ANDROID
            timer.Starting();
            Taptic.Heavy(); //震动指令
#endif
        
    } 
    public void buttonEvent()
    { 
         timer.pass();
    }
     
}
