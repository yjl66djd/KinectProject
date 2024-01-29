using SonicBloom.Koreo.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CubeGestureListener;

public class GestureLinster : MonoBehaviour
{

    public GameObject ShiBai;
    public GameObject ChengGong;
    public GameObject HuanCong;
    public CubeGestureListener gestureListener;
    public bool Final = false;

    private void Start()
    {
        if (gestureListener == null)
        {
            gestureListener = FindObjectOfType<CubeGestureListener>();
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        FindObjectOfType<SimpleMusicPlayer>().Pause();
        ShiBai.SetActive(false);
        ChengGong.SetActive(false);
        HuanCong.SetActive(true);
    }

    public GestureType gestureType;
    public void PanDuan() {
        if (gestureListener.gestureType== gestureType)
        {
            ChengGong.SetActive(true);
        }
        else
        {
            ShiBai.SetActive(true);
        }
    }
    public GameObject NextObj;
    public void Chenggong()
    { 
        GameRoot.Instance.Pop(null);
        FindObjectOfType<SimpleMusicPlayer>().Play();
        VibeConnection();
        if (Final)
        {
            FindObjectOfType<SimpleMusicPlayer>().Pause();
            Debug.Log("6666666666666666666");
            GameRoot.Instance.Scene2AnimationLast2EnAble();
        }

        //switch (gestureType) 
        //{
        //    case GestureType.Gesture1:
        //        this.gameObject.SetActive(false);
        //        NextObj?.SetActive(true);
        //        if (Final)
        //        {
        //            Debug.Log(6);
        //            GameRoot.Instance.Scene2AnimationLast2EnAble();
        //        }
        //        break;
        //    case GestureType.Gesture2:
        //        this.gameObject.SetActive(false);
        //        NextObj?.SetActive(true);
        //        if (Final)
        //        {
        //            Debug.Log(6);
        //            GameRoot.Instance.Scene2AnimationLast2EnAble();
        //        }
        //        //GameRoot.Instance.Pop(null);
        //        //GameRoot.Instance.Push(new Gesture3Panel());
        //        break;
        //    case GestureType.Gesture3:
        //        this.gameObject.SetActive(false);
        //        if(NextObj!= null)
        //        {
        //            NextObj?.SetActive(true);
        //        }

        //        if (Final)
        //        {
        //            Debug.Log(6);
        //            GameRoot.Instance.Scene2AnimationLast2EnAble();
        //        }

        //        break; 
        //    case GestureType.GestureLose:
        //        this.gameObject.SetActive(false); 
        //        if (Final)
        //        {
        //            GameRoot.Instance.Scene2AnimationLast2EnAble();
        //        } 
        //        break; 
        //}

    }
    public void shibai()
    {
        VibeConnection();
        ShiBai.SetActive(false);
        ChengGong.SetActive(false);
        HuanCong.SetActive(true);
    }
    void VibeConnection()
    {
        string _value = Random.value.ToString();
        FMNetworkManager.instance.SendToOthers(_value);
    }
}
