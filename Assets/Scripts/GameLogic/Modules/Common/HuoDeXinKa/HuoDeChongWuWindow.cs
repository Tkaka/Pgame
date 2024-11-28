using System;
using FairyGUI;
using UI_Common;
using UnityEngine;
using Message.Pet;
using Data.Beans;
using System.Collections.Generic;

public class HuoDeChongWuWindow : BaseWindow
{
    UI_HuoDeChongWuWindow window;
    private ActorUI actor;
    private int petId;
   

    public override void OnOpen()
    {
        window = getUiWindow<UI_HuoDeChongWuWindow>();
        window.m_CloseBtn.onClick.Add(Close);
        InitView();
    }
    public override void InitView()
    {
        base.InitView();
        //播放动画
        window.m_YiHuoDe.visible = true;
        if (Info.param == null)
        {
            Logger.err("未获得对应道具id无法加载模型");
            return;
        }
        OnIsAcquire((int)Info.param);
    }

    private void OnIsAcquire(int itemid)
    {
        PetInfo petInfo = PetService.Singleton.GetPetInfo(itemid);
        if (petInfo == null)
        {
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemid);
            if (itemBean != null)
            {
                if (string.IsNullOrEmpty(itemBean.t_value))
                { }
                else
                {
                    string[] pet = GTools.splitString(itemBean.t_value, ';');
                    string miaoshu = "已拥有该宠物，自动转换为{0}个碎片";
                    string[] number = GTools.splitString(pet[1]);
                    miaoshu = string.Format(miaoshu, number[1]);
                    petId = int.Parse(pet[0]);
                    if (PetService.Singleton.yongyou == false)
                    {
                        //获得新宠物
                        window.m_YiHuoDe.visible = false;
                    }
                    else
                    {
                        //获得已拥有宠物

                        window.m_YiHuoDe.visible = true;
                        window.m_YiHuoDe.text = miaoshu;
                    }
                }
            }
        }
        else
        {
            petId = itemid;
            window.m_YiHuoDe.visible = false;
        }
        
    }
    protected override void OnClose()
    {
        actor = null;
        if (window.m_YiHuoDe.visible == false)
        {
            WinInfo info = new WinInfo();
            info.param = petId;
            WinMgr.Singleton.Open<ChongWuDongHuaWindow>(info,UILayer.Popup);
        }
        else
            GED.ED.dispatchEvent(EventID.OnDrawCardDongHuaClose);
        window = null;
        base.OnClose();
    }
}
