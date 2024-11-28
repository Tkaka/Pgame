using UI_GeDouJia;
using Data.Beans;
using System.Collections.Generic;

public enum LaiYuanType
{
    Prop = 1,          // 道具
    Currency = 2,      // 货币
}

public class LaiYuanWindow : BaseWindow
{
    private UI_LaiYuanWindow window;
    //来源表
    private t_itemBean itemBeam;
    List<int> cishu = new List<int>();
    //宠物ID
    private int petid;
    private int type;//当前是1、主线关卡2、精英关卡

    private int curCiShu;
    private int xuqiushuliang;//需求数量
    private int guankashu;//关卡获得的关卡数量
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_LaiYuanWindow>();
        AddKeyEvent();
        InitView();
        PlayPopupAnim(window.m_mask, window.m_popupView);
    }

    private void AddKeyEvent()
    {
        window.m_popupView.m_Close.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_popupView.m_CiShuXuanZe.onChanged.Add(OnChange);

    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnCloseLaiYuan, OnGuanBi);
        GED.ED.addListener(EventID.ResBagUpdate, OnGengXingYongYou);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnCloseLaiYuan, OnGuanBi);
        GED.ED.removeListener(EventID.ResBagUpdate, OnGengXingYongYou);
    }
    public override void InitView()
    {
        base.InitView();

        //根据打开窗口时传进来的道具id打开道具表读取数据
        if (Info.param != null)
        {
            TwoParam<int, int> twoParam;//v1是id，v2需求数量
            twoParam = Info.param as TwoParam<int, int>;
            if (twoParam != null)
            {
                int itemid = twoParam.value1;
                xuqiushuliang = twoParam.value2;
                itemBeam = ConfigBean.GetBean<t_itemBean, int>(itemid);
                if (GPlayerPrefs.HasKey("SaoDangCiShu"))
                {
                    curCiShu = GPlayerPrefs.GetInt("SaoDangCiShu");
                }
                else
                    curCiShu = 1;
                guankashu = 0;
                FillData();
                OnLaiYuanList();
                OnXuanZeList();
            }
        }
        else
        {
            Logger.err("未传入道具Id，无法打开配置表");
            return;
        }
    }
    private void OnGengXingYongYou(GameEvent evt)
    {
        FillData();
    }
    private void FillData()
    {
        int yongyou = BagService.Singleton.GetItemNum(itemBeam.t_id);
        if (xuqiushuliang > 0)
        {
            window.m_popupView.m_number.text = yongyou + "/" + xuqiushuliang;
        }
        else
        {
            window.m_popupView.m_number.visible = false;
            window.m_popupView.m_number.text = yongyou + "";
        }
        if (itemBeam.t_type != 5)
        {
            window.m_popupView.m_type.visible = false;
        }
        else
        {
            window.m_popupView.m_type.visible = true;
        }
        window.m_popupView.m_Name.text = itemBeam.t_name;
        window.m_popupView.m_Name.color = UIUtils.GetItemColor(itemBeam.t_id);
        UIGloader.SetUrl(window.m_popupView.m_TouXiang, itemBeam.t_icon);
        UIGloader.SetUrl(window.m_popupView.m_PinJie, UIUtils.GetItemBorder(itemBeam.t_id));
    }
    /// <summary>
    /// 扫荡次数下拉条选择
    /// </summary>
    private void OnXuanZeList()
    {
        int vip = RoleService.Singleton.RoleInfo.roleInfo.vip;
        int level = RoleService.Singleton.RoleInfo.roleInfo.level;
        if (type == 1)
        {
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1901001);
            if (globalBean != null)
            {
                string[] dengjixianzhi = GTools.splitString(globalBean.t_string_param, ';');
                for (int i = 0; i < dengjixianzhi.Length; ++i)
                {
                    int[] dengji = GTools.splitStringToIntArray(dengjixianzhi[i]);
                    if (level >= dengji[0])
                    {
                        cishu.Add(dengji[1]);
                    }
                }
            }
            List<t_vipBean> vipBeans = ConfigBean.GetBeanList<t_vipBean>();
            for (int i = 0; i <= vip; ++i)
            {
                if (cishu[cishu.Count - 1] < vipBeans[i].t_sd)
                {
                    cishu.Add(vipBeans[i].t_sd);
                }
            }
        }
        else if (type == 2)
        {
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean,int>(1901002);
            if (globalBean != null)
            {
                string[] dengjixianzhi = GTools.splitString(globalBean.t_string_param,';');
                if (dengjixianzhi != null)
                {
                    for (int i = 0; i < dengjixianzhi.Length; ++i)
                    {
                        int[] dengji = GTools.splitStringToIntArray(dengjixianzhi[i]);
                        if (level >= dengji[0])
                        { cishu.Add(dengji[1]); }
                    }
                }
            }
        }
        string[] names = new string[cishu.Count];
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(71901010);
        if (languageBean != null)
        {
            for (int i = 0; i < names.Length; ++i)
            {
                names[i] = string.Format(languageBean.t_content, cishu[i].ToString());
            }
        }
        window.m_popupView.m_CiShuXuanZe.items = names;
        OnBiaoTi();
    }
    private void OnGuanBi(GameEvent evt)
    {
        OnCloseBtn();
    }
    private void OnBiaoTi()
    {
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(71901008);
        if (languageBean != null)
        {
            string miaoshu = string.Format(languageBean.t_content, curCiShu);
            window.m_popupView.m_CiShuXuanZe.title = miaoshu;
        }
    }
    private void OnChange()
    {
        curCiShu = cishu[window.m_popupView.m_CiShuXuanZe.selectedIndex];
        GPlayerPrefs.SetInt("SaoDangCiShu", curCiShu);
        OnQieHuan();
    }
    private void OnQieHuan()
    {
        GuanKaListItem listItem;
        for (int i = 0; i < guankashu; ++i)
        {
            listItem = window.m_popupView.m_GuanKaList.GetChildAt(i) as GuanKaListItem;
            if (listItem != null)
            {
                listItem.OnAnJianWenZi(curCiShu);
            }
        }
        OnBiaoTi();
    }
    private void OnLaiYuanList()
    {
        window.m_popupView.m_JingQingQiDai.visible = false;
        if (string.IsNullOrEmpty(itemBeam.t_source))
        {
           //来源关卡为空，
        }
        else
        {
            int[] laiyuan = GTools.splitStringToIntArray(itemBeam.t_source);
            guankashu = laiyuan.Length;
            GuanKaListItem listItem;
            for (int i = 0; i < laiyuan.Length; ++i)
            {
                listItem = GuanKaListItem.CreateInstance();
                listItem.Init(laiyuan[i], curCiShu);
                window.m_popupView.m_GuanKaList.AddChild(listItem);
            }
            t_dungeon_actBean actBean;
            for (int i = 0; i < laiyuan.Length; ++i)
            {
                actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(UIUtils.GetLaiYuanActID(laiyuan[i]));
                if (actBean != null)
                {
                    if (actBean.t_act_type != 0)
                    {
                        type = actBean.t_act_type;
                    }
                }
            }
        }
        if (string.IsNullOrEmpty(itemBeam.t_shop_source))
        {
            //来源商店字段为空
        }
        else
        {
            string[] laiyuan = GTools.splitString(itemBeam.t_shop_source,';');
            ShangDianHeWanFaListItem listItem;
            for (int i = 0; i < laiyuan.Length; ++i)
            {
                int[] item = GTools.splitStringToIntArray(laiyuan[i]);
                if (item.Length > 1)
                {
                    listItem = ShangDianHeWanFaListItem.CreateInstance();
                    listItem.Init(item[0], item[1],itemBeam.t_use_jump_ly);
                    window.m_popupView.m_GuanKaList.AddChild(listItem);
                }
                else
                    continue;
            }
        }
        if (window.m_popupView.m_GuanKaList.numItems == 0)
        {
            //没有来源，敬请期待
            window.m_popupView.m_JingQingQiDai.visible = true;
            window.m_popupView.m_CiShuXuanZe.visible = false;
        }
    }
    protected override void OnCloseBtn()
    {
        cishu = null;
        itemBeam = null;
        base.OnCloseBtn();
    }
}
