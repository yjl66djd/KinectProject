using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public GameObject Cursur;
    // Start is called before the first frame update
    void Start()
    {
       
        this.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 3).OnComplete(() => {
            
            GameRoot.Instance.Pop(null);
            Cursur.name = "1";
            GameRoot.Instance.Push(new BluePrintPanel());
        }) ;
    }
 
}
