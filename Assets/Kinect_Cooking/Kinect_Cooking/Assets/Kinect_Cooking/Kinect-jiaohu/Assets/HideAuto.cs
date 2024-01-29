using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAuto : MonoBehaviour
{
    public string state;
    public int Index;
    public void Awake()
    {
        Index = 0;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
       
    }

    public void Hide() {
        Invoke("Didedad", 2);
    }

    private void Didedad() {
        this.transform.gameObject.SetActive(false);
       
        
    }
    public GameObject adadawdawda; 
    public GameObject End;
    public void OnDisable()
    {
        Index++;
        adadawdawda.BroadcastMessage("SelectColor", Index.ToString());
        if (Index==8)
        {
            //End.SetActive(true);
            GameRoot.Instance.Pop(null);
            GameRoot.Instance.Scene4Animation3EnAble();
        }
    }
}
