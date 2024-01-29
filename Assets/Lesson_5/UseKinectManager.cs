using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseKinectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage KinectImg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isInit = KinectManager.Instance.IsInitialized();
        if(isInit)
        {
            //设备准备好，可以读取了
            if(KinectImg.texture==null)
            {
                //Texture2D kinectPic = KinectManager.Instance.GetUsersClrTex2D();//从设备获取彩色数据
                Texture2D kinectUseMap = KinectManager.Instance.GetUsersLblTex2D(); 
                KinectImg.texture = kinectUseMap;//把彩色图片给控件显示
            }

            if(KinectManager.Instance.IsUserDetected())
            {
                //检测到玩家
                long userId = KinectManager.Instance.GetPrimaryUserID();//获取人物ID
                Vector3 userPos=KinectManager.Instance.GetUserPosition(userId);//获取人物信息
                //print("x="+userPos.x+"y="+userPos.y+"z="+userPos.z);

                int jointType= (int)KinectInterop.JointType.HandRight;
                if (KinectManager.Instance.IsJointTracked(userId, jointType))
                {
                    //关节点被追踪到
                 //   Vector3 leftHandKinectPos = KinectManager.Instance.GetJointKinectPosition(userId,jointType);//获取左手关节点位置
                 //   Vector3 leftHandPos=KinectManager.Instance.GetJointPosition(userId,jointType);
                 //   print("kx="+leftHandKinectPos.x+"ky="+leftHandKinectPos.y+"kz="+leftHandKinectPos.z+"x=" + leftHandPos.x + "y=" + leftHandPos.y + "z=" + leftHandPos.z);
                    //KinectInterop.HandState rightHandState=KinectManager.Instance.GetRightHandState(userId);//检测手势信息
                    //if(rightHandState==KinectInterop.HandState.Closed)
                    //{ 
                    //    print("右手握拳");
                    //}
                    //else if(rightHandState==KinectInterop.HandState.Open)
                    //{
                    //    print("右手展开");
                    //}
                    //else if(rightHandState==KinectInterop.HandState.Lasso)
                    //{
                    //    print("yes 手势");
                    //}
                }

            }
            
           
        }
    }
}
