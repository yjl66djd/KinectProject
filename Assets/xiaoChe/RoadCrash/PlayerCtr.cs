using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerCtr : MonoBehaviour
{
    public List<Vector3> LocalPosition;
    private int PosID;
    public VideoPlayer videoPlayer;
    public VideoPlayer videoPlayerSuccess;
    public float GameTime;
    public Text textDaoJiShi;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += ENDRestart;
        videoPlayerSuccess.loopPointReached += NextScene;   
        PosID = 0;
        GameTime = 45;
        this.transform.DOLocalMove(LocalPosition[PosID], 0.5f);
    }
    public GameObject NextSceneaaa;

    private void NextScene(VideoPlayer source)
    {
        //下一关
        //FindObjectOfType<MyGestureListener>().ges
        GameRoot.Instance.Pop(null);
        GameRoot.Instance.Scene4Animation2EnAble();
    }

    private void ENDRestart(VideoPlayer source)
    {
        ReStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftMove();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightMove();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (index == 3) return;
        
        GameTime -= Time.deltaTime;
        GameTime = Mathf.Clamp(GameTime,0,45);
        slider.value = GameTime / 45;
        if (GameTime==0)
        {
            print("游戏结束");
            NextSceneaaa.SetActive(true);
        }
        textDaoJiShi.text = "游戏倒计时：" + Mathf.CeilToInt(GameTime);
    }

    public void LeftMove() {
        PosID--;
        PosID = Mathf.Clamp(PosID, 0,4);
        this.transform.DOLocalMove(LocalPosition[PosID],0.5f);
    }
    private int index;
    public void RightMove()
    {
        PosID++;
        PosID = Mathf.Clamp(PosID, 0, 4);
        this.transform.DOLocalMove(LocalPosition[PosID], 0.5f);
    }
    public List<GameObject> gameObjectsshengmng;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="DiRen")
        {
            gameObjectsshengmng[index].SetActive(false);
            index++;
            if (index==3)
            {
                print("游戏结束");
                End.SetActive(true);
            }
            Destroy(collision.gameObject);
        }
    }
    public SpwonEneny spwonEneny;
    public GameObject End;
    public void ReStart() {
        for (int i = 0; i < gameObjectsshengmng.Count; i++)
        {
            gameObjectsshengmng[i].SetActive(true);
            index = 0;
        }
        spwonEneny.InitSpw();
        End.SetActive(false);
        GameTime = 45;
    }
}
