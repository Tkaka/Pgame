/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 *  Coder：Zhou XiQuan
 *  Time ：2017.10.20
 */ 
using UnityEngine;
using System.Collections.Generic;

public class AudioData
{
    public bool playing;
    public AudioSource source;
    public System.Action callback;
    public SoundType sndType = SoundType.Music;
}

public enum SoundType
{
    Music = 1,      //循环音乐
    Effect,         //单次音效
    Voice,          //语音
    Music3D,        //3D声音(环境音效等)
    Effect3D,       //3D音效
}

public class AudioManager : SingletonTemplate<AudioManager>
{
    private GameObject soundGroup;
    private ResPack resPacker = new ResPack("AudioManager");

    private long m_idxNow = 0;
    private Dictionary<SoundType, bool> m_sndOffMap = new Dictionary<SoundType, bool>();
    private Dictionary<SoundType, float> m_tempVMap = new Dictionary<SoundType, float>();
    private Dictionary<SoundType, float> m_volumeMap = new Dictionary<SoundType, float>();
    private Dictionary<long, AudioData> m_sndPlayingMap = new Dictionary<long, AudioData>();

    //bgm只能存在一个
    private long m_nowBgmID = -1;

    //缓存
    private int m_maxCacheNum = 10;
    private Stack<AudioSource> m_sourceCache = new Stack<AudioSource>();

    /// <summary>
    /// 背景音乐,Stop前有效
    /// </summary>
    /// <param name="name">name（string）音乐名字</param>
    /// <returns>唯一识别id</returns>
    public long PlayBGM(string name)
    {
        StopBGM();
        m_nowBgmID = PlayMusic(name);
        return m_nowBgmID;
    }

    /// <summary>
    /// 关闭背景音乐
    /// </summary>
    public void StopBGM()
    {
        if(m_nowBgmID != -1)
        {
            Stop(m_nowBgmID);
            m_nowBgmID = -1;
        }
    }

