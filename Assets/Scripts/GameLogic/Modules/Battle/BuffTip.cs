using UnityEngine;
using FairyGUI;
using UI_Battle;

public class BuffTip
{
    private UI_HurtNumberWrapper numberWrapper;

    private Actor mActor;

    private int posId;

    public BuffTip(Actor actor, string imgName)
    {
        mActor = actor;
        if (mActor == null || mActor.TransformExt == null)
            return;
        numberWrapper = UI_HurtNumberWrapper.CreateInstance();
        WinMgr.Singleton.TopHudLayer.AddChild(numberWrapper);

        Vector3 uiPos = Vector3.zero;
        posId = mActor.posDistributer.GetNextPosition(out uiPos);
        Vector3 targetPos = WinMgr.Singleton.WorldToScreen(mActor.TransformExt.position, 1);
        targetPos.x += uiPos.x;
        targetPos.y += uiPos.y;
        numberWrapper.SetXY(targetPos.x, targetPos.y);

        GObject gob = UIPackage.CreateObject(WinEnum.UI_Battle, imgName);
        if (gob != null)
        {
            GImage img = gob.asImage;
            numberWrapper.m_hurtNumber.AddChild(img);
            numberWrapper.m_ani.Play(OnPlayCmp);
        }
        else
        {
            numberWrapper.Dispose();
        }
    }

    private void OnPlayCmp()
    {
        if(numberWrapper != null)
            numberWrapper.Dispose();
        if (mActor != null && mActor.TransformExt != null)
        {
            mActor.posDistributer.RestorePositionToPool(posId);
        }
    }

}

