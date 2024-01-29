using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwonEneny : MonoBehaviour
{
    public List<GameObject> gameObjectsCar;
    public List<Vector3> InitPos;
    private float Timer;
    public bool IsSpown;
    public float initSpeed;
    // Start is called before the first frame update
    void Start()
    {
        IsSpown = true;
        InvokeRepeating("SpeedAdd",0,5);
    }
    public void InitSpw() {
        initSpeed = 0.02f;
    }
    public void SpeedAdd() {
        initSpeed+=0.01f;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsSpown == false) return;
        if ((Timer-=Time.deltaTime)<0)
        {
            int a = Random.Range(0, gameObjectsCar.Count);
            GameObject gameObject = GameObject.Instantiate(gameObjectsCar[a],this.transform);
            gameObject.GetComponent<AutoMove>().speed = initSpeed;
            gameObject.transform.localPosition = InitPos[a];
            Timer = Random.Range(2,5);
        }
         
    }
}
