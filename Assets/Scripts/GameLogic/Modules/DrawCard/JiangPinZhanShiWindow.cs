using UI_DrawCard;
using Data.Beans;
using Message.DrawCard;
using System.Collections.Generic;
using FairyGUI;

public class JiangPinZhanShiWindow : BaseWindow
{
    private UI_JiangPinZhanShiWindow window;
    private List<ItemInfo> itemInfos;
    private int num;//计数
    private DoActionInterval DoAction;
    private DrawInfo drawInfo;
    private JiangPingListItem listItem;
    private float intervalTime;//协程间隔时间
    private float dongxiaoTime;//动效间隔时间
    private int time = 2;

    private ResDraw m_resDraw;

    public override void OnOpen()
    {
        window = getUiWindow<UI_JiangPinZhanShiWindow>();
        AddKeyEvent();
        InitView();
    }
    private void AddKeyEvent()
    {
        window.m_QueDingBtn.onClick.Add(OnQueDing);
        window.m_ZaiLaiYiCiBtn.onClick.Add(OnZaiLaiYiCi);
        window.m_BeiJingChuFa.onClick.Add(OnBeiJinChuFa);
        AddEventListener();
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnDrawCardDongHuaClose, OnDongHuaClose);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnDrawCardDongHuaClose, OnDongHuaClose);
    }
    public override void InitView()
    {
        if (Info.param == null)
        {
            Logger.err("未能获得抽奖数据");
            return;
        }
        TwoParam<int, ResDraw> twoParam = Info.param as TwoParam<int, ResDraw>;

        m_resDraw = (ResDraw)twoParam.value2;
        int type = twoParam.value1;
        if (type == 1)
            UIGloader.SetUrl(window.m_priceLoader, UIUtils.GetBuyGoodsPriceIcon((int)ItemType.Gold));
        else if (type == 2)
            UIGloader.SetUrl(window.m_priceLoader, UIUtils.GetBuyGoodsPriceIcon((int)ItemType.Damond));
        window.m_priceLoader.visible = false;

        drawInfo = m_resDraw.info;
        itemInfos = m_resDraw.items;
        if (itemInfos.Count == 0)
        {
            Logger.err("JiangPingZhanShiWindow:InitView:奖品列表长度不正确！");
            return;
        }
        if (itemInfos.Count == 10)
        {
            window.m_JiangPinList.touchable = false;
        }
        num = 0;
        intervalTime = 0.17f;
        dongxiaoTime = 0.5f;
        Init();
    }
    private void Init()
    {
        window.m_QueDingBtn.visible = false;
        window.m_ZaiLaiYiCiBtn.visible = false;
        window.m_ShengYu.visible = false;
        window.m_JiaGe.visible = false;
        window.m_ShiLianChou.visible = false;
        window.m_DanCiChou.visible = false;
        window.m_BaiLianChou.visible = false;
        window.m_priceLoader.visible = false;
        DoAction = new DoActionInterval();
        DoAction.doAction(intervalTime, LoadTrophy, null, true);
        OnJieShu();
        if (drawInfo.numInfo.num == 1)
        {
            window.m_DanCiChou.visible = true;
            window.m_DanChou.Play(OnJieShu);
        }
        else if (drawInfo.numInfo.num == 10)
        {
            window.m_ShiLianChou.visible = true;
            window.m_FaPai.timeScale = dongxiaoTime;
            window.m_FaPai.Play(OnJieShu);
        }
        FillData();
    }
    private void FillData()
    {
        t_draw_cardBean draw_CardBean = ConfigBean.GetBean<t_draw_cardBean, int>(drawInfo.type * 10000 + drawInfo.numInfo.num);
        if (draw_CardBean == null)
        { Logger.err("未能获取对应数据"); }
        else
        {
            if (drawInfo.type == 1)
            {
                window.m_ShengYu.text = "今日剩余次数" + (draw_CardBean.t_sum_num - drawInfo.numInfo.buyNum).ToString() + "/" + draw_CardBean.t_sum_num.ToString();
            }
            if (drawInfo.numInfo.hasHalfPrice())
            {
                if (drawInfo.numInfo.halfPrice)
                {
                    window.m_JiaGe.text = (draw_CardBean.t_price / 2).ToString();
                }
                else
                    window.m_JiaGe.text = draw_CardBean.t_price.ToString();
            }
            else
                window.m_JiaGe.text = draw_CardBean.t_price.ToString();
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(7120101);
            t_globalBean globalBean;
            t_itemBean itemBean;
            if (languageBean != null)
            {
                if (drawInfo.type == 1)
                {
                    globalBean = ConfigBean.GetBean<t_globalBean, int>(120101);
                    if (globalBean != null)
                    {
                        itemBean = ConfigBean.GetBean<t_itemBean,int>(globalBean.t_int_param);
                        if (itemBean == null)
                        {
                            window.m_BiaoTi.text = "未能读取到道具表道具数据";
                            return;
                        }
                        window.m_BiaoTi.text = string.Format(languageBean.t_content, itemBean.t_name, drawInfo.numInfo.num.ToString());
                    }
                    else
                    {
                        window.m_BiaoTi.text = "未能读取到全局表金币购买道具类型数据";
                    }
                }
                else if (drawInfo.type == 2)
                {
                    globalBean = ConfigBean.GetBean<t_globalBean, int>(120102);
                    if (globalBean != null)
                    {
                        itemBean = ConfigBean.GetBean<t_itemBean, int>(globalBean.t_int_param);
                        if (itemBean == null)
                        {
                            window.m_BiaoTi.text = "未能读取到道具表道具数据";
                            return;
                        }
                        window.m_BiaoTi.text = string.Format(languageBean.t_content, itemBean.t_name, drawInfo.numInfo.num.ToString());
                    }
                    else
                    {
                        window.m_BiaoTi.text = "未能读取到全局表钻石购买道具类型数据";
                    }
                }
            }
            else
            { window.m_BiaoTi.text = "未能读取到语言包数据"; }
        }
    }
    public override void RefreshView()
    {
        base.RefreshView();
    }
    private void OnQueDing()
    {
        OnCloseBtn();
        GED.ED.dispatchEvent(EventID.OnDrawCardEndZhanShi);
    }
    protected override void OnCloseBtn()
    {
        if (DoAction != null)
        {
            DoAction.kill();
            DoAction = null;
        }
        window.m_JiangPinList.RemoveChildren(0, -1, true);
        window = null;
        RemoveEventListener();
        base.OnCloseBtn();
    }
    private void OnZaiLaiYiCi()
    {
        t_draw_cardBean cardBean = ConfigBean.GetBean<t_draw_cardBean,int>(drawInfo.type * 10000 + drawInfo.numInfo.num);
        if (cardBean != null)
        {
            if (drawInfo.type == 1)
            {
                //判断代币数量是否足够
                long number = RoleService.Singleton.RoleInfo.roleInfo.gold;
                if (cardBean.t_price > number)
                {
                    TipWindow.Singleton.ShowTip("金币数量不足");
                    return;
                }
                if (drawInfo.numInfo.buyNum > cardBean.t_sum_num)
                {
                    TipWindow.Singleton.ShowTip("抽取次数不足");
                    return;
                }
                //判断抽奖次数是否足够
            }
            else if (drawInfo.type == 2)
            {
                //钻石数量是否足够
                int number = RoleService.Singleton.RoleInfo.roleInfo.damond;
                if (cardBean.t_price > number)
                { //判断是否有抽奖券
                    t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(12020);
                    if (globalBean == null)
                    {
                        Logger.err("MoshiMianBan:OnZuanShiDanChou:未能从全局表获得钻石抽奖券id数据");
                    }
                    else
                    {
                        int quanNumber = BagService.Singleton.GetItemNum(globalBean.t_int_param);
                        if (quanNumber <= 0)
                        { return; }
                    }
                    TipWindow.Singleton.ShowTip("钻石数量不足");
                    return;
                }
               
            }
            window.m_all.visible = false;

            OnQingQiu(null);
            //GED.ED.addListenerOnce(EventID.OnDrawCardEndAnimition, OnQingQiu);
            //GED.ED.dispatchEvent(EventID.OnDrawCard);
        }
        
    }
    private void OnQingQiu(GameEvent evt)
    {
        bool useticket = false;
        if (drawInfo.type == 2)
        {
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(12020);
            if (globalBean == null)
            {
                Logger.err("MoshiMianBan:OnZuanShiDanChou:未能从全局表获得钻石抽奖券id数据");
            }
            else
            {
                int quanNumber = BagService.Singleton.GetItemNum(globalBean.t_int_param);
                if (quanNumber > 0)
                { useticket = true; }
            }
        }
        DrawCardService.Singleton.OnReqDraw(drawInfo.type, drawInfo.numInfo.num, false, useticket);
        OnCloseBtn();
    }
    private void LoadTrophy(object obj)
    {
        //int a = (int)obj;
        //Debuger.Log("----------------------->>>>>>" + a);
        if (drawInfo.numInfo.num == 1)
        {
            time = -1;
        }
        else
        {
            time--;
        }
        if (time < 0)
        {
            if (drawInfo.numInfo.num == 1)
            {
                if (num == 3)
                {
                    string miaoshu = "再来{0}次";
                    window.m_BeiJingChuFa.onClick.Remove(OnBeiJinChuFa);
                    window.m_BeiJingChuFa.touchable = false;
                    if(DoAction != null)
                        DoAction.kill();
                    //DoAction = null;
                    window.m_ZaiLaiYiCiBtn.visible = true;
                    window.m_ZaiLaiYiCiBtn.m_miaoshu.text = string.Format(miaoshu, drawInfo.numInfo.num);
                    window.m_QueDingBtn.visible = true;
                    window.m_ShengYu.visible = true;
                    window.m_JiaGe.visible = true;

                    window.m_priceLoader.visible = true;

                    return;
                }
            }
            else
            {
                if (num == itemInfos.Count)
                {
                    string miaoshu = "再来{0}次";
                    window.m_BeiJingChuFa.onClick.Remove(OnBeiJinChuFa);
                    window.m_BeiJingChuFa.touchable = false;
                    if(DoAction != null)
                        DoAction.kill();
                    DoAction = null;
                    window.m_ZaiLaiYiCiBtn.visible = true;
                    window.m_ZaiLaiYiCiBtn.m_miaoshu.text = string.Format(miaoshu, drawInfo.numInfo.num);
                    window.m_QueDingBtn.visible = true;
                    window.m_ShengYu.visible = true;
                    window.m_JiaGe.visible = true;

                    window.m_priceLoader.visible = true;
                    return;
                }
            }
            Margin margin = new Margin();
            listItem = JiangPingListItem.CreateInstance();
            if (drawInfo.numInfo.num == 1)
            {
                margin.top = 35;
                window.m_JiangPinList.margin = margin;
                if (num < 2)
                {
                    listItem.Init(-11111);
                    window.m_JiangPinList.AddChild(listItem);
                }
                else
                {
                    listItem.Init(itemInfos[0].id, itemInfos[0].num);
                    window.m_JiangPinList.AddChild(listItem);
                    OnIsGetPet(itemInfos[0].id);
                }
            }
            else if (drawInfo.numInfo.num == 10)
            {
                margin.top = 0;
                window.m_JiangPinList.margin = margin;
                if (num > 10)
                {
                    if (window.m_BaiLianChou_2.playing == false)
                    {
                        window.m_BaiLianChou_2.Play();
                    }
                }
                listItem.Init(itemInfos[num].id, itemInfos[num].num);
                window.m_JiangPinList.AddChild(listItem);
                window.m_JiangPinList.ScrollToView(num, true);//滑动到当前下标
                OnIsGetPet(itemInfos[num].id);
            }
            else if (drawInfo.numInfo.num == 100)
            {
                if (num > 10)
                {
                    if (window.m_BaiLianChou_2.playing == false)
                    {
                        window.m_BaiLianChou_2.Play();
                    }
                }
                listItem.Init(itemInfos[num].id, itemInfos[num].num);
                window.m_JiangPinList.AddChild(listItem);
                window.m_JiangPinList.ScrollToView(num, true);//滑动到当前下标
                OnIsGetPet(itemInfos[num].id);
            }
            num++;
        }
    }
    private void OnDongHuaClose(GameEvent evt)
    {
        if (DoAction == null)
        {
            DoAction = new DoActionInterval();            
            DoAction.doAction(intervalTime, LoadTrophy,null,true);
            if(window.m_FaPai.timeScale == 0)
                window.m_FaPai.timeScale = dongxiaoTime;
        }
    }
    private void OnIsGetPet(int itemid)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemid);
        if (itemBean != null)
        {
            if (itemBean.t_type == 23)
            {
                if (window.m_FaPai.playing == true)
                    window.m_FaPai.timeScale = 0;
                DoAction.kill();
                DoAction = null;
                string[] pet = GTools.splitString(itemBean.t_value,';');
                if (pet != null)
                {
                    WinInfo info = new WinInfo();
                    info.param = int.Parse(pet[0]);
                    WinMgr.Singleton.Open<ChongWuDongHuaWindow>(info, UILayer.Popup);
                }
            }
        }
    }
    private void OnJieShu()
    {
        window.m_DanCiChou.visible = false;
        window.m_ShiLianChou.visible = false;
    }
    private void OnBeiJinChuFa()
    {
        if(DoAction != null)
            DoAction.changeIntervalTime(0.05f);
    }

    protected override void OnClose()
    {
        if (DoAction != null)
        {
            DoAction.kill();
            DoAction = null;
        }

        base.OnClose();
 
    }
}