    /// <summary>
    /// 播放音乐/背景音乐/循环
    /// </summary>
    /// <param name="name">声音资源名</param>
    /// <param name="volume">声音 >=0生效</param>
    public long PlayMusic(string name, float volume = -1)
    {
        return _PlaySnd(name, SoundType.Music, null, -1, volume);
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name">声音资源名</param>
    /// <param name="callback">回调</param>
    /// <param name="otherVolume">修改正在播放声音的音量（小于0无效）</param>
    /// <param name="volume">播放音量</param>
    public long PlayEffect(string name, System.Action callback = null, float otherVolume = -1, float volume = -1f)
    {
        return _PlaySnd(name, SoundType.Effect, callback, otherVolume, volume);
    }

    /// <summary>
    /// 播放3d循环
    /// </summary>
    /// <param name="name">声音资源</param>
    /// <param name="distance">最远距离</param>
    /// <param name="pos">位置</param>
    /// <param name="volume">音量</param>
    public long Play3DMusic(string name, Vector3 pos, float distance = -1f, float volume = -1f)
    {
        return _PlaySnd(name, SoundType.Effect, null, -1f, volume, distance, pos);
    }

    /// <summary>
    /// 播放3d音效
    /// </summary>
    /// <param name="name">声音资源</param>
    /// <param name="distance">最远距离</param>
    /// <param name="pos">位置</param>
    /// <param name="volume">音量</param>
    public long Play3DEffect(string name, Vector3 pos, float distance = -1f, float volume = -1f)
    {
        return _PlaySnd(name, SoundType.Effect, null, -1f, volume, distance, pos);
    }

    /// <summary>
    /// 播放声音
    /// </summary>
    private long _PlaySnd(string name, SoundType sndType, System.Action callback = null, float otherVolume = -1f, float volume = -1f, float distance = -1f, Vector3 ? pos = null)
    {
        if(!_GetToggle(sndType))
            return -1;

        long id = _NewId();
        AudioSource source = _GetSource();
        source.gameObject.SetActive(true);
        AudioData ad = new AudioData();
        ad.playing = true;
        ad.source = source;
        ad.sndType = sndType;
        m_sndPlayingMap.Add(id, ad);

        resPacker.Request(name, name, typeof(AudioClip), (a, b, t) =>
        {
            if(m_sndPlayingMap.ContainsKey(id))
            {
                AudioClip clip = resPacker.GetObject(a, b, t) as AudioClip;
                if (clip != null)
                {
                    bool loop = _ShouldLoop(sndType);
                    AudioData data = m_sndPlayingMap[id];
                    data.source.clip = clip;
                    data.source.loop = loop;
                    if(_Is3DSnd(sndType))
                    {
                        //3d声音
                        if (distance > 0)
                            data.source.maxDistance = distance;
                        data.source.spatialBlend = 1;
                        data.source.rolloffMode = AudioRolloffMode.Linear;
                        data.source.transform.position = pos.Value;
                    }
                    else
                    {
                        data.source.spatialBlend = 0;
                    }
                    float tmpVolume = _GetTmpVolume(sndType);
                    if (otherVolume >= 0)
                    {
                        if (tmpVolume > -1)
                            Debuger.Wrn("当前有louder声音在播放", name);
                        SetTempVolume(otherVolume);
                        callback += _LouderClipEnd;
                    }
                    if (volume < 0)
                        volume = tmpVolume >= 0 ? tmpVolume : _GetVolume(sndType);
                    data.source.volume = volume;
                    if (ad.playing)
                    {
                        data.source.Play();
                        if (!loop)
                        {
                            CoroutineManager.Singleton.delayedCall(ad.source.clip.length - ad.source.time + 0.1f, () =>
                            {
                                if (m_sndPlayingMap.ContainsKey(id) && ad.playing)
                                    this.Stop(id);
                            });
                        }
                    }
                }
                else
                {
                    Stop(id);
                    //资源不低，归还引用
                    resPacker.ReleaseRes(name);
                }
            }
        });
        return id;
    }
    
    //louder声音播放完成
    private void _LouderClipEnd()
    {
        SetTempVolume();
    }

    /// <summary>
    /// 播放单独音效，比如聊天语音
    /// </summary>
    public long PlayClip(AudioClip clip, System.Action callback = null, float otherVolume = -1, float volume = -1)
    {
        if(clip == null)
        {
            if(callback != null)
                callback();
            return -1;
        }

        SoundType sndType = SoundType.Voice;
        if(!_GetToggle(sndType))
            return -1;

        long id = _NewId();
        AudioSource source = _GetSource();
        source.gameObject.SetActive(true);
        AudioData ad = new AudioData();
        ad.source = source;
        ad.sndType = sndType;
        m_sndPlayingMap.Add(id, ad);
        ad.callback = callback;
        source.clip = clip;
        source.loop = false;

        float tmpVolume = _GetTmpVolume(sndType);
        if(otherVolume >= 0)
        {
            if(tmpVolume > -1)
                Debuger.Log("当前有louder声音在播放", clip.name);
            SetTempVolume(otherVolume);
            ad.callback += _LouderClipEnd;
        }
        if(volume < 0)
            volume = tmpVolume >= 0 ? tmpVolume : _GetVolume(sndType);
        source.volume = volume;
        source.Play();
        ad.playing = true;
        CoroutineManager.Singleton.delayedCall(ad.source.clip.length - ad.source.time + 0.1f, () =>
        {
            if (m_sndPlayingMap.ContainsKey(id) && ad.playing)
                this.Stop(id);
        });
        return id;
    }

    /// <summary>
    /// 播放一个暂停的声音
    /// </summary>
    public void Play(long id)
    {
        if(m_sndPlayingMap.ContainsKey(id))
        {
            AudioData ad = m_sndPlayingMap[id];
            if(!ad.playing)
            {
                float tmpVolume = _GetTmpVolume(ad.sndType);
                if(tmpVolume < 0)
                    ad.source.volume = _GetTmpVolume(ad.sndType);
                else
                    ad.source.volume = tmpVolume;
                if(!_ShouldLoop(ad.sndType))
                {
                    CoroutineManager.Singleton.delayedCall(ad.source.clip.length - ad.source.time + 0.1f, () =>
                    {
                        if (m_sndPlayingMap.ContainsKey(id) && ad.playing)
                            this.Stop(id);
                    });
                }
                ad.playing = true;
                ad.source.Play();
            }
        }
    }

    // 是否正在播放
    public bool IsPlaying(int id)
    {
        return m_sndPlayingMap.ContainsKey(id) && m_sndPlayingMap[id].playing;
    }

    /// <summary>
    /// 声音开关
    /// </summary>
    /// <param name="sndType">声音类型</param>
    public void SetToggle(SoundType sndType, bool value)
    {
        m_sndOffMap[sndType] = !value;
        if(!value)
            StopAllType(sndType);
    }

    /// <summary>
    /// 设置音量大小
    /// </summary>
    /// <param name="volume">0-1的值</param>
    public void SetVolume(float volume = 1)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        var arr = System.Enum.GetValues(typeof(SoundType)) as SoundType[];
        for(int i = arr.Length - 1; i >= 0; --i)
            SetVolume(arr[i], volume);
    }

