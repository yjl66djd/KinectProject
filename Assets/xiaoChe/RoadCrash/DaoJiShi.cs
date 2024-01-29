using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaoJiShi : MonoBehaviour
{

    public void StartGame() {
       
        this.GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(()=> {
            print("开始游戏");
            GameManager.Instance.StartGame();
            this.gameObject.SetActive(false);
        }); ;
    }
}
