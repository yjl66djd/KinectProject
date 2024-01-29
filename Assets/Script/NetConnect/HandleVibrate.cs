using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleVibrate : MonoBehaviour
{
     
    public void VibratButton()
    {
        string _value = Random.value.ToString();
        FMNetworkManager.instance.SendToOthers(_value);
    }

    public void HandleVibAction()
    {
        Taptic.Heavy();
    }

    //AndroidJavaClass VibratorTool = null;
    //Start is called before the first frame update
    //void Start()
    //{
    //    VibratorTool = new AndroidJavaClass("com.tools.common.VibratorTool");
    //    smsDialog.CallStatic<AndroidJavaObject>("getInstance").Call("init", getContext());
    //}

    //public void CVibrate(int milliseconds)
    //{
    //    Debug.LogError("---- 调用震动");
    //    VibratorTool.CallStatic("CVibrate", milliseconds);
    //}

    //public void CVibrateShort()
    //{
    //    Debug.LogError("---- 调用震动 -- 短");
    //    VibratorTool.CallStatic("CVibrateShort");
    //}

    //public void CVibrateLong()
    //{
    //    Debug.LogError("---- 调用震动 -- 长");
    //    VibratorTool.CallStatic("CVibrateLong");
    //}

    //public void CCannelVibrate()
    //{
    //    Debug.LogError("---- 调用震动 -- 取消");
    //    VibratorTool.CallStatic("CCancelVibrate");
    //}
}
