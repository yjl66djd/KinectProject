using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAi : MonoBehaviour
{
    public List<Transform> transformsPath;
    private NavMeshAgent navMeshAgentmy;
    private bool IsStart;
    public int Index;
    public bool isNavMesh;
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
        if (IsStart==true)
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
            if (isNavMesh==true)
            {
                this.GetComponent<Animator>().SetFloat("Speed", navMeshAgentmy.speed);
                if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                {
                    this.GetComponent<Animator>().speed =1+ navMeshAgentmy.speed * 0.05f;
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
    public void StartGame() {
        IsStart = true;
        Time.timeScale = 1;
        this.GetComponent<Animator>().SetBool("IsStart",true);
        this.transform.DORotate(new Vector3(0, 0, 0), 1.5f).OnComplete(()=> {
            navMeshAgentmy.SetDestination(transformsPath[Index].position);
        }); ;
    }
}
