using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrupeOnline : MonoBehaviour
{
    public static GrupeOnline grupeOnline;
    public int ONGJ;
    public bool isDrug;
    private void Awake()
    {
        isDrug = false;
        grupeOnline = this;
        ONGJ = 1;
    }
}
