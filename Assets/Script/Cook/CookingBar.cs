using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingBar : MonoBehaviour
{
    public Image image;
    public GameObject finshText;
    private bool finish = false;
    
    void Update()
    { 
        if(image.fillAmount >= 1 && !finish)
        {
            Debug.Log(11);
            finish = true;
            finshText.SetActive(true);
        }
        else if(!finish)
        {
            image.fillAmount -= Time.deltaTime * 0.1f;
        }
    }
     
    public void AddProgress(float amount)
    {
        if (image != null)
        {
            image.fillAmount += amount;
        }
    }
}
