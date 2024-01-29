using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetingBGMvolume(float value)
    {
        mixer.SetFloat("BGM", value);
    }
    public void SetingSFXvolume(float value)
    {
        mixer.SetFloat("SFX", value);
    }

}
