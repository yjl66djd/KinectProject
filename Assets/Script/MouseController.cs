using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Texture2D crosshairTexture;//设置图标的图片
    public Texture2D crosshai2;//替换贴图
    Texture2D crosshair1;//暂存贴图

    void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        crosshair1 = crosshairTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            crosshairTexture = crosshai2;

        }
        else
        {
            crosshairTexture = crosshair1;
        }
    }

    void OnGUI()
    {
        Vector3 mousePos = Input.mousePosition;
        //这里面的设置根据需求来修改
        Rect pos = new Rect(mousePos.x - crosshairTexture.width * 0.5f, Screen.height - mousePos.y - crosshairTexture.height * 0.5f,
                            crosshairTexture.width, crosshairTexture.height);
        GUI.DrawTexture(pos, crosshairTexture);
    }




    public void GetExit()//退出运行
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//用于退出运行

        #else
        Application.Quit();
        #endif

    }
}
