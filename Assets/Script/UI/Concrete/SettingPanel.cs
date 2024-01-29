using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/SettingPanel";
    AudioMixer audio;
    public SettingPanel() : base(new UIType(path))
    {
        audio  = Resources.Load<AudioMixer>("AudioMixer/AudioMixer");
    }
    public override void OnEnter()
    {
        Slider bgmSlider = UITool.GetOrAddComponentInChildren<Slider>("BGM");
        Slider sfxSlider = UITool.GetOrAddComponentInChildren<Slider>("SFX");

        float bgmValue ,sfxValue ;

        audio.GetFloat("BGM", out bgmValue);
        audio.GetFloat("SFX", out sfxValue);

        bgmSlider.value = bgmValue;
        sfxSlider.value = sfxValue;


        bgmSlider.onValueChanged.AddListener(value =>
        {
            //lambda表达式
            audio.SetFloat("BGM", value);
            if(value == bgmSlider.minValue)
            {
                audio.SetFloat("BGM", -80f);
            }
        });

        sfxSlider.onValueChanged.AddListener(value =>
        {
            //lambda表达式
            audio.SetFloat("SFX", value);
            if (value == sfxSlider.minValue)
            {
                audio.SetFloat("SFX", -80f);
            }
        });

        UITool.GetOrAddComponentInChildren<Button>("Child").onClick.AddListener(() =>
        {
            PanelManager.Pop();

        });
        
    }

}
