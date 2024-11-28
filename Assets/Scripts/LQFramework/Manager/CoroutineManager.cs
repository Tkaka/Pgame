using System;
using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    /// <summary>
    /// 内部辅助类
    /// </summary>
    private class CoroutineTask
    {
        public Int64 Id { get; set; }
        public bool Running { get; set; }
        public bool Paused { get; set; }

        public CoroutineTask(Int64 id)
        {
            Id = id;
            Running = true;
            Paused = false;
        }

        public IEnumerator coroutineWrapper(IEnumerator co)
        {
            IEnumerator coroutine = co;
            while (Running)
            {
                if (Paused)
                    yield return null;
                else
                {
                    if (coroutine != null)
                    {
                        try
                        {
                            coroutine.MoveNext();
                        }catch(Exception e)
                        {
                            Debug.LogError(e.Message + "\n" + e.StackTrace);
                        }
                        yield return coroutine.Current;
                    }
                    else
                        Running = false;
                }
            }
            mCoroutines.remove(Id.ToString());
        }
    }

    private static Map<string, CoroutineTask> mCoroutines;
    public static CoroutineManager Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
        mCoroutines = new Map<string, CoroutineTask>();
    }

    /// <summary>
    /// 启动一个协程
    /// </summary>
    /// <param name="co"></param>
    /// <returns></returns>
    public long startCoroutine(IEnumerator co)
    {
        if (this.gameObject.activeSelf)
        {
            CoroutineTask task = new CoroutineTask(IdAssginer.getId(IdAssginer.IdType.CoroutineId));
            mCoroutines.add(task.Id.ToString(), task);
            StartCoroutine(task.coroutineWrapper(co));
            return task.Id;
        }
        return -1;
    }

    /// <summary>
    /// 停止一个协程
    /// </summary>
    /// <param name="id"></param>
    public void stopCoroutine(long id)
    {
        CoroutineTask task = mCoroutines.get(id.ToString());
        if (task != null)
        {
            task.Running = false;
            mCoroutines.remove(id.ToString());
        }
    }

    /// <summary>
    /// 暂停协程的运行
    /// </summary>
    /// <param name="id"></param>
    public void pauseCoroutine(Int64 id)
    {
        CoroutineTask task = mCoroutines.get(id.ToString());
        if (task != null)
        {
            task.Paused = true;
        }
        else
        {
            Debug.LogError("coroutine: " + id.ToString() + " is not exist!");
        }
    }

    /// <summary>
    /// 恢复协程的运行
    /// </summary>
    /// <param name="id"></param>
    public void resumeCoroutine(Int64 id)
    {
        CoroutineTask task = mCoroutines.get(id.ToString());
        if (task != null)
        {
            task.Paused = false;
        }
        else
        {
            Debug.LogError("coroutine: " + id.ToString() + " is not exist!");
        }
    }

    public long delayedCall(float delayedTime, Action callback)
    {
        return startCoroutine(delayedCallImpl(delayedTime, callback));
    }

    private IEnumerator delayedCallImpl(float delayedTime, Action callback)
    {
        if (delayedTime >= 0)
            yield return new WaitForSeconds(delayedTime);
        callback();
    }


    public long delayedCall(float delayedTime, Action<object> callback, object param)
    {
        return startCoroutine(delayedCallImpl(delayedTime, callback, param));
    }

    private IEnumerator delayedCallImpl(float delayedTime, Action<object> callback, object param)
    {
        if (delayedTime >= 0)
            yield return new WaitForSeconds(delayedTime);
        callback(param);
    }

    private void OnDestroy()
    {
        foreach (CoroutineTask task in mCoroutines.Container.Values)
        {
            task.Running = false;
        }
        mCoroutines.clear();
    }

}
