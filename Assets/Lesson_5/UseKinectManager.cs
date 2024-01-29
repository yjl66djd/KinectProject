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
            //�豸׼���ã����Զ�ȡ��
            if(KinectImg.texture==null)
            {
                //Texture2D kinectPic = KinectManager.Instance.GetUsersClrTex2D();//���豸��ȡ��ɫ����
                Texture2D kinectUseMap = KinectManager.Instance.GetUsersLblTex2D(); 
                KinectImg.texture = kinectUseMap;//�Ѳ�ɫͼƬ���ؼ���ʾ
            }

            if(KinectManager.Instance.IsUserDetected())
            {
                //��⵽���
                long userId = KinectManager.Instance.GetPrimaryUserID();//��ȡ����ID
                Vector3 userPos=KinectManager.Instance.GetUserPosition(userId);//��ȡ������Ϣ
                //print("x="+userPos.x+"y="+userPos.y+"z="+userPos.z);

                int jointType= (int)KinectInterop.JointType.HandRight;
                if (KinectManager.Instance.IsJointTracked(userId, jointType))
                {
                    //�ؽڵ㱻׷�ٵ�
                 //   Vector3 leftHandKinectPos = KinectManager.Instance.GetJointKinectPosition(userId,jointType);//��ȡ���ֹؽڵ�λ��
                 //   Vector3 leftHandPos=KinectManager.Instance.GetJointPosition(userId,jointType);
                 //   print("kx="+leftHandKinectPos.x+"ky="+leftHandKinectPos.y+"kz="+leftHandKinectPos.z+"x=" + leftHandPos.x + "y=" + leftHandPos.y + "z=" + leftHandPos.z);
                    //KinectInterop.HandState rightHandState=KinectManager.Instance.GetRightHandState(userId);//���������Ϣ
                    //if(rightHandState==KinectInterop.HandState.Closed)
                    //{ 
                    //    print("������ȭ");
                    //}
                    //else if(rightHandState==KinectInterop.HandState.Open)
                    //{
                    //    print("����չ��");
                    //}
                    //else if(rightHandState==KinectInterop.HandState.Lasso)
                    //{
                    //    print("yes ����");
                    //}
                }

            }
            
           
        }
    }
}
