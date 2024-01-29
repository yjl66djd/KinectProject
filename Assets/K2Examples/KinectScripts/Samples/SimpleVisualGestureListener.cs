using UnityEngine;
using System.Collections;

public class SimpleVisualGestureListener : MonoBehaviour, VisualGestureListenerInterface
{
	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("UI-Text to display the discrete gesture information.")]
	public UnityEngine.UI.Text discreteInfo;

	[Tooltip("UI-Text to display the continuous gesture information.")]
	public UnityEngine.UI.Text continuousInfo;


	private bool discreteGestureDisplayed;
	private bool continuousGestureDisplayed;

	private float discreteGestureTime;
	private float continuousGestureTime;


    //public void GestureInProgress(long userId, int userIndex, string gesture, float progress)
    //{
    //	if (userIndex != playerIndex)
    //		return;

    //	if(continuousInfo != null)
    //	{
    //		string sGestureText = string.Format ("{0} {1:F0}%", gesture, progress * 100f);
    //		continuousInfo.text = sGestureText;

    //		continuousGestureDisplayed = true;
    //		continuousGestureTime = Time.realtimeSinceStartup;
    //	}
    //}

    //public bool GestureCompleted(long userId, int userIndex, string gesture, float confidence)
    //{
    //	if (userIndex != playerIndex)
    //		return false;

    //	if(discreteInfo != null)
    //	{
    //		string sGestureText = string.Format ("{0}-gesture detected, confidence: {1:F0}%", gesture, confidence * 100f);
    //		discreteInfo.text = sGestureText;

    //		discreteGestureDisplayed = true;
    //		discreteGestureTime = Time.realtimeSinceStartup;
    //	}

    //	// reset the gesture
    //	return true;
    //}
    public void GestureInProgress(long userId, int userIndex, string gesture, float progress)
    {
        if (userIndex != playerIndex)
            return;

        if (auto == null)
        {
            return ;
        }
        if (auto.gameObject.activeInHierarchy == false)
            return;
        string sGestureText = string.Format("{0} {1:F0}%", gesture, progress * 100f);
        //continuousInfo.text = sGestureText;
        print(sGestureText);
        continuousGestureDisplayed = true;
        continuousGestureTime = Time.realtimeSinceStartup;
        if (auto.gameObject.activeInHierarchy == true)
        {
            print(gesture + "-----------------" + auto.state);
            if (gesture == auto.state)
            {
                Debug.LogError("动作匹配成功，隐藏");
                auto.gameObject.SetActive(false);
            }
            else
            {
                return;
            }

        }
    }
    public bool GestureCompleted(long userId, int userIndex, string gesture, float confidence)
    {
        if (userIndex != playerIndex)
            return false;
        if (auto == null)
        {
                return false;
        }
           
        if (auto.gameObject.activeInHierarchy == false)
            return false;
        string sGestureText = string.Format("{0}-gesture detected, confidence: {1:F0}%", gesture, confidence * 100f);
        print(sGestureText);
        //discreteInfo.text = sGestureText;
        discreteGestureDisplayed = true;
        discreteGestureTime = Time.realtimeSinceStartup;
        if (confidence * 100f < 70)
            return false;
        if (auto.gameObject.activeInHierarchy == true)
        {
            print(gesture + "-----------------" + auto.state);
            if (gesture == auto.state)
            {
                Debug.LogError("动作匹配成功，隐藏");
                auto.Hide();
                return true;
            }
            else
            {
                return false;
            }

        }
        // reset the gesture
        return true;
    }
    public HideAuto auto;

    float timer = 0; 
    public void Update()
	{
        if(timer <= 3f)
        {
            timer += Time.deltaTime; 
        }
        else
        {
            timer = 0;
            if (auto == null)
            {
                auto = FindObjectOfType<HideAuto>();
            }
        }
       
		// clear gesture infos after a while
		if(continuousGestureDisplayed && ((Time.realtimeSinceStartup - continuousGestureTime) > 2f))
		{
			continuousGestureDisplayed = false;

			if(continuousInfo != null)
			{
				continuousInfo.text = string.Empty;
			}
		}

		if(discreteGestureDisplayed && ((Time.realtimeSinceStartup - discreteGestureTime) > 2f))
		{
			discreteGestureDisplayed = false;
			
			if(discreteInfo != null)
			{
				discreteInfo.text = string.Empty;
			}
		}
	}

}
