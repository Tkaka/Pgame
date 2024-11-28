using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using DG.Tweening;
using UI_Battle;
public class DropItemMgr : SingletonTemplate<DropItemMgr>
{
    private List<GObject> gImages = new List<GObject>();
    private int mSingleDropNum = 6;  //单次掉落金币数量


    //获得金币掉落的数量
    private int _GetDropCoinNum(Actor actor, long hurt)
    {
        int num = 0;
        int dropCount = 0;  //掉落次数

        LNumber totalHp = FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, true);//actor.getBaseProperty(PropertyType.Hp);
        LNumber curHp = FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, false);//actor.getProperty(PropertyType.Hp);
        LNumber hurtBefore = totalHp - curHp;
        LNumber hurtAfter = hurtBefore + (LNumber)hurt;
       
        for (int i = 0; i < 20; i++)
        {
            float targetNum = (i + 1) * totalHp * 0.05f;
            if (hurtBefore >= targetNum)
                continue;

            if (hurtAfter >= targetNum)
                dropCount++;
            else
                break;
        }
       // Debug.LogError("-------------------->>>掉落信息" + (hurtBefore * 1.0) / totalHp + "      " + (hurtAfter * 1.0) / totalHp + "        " + dropCount + "    " + totalHp + "      " + curHp);
        num = dropCount * mSingleDropNum;
        return num;
    }

    public void ShowDropItems(Actor actor, long totalHurt)
    {
        int num = _GetDropCoinNum(actor, totalHurt);
        if (num == 0)
            return;

        Vector3 worldPos = actor.TransformExt.position;
        for (int i = 0; i < num; i++)
        {

            GObject aImage = null;
            foreach (var info in gImages)
            {
                if (info.visible == false)
                {
                    aImage = info;
                    break;
                }  
            }

            if (aImage == null)
            {
                aImage = UI_dropItemAni.CreateInstance();// UIPackage.CreateObject(WinEnum.UI_Battle, "dropItemAni").asImage;
                gImages.Add(aImage);
            }
                 
            if (aImage != null)
            {
                aImage.visible = true;
                Vector3 pos = WinMgr.Singleton.WorldToScreen(worldPos);                 
                int randNum = Random.Range(-100, 100);
                aImage.SetXY(pos.x + randNum, pos.y);
                WinMgr.Singleton.Hud1Layer.AddChild(aImage);
                UI_dropItemAni ani = aImage as UI_dropItemAni;
                if (ani != null)
                    ani.m_dropAni.Play();
            }
        }
 
    }

    public void StarMove()
    {
        if (gImages.Count == 0)
            return;

        for(int i = 0; i < gImages.Count; i++)
        {
            GObject aImage = gImages[i];
            Vector3 targetPos = BattleWindow.Singleton.GetGoldPos();
            aImage.TweenMove(targetPos, 0.5f).OnComplete(()=> 
            {
                aImage.visible = false;
            });
        }
    }




    public void Clear()
    {
        for (int i = 0; i < gImages.Count; i++)
        {
            gImages[i].Dispose();
        }
        gImages.Clear();
    }
}