using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class ReflectTimer : MonoBehaviour
{
    [SerializeField]
    SimpleMusicPlayer player;

    [SerializeField]
    Koreography koreography;
    public ParticleSystem particle;
    public float waitTime; //计时器
    public Text waitText;//显示计时
    private bool StartTimer = false; 

    private float times;

    private void Start()
    {
        waitText.enabled = false; 
    }

    private void Update()
    {
        if (StartTimer)
        {
            times += Time.deltaTime;
            waitText.text = (waitTime-times).ToString();

            if (times > waitTime)
            {
                Debug.Log("restart");
                Restart();
            }
        }
    }


    /// <summary>
    /// 开始计时识别
    /// </summary>
    public void Starting()
    {
        waitText.enabled = true;
        waitText.text = waitTime.ToString();
        StartTimer = true;
        player.Pause();
    }

    /// <summary>
    /// 识别成功调用
    /// </summary>
    public void pass() 
    {
        if (StartTimer)
        { 
            particle.Play();
            waitText.text = "你真棒！";
            StartTimer = false;
            times = 0;
            Invoke("passDelay", 2f);
        }
    }
     
    private void passDelay()
    {
        waitText.enabled = false;
        player.Play();
    }
    /// <summary>
    /// 失败重新开始
    /// </summary>
    private void Restart()
    { 
        waitText.enabled = false;
        StartTimer = false;
        times = 0; 
        player.LoadSong(koreography);
        player.Play();
    }
}
