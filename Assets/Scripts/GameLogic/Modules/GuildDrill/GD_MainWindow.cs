using UI_GuildDrill;
using Data.Beans;
using System.Collections.Generic;
using FairyGUI;
using Message.Guild;


public class GD_MainWindow : BaseWindow
{
    private UI_GD_MainWindow window;
    private GD_GameDataManger manger;
    private DoActionInterval doAction;
    private int time;
    private List<t_exphomeBean> exphomeBeans;
    private int[] tongzhi = { 71601034,71601035 };//最上部广告条语言包id
    private bool xianshi;//用于最上部广告条
    List<int> xunlianindex;//已显示的训练位的Id

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_GD_MainWindow>();
        manger = new GD_GameDataManger();
        exphomeBeans = ConfigBean.GetBeanList<t_exphomeBean>();
        GuildService.Singleton.ReqOpenExpHome();
        GuildService.Singleton.ReqExpHomeRoleList();
        doAction = new DoActionInterval();
        OnXunLianIndex();
        AddKeyEvent();
        doAction.doAction(1,OnDaoJiShi);
        xianshi = false;
        time = 10;
    }

    private void TestData()
    {
        InitView();
    }
    public override void InitView()
    {
        base.InitView();
        RefreshView();
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnGuildDrillOpenMainWin, OnGuildDrillOpenMainWin);
        GED.ED.addListener(EventID.OnGuildDrillChangePetWindow, OnOpenChangeWindow);
        GED.ED.addListener(EventID.OnGuildDrillAffirmChange, OnSetExpPet);
        GED.ED.addListener(EventID.OnGuildDrillOpenNewPlace, OnOpenNewPlace);
        GED.ED.addListener(EventID.OnGuildDrillTaRenJiaSuFanHui, OnTaRenJiaSuFanHui);
        GED.ED.addListener(EventID.OnGuildDrillExpediteRole,OnSleepTherr);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnGuildDrillOpenMainWin, OnGuildDrillOpenMainWin);
        GED.ED.removeListener(EventID.OnGuildDrillChangePetWindow, OnOpenChangeWindow);
        GED.ED.removeListener(EventID.OnGuildDrillAffirmChange, OnSetExpPet);
        GED.ED.removeListener(EventID.OnGuildDrillOpenNewPlace, OnOpenNewPlace);
        GED.ED.removeListener(EventID.OnGuildDrillTaRenJiaSuFanHui, OnTaRenJiaSuFanHui);
        GED.ED.removeListener(EventID.OnGuildDrillExpediteRole, OnSleepTherr);
    }
    private void AddKeyEvent()
    {
        window.m_XunLianWeiList.m_XunLianWeiList.SetVirtual();
        window.m_XunLianWeiList.m_XunLianWeiList.itemRenderer = RenderListItem;
        window.m_XunLianWeiList.m_XunLianWeiList.numItems = exphomeBeans.Count;
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        window.m_type.onChanged.Add(OnTypeChange);
        window.m_jiasuList.onClickItem.Add(OnItem);
        window.m_suijiyici.onClick.Add(OnSuiJiYiCi);
        window.m_suijiquanbu.onClick.Add(OnSuiJiQuanBu);
        window.m_lastBtn.onClick.Add(OnLastBtn);
        window.m_nextBtn.onClick.Add(OnNextBtn);
        window.m_AnNiu.Play();
    }
    private void OnSuiJiYiCi()
    {
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(1602010);
        if (bean == null)
        {
            Logger.err("公会训练所被加速次数上限没有---" + 1602010);
        }
        else
            window.m_beidongjiashucishu.text = (bean.t_int_param - manger.roleList.beQuickNum).ToString();
        if (manger.roleList.quickNum >= bean.t_int_param)
        {
            TipWindow.Singleton.ShowTip("加速次数已达上限");
            return;
        }
        GuildService.Singleton.ReqRandomQuick(true);
    }
    private void OnLastBtn()
    {
        OnXianShiBtn(true);
    }
    private void OnNextBtn()
    {
        OnXianShiBtn(false);
    }
    private void OnXianShiBtn(bool shang)//上为真
    {
        GD_XunLianWei xunlianwei = window.m_XunLianWeiList.m_XunLianWeiList.GetChildAt(1) as GD_XunLianWei;
        int with = (int)(window.m_XunLianWeiList.width / xunlianwei.width);
        int dangqian = window.m_XunLianWeiList.m_XunLianWeiList.ChildIndexToItemIndex(1);
        int index = window.m_XunLianWeiList.m_XunLianWeiList.ChildIndexToItemIndex(1);
       
        if (shang)
        {
            if (index <= with)
            {
                window.m_XunLianWeiList.m_XunLianWeiList.ScrollToView(0,true);
            }
            else
            {
                window.m_XunLianWeiList.m_XunLianWeiList.ScrollToView(dangqian - with,true);
            }
        }
        else
        {
            if (index >= exphomeBeans.Count - (with * 2))
            {
                window.m_XunLianWeiList.m_XunLianWeiList.ScrollToView(exphomeBeans.Count - 1,true);
            }
            else
            {
                window.m_XunLianWeiList.m_XunLianWeiList.ScrollToView(dangqian + with,true);
            }
        }
    }
    private void OnSuiJiQuanBu()
    {
        
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(1602010);
        if (bean == null)
        {
            Logger.err("公会训练所被加速次数上限没有---" + 1602010);
        }
        else
            window.m_beidongjiashucishu.text = (bean.t_int_param - manger.roleList.beQuickNum).ToString();
        if (manger.roleList.quickNum >= bean.t_int_param)
        {
            TipWindow.Singleton.ShowTip("加速次数已达上限");
            return;
        }
        GuildService.Singleton.ReqRandomQuick(false);
        OnJiaSuZhiHui(false);
    }
    public override void RefreshView()
    {
        base.RefreshView();
        OnMyCampsite();
    }
    private void OnGuildDrillOpenMainWin(GameEvent evt)
    {
        InitView();
    }
    private void OnItem()
    {
        Logger.err("Item相应");
    }

    private void OnMyCampsite()//我的营地
    {
        //刷新列表
        OnXunLianIndex();
        window.m_XunLianWeiList.m_XunLianWeiList.RefreshVirtualList();
    }
    private void FillData()
    {
        t_globalBean bean = ConfigBean.GetBean<t_globalBean,int>(1602009);
        if (bean == null)
        {
            Logger.err("公会训练所加速次数上限没有---" + 1602009);
        }
        else
        {
            window.m_jiasucishu.text = (bean.t_int_param - manger.roleList.quickNum).ToString();
            if (bean.t_int_param - manger.roleList.quickNum <= 0)
                OnJiaSuZhiHui(false);

        }
        bean = ConfigBean.GetBean<t_globalBean,int>(1602010);
        if (bean == null)
        {
            Logger.err("公会训练所被加速次数上限没有---" + 1602009);
        }
        else
            window.m_beidongjiashucishu.text = (bean.t_int_param - manger.roleList.beQuickNum).ToString();
        window.m_jiasurizhiList.RemoveChildren(0, -1, true);

        GD_JiaSuRiZhiListItem listItem;
        for (int i = 0; i < manger.roleList.log.Count; ++i)
        {
            listItem = GD_JiaSuRiZhiListItem.CreateInstance();
            listItem.Init(manger.roleList.log[i]);
            window.m_jiasurizhiList.AddChild(listItem);
        }
    }
    private void OnTherrCampsite()
    {
        
        //加速日志
        bool jiasu = true;
        if (manger.roleList.quickNum >= 6)
            jiasu = false;
        window.m_jiasuList.RemoveChildren(0,-1,true);
        GD_JiaSuiListItem rolelistitem;
        for (int i = 0; i < manger.roleList.roleList.Count; ++i)
        {
            rolelistitem = GD_JiaSuiListItem.CreateInstance();
            rolelistitem.Init(manger.roleList.roleList[i],jiasu);
            window.m_jiasuList.AddChild(rolelistitem);
        }
        FillData();
    }
    private void RenderListItem(int index, GObject obj)
    {
        int xiabiao = window.m_XunLianWeiList.m_XunLianWeiList.ChildIndexToItemIndex(1);
        if (xiabiao > 1)
        {
            window.m_last.visible = true;
        }
        else
        {
            window.m_last.visible = false;
        }
        
        GD_XunLianWei list_Item = obj as GD_XunLianWei;
        int with = (int)(window.m_XunLianWeiList.width / list_Item.width);
        if ((xiabiao + with) < exphomeBeans.Count - 1)
        {
            window.m_next.visible = true;
        }
        else
        {
            window.m_next.visible = false;
        }
        if (index < manger.mydrill.pets.Count)
        {
            list_Item.Init(true, xunlianindex[index], manger.mydrill.pets[index].petId);
        }
        else
        {
            list_Item.Init(false, xunlianindex[index]);
        }
    }
    private void OnOpenChangeWindow(GameEvent evt)
    {
        TwoParam<GD_GameDataManger, int> param = new TwoParam<GD_GameDataManger, int>();
        param.value1 = manger;
        param.value2 = (int)evt.Data;
        WinInfo info = new WinInfo();
        info.param = param;
        WinMgr.Singleton.Open<GD_XuanZeWindow>(info,UILayer.Popup);
    }
    /// <summary>
    /// 加速刷新
    /// </summary>
    /// <param name="evt"></param>
    private void OnTaRenJiaSuFanHui(GameEvent evt)
    {
        FillData();
    }
    private void OnSetExpPet(GameEvent evt)
    {
        //第一个是下标，第二个是宠物id;
        TwoParam<int, int> twoParam = evt.Data as TwoParam<int, int>;
        if (twoParam == null)
        {
            Logger.err("GD_MainWindow:OnSetExpPet:类型转换失败，无法替换宠物");
            return;
        }
        int index = window.m_XunLianWeiList.m_XunLianWeiList.ItemIndexToChildIndex(twoParam.value1 - 1);
        GD_XunLianWei xunlian = window.m_XunLianWeiList.GetChildAt(index) as GD_XunLianWei;
        if (xunlian == null)
        {
            Logger.err("GD_MainWindow:OnSetExpPet:类型转换失败，无法替换宠物");
            return;
        }
        xunlian.OnChangeModel(twoParam.value2);
        RefreshView();
    }
    /// <summary>
    /// 接收消息，重置状态,加速按钮
    /// </summary>
    private void OnJiaSuZhiHui(bool zhuangtai)
    {
        GD_JiaSuiListItem rolelistitem;
        for (int i = 0; i < window.m_jiasuList.numChildren; ++i)
        {
            rolelistitem = window.m_jiasuList.GetChildAt(i) as GD_JiaSuiListItem;
            if (rolelistitem != null)
            {
                rolelistitem.RefreshView(zhuangtai);
            }
        }
    }
    //开通训练位
    private void OnOpenNewPlace(GameEvent evt)
    {
        if (evt != null)
        {
            GD_XunLianWei rolelistitem;
            int index = window.m_XunLianWeiList.m_XunLianWeiList.ItemIndexToChildIndex((int)evt.Data - 1);
            PosPet posPet = new PosPet();
            posPet.id = (int)evt.Data;
            posPet.petId = -1;

            manger.mydrill.pets.Add(posPet);
        }
        else
        {
            Logger.err("游戏内消息未传参：OnGuildDrillOpenNewPlace");
        }
        window.m_XunLianWeiList.m_XunLianWeiList.RefreshVirtualList();
        OnXunLianIndex();
    }
    private void OnTypeChange()
    {
        if (window.m_type.selectedIndex == 0)
        {
            OnMyCampsite();
        }
        else if(window.m_type.selectedIndex == 1)
        {
            OnTherrCampsite();
        }
    }
    //为别人加速返回的消息，刷新界面
    private void OnSleepTherr(GameEvent evt)
    {
        OnTherrCampsite();
    }
    //最上部广告条
    private void OnDaoJiShi(object obj)
    {
        if(time <= 0)
        {
            time = 12;
        }
        if (time == 13)
        {
            //System.Random random = new System.Random();
            //int index = random.Next(0, tongzhi.Length - 1);
            int index = 0;
            if (xianshi)
            { index = 1; xianshi = !xianshi; }
            else
            { index = 0;xianshi = !xianshi; }
            t_languageBean bean = ConfigBean.GetBean<t_languageBean, int>(tongzhi[index]);
            if (bean == null)
            {
                Logger.err("语言包中没有对应语言" + tongzhi[index]);
                return;
            }
            window.m_gundong.text = bean.t_content;
            window.m_WenZiTiShi.Play();
        }
        time--;
    }
    private void OnXunLianIndex()
    {
        if (xunlianindex == null)
        {
            xunlianindex = new List<int>();
        }
        xunlianindex.Clear();
        for (int i = 0; i < manger.mydrill.pets.Count; ++i)
        {
            xunlianindex.Add(manger.mydrill.pets[i].id);
        }
        for (int i = 0; i < exphomeBeans.Count; ++i)
        {
            bool tianjia = true;
            for (int j = 0; j < manger.mydrill.pets.Count; ++j)
            {
                if (exphomeBeans[i].t_id == manger.mydrill.pets[j].id)
                {
                    tianjia = false;
                    break;
                }
            }
            if (tianjia)
            {
                xunlianindex.Add(exphomeBeans[i].t_id);
            }
        }
    }
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        xunlianindex = null;
        window = null;
        if (manger != null)
        {
            manger.Close();
            manger = null;
        }
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        base.OnCloseBtn();
    }
}
