using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuanChong : MonoBehaviour
{
    public void End() {
        this.transform.parent.GetComponent<GestureLinster>().PanDuan();
        this.gameObject.SetActive(false);
    }
}
