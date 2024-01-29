using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PandaAI : MonoBehaviour
{
    public List<Transform> transformsPath;
    private NavMeshAgent navMeshAgentmy;
    private bool IsStart;
    public int Index;
    public bool isNavMesh;
    public bool IsSkill;
    public GameObject 竹子;
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        IsSkill = false;
        navMeshAgentmy = this.GetComponent<NavMeshAgent>();
        IsStart = false;
        Index = 0;
        竹子.SetActive(false);
        canvasGroup.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            StartGame();

        }
        if (GameManager.Instance.isEnd == true)
        {
            navMeshAgentmy.isStopped = true;
            this.GetComponent<Animator>().Play("idle");
        }
         


        if (IsStart == true&&IsSkill==false)
        {
            if (Vector3.Distance(this.transform.position, transformsPath[Index].position) < 2)
            {

                Index++;
                if (Index > transformsPath.Count - 1)
                {
                    navMeshAgentmy.isStopped = true;
                }
                else
                {

                    navMeshAgentmy.SetDestination(transformsPath[Index].position);
                }
            }
            if (isNavMesh == true)
            {
                this.GetComponent<Animator>().SetFloat("Speed", navMeshAgentmy.speed);
                if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                {
                    this.GetComponent<Animator>().speed = 1 + navMeshAgentmy.speed * 0.05f;
                }
                else
                {
                    this.GetComponent<Animator>().speed = 1;
                }
            }
        }

    }
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        IsStart = true;
        Time.timeScale = 1;
        this.transform.DORotate(new Vector3(0, 0, 0), 0.5f).OnComplete(() => {
            navMeshAgentmy.SetDestination(transformsPath[Index].position);
            this.GetComponent<Animator>().SetBool("IsStart", true);
        }); ;
    }

    public void Skill()
    {
        //播放吃东西的动画
        IsSkill = true;
        navMeshAgentmy.enabled = false;
        this.GetComponent<Animator>().Play("eat");
        竹子.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(0, 0.3f).OnComplete(()=> {
            canvasGroup.DOFade(1, 0.2f);
        }); ;
        Invoke("SkillEnd",3);
    }
    private void SkillEnd() {
        IsSkill = false;
        navMeshAgentmy.enabled = true;
        navMeshAgentmy.isStopped = false;
        navMeshAgentmy.SetDestination(transformsPath[Index].position);
        竹子.SetActive(false);
        canvasGroup.DOFade(1, 0.3f).OnComplete(() =>
        {
            canvasGroup.gameObject.SetActive(false);
        });
        this.GetComponent<Animator>().Play("run");
    }
}
