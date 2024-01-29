using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChengGong : MonoBehaviour
{
    public void ChengGongEnd() {
        this.transform.parent.GetComponent<GestureLinster>().Chenggong();
        this.gameObject.SetActive(false);
         
    }
}
