using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiBai : MonoBehaviour
{
    public void ShiBaiEnd()
    {
        this.transform.parent.GetComponent<GestureLinster>().shibai();
        this.gameObject.SetActive(false);
    }
}
