using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    public List<Transform> transformsPath;
    private NavMeshAgent navMeshAgentmy;
    private bool IsStart;
    public int Index;
    public bool isNavMesh;
    public GameObject FollowCamra;
  
    // Start is called before the first frame update
    void Start()
    {

        navMeshAgentmy = this.GetComponent<NavMeshAgent>();
        IsStart = false;
        Index = 0;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            StartGame();

        }
        if (navMeshAgentmy.speed>2)
        {
            this.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            this.GetComponent<AudioSource>().enabled = false;

        }
        if (GameManager.Instance.isEnd == true)
        {
            navMeshAgentmy.enabled = false;
            this.GetComponent<Animator>().Play("idle");
        }


        if (IsStart == true)
        {
            KinectManager manager = KinectManager.Instance;
            if (!manager) {
                return;
            }
            else
            {
                navMeshAgentmy.speed = MyGestureListener.myGestureListener.RunSpeed * 8f;
            }
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
        FollowCamra.SetActive(true);
        this.transform.DORotate(new Vector3(0, 0, 0), 0.5f).OnComplete(() => {
            navMeshAgentmy.SetDestination(transformsPath[Index].position);
            this.GetComponent<Animator>().SetBool("IsStart", true);
        }); ;
    }
}
