using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RTVideoPlayer
{
    bool played;
    GameObject obj;
    VideoPlayer player;
    System.Action cmpCallback;
    public void Play(string url, RenderTexture rt, System.Action callback)
    {
        cmpCallback = callback;
        obj = new GameObject("RTVideoPlayer");
        player = obj.AddComponent<VideoPlayer>();

        player.url = url;
        player.targetTexture = rt;
        player.source = VideoSource.Url;
        player.waitForFirstFrame = true;
        player.aspectRatio = VideoAspectRatio.FitInside;
        player.renderMode = VideoRenderMode.RenderTexture;
        player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        player.SetTargetAudioSource(0, GameManager.Singleton.getComponent<AudioSource>());
        player.prepareCompleted += (s) => play();
        player.Prepare();
    }

    public void Play(VideoClip clip, RenderTexture rt, System.Action callback)
    {
        cmpCallback = callback;

        obj = new GameObject("RTVideoPlayer");
        player = obj.AddComponent<VideoPlayer>();

        player.clip = clip;
        player.targetTexture = rt;
        player.source = VideoSource.VideoClip;
        player.waitForFirstFrame = true;
        player.aspectRatio = VideoAspectRatio.FitInside;
        player.renderMode = VideoRenderMode.RenderTexture;
        player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        player.SetTargetAudioSource(0, GameManager.Singleton.getComponent<AudioSource>());
        player.prepareCompleted += (s) => play();
        player.Prepare();
    }

    public void Play(string url, System.Action callback, int cameraDep = 1)
    {
        cmpCallback = callback;

        obj = new GameObject("RTVideoPlayer");
        player = obj.AddComponent<VideoPlayer>();

        var cam = new GameObject("videoCamera");
        cam.transform.SetParent(obj.transform);
        var camera = cam.AddComponent<Camera>();
        camera.backgroundColor = Color.black;
        camera.depth = cameraDep;
        camera.cullingMask = 0;

        player.url = url;
        player.source = VideoSource.Url;
        player.waitForFirstFrame = true;
        player.aspectRatio = VideoAspectRatio.FitInside;
        player.renderMode = VideoRenderMode.CameraNearPlane;
        player.targetCamera = camera;
        player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        player.SetTargetAudioSource(0, GameManager.Singleton.getComponent<AudioSource>());
        player.prepareCompleted += (s) => play();
        player.Prepare();
    }

    private void play()
    {
        played = true;
        player.Play();
        CoroutineManager.Singleton.startCoroutine(checkCmp());
    }

    private IEnumerator checkCmp()
    {
        while(true)
        {
            if (!played || player.isPlaying)
                yield return null;
            else
                break;
        }
        if (cmpCallback != null)
            cmpCallback();

        played = false;
        Object.DestroyImmediate(obj);
    }

    public void Stop(bool toComplete = false)
    {
        if (toComplete && cmpCallback != null)
            cmpCallback();
        played = false;
        Object.DestroyImmediate(obj);
    }
}