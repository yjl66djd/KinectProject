using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GestureController : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    private static CubeGestureListener instance = null;
    private Text text;
    public static CubeGestureListener Instance
	{
		get
		{
			return instance;
		}
	}
    public void UserDetected(long userId, int userIndex)
	{
		// the gestures are allowed for the primary user only
		text.text += " 检测到用户了 ";
	}
    // Start is called before the first frame update

    public void UserLost(long userId, int userIndex)
	{
		// the gestures are allowed for the primary user only
		text.text += " 用户离开摄像头 ";
	}

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectInterop.JointType joint, Vector3 screenPos)
	{
		// the gestures are allowed for the primary user only
		
		
	}

    public bool GestureCompleted (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint, Vector3 screenPos)
	{
		// the gestures are allowed for the primary user only
        if(gesture == KinectGestures.Gestures.SwipeRight)
        {
            text.text += " run ";
            SceneManager.LoadScene("one");
        }
        
		return true ;
	}

	/// <summary>
	/// Invoked if a gesture is cancelled.
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	/// <param name="gesture">Gesture type</param>
	/// <param name="joint">Joint type</param>
	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint)
	{
		// the gestures are allowed for the primary user only
		return true;
	}

    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



	
	
    

}
