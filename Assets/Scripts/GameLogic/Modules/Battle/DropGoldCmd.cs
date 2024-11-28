
using FairyGUI;
using UnityEngine;
using DG.Tweening;

public class DropGoldCmd
{

    private GImage aImage;
    public void Excute(Vector3 worldPos)
    {
        aImage = UIPackage.CreateObject(WinEnum.UI_Common, "_common_79").asImage;
        if (aImage != null)
        {
            Vector3 pos = WinMgr.Singleton.WorldToScreen(worldPos);
            aImage.SetXY(pos.x, pos.y);
            WinMgr.Singleton.Hud1Layer.AddChild(aImage);
            Vector3 targetPos = BattleWindow.Singleton.GetGoldPos();
            aImage.TweenMove(targetPos, 0.5f).OnComplete(OnAniCmp);
        }
    }

    private void OnAniCmp()
    {
        //加金币
        BattleWindow.Singleton.SetGoldNum();
        if (aImage != null)
        {
            aImage.Dispose();
        }
    }

}