using System;
using UI_Battle;

public class MasterSkillCtrl : SingletonTemplate<MasterSkillCtrl>
{
    private UI_MasterSkillAni ani;

    private Action callback;

    public void Init()
    {
        ani = UI_MasterSkillAni.CreateInstance();
        WinMgr.Singleton.NoticeLayer.AddChild(ani);
        ani.visible = false;
    }

    public void Play(Action callback)
    {
        if (ani != null)
        {
            ani.visible = true;
            this.callback = callback;
            ani.m_t0.Play(OnAniCmp);
        }
    }

    private void OnAniCmp()
    {
        if (ani != null)
        {
            ani.m_t0.Stop();
            ani.visible = false;
            if (callback != null)
                callback();
            callback = null;
        }
    }

    public void OnClose()
    {
        if (ani != null)
            ani.Dispose();
        mSingleton = null;
    }

}
