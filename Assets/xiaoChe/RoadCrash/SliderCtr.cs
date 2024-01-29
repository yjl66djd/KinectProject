using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SliderCtr : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void Update()
    {
        this.GetComponent<Slider>().value = MyGestureListener.myGestureListener.RunSpeed;
    }
}
