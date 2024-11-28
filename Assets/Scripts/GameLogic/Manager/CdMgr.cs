using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


/// <summary>
/// 冷却管理器
/// </summary>
public class CdMgr
{

    private class CoolDown
    {
        public string Key { get; set; }
        public float BeginTime { get; set; }
        public float EndTime { get; set; }

        //当前进度
        public float Progress
        {
            get
            {
                float nowTime = Time.time;
                float passedTime = nowTime - BeginTime;
                float during = EndTime - BeginTime;
                if (during <= 0)
                    return 1;
                else
                    return passedTime / during;
            }
        }

        //剩余时间
        public float LeftTime
        {
            get
            {
                float nowTime = Time.time;
                float passedTime = nowTime - BeginTime;
                float during = EndTime - BeginTime;
                if (during <= 0)
                    return 0;
                else
                    return during;
            }
        }

        public CoolDown(string key, float start, float during)
        {
            Key = key;
            reset(start, during);
        }

        public void reset(float start, float during)
        {
            BeginTime = start;
            EndTime = BeginTime + during;
        }


        public CoolDown(string key, float during)
        {
            Key = key;
            reset(during);
        }

        public void reset(float during)
        {
            BeginTime = Time.time;
            EndTime = BeginTime + during;
        }

        public string toString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Key:").Append(Key).Append(",")
               .Append("BeginTime").Append(BeginTime).Append(",")
               .Append("EndTime").Append(EndTime);
            return str.ToString();
        }
    }


    private Dictionary<string, CoolDown> mCoolDowns;

    public CdMgr()
    {
        mCoolDowns = new Dictionary<string, CoolDown>();
    }


    /// <summary>
    /// 添加一个冷却 (毫秒)
    /// </summary>
    /// <param name="key"></param>
    /// <param name="during"></param>
    /// <param name="isSaveBack"></param>
    public void addCoolDown(string key, float during)
    {
        CoolDown exist = getCoolDown(key);
        if (exist != null)
        {
            exist.reset(during);
        }
        else
        {
            exist = new CoolDown(key, during);
            mCoolDowns.Add(key, exist);
        }

    }

    /// <summary>
    /// 添加一个指定开始时间的定时器
    /// </summary>
    /// <param name="key"></param>
    /// <param name="start"></param>
    /// <param name="during"></param>
    public void addCoolDown(string key, float start, float during)
    {
        CoolDown exist = getCoolDown(key);
        if (exist != null)
        {
            exist.reset(start, during);
        }
        else
        {
            exist = new CoolDown(key, start, during);
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
            return Time.time >= exist.EndTime;
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
            return exist.LeftTime;
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
