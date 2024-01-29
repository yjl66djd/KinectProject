using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlMouse : MonoBehaviour
{
    public Canvas canvas;
    public Image rightHand;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(KinectManager.Instance.IsUserDetected())
        {
            //检测到玩家
            long userId = KinectManager.Instance.GetPrimaryUserID(); //获取用户id
            int jointType = (int)KinectInterop.JointType.HandRight;//表示右手

        
            if(KinectManager.Instance.IsJointTracked(userId,jointType))
            {
                //关节点被追踪到
                Vector3 righthandPos = KinectManager.Instance.GetJointKinectPosition(userId,jointType);
                Vector3 rightHandScreenPos = Camera.main.WorldToScreenPoint(righthandPos);//将三维坐标转换到荧幕坐标
                Vector2 rightHandSenPos = new Vector2(rightHandScreenPos.x,rightHandScreenPos.y);
                Vector2 rightHandUguiPos ;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform,rightHandSenPos,null,out rightHandUguiPos));
                {
                    //表示右手在canvas表示的矩形范围内
                    RectTransform rightRectTf = rightHand.transform as RectTransform;
                    rightRectTf.anchoredPosition = rightHandUguiPos;
                   
                }
                KinectInterop.HandState rightHandState = KinectManager.Instance.GetRightHandState(userId);

            }

            

        }
            
    }
}
