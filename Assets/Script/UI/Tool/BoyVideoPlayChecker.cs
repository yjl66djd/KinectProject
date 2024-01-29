using CustomFrame;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BoyVideoPlayChecker : MonoBehaviour
{ 
    [SerializeField]
    VideoPlayer player;
     
    [SerializeField]
    Button Continuebutton;

    [SerializeField]
    Button Back;
     
    private void Start()
    { 
        Continuebutton.gameObject.SetActive(false);
        Back.gameObject.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (player.isPlaying)
        { 
            if ((ulong)player.frame >= player.frameCount - 1 )
            {
                Continuebutton.gameObject.SetActive(true);
                Back.gameObject.SetActive(true);

            }
        } 
       
    } 
}
