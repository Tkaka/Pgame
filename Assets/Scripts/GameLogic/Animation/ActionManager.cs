using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{

    private AngelaBaby owner;

    public const float FRAME_RATE = 30;

    /// <summary>
    /// 动作名字 --- 关键帧管理器
    /// </summary>
    private Dictionary<string, KeyframeCaller> keyframeCallers;

    private IAnimationCtrl mAnimationCtrl;

    /// <summary>
    /// 当前动画帧回调
    /// </summary>
    private KeyframeCaller curCaller;

    public ActionManager(AngelaBaby owner)
    {
        this.owner = owner;
        keyframeCallers = new Dictionary<string, KeyframeCaller>();
    }

    public void SetSimpleAnimation(SimpleAnimation anim)
    {
        mAnimationCtrl = new SimpleAnimationCtrl(anim);
    }

    /// <summary>
    /// 用于直接播放动画的接口
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private AnimationClip PlayAni(string name, float speed = 1.0f)
    {
        if (mAnimationCtrl == null)
            return null;
        AnimationClip clip = mAnimationCtrl.GetClip(name);
        if (clip != null)
        {
            if (name == ActorState.hurt)
                mAnimationCtrl.PlayAnimation(name, 0, speed);
            else
                mAnimationCtrl.PlayAnimation(name, 0.2f, speed);
            return clip;
        }
        else
        {
            Logger.err(owner.Name + "不存在的动画Clip:[" + name + "]");
            return null;
        }
    }

    public void PlayCommonAnimation(string stateKey, Action<int> endCallBack=null, float speed = 1.0f)
    {
        Stop();
        AnimationClip clip = PlayAni(stateKey, speed);
        if (endCallBack != null)
        {
            //第二次播放不需要注册关键帧
            bool isNew;
            KeyframeCaller keyCaller = GetKeyframeCaller(stateKey, out isNew);
            if (isNew)
            {
                if (clip != null)
                {
                    keyCaller.RegisterKeyFrameCallback(0, clip.length, endCallBack);
                }
                else
                {
                    //如果动画为空，则默认为1帧
                    keyCaller.RegisterKeyFrameCallback(0, GTools.Frame2Time(1), endCallBack);
                }
            }
            if (keyCaller != null)
            {
                curCaller = keyCaller;
                keyCaller.Run();
            }
        }
    }

    public void PlayAnimation(string name, int[] keyframes, Action<int> endCallBack, Action<int> keyCallBack)
    {
        Stop();
        AnimationClip clip = PlayAni(name);
        if (keyframes == null || keyframes.Length <= 0)
            return;
        //添加保护时间
        //AddProtected(name, keyframes);
        //第二次播放不需要注册关键帧
        bool isNew;
        KeyframeCaller keyCaller = GetKeyframeCaller(name, out isNew);
        if (isNew)
        {
            if (clip != null)
            {
                keyCaller.RegisterKeyFrameCallback(keyframes, keyCallBack);
                keyCaller.RegisterKeyFrameCallback(keyframes.Length, clip.length, endCallBack);
            }
            else
            {
                keyCaller.RegisterKeyFrameCallback(keyframes, keyCallBack);
                //如果动画为空，则为最大关键帧+1帧
                int endKey = keyframes[keyframes.Length - 1] + 1;
                keyCaller.RegisterKeyFrameCallback(keyframes.Length, GTools.Frame2Time(endKey), endCallBack);
            }
        }
        if (keyCaller != null)
        {
            curCaller = keyCaller;
            keyCaller.Run();
        }
    }

    /// <summary>
    /// 当前动画名字
    /// </summary>
    private string curAniName;
    /// <summary>
    /// 将最大关键帧作为保护时间
    /// </summary>
    /// <param name="templateId"></param>
    public void AddProtected(string name, int[] keyframes)
    {
        curAniName = name;
        if (keyframes != null && keyframes.Length > 0)
        {
            float protectedTime = 0;
            //设置最大关键帧为保护时间
            protectedTime = keyframes[keyframes.Length - 1] / FRAME_RATE;
            if (protectedTime > 0)
            {
                owner.CdMgr.addCoolDown("ani:" + name, Time.time, protectedTime);
                //Logger.wrn("动作[" + templateId.ToString() + "]需要添加的一段保护时间为: " + firstDuring);
            }
        }
    }

    /// <summary>
    /// 指定id的动作是否能打断当前动作
    /// </summary>
    /// <param name="templateId"></param>
    /// <returns></returns>
    public bool CanInterruptCurrentAction()
    {
        if (string.IsNullOrEmpty(curAniName))
        {
            // 角色当前无任何动作
            return true;
        }
        //当机器卡顿时候，有可能会差2到3毫秒
        bool flag = owner.CdMgr.isCoolDown("ani:" + curAniName);
        //if (!flag)
        //{
        //    Logger.err("can not interrupt : " + curAniName + " left time: " + mOwner.getCoolDownManager().GetLeftTime(AniCd + curAniName));
        //}
        return flag;
    }

    /// <summary>
    /// 停止当前动画
    /// </summary>
    public void Stop()
    {
        if (curCaller != null)
            curCaller.Stop();
    }

    public float GetAniLen(string name)
    {
        if (mAnimationCtrl != null)
        {
            return mAnimationCtrl.GetAniLen(name);
        }
        return 0;
    }

    public bool IsExist(string name)
    {
        if (mAnimationCtrl != null)
        {
            return mAnimationCtrl.IsExist(name);
        }
        return false;
    }

    private KeyframeCaller GetKeyframeCaller(string key, out bool isNew)
    {
        KeyframeCaller keyCaller = null;
        if (keyframeCallers.ContainsKey(key))
        {
            keyCaller = keyframeCallers[key];
            isNew = false;
        }
        else
        {
            keyCaller = new KeyframeCaller();
            keyframeCallers[key] = keyCaller;
            isNew = true;
        }
        return keyCaller;
    }

    public void Clear()
    {
        foreach (KeyValuePair<string, KeyframeCaller> keyVal in keyframeCallers)
        {
            KeyframeCaller caller = keyVal.Value;
            if (caller != null)
                caller.Clear();
        }
        keyframeCallers.Clear();
    }

}

