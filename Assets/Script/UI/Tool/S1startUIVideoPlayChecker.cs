using CustomFrame;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class S1startUIVideoPlayChecker : MonoBehaviour
{
    [SerializeField]
    VideoPlayer player;
    [SerializeField]
    Button button;

    bool pasue;
      

    // Update is called once per frame
    void Update()
    {
        if (player.isPlaying)
        {
            Debug.Log(1);
            if ((ulong)player.frame >= player.frameCount - 1 && !pasue)
            {
                pasue = true;
                Debug.Log("isPaused");
                button.gameObject.SetActive(true);
            }
        }
       
    }
}
