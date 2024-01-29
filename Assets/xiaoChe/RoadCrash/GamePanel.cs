using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    //游戏初始化
    public GameObject ShuoMing;
    public Button StartGame;
    public GameObject StartPanel;
    //显示说明
    // Start is called before the first frame update


    public void Awake()
    {
        StartGame.onClick.AddListener(()=> {
            if (StartPanel.activeInHierarchy==true)
            return;
            print("游戏开始");
            ShuoMing.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
            {
                ShuoMing.SetActive(false);
               
            });
            StartPanel.SetActive(true);
            StartPanel.GetComponent<CanvasGroup>().DOFade(1, 0.2f).OnComplete(() =>
            {
                print("开始倒计时");
            });
        });
    }
    void OnEnable()
    {
        ShuoMing.SetActive(true);
        ShuoMing.GetComponent<CanvasGroup>().alpha = 1;
        StartPanel.SetActive(false);
        StartPanel.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
