using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UI : MonoBehaviour
{
    float times = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameRoot.Instance.Push(new SettingPanel());
    }

    // Update is called once per frame
    void Update()
    {
        //times += Time.deltaTime;

        //if(times > 5)
        //{

        //}
    }
}
