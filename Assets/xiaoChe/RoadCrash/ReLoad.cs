using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReLoad : MonoBehaviour
{
    public Text text;
    private float Timerdd;
    // Start is called before the first frame update
    void Start()
    {
        Timerdd = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Timerdd -= Time.deltaTime;
        Timerdd = Mathf.Clamp(Timerdd, 0, 3);
        text.text = Timerdd.ToString("0")+ "秒后重新游戏";
        if (Timerdd == 0)
        {
            MyGestureListener.myGestureListener.RunSpeed = 0;
            SceneManager.LoadScene(0);
        }
    }
}
