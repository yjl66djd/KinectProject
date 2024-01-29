using CustomFrame;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class S1endUIVideoPlayChecker1 : MonoBehaviour
{
    public VideoClip s2clip;
    public VideoClip s1Gclip, s1Bclip;

    [SerializeField]
    VideoPlayer player;

    [SerializeField]
    Button Tabbutton;
    [SerializeField]
    Button Continuebutton;

    bool s1pasue = false;
    bool s2pasue = false;
    bool fanye = false;
    private void Start()
    {
        Tabbutton.onClick.AddListener(ClickTab);
        Tabbutton.gameObject.SetActive(false);
        Continuebutton.gameObject.SetActive(false);
        if (GameRoot.Instance.Sexual == "Girl")
        {
            player.clip = s1Gclip;
        }
        else
        {
            player.clip = s1Bclip;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player.isPlaying)
        {
            Debug.Log(1);
            if ((ulong)player.frame >= player.frameCount - 1 && !s1pasue)
            {
                s1pasue = true;
                Debug.Log("isPaused");
                Tabbutton.gameObject.SetActive(true);
            }
        }

        if (s1pasue && fanye)
        {
            if (player.isPlaying)
            {
                if ((ulong)player.frame >= player.frameCount - 1 && !s2pasue)
                {
                    s2pasue = true;
                    Debug.Log("isPaused");
                    Continuebutton.gameObject.SetActive(true); 
                }
            }
            
        }
       
    }

    private void ClickTab()
    { 
        player.clip = s2clip;
        player.Play();
        fanye = true;
        Tabbutton.gameObject.SetActive(false);
    }
}
