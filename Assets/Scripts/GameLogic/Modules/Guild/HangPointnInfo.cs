using FairyGUI;
using UI_Guild;
using UnityEngine;
using DG.Tweening;

public class HangPointnInfo : UI_HangPointnInfo
{

    private ActorMC m_actor;

    public void Init(ActorMC actor)
    {
        this.m_actor = actor;
        WinMgr.Singleton.HudLayer.AddChild(this);

    }


    public static new HangPointnInfo CreateInstance()
    {
        return UI_HangPointnInfo.CreateInstance() as HangPointnInfo;
    }



    protected override void OnUpdate()
    {
        base.OnUpdate();
        //修正位置
        fixPos();
    }

    protected void fixPos()
    {

        Vector3 ownerPos = m_actor.monoBehavior.headBarPos;
        ownerPos.y += 1;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(ownerPos);
        screenPos.y = Screen.height - screenPos.y; //convert to Stage coordinates system

        Vector3 pt = GRoot.inst.GlobalToLocal(screenPos);
        this.SetXY(Mathf.RoundToInt(pt.x - actualWidth * 0.5f), Mathf.RoundToInt(pt.y - actualHeight * 0.5f));
    }

    public void RefreshView(string strInfo)
    {
        m_txtInfo.text = strInfo;
    }

    public override void Dispose()
    {
        m_actor = null;
        base.Dispose();
    }

    public void ToggleVisible(bool flag)
    {
        visible = flag;
    }

    public void TouchToggle(bool flag)
    {
        touchable = flag;
    }
    public void ShowHeadBar()
    {
    }


    public void Destroy(float delay = 0)
    {
        Dispose();
    }


}
