using FairyGUI;
using Message.KingFight;
using System.Collections.Generic;
using UI_StriveHegemong;
using UnityEngine;

public class SH_ZS_NO
{
    private UI_SH_ZS_NO window;
    private List<MainInfo> mainInfos;
    private UIResPack resPacker;
    public SH_ZS_NO(UI_SH_ZS_NO ZS)
    {
        window = ZS;
        resPacker = new UIResPack(this);
        AddKeyEvent();
    }
    private void AddEventListener()
    {
        GED.ED.addListener(EventID.OnStriveHegemongJoin,OnJoin);
    }
    private void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.OnStriveHegemongJoin,OnJoin);
    }
    public void Init()
    {
       
        if (StriveHegemongService.Singleton.maininfo == null || StriveHegemongService.Singleton.maininfo.mainInfo == null)
        {
            Logger.err("SH_ZS_NO:Init:未能获取到主赛场信息数据");
            return;
        }
        mainInfos = StriveHegemongService.Singleton.maininfo.mainInfo;
        OnFillData();
        OnShowModel();
        OnBaoMingBtn();
    }
    private void AddKeyEvent()
    {
        AddEventListener();
        window.m_XiaZhuBtn.onClick.Add(OnOpenXiaZhuWindow);
        window.m_BaoMingBtn.onClick.Add(OnOpenBaoMingWindow);
    }
    private void OnFillData()
    {
       
        switch (mainInfos.Count)
        {
            case 0: { } break;
            case 1:
                {
                    OnFillDataOne();
                    OnFillDataTwo(false);
                    OnFillDataThree(false);
                }
                break;
            case 2:
                {
                    OnFillDataOne();
                    OnFillDataTwo();
                    OnFillDataThree(false);
                }
                break;
            case 3:
                {
                    OnFillDataOne();
                    OnFillDataTwo();
                    OnFillDataThree();
                }
                break;
        }
    }
    private void OnFillDataOne(bool own = true)
    {
        window.m_One.m_PaiMingText.text = "1";
        if (own)
        {
            window.m_One.m_PlayerName.text = mainInfos[0].name;
            window.m_One.m_SheTuanName.text = mainInfos[0].guildName;

            UIGloader.SetUrl(window.m_One.m_PaiMingIcon,"");//固定图标
        }
        else
        {

        }
    }
    private void OnFillDataTwo(bool own = true)
    {
        window.m_Two.m_PaiMingText.text = "2";
        if (own)
        {
            window.m_Two.m_PlayerName.text = mainInfos[1].name;
            window.m_Two.m_SheTuanName.text = mainInfos[1].guildName;

            UIGloader.SetUrl(window.m_Two.m_PaiMingIcon, "");//固定图标
        }
        else
        {

        }
       
    }
    private void OnFillDataThree(bool own = true)
    {
        window.m_Three.m_PaiMingText.text = "3";
        if (own)
        {
            window.m_Three.m_PlayerName.text = mainInfos[2].name;
            window.m_Three.m_SheTuanName.text = mainInfos[2].guildName;

            UIGloader.SetUrl(window.m_Three.m_PaiMingIcon,"");//固定图标
        }
       
    }
    //报名按钮状态设置
    private void OnBaoMingBtn()
    {
        if (StriveHegemongService.Singleton.isjoin)
        {
            window.m_RenShu.text = StriveHegemongService.Singleton.maininfo.num + "/" +"1000";
            window.m_ShiJian.text = "";
            if (StriveHegemongService.Singleton.join)
            {
                window.m_BaoMingBtn.grayed = true;
                window.m_BaoMingBtn.touchable = false;
                window.m_BaoMingBtn.m_baoming.visible = false;
                window.m_BaoMingBtn.m_yibaoming.visible = true;
            }
            else
            {
                window.m_BaoMingBtn.grayed = false;
                window.m_BaoMingBtn.touchable = true;
                window.m_BaoMingBtn.m_yibaoming.visible = false;
                window.m_BaoMingBtn.m_baoming.visible = true;
            }
        }
        else
        {
            window.m_RenShu.visible = false;
            window.m_ShiJian.visible = false;
            window.m_BaoMingBtn.visible = false;
        }
    }
    private void OnOpenXiaZhuWindow()
    {
        WinInfo info = new WinInfo();
        WinMgr.Singleton.Open<SH_XiaZhuWindow>(info,UILayer.Popup);
    }
    private void OnOpenBaoMingWindow()
    {
        WinInfo info = new WinInfo();
        WinMgr.Singleton.Open<SH_ApplyWindow>(info,UILayer.Popup);
    }
    /// <summary>
    /// 接收到已报名的消息
    /// </summary>
    private void OnJoin(GameEvent evt)
    {
        OnBaoMingBtn();
    }
    private void OnShowModel()
    {
        
        switch (mainInfos.Count)
        {
            case 0: { }break;
            case 1:
                {
                    OnShowModeOne();
                    OnShowModeTwo(false);
                    OnShowModeThree(false);
                } break;
            case 2:
                {
                    OnShowModeOne();
                    OnShowModeTwo();
                    OnShowModeThree(false);
                } break;
            case 3:
                {
                    OnShowModeOne();
                    OnShowModeTwo();
                    OnShowModeThree();
                } break;
        }
    }
    private void OnShowModeOne(bool own = true)
    {
        if (own)
        {
            GoWrapper one = OnShowPetModel(mainInfos[0].petBaseInfos[0].id, 300);
            window.m_One.m_One.SetNativeObject(one);
            //one.setLayer("UIActor");
            if (mainInfos[0].petBaseInfos.Count > 1)
            {
                GoWrapper two = OnShowPetModel(mainInfos[0].petBaseInfos[1].id, 400);
                window.m_One.m_Two.SetNativeObject(two);
                //two.setLayer("UIActor");
            }
            if (mainInfos[0].petBaseInfos.Count > 2)
            {
                GoWrapper three = OnShowPetModel(mainInfos[0].petBaseInfos[2].id, 500);
                window.m_One.m_Three.SetNativeObject(three);
                //three.setLayer("UIActor");
            }
        }
        else
        {
            window.m_One.m_Two.visible = false;
            window.m_One.m_One.visible = false;
            window.m_One.m_Three.visible = false;
        }
    }
    private void OnShowModeTwo(bool own = true)
    {
        if (own)
        {
            GoWrapper one = OnShowPetModel(mainInfos[1].petBaseInfos[0].id, 300);
            window.m_Two.m_One.SetNativeObject(one);
           // one.setLayer("UIActor");
            if (mainInfos[1].petBaseInfos.Count > 1)
            {
                GoWrapper two = OnShowPetModel(mainInfos[1].petBaseInfos[1].id, 400);
                window.m_Two.m_Two.SetNativeObject(two);
                //two.setLayer("UIActor");
            }
            if (mainInfos[1].petBaseInfos.Count > 2)
            {
                GoWrapper three = OnShowPetModel(mainInfos[1].petBaseInfos[2].id, 500);
                window.m_Two.m_Three.SetNativeObject(three);
                //three.setLayer("UIActor");
            }
        }
        else
        {
            window.m_Two.m_One.visible = false;
            window.m_Two.m_Two.visible = false;
            window.m_Two.m_Three.visible = false;
        }
    }
    private void OnShowModeThree(bool own = true)
    {
        if (own)
        {
            GoWrapper one = OnShowPetModel(mainInfos[2].petBaseInfos[0].id, 300);
            window.m_Three.m_One.SetNativeObject(one);
            //one.setLayer("UIActor");
            if (mainInfos[2].petBaseInfos.Count > 1)
            {
                GoWrapper two = OnShowPetModel(mainInfos[2].petBaseInfos[1].id, 400);
                window.m_Three.m_Two.SetNativeObject(two);
                //two.setLayer("UIActor");
            }
            if (mainInfos[2].petBaseInfos.Count > 2)
            {
                GoWrapper three = OnShowPetModel(mainInfos[2].petBaseInfos[2].id, 500);
                window.m_Three.m_Three.SetNativeObject(three);
                //three.setLayer("UIActor");
            }
        }
        else
        {
            window.m_Three.m_One.visible = false;
            window.m_Three.m_Two.visible = false;
            window.m_Three.m_Three.visible = false;
        }
    }
    private GoWrapper OnShowPetModel(int petId,int zZhou)
    {
        //GameObject game = Res.Singleton.InstantiateModel(UIUtils.GetPetStartModel(petId));
       Vector3 pos = new Vector3(0,0, zZhou);
        //game.transform.localScale = new Vector3(100,100,100);
        //game.transform.localEulerAngles = new Vector3(0,180,0);

        var wrapper = new GoWrapper();
        var actor = resPacker.NewActorUI(petId, ActorType.Pet,wrapper);
        actor.initialize(ActorParam.create(Vector3.zero, Vector3.zero));
        actor.SetTransform(pos, 100);

        return wrapper;
    }
    public void Close()
    {
        RemoveEventListener();
    }
}
