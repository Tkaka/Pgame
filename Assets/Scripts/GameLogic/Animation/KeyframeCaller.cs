using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyframeCaller
{

    private class KeyFrame
    {
        public int Name { get; set; }
        public float CallbackTime { get; set; }
        public Action<int> Callback { get; set; }

        public KeyFrame(int name, float callbackTime, Action<int> callback)
        {
            Name = name;
            CallbackTime = callbackTime;
            Callback = callback;
        }

    }

    public const float FRAME_RATE = 30;

    private long coroId;

    private SortedDictionary<int, KeyFrame> mCallbacks = new SortedDictionary<int, KeyFrame>();

    public void RegisterKeyFrameCallback(int keyName, float callbackTime, Action<int> callback)
    {
        KeyFrame key = null;
        mCallbacks.TryGetValue(keyName, out key);
        if (key != null)
        {
            key.CallbackTime = callbackTime;
            key.Callback = callback;
        }
        else
        {
            //必须保证关键帧从小到大的填写(SortedDictionary已经保证了顺序？)
            mCallbacks.Add(keyName, new KeyFrame(keyName, callbackTime, callback));
        }
    }

    /// <summary>
    /// 以关键帧注册
    /// </summary>
    /// <param name="keyframes"></param>
    /// <param name="keyAction"></param>
    public void RegisterKeyFrameCallback(int[] keyframes, Action<int> keyAction)
    {
        if (keyframes != null && keyframes.Length > 0)
        {
            for (int i = 0; i < keyframes.Length; i++)
            {
                float callbackTime = keyframes[i] / FRAME_RATE;
                RegisterKeyFrameCallback(i, callbackTime, keyAction);
            }
        }
    }

    /// <summary>
    /// 以回调时间注册(暂不开放)
    /// </summary>
    /// <param name="keyframes"></param>
    /// <param name="keyAction"></param>
    /*public void RegisterKeyFrameCallback(float[] callbackTimes, Action<string> keyAction)
    {
        if (callbackTimes != null && callbackTimes.Length > 0)
        {
            for (int i = 0; i < callbackTimes.Length; i++)
            {
                RegisterKeyFrameCallback(i + "", callbackTimes[i], keyAction);
            }
        }
    }*/

    public long Run()
    {
        if (coroId > 0)
            CoroutineManager.Singleton.stopCoroutine(coroId);
        coroId = CoroutineManager.Singleton.startCoroutine(RunImpl());
        return coroId;
    }

    private IEnumerator RunImpl()
    {
        float lastCallbackTime = 0.0f;
        foreach (KeyValuePair<int, KeyFrame> keyVal in mCallbacks)
        {
            KeyFrame keyframe = keyVal.Value;
            if (keyframe != null)
            {
                yield return new WaitForSeconds(Mathf.Max(0, keyframe.CallbackTime - lastCallbackTime));
                lastCallbackTime = keyframe.CallbackTime;
                if (keyframe.Callback != null)
                    keyframe.Callback(keyframe.Name);
            }
        }
    }

    public void Stop()
    {
        if (coroId > 0)
        {
            CoroutineManager.Singleton.stopCoroutine(coroId);
        }
    }

    /// <summary>
    /// 释放回调函数引用 
    /// </summary>
    public void Clear()
    {
        Stop();
        foreach (KeyValuePair<int, KeyFrame> keyVal in mCallbacks)
        {
            KeyFrame keyframe = keyVal.Value;
            if (keyframe != null)
                keyframe.Callback = null;
        }
        //此时清除会导致RunImpl回调时mCallbacks已更改
        //mCallbacks.Clear();
    }

}