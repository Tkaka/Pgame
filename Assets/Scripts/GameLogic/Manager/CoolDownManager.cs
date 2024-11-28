/*
 * file CoolDownManager.cs
 *
 * author: Pengmian
 * date: 2014/11/11   
 */

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


/// <summary>
/// 冷却管理器
/// </summary>
public class CoolDownManager
{

    private class CoolDown
    {
        public string Key { get; set; }
        public Int64 BeginTime { get; set; }
        public Int64 EndTime { get; set; }
        public bool IsSaveBack { get; set; }

        public Int64 PausedTime = 0;          //暂停时间

        //当前进度
        public float Progress
        {
            get
            {
                Int64 nowTime = TimeUtils.currentMilliseconds();
                Int64 passedTime = nowTime - BeginTime;
                Int64 during = EndTime - BeginTime;
                if (during <= 0)
                    return 1;
                else
                    return passedTime * 1.0f / during * 1.0f;
            }
        }

        //剩余时间
        public long LeftTime
        {
            get
            {
                Int64 nowTime = TimeUtils.currentMilliseconds();
                Int64 passedTime = nowTime - BeginTime;
                Int64 during = EndTime - BeginTime;
                if (during <= 0)
                    return 0;
                else
                    return during;
            }
        }

        public CoolDown(string key, Int64 start, Int64 during, bool isSaveBack)
        {
            Key = key;
            reset(start, during, isSaveBack);
        }

        public void reset(Int64 start, Int64 during, bool isSaveBack)
        {
            BeginTime = start;
            EndTime = BeginTime + during;
            IsSaveBack = IsSaveBack;
        }


        public CoolDown(string key, Int64 during, bool isSaveBack)
        {
            Key = key;
            reset(during, IsSaveBack);
        }

        public void reset(Int64 during, bool isSaveBack)
        {
            BeginTime = TimeUtils.currentMilliseconds();
            EndTime = BeginTime + during;
            IsSaveBack = isSaveBack;
        }

        public string toString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Key:").Append(Key).Append(",")
               .Append("BeginTime").Append(BeginTime).Append(",")
               .Append("EndTime").Append(EndTime).Append(",")
               .Append("IsSaveBack").Append(IsSaveBack ? 1 : 0);

            return str.ToString();
        }
    }


    //统一冷却管理器,考虑游戏暂停(暂时先干掉，后面替换为Time.time)
    private static List<CoolDownManager> mCDManagerList = new List<CoolDownManager>();

    private Dictionary<string, CoolDown> mCoolDowns;

    public CoolDownManager()
    {
        mCoolDowns = new Dictionary<string, CoolDown>();
        //if (mCDManagerList.IndexOf(this) == -1)
            //mCDManagerList.Add(this);
    }

    public static void onGameResume(long pausedTime)
    {
        foreach(CoolDownManager manager in mCDManagerList)
        {
            foreach(KeyValuePair<string, CoolDown> kv in manager.mCoolDowns)
            {
                kv.Value.EndTime += pausedTime;
            }
        }
    }

    public static void removeCDManager(CoolDownManager cdManager)
    {
        return;
        if (cdManager == null)
            return;
        if (mCDManagerList.IndexOf(cdManager) >= 0)
            mCDManagerList.Remove(cdManager);
    }

    /// <summary>
    /// 添加一个冷却 (毫秒)
    /// </summary>
    /// <param name="key"></param>
    /// <param name="during"></param>
    /// <param name="isSaveBack"></param>
    public void addCoolDown(string key, Int64 during, bool isSaveBack = false)
    {
        CoolDown exist = getCoolDown(key);
        if (exist != null)
        {
            exist.reset(during, isSaveBack);
        }
        else
        {
            exist = new CoolDown(key, during, isSaveBack);
            mCoolDowns.Add(key, exist);
        }

    }

    /// <summary>
    /// 添加一个指定开始时间的定时器
    /// </summary>
    /// <param name="key"></param>
    /// <param name="start"></param>
    /// <param name="during"></param>
    /// <param name="isSaveBack"></param>
    public void addCoolDown(string key, Int64 start, Int64 during, bool isSaveBack = false)
    {
        CoolDown exist = getCoolDown(key);
        if (exist != null)
        {
            exist.reset(start, during, isSaveBack);
        }
        else
        {
            exist = new CoolDown(key, start, during, isSaveBack);
            mCoolDowns.Add(key, exist);
        }
    }


    /// <summary>
    /// 移除一个冷却
    /// </summary>
    /// <param name="key"></param>
    public void removeCoolDown(string key)
    {
        mCoolDowns.Remove(key);
    }

    /// <summary>
    /// 是否冷却 (已经冷却返回true)
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool isCoolDown(string key)
    {
        CoolDown exist = getCoolDown(key);
        if (exist != null)
        {
            return TimeUtils.currentMilliseconds() >= exist.EndTime;
        }
        return true;
    }

    /// <summary>
    /// 获取当前CD进度
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public float getProgress(string key)
    {
        CoolDown exist = getCoolDown(key);
        if (exist != null)
        {
            return exist.Progress;
        }
        return 1.0f; 
    }

    public float GetLeftTime(string key)
    {
        CoolDown exist = getCoolDown(key);
        if (exist != null)
        {
            return exist.LeftTime * 0.001f;
        }
        return 0f;
    }

    /// <summary>
    /// 返回冷却结构
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private CoolDown getCoolDown(string key)
    {
        CoolDown exist;
        return mCoolDowns.TryGetValue(key, out exist) ? exist : null;
    }

    /// <summary>
    /// 是否存在冷却
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool hasCoolDown(string key)
    {
        return getCoolDown(key) == null ? false : true;
    }


    public void clear()
    {
        mCoolDowns.Clear();
    }
}
