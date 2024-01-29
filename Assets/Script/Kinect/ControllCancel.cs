using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCancel : MonoBehaviour
{
    public InteractionManager manager; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { manager.controlMouseCursor = false; }
         if (Input.GetKeyDown(KeyCode.E)) { manager.controlMouseCursor = true; }

    }
}
