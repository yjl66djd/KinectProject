using CustomFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 音频管理器，储存所有音频可以播放和停止
/// </summary>
public class AudioManager : SingletonMono<AudioManager>
{
    [System.Serializable]
    public class Sound
    {
        [Header("音频剪辑")]
        public AudioClip clip;

        [Header("音频分组")]
        public AudioMixerGroup outputGroup;

        [Header("音频音量")]
        public float volume = 0.2f;

        [Header("音频是否开局播放")]
        public bool playOnAwake;

        [Header("音频是否循环播放")]
        public bool loop;

    }

    /// <summary>
    /// 存储所有音频信息
    /// </summary>
    public List<Sound> sounds = new List<Sound>();

    /// <summary>
    /// 每一个音频剪辑对应一个音频组件
    /// </summary>
    private Dictionary<string, AudioSource> audioDic;
      

    private void Start()
    { 
        audioDic = new Dictionary<string, AudioSource>();

        foreach (var sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);

            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;
            source.volume = sound.volume;
            source.outputAudioMixerGroup = sound.outputGroup;

            if (sound.playOnAwake)
            {
                source.Play();
            }

            audioDic.Add(source.clip.name, source);
        }
    }

    /// <summary>
    /// 播放一个音频
    /// </summary>
    /// <param name="name">音频名称</param>
    /// <param name="isWait">是否等待音频播放完</param>
    public static void PlayerAudio(string name , bool isWait = false)
    {
        if (!Instance.audioDic.ContainsKey(name))
        {
            Debug.LogWarning("音频不存在");
            return;
        }
        if (isWait)
        {
            if (!Instance.audioDic[name].isPlaying)
            {
                Instance.audioDic[name].Play(); 
            }
        }
        else
        {
            Instance.audioDic[name].Play();   
        }
    }

    /// <summary>
    /// 停止某一音频播放
    /// </summary>
    /// <param name="name"></param>
    public static void StopAudio(string name)
    {
        if (!Instance.audioDic.ContainsKey(name))
        {
            Debug.LogWarning($"名为{name}的音频不存在");
            return;
        }
        Instance.audioDic[name].Stop();
            
    }

}
