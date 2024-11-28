using System.Collections;
using UI_Common;
using Data.Beans;
using FairyGUI;
using UnityEngine;

public class GuideTip : UI_GuideTip, IGuideTip
{
    public bool EffectEnd()
    {
        return true;
    }

    public void Init(t_guide_stepBean guideBean, GObject clickObj)
    {
        this.touchable = false;
        this.m_tip.text = guideBean.t_tip;

        if (clickObj != null)
        {
            var pos = clickObj.TransformPoint(Vector2.zero, WinMgr.Singleton.GuideLayer);
            this.SetXY(pos.x, pos.y - this.height - 20);
        }
        else
        {
            this.SetXY(Screen.width / 2f, Screen.height / 2f);
        }
    }
}