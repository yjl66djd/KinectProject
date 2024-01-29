using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuccessPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1),3).OnComplete(() => {
            GameObject obj = GameObject.Find("TimeLineTwo");
            obj = obj.transform.Find("TimeLine2").gameObject;
            obj?.SetActive(true);
            GameRoot.Instance.Pop(null);
        });
    }

    
}
