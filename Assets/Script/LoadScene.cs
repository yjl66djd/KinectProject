using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void test(){
        SceneManager.LoadScene("one"); 
    }

    public void playAnimation(){
        GameObject.Find("Circle half rotating fading 4");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
    
}
