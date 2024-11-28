/*
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : SingletonBehaviour<SoundManager>
{

    private AudioClip[] SFXSounds;

    private AudioSource SFXAudioSource;

    private AudioSource bgmAudioSource;

    private Tween tween;

    protected override void Awake()
    {
        base.Awake();
        bgmAudioSource = GetOrAddComponent<AudioSource>();
    }


    public void SetBgmAudioSource(AudioSource audioSource)
    {
        bgmAudioSource = audioSource;
    }


    public void PlaySFX(string path, Vector3? position = null, Transform parent = null, bool loop = false)
    {
        if (string.IsNullOrEmpty(path))
            return;
        GameObject go = new GameObject("Audio:" + path);
        if(position.HasValue)
            go.transform.position = position.Value;
        go.transform.parent = parent;

        // create the source
        AudioSource source = go.AddComponent<AudioSource>();
        AudioClip clip = Res.Singleton.LoadARes<AudioClip>("Sound/" + path);
        if (clip != null)
        {
            source.clip = clip;
            source.volume = 1;
            source.loop = loop;
            source.spatialBlend = 0;
            source.Play();
            if (!loop)
            {
                Destroy(go, clip.length);
            }
        }
        else
        {
            Destroy(go);
        }
    }



    public void PlayBGM(string path, bool loop=true)
    {
        if (bgmAudioSource != null)
        {
            //删除之前的背景音乐
            Resources.UnloadAsset(bgmAudioSource.clip);
            AudioClip clip = Res.Singleton.LoadARes<AudioClip>(path);
            if (clip != null)
            {
                bgmAudioSource.clip = clip;
                bgmAudioSource.loop = loop;
                //bgmAudioSource.DOFade(1.0f, 0.5f);
                bgmAudioSource.Play();
            }
        }
    }


    public void RemoveSound(long id)
    {

    }

}*/