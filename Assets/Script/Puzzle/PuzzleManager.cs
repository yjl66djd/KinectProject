using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int Totle;
    public int Current = 0;
    public bool hasGrab = false;

    public GameObject TimeLine;
     
    void Update()
    {
        if(Current == Totle)
        {
            TimeLine.SetActive(true);
        }
    }
     
    public void Complite()
    {
        Current ++;
    }
}
