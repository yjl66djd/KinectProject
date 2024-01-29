using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject LoseOBJ;
    public GameObject VectoryOBJ;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            VectoryOBJ.SetActive(true);
            GameManager.Instance.isEnd = true;
          
        }
        else if (other.tag == "Panda")
        {
            LoseOBJ.SetActive(true);
            GameManager.Instance.isEnd = true;
           
        }
    }
}
