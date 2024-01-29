using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class OptionDataForSano
{
    public string Size;
    public string DistanceLeftRight;
    public string DistanceMin;
    public string DistanceMax;
    public string ConfigTime;
}
public class MyGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{

    [Tooltip("由该组件跟踪的播放器索引。0代表第一人，1人，第二人，2人，第三人，等等。")]
    public int playerIndex = 0;

    public static MyGestureListener myGestureListener;

    public PlayerCtr playerCtr;

    public float Size;
    public float DistanceLeftRight;
    public float DistanceMin;
    public float DistanceMax;
    [Tooltip("Text 用于显示监听到的手势动作的信息")]
    public Text gestureInfo;

    [Tooltip("用于外界获取是否检测到人")]
    public bool HaveUser;

    //跟踪进程消息是否已显示的内部变量
    private bool progressDisplayed;
    private float progressGestureTime;

    //是否检测到需要的手势
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    #region 周期函数  
    //初始化内容
    void Awake()
    {
        StartCoroutine(OptionDataForSano());
        HaveUser = false;
        if (myGestureListener != null)
        {
            Destroy(gameObject);
        }
        else
        {
            myGestureListener = this;
            DontDestroyOnLoad(this);
        }


        //DontDestroyOnLoad(this.gameObject);
    }
    float timer;
    void Update()
    {
        if (timer <= 3f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            if (playerCtr == null)
            {
                playerCtr = FindObjectOfType<PlayerCtr>();
                Skill = playerCtr.transform.GetChild(0).gameObject;
            }
        }
    }
    IEnumerator OptionDataForSano()
    {
        string sPath = Application.streamingAssetsPath + "/OptionDataForSano.json";
        WWW www = new WWW(sPath);
        if (www.text != null)
        {
            OptionDataForSano optionData = JsonUtility.FromJson<OptionDataForSano>(www.text);
            float.TryParse(optionData.Size, out Size);
            float.TryParse(optionData.DistanceLeftRight, out DistanceLeftRight);
            float.TryParse(optionData.DistanceMax, out DistanceMax);
            float.TryParse(optionData.DistanceMin, out DistanceMin);

        }
        //-------------------------------读取失败默认赋值----------------------------------
        else
        {
            Size = 1;
        }
        yield return www;
    }
    #endregion
    public GameObject Skill;
    /// <summary>
    /// 当检测到新用户时调用。在这里，可以通过调用KinectManager.DetectGesture()来开始手势跟踪。
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    public void UserDetected(long userId, int userIndex)
    {
        if (userIndex==1)
        {
            print("开始释放技能");
            Skill.SetActive(true);
        }
        // 只允许主要用户使用手势
        KinectManager manager = KinectManager.Instance;
        if (!manager || (userIndex != playerIndex))
            return;
        HaveUser = true;

        print("UserDetected");
        //左右手切换内容
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);//极少触发
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);//极少触发
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);//极少触发
        manager.DetectGesture(userId, KinectGestures.Gestures.Tpose);//极少触发
        if (gestureInfo != null)
        {
            //gestureInfo.text = "Swipe left, right or up to change the slides.";
            gestureInfo.text = "向左、向右或向上滑动，以更改信息。";
        }
    }
    /// <summary>
    /// 当用户丢失时调用。此用户的所有跟踪手势都会自动清除
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    public void UserLost(long userId, int userIndex)
    {
        // 只允许主要用户使用手势
        if (userIndex != playerIndex)
            return;
        HaveUser = false;
    }
    /// <summary>
    /// 当手势正在进行时调用
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    /// <param name="gesture">Gesture type</param>
    /// <param name="progress">Gesture progress [0..1]</param>
    /// <param name="joint">Joint type</param>
    /// <param name="screenPos">Normalized viewport position</param>
    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {

        // 只允许主要用户使用手势
        if (userIndex != playerIndex)
            return;
        KinectInterop.HandState leftHandState = KinectManager.Instance.GetLeftHandState(userId);
        if (gesture == KinectGestures.Gestures.SwipeLeft && progress > 0.5f)
        {
            print("向左");

        }
        if (gesture == KinectGestures.Gestures.SwipeRight && progress > 0.5f)
        {
            print("向右");

        }
        else
        {
            print("没有匹配可识别动作++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            RunSpeed = 0;
        }
    }
    
    public float RunSpeed;
    public GameObject End;
    /// <summary>
    /// 如果手势完成，则调用。
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    /// <param name="gesture">Gesture type</param>
    /// <param name="joint">Joint type</param>
    /// <param name="screenPos">Normalized viewport position    标准视口位置</param>
    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {

        // 只允许主要用户使用手势
        if (userIndex != playerIndex)
            return false;
        if (gestureInfo != null)
        {
            string sGestureText = gesture + " detected";
            gestureInfo.text = sGestureText;
        }
        if (gesture == KinectGestures.Gestures.SwipeLeft)
        {
            playerCtr?.LeftMove();
            print("向左滑动");//打开内容选择界面
        }
        else if (gesture == KinectGestures.Gestures.SwipeRight)
        {
            playerCtr?.RightMove();
            print("向右滑动");//打开内容选择界面
        }
        else if (gesture == KinectGestures.Gestures.RaiseRightHand)
        {
            print("举起右手,打开互动界面");//打开内容选择界面

        }
        else if (gesture == KinectGestures.Gestures.Tpose)
        {
            print("摆出Tpose");
            //重新开始哟咻
            playerCtr?.ReStart();


        }
        else
        {

        }
        return true;
    }
    /// <summary>
    /// 如果手势被取消，则调用
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    /// <param name="gesture">Gesture type</param>
    /// <param name="joint">Joint type</param>
    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint)
    {
        // 只允许主要用户使用手势
        if (userIndex != playerIndex)
            return false;

        if (progressDisplayed)
        {
            progressDisplayed = false;

            if (gestureInfo != null)
            {
                gestureInfo.text = String.Empty;
            }
        }

        if (gesture == KinectGestures.Gestures.Run)
        {

            RunSpeed = 0;
        }

        return true;
    }

}