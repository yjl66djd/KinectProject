using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,20);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(speed,0,0)) ;
    }


}
