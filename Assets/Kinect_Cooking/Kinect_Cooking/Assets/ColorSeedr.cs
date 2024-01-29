using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSeedr : MonoBehaviour
{
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = this.GetComponent<Image>().color;
        this.GetComponent<Image>().color = Color.white;
    }
    public void SelectColor(string code) {
        if (code==this.name)
        {
            this.GetComponent<Image>().DOColor(color,0.5f);
        }
    }
}
