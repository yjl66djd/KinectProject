using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CanvasGroup StartPanel;
    public CanvasGroup GamePanel;
    private float IdleTime=60;
    public float GameTime = 60;
    public bool isStart;
    private float SkillTime;
    public Image SkillImage;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        isEnd = false;
        SkillTime = 0;
    }
    public bool isEnd;
    public void YouRen() {
        IdleTime = 60;
        StartPanel.GetComponent<Animator>().Play("Start");//Idel
    }


    public void Reload()
    {
        if (isEnd == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    //-----------------------------------
    public Transform CameraMark;
    public Transform CameraTarget;
    public GameObject DaoJiShi;
    public GameObject IdleXiongMao;
    //挥手游戏开始
    public void HuiShow() {
        StartPanel.DOFade(0, 0.2f).OnComplete(()=> {
            //相机开始运动
            CameraTarget.DOMove(CameraMark.position, 1.5f);
            CameraTarget.DORotate(CameraMark.rotation.eulerAngles, 1.5f).OnComplete(()=> {
                //隐藏原来的熊猫
                IdleXiongMao.SetActive(false);
                //开始显示倒计时 3秒
                DaoJiShi.GetComponent<CanvasGroup>().alpha = 0;
                DaoJiShi.SetActive(true);
                DaoJiShi.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            }); ;
        }); ;
    }

    public Text textTime;
    //
    public PlayerAI playerAI;
    public PandaAI pandaAI;

    public void StartGame() {
        print("打开游戏面板");
        GamePanel.alpha = 0;
        GamePanel.gameObject.SetActive(true);
        GamePanel.DOFade(1, 0.3f).OnComplete(()=> {
            print("开始比赛开始倒计时");
            isStart = true;//开始倒计时
            GameTime = 60;
            //俩人开始跑
            playerAI.StartGame();
            pandaAI.StartGame();
        }); ;
    }
    public GameObject EndPanel;
    public void FixedUpdate()
    {
        if (isStart == false)
        {
            if ((IdleTime-=Time.deltaTime)<0)
            {
                StartPanel.GetComponent<Animator>().Play("Idel");//Idel
            }
            SkillTime = 0;
        }
        else
        {
            //游戏开始
            GameTime -= Time.deltaTime;
            GameTime = Mathf.Clamp(GameTime, 0,60);
            textTime.text = GameTime.ToString("0");
            if (GameTime==0)
            {
                EndPanel.SetActive(true);
                isEnd = true;
            }
            if (MyGestureListener.myGestureListener.RunSpeed>0.7f && pandaAI.IsSkill == false) //只有急速奔跑的时候才能积攒能量条
            {
                SkillTime += Time.deltaTime;
                if (SkillTime>10)
                {
                    //
                    pandaAI.Skill();
                }
            }
            else
            {
                SkillTime = 0;
            }
            SkillTime = Mathf.Clamp(SkillTime,0,10);
            SkillImage.fillAmount = SkillTime / 10;
        }
    }
}