    /// <summary>
    /// 设置特定音量大小
    /// </summary>
    /// <param name="sndType">声音类型</param>
    /// <param name="volume">0-1的值</param>
    public void SetVolume(SoundType sndType, float volume = 1)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        m_volumeMap[sndType] = volume;
        var enu = m_sndPlayingMap.Values.GetEnumerator();
        while(enu.MoveNext())
        {
            if(enu.Current.sndType == sndType)
                enu.Current.source.volume = volume;
        }
        enu.Dispose();
    }

    /// <summary>
    /// 临时设置声音大小, volume小于0时恢复SetVolume的音量
    /// </summary>
    public void SetTempVolume(float volume = -1)
    {
        var arr = System.Enum.GetValues(typeof(SoundType)) as SoundType[];
        for(int i = arr.Length - 1; i >= 0; --i)
            SetTempVolume(arr[i], volume);
    }

    /// <summary>
    /// 设置音量,volume小于0时恢复SetVolume的音量
    /// </summary>
    /// <param name="sndType">声音类型</param>
    /// <param name="volume">音量大小</param>
    public void SetTempVolume(SoundType sndType, float volume = -1)
    {
        if(volume < 0)
            volume = _GetVolume(sndType);

        m_tempVMap[sndType] = volume;
        var enu = m_sndPlayingMap.Values.GetEnumerator();
        while(enu.MoveNext())
        {
            if(enu.Current.sndType == sndType)
                enu.Current.source.volume = volume;
        }
        enu.Dispose();
    }

    /// <summary>
    /// 停止播放声音
    /// </summary>
    public void Stop(long id)
    {
        if(!m_sndPlayingMap.ContainsKey(id))
            return;

        AudioData snd = m_sndPlayingMap[id];
        m_sndPlayingMap.Remove(id);
        if(snd.source != null)
        {
            if(snd.source.isPlaying)
                snd.source.Stop();
            snd.source.gameObject.SetActive(false);
            if(snd.callback != null)
                snd.callback();
            AudioClip clip = snd.source.clip;
            if(clip != null)
                resPacker.ReleaseRes(clip.name);
            snd.callback = null;
            snd.playing = false;
            snd.source.clip = null;
            m_sourceCache.Push(snd.source);
        }

        //缓存的gameObject的清理
        if(m_sourceCache.Count > m_maxCacheNum)
        {
            AudioSource aud = m_sourceCache.Pop();
            GameObject.DestroyImmediate(aud.gameObject);
        }
    }

    /// <summary>
    /// 停止所有指定类型的声音
    /// </summary>
    public void StopAllType(SoundType sndType)
    {
        List<long> ids = new List<long>(m_sndPlayingMap.Keys);
        for(int i=0; i < ids.Count; ++i)
        {
            if(m_sndPlayingMap[ids[i]].sndType == sndType)
                Stop(ids[i]);
        }
    }

    /// <summary>
    /// 停止所有声音
    /// </summary>
    public void StopAll()
    {
        List<long> ids = new List<long>(m_sndPlayingMap.Keys);
        for(int i = 0; i < ids.Count; ++i)
            Stop(ids[i]);
    }

    /// <summary>
    /// 暂停一个声音
    /// </summary>
    /// <param name="id">id</param>
    public void Pause(long id)
    {
        if(m_sndPlayingMap.ContainsKey(id))
        {
            m_sndPlayingMap[id].playing = false;
            m_sndPlayingMap[id].source.Pause();
        }
    }

    /// 获取缓存AudioSource
    private AudioSource _GetSource()
    {
        if(soundGroup == null)
        {
            soundGroup = new GameObject("[SoundGroup]");
            GameObject.DontDestroyOnLoad(soundGroup);
        }

        AudioSource source = null;
        if (m_sourceCache.Count > 0)
            source = m_sourceCache.Pop();
        else
            source = new GameObject("snd_obj").AddComponent<AudioSource>();
        source.transform.SetParent(soundGroup.transform);
        source.playOnAwake = false;
        return source;
    }

    /// 获取临时音量
    private float _GetTmpVolume(SoundType sndType)
    {
        if(m_tempVMap.ContainsKey(sndType))
            return m_tempVMap[sndType];
        return -1;
    }

    /// 获取预设音量
    private float _GetVolume(SoundType sndType)
    {
        if(m_volumeMap.ContainsKey(sndType))
            return m_volumeMap[sndType];
        return 1;
    }

    /// 获取开关
    private bool _GetToggle(SoundType sndType)
    {
        if(m_sndOffMap.ContainsKey(sndType))
            return !m_sndOffMap[sndType];
        return true;
    }

    /// 是否应该循环
    private bool _ShouldLoop(SoundType sndType)
    {
        return sndType == SoundType.Music || sndType == SoundType.Music3D;
    }

    private bool _Is3DSnd(SoundType sndType)
    {
        return sndType == SoundType.Music3D || sndType == SoundType.Effect3D;
    }

    /// 分配id
    private long _NewId()
    {
        while(true)
        {
            m_idxNow++;
            if(m_idxNow < 0)
                m_idxNow = 1;
            if(!m_sndPlayingMap.ContainsKey(m_idxNow))
                return m_idxNow;
        }
    }
}