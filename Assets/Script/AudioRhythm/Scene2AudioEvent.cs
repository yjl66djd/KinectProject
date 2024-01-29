using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using UnityEngine;


public class Scene2AudioEvent : MonoBehaviour
{ 
    [EventID]
    public string EventName;

    [HideInInspector]
    public int CurrentCount = 0;

    private void Awake()
    {
        CurrentCount = 0;
        Koreographer.Instance.RegisterForEvents(EventName, ViberatEvent);
    }

    //节奏调用事件，注册后每个节奏点触发
    void ViberatEvent(KoreographyEvent evt)
    {
#if UNITY_EDITOR

        CurrentCount++;
        if(CurrentCount == 1)
        {
            VibeConnection();
            GameRoot.Instance.Push(new Gesture1Panel());
            Debug.Log(1);
        }
        else if(CurrentCount == 2)
        {
            VibeConnection();
            GameRoot.Instance.Push(new Gesture2Panel());
            Debug.Log(2);
        }
        else if(CurrentCount == 3)
        {
            VibeConnection();
            GameRoot.Instance.Push(new Gesture3Panel());
            Debug.Log(3);
        }
        else if(CurrentCount == 4)
        {
            VibeConnection();
            GameRoot.Instance.Push(new Gesture4Panel());
            Debug.Log(4);
        } 

       

#elif UNITY_ANDROID
             
            
#endif
          
    } 
    void VibeConnection()
    {
        string _value = Random.value.ToString();
        FMNetworkManager.instance.SendToOthers(_value);
    }

}
