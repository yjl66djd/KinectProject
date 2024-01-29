using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Lesson_8 : MonoBehaviour
{
    public Canvas canvas;
    public Image rightHand;
    public Animator animator;

    public Image btn1;
   
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isEnter", false);
        animator.SetBool("isLeave", false);
        animator.SetBool("isStart",false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //print("dw=" + KinectManager.Instance.GetDepthImageWidth() + "dh=" + KinectManager.Instance.GetDepthImageHeight());
        if (KinectManager.Instance.IsUserDetected())
        {
            //检测到玩家
            long userId = KinectManager.Instance.GetPrimaryUserID();//获取用户id

            int jointType = (int)KinectInterop.JointType.HandRight;//表示右手
            
            if (KinectManager.Instance.IsJointTracked(userId, jointType))
            {
                //关节点被追踪到
                Vector3 RightHandKinectPos = KinectManager.Instance.GetJointKinectPosition(userId,jointType);//1
                Vector3 rightHandscreenPos = Camera.main.WorldToScreenPoint(RightHandKinectPos);//右手转换到屏幕坐标
                Vector2 rightHandSenPos=new Vector2(rightHandscreenPos.x,rightHandscreenPos.y);
                Vector2 rightHandUguiPos;
                if(RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform,rightHandSenPos,null,out rightHandUguiPos))
                {
                    //表示右手在canvas所表示的矩形范围内
                    RectTransform rightRectTf=rightHand.transform as RectTransform;
                    rightRectTf.anchoredPosition = rightHandUguiPos;

                }
                if (RectTransformUtility.RectangleContainsScreenPoint(btn1.rectTransform, rightHandSenPos, null))//��⵽����rect�����ʱ��Ż᷵��true
                {
                    print("手悬停");
                    KinectInterop.HandState rightHandState = KinectManager.Instance.GetRightHandState(userId);//获取左右手的姿势
                    if (rightHandState == KinectInterop.HandState.Closed)
                    {
                        print("右手握拳");
                        animator.SetBool("isEnter", true);
                        animator.SetBool("isLeave", false);
                    }
                   
                }
                else
                {
                    print("手离开");
                    animator.SetBool("isEnter", false);
                    animator.SetBool("isLeave", true);
                }

                    
                
            }

        }
    }
}
