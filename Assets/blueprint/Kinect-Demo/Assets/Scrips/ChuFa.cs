using DG.Tweening;
using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChuFa : MonoBehaviour
{

    public Image ShowImage;
    public string Nextname;
    public GameObject End;
    public GameObject Success;

    public void Start()
    {
        Debug.LogError(this.name);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError(collision.name);
        if (collision.tag=="Player"&& collision.name == this.name)
        {
            ShowImage.DOColor(new Color(1, 0.821988f, 0,1),0.3f);
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<CanvasGroup>().DOFade(0,0.3f).OnComplete(()=> {
                this.gameObject.SetActive(false);
            });
            collision.name = Nextname;

            if (collision.tag == "Player" && collision.name == "7")
            {
                Success.SetActive(true);
            }

        }
        else 
        {
                End.SetActive(true);
                End.GetComponent<EndPanel>().Cursur = collision.gameObject;
        }
    }
}
