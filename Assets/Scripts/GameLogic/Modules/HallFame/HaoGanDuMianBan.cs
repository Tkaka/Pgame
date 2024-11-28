using UI_HallFame;
using Data.Beans;
using Message.Team;
using FairyGUI;
using System.Collections.Generic;

public class HaoGanDuMianBan
{
    private UI_HaoGanDuMianBan window;
    private t_hof_teamBean fameBean;
    private t_hof_level_upBean favorabilityBean;
    private HofItem hofItem;
    private int petId;
    private int useCateId;

    public HaoGanDuMianBan(UI_HaoGanDuMianBan haogandu)
    {
        window = haogandu;
        AddKeyEvent();
    }
    public void Init(int teamid, int petid)
    {
        fameBean = ConfigBean.GetBean<t_hof_teamBean, int>(teamid);
        if (fameBean == null)
        {
            Logger.err("HaoGanDuMianBan:Init:未能从战队表找到对应战队id，请检查宠物表中---" + petid + "---对应的字段是否正确");
            return;
        }
        //获得好感度信息
        hofItem = HallFameService.Singleton.GetHofItem(petid);
        if (hofItem == null)
        {
            Logger.err("服务器发来的好感度数据为空！");
            NoAcquireData();
        }
        else
        {
            favorabilityBean = ConfigBean.GetBean<t_hof_level_upBean, int>(hofItem.color);
            if (favorabilityBean == null)
            {
                Logger.err("未能获得好感度表中对应数据---" + hofItem.color + "这个id不存在");
                return;
            }
        }
        petId = petid;
        if (hofItem == null)
        { NoAcquireData(); }
        else
        {
            ColorUpBtn();
        }
    }
    //添加事件
    private void AddKeyEvent()
    {
        window.m_HF_xiangqing.onClick.Add(OnOpenGuiZeShuoMing);
        window.m_HF_LaiYuan.onClick.Add(OnOpenLaiYuanWindow);
        window.m_HF_jiacheng.onClick.Add(OnOpenJiaChengWindow);
        window.m_CateList.onClickItem.Add(OnUseCateItem);
        window.m_HF_haogantisheng.onClick.Add(OnColorUp);
    }
    //未获得
    private void NoAcquireData()
    {
        List<t_hof_level_upBean> upBeans = ConfigBean.GetBeanList<t_hof_level_upBean>();
        window.m_HF_xiangqing.visible = false;
        window.m_HF_LaiYuan.visible = true;
        window.m_HF_haogantisheng.visible = false;
        window.m_HF_dengji.m_name.text = "尚未获得（非）";
        window.m_HF_JinDuTiao.max = 0;
        window.m_HF_JiShu.visible = false;
        window.m_HF_JinDuTiao.value = 0;
        window.m_HF_jindutiaotext.text = 0 + "/" + 0;
        window.m_HF_JiShu.m_HF_number.text = upBeans[0].t_level_count.ToString();
        window.m_HF_XianShouZhi.text = "0";
        OnFillCateList(false);
    }
    //判断是否可以升品
    private void ColorUpBtn()
    {
        window.m_HF_haogantisheng.visible = false;
        if (hofItem.level == favorabilityBean.t_level_count)
        { AcquireData(true);}
        else
        { AcquireData(); }
    }
    //已获得
    private void AcquireData(bool tisheng = false)
    {
        window.m_HF_LaiYuan.visible = false;
        if (tisheng)
        {
            window.m_HF_haogantisheng.visible = true;
            window.m_HF_xiangqing.visible = false;
        }
        else
        {
            window.m_HF_xiangqing.visible = true;
            window.m_HF_haogantisheng.visible = false;
        }
        window.m_HF_XianShouZhi.text = hofItem.priority.ToString();
        UIGloader.SetUrl(window.m_HF_dengji.m_HF_HaoGanDuIcon, favorabilityBean.t_icon);
        window.m_HF_dengji.m_name.text = favorabilityBean.t_name;
        window.m_HF_JiShu.text = hofItem.level.ToString();
        //好感度表
        window.m_HF_JiShu.m_HF_number.text = hofItem.level.ToString();

        int expnumber = 0;
        if (hofItem.level == favorabilityBean.t_level_count)
        {
            expnumber = favorabilityBean.t_color_base_exp + ((favorabilityBean.t_exp_add) * ((hofItem.level - 1) / 5));
        }
        else
        {
            expnumber = favorabilityBean.t_color_base_exp + (favorabilityBean.t_exp_add * (hofItem.level / 5));
        }
        window.m_HF_JinDuTiao.max = expnumber;
        window.m_HF_JinDuTiao.value = hofItem.exp;
        if (tisheng)
        {
            window.m_HF_JinDuTiao.max = expnumber;
            window.m_HF_JinDuTiao.value = expnumber;
            window.m_HF_jindutiaotext.text = expnumber.ToString() + "/" + expnumber.ToString();
        }
        else
        {
            window.m_HF_JinDuTiao.max = expnumber;
            window.m_HF_JinDuTiao.value = hofItem.exp;
            window.m_HF_jindutiaotext.text = hofItem.exp.ToString() + "/" + expnumber.ToString();
        }
            
        OnFillCateList(true);
    }
    //规则说明窗口
    private void OnOpenGuiZeShuoMing()
    {
        WinInfo info = new WinInfo();
        WinMgr.Singleton.Open<ExplainWindow>(info, UILayer.Popup);
    }
    //打开来源窗口
    private void OnOpenLaiYuanWindow()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean != null)
        {
            WinInfo info = new WinInfo();
            TwoParam<int, int> twoParam = new TwoParam<int, int>();
            twoParam.value1 = petBean.t_fragment_id;
            twoParam.value2 = -1;
            info.param = twoParam;
            WinMgr.Singleton.Open<LaiYuanWindow>(info, UILayer.Popup);
        }
        else
        {
            Logger.err("HaoGanDuMianBan:OnOpenLaiYuanWindow:传入的宠物id有误！---" + petId);
        }
    }
    //打开属性加成窗口
    private void OnOpenJiaChengWindow()
    {
        WinInfo info = new WinInfo();
        info.param = petId;
        WinMgr.Singleton.Open<HF_ShuXingXianShiWindow>(info,UILayer.Popup);
    }
    //请求好感度提升
    private void OnColorUp()
    {
        HallFameService.Singleton.OnReqHofColorUp(petId);
    }
    /// <summary>
    /// 美食图片加载
    /// </summary>
    /// <param name="acquire"></param>
    private void OnFillCateList(bool acquire)
    {
        HF_MeiShi meiShi;
        window.m_CateList.RemoveChildren(0, -1, true);
        int[] meishiArry = GTools.splitStringToIntArray(fameBean.t_food);
        for (int i = 0; i < meishiArry.Length; ++i)
        {
            meiShi = HF_MeiShi.CreateInstance();
            meiShi.Init(meishiArry[i], acquire);
            window.m_CateList.AddChild(meiShi);
        }
    }
    //使用美食
    private void OnUseCateItem(EventContext context)
    {
        
        HF_MeiShi meiShi = context.data as HF_MeiShi;
        if (meiShi != null)
        {
            if (PetService.Singleton.GetPetInfo(petId) != null)
            {
                if (meiShi.GetNumber() > 0)
                {
                    if (window.m_HF_haogantisheng.visible)
                        TipWindow.Singleton.ShowTip("该宠物好感度已满，可提升等级（非语言包id）");
                    else
                    {
                        useCateId = meiShi.GetItemId();
                        //播放动效
                        //播放完发送
                        int index = window.m_CateList.GetChildIndex(meiShi);
                        switch (index)
                        {
                            case 0:
                                {
                                    UIGloader.SetUrl(window.m_OneCate, meiShi.GetCateIcon());
                                    window.m_One.Play();
                                    window.m_One.SetHook("one", OnUseCate);
                                } break;
                            case 1:
                                {
                                    UIGloader.SetUrl(window.m_TwoCate, meiShi.GetCateIcon());
                                    window.m_Two.Play();
                                    window.m_Two.SetHook("two", OnUseCate);
                                } break;
                            case 2:
                                {
                                    UIGloader.SetUrl(window.m_ThreeCate, meiShi.GetCateIcon());
                                    window.m_Three.Play();
                                    window.m_Three.SetHook("three", OnUseCate);
                                } break;
                        }
                    } 
                }
                else
                    TipWindow.Singleton.ShowTip("没有该美食(非语言包id)");
            }
            else
               TipWindow.Singleton.ShowTip("尚未拥有该宠物（非语言包id）");
        }
    }
    private void OnUseCate()
    {
        HallFameService.Singleton.OnReqHofAddExp(fameBean.t_id, petId, useCateId, 1);
    }

    public void Close()
    {
        window.m_HF_LaiYuan.onClick.Remove(OnOpenLaiYuanWindow);
        window.m_HF_xiangqing.onClick.Remove(OnOpenGuiZeShuoMing);
        fameBean = null;
        favorabilityBean = null;
        hofItem = null;
        if (window != null)
            window = null;
    }
}
