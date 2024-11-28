using UI_GeDouJia;
using Data.Beans;
using Message.Dungeon;
using System.Collections.Generic;
using FairyGUI;

public class GuanKaListItem : UI_GuanKaListItem
{
    private t_dungeon_actBean actBean;
    private int cishu;//扫荡次数 
    private int star;//关卡星级
    public new static GuanKaListItem CreateInstance()
    {
        return (GuanKaListItem)UI_GuanKaListItem.CreateInstance();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="guankaId">关卡id</param>
    public void Init(int guankaId,int number)
    {
        actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(UIUtils.GetLaiYuanActID(guankaId));
        if (actBean == null)
        {
            Logger.err("GuanKaListItem:Init():未能找到对应关卡id,无法加载item");
            return;
        }
        cishu = number;
        m_GK_QianWang.onClick.Add(OnQianQuang);
        //判断是
        FillText();
        OnAnJianWenZi(cishu);//扫荡次数文字设置
        FillList();
    }
    /// <summary>
    /// 文本填充
    /// </summary>
    private void FillText()
    {
        if (actBean.t_act_type == 1)
        {
            m_GK_Type.text = "主线";
        }
        else if (actBean.t_act_type == 2)
        {
            m_GK_Type.text = "精英";
        }
        else if (actBean.t_act_type == 3)
        {
            m_GK_Type.text = "活动";
        }
        int zhangjiexuhao = actBean.t_chapter_id % 100;
        int guankaxuhao = actBean.t_id % 100;
        string xuhao = zhangjiexuhao + "-" + guankaxuhao;
        string guankaming = actBean.t_name_id;
        m_GK_Name.text = xuhao + "   " + guankaming;
        string zhangjie = "第{0}章";
        m_ZhangJie.text = string.Format(zhangjie,zhangjiexuhao.ToString());
        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(actBean.t_id);
        star = actInfo == null ? 0 : actInfo.star;
        //按钮显示
        {
            if (star <= 0)
            {
                m_GK_QianWang.m_weikaiqi.visible = true;
                m_GK_QianWang.m_YiKaiQi.visible = false;
                m_GK_QianWang.m_QianWang.visible = false;
            }
            else if ((star >= 3) || (RoleService.Singleton.RoleInfo.roleInfo.level - actBean.t_level_limit >= 10))
            {
                m_GK_QianWang.m_weikaiqi.visible = false;
                m_GK_QianWang.m_YiKaiQi.visible = true;
                m_GK_QianWang.m_QianWang.visible = false;
            }
            else if (star > 0 && star < 3)
            {
                m_GK_QianWang.m_weikaiqi.visible = false;
                m_GK_QianWang.m_YiKaiQi.visible = false;
                m_GK_QianWang.m_QianWang.visible = true;
            }
        }
    }
    private void OnQianQuang()
    {
        int tili = RoleService.Singleton.RoleInfo.roleInfo.energy;
        if (tili > actBean.t_comsumePower)
        {
            if (star >= 3)
            {
                List<SweepReqInfo> sweeps = new List<SweepReqInfo>();
                SweepReqInfo sweepInfo = new SweepReqInfo();
                sweepInfo.actId = actBean.t_id;
                sweepInfo.num = cishu;
                sweeps.Add(sweepInfo);
                LevelService.Singleton.ReqSweepAct(sweeps);
            }
            else if ((star >= 1 && star < 3))
            {
                if (RoleService.Singleton.RoleInfo.roleInfo.level - actBean.t_level_limit >= 10)
                {
                    List<SweepReqInfo> sweeps = new List<SweepReqInfo>();
                    SweepReqInfo sweepInfo = new SweepReqInfo();
                    sweepInfo.actId = actBean.t_id;
                    sweepInfo.num = cishu;
                    sweeps.Add(sweepInfo);
                    LevelService.Singleton.ReqSweepAct(sweeps);
                }
                else
                {
                    if (actBean.t_type == 1)
                    {
                        LevelService.Singleton.LevelModel = LevelModel.Main;
                    }
                    else if (actBean.t_type == 2)
                    {
                        LevelService.Singleton.LevelModel = LevelModel.Elite;
                    }
                    WinMgr.Singleton.Open<LevelMainWindow>(null, UILayer.Popup);
                    OneParam<int> param = new OneParam<int>();
                    param.value = actBean.t_id;
                    WinInfo winInfo = new WinInfo();
                    winInfo.param = param;
                    WinMgr.Singleton.Open<GuanQiaWindow>(winInfo, UILayer.Popup);
                    GED.ED.dispatchEvent(EventID.OnCloseLaiYuan);
                    t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(61801035);
                    if (languageBean != null)
                    { TipWindow.Singleton.ShowTip(languageBean.t_content); }
                }
            }
            else
            {
                t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(71901009);
                if (languageBean != null)
                { TipWindow.Singleton.ShowTip(languageBean.t_content); }
            }
        }
        else
        {
            //体力购买
            RoleService.Singleton.BuyEnergy();
        }
        
    }
    public void OnAnJianWenZi(int shuliang)
    {
        cishu = shuliang;
        if (star > 0)
        {
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean,int>(71901010);
            if (languageBean != null)
            {
                string saodang = languageBean.t_content;
                m_GK_QianWang.m_YiKaiQi.visible = true;
                m_GK_QianWang.m_WeiKaiQi.visible = false;
                m_GK_QianWang.m_CiShu.text = string.Format(saodang, cishu.ToString());
            }
        }
        else
        {
            string weijiesuo = "未解锁";
            m_GK_QianWang.m_YiKaiQi.visible = false;
            m_GK_QianWang.m_WeiKaiQi.visible = true;
            m_GK_QianWang.m_weikaiqi.text = weijiesuo;
        }
    }
    /// <summary>
    /// 列表填充
    /// </summary>
    private void FillList()
    {
        //获得道具数据数据
        if (string.IsNullOrEmpty(actBean.t_drop_show_id))
        {
            Logger.err("GuanKalistItem：FillList():未获得道具列表");
            return;
        }
        int[] items = GTools.splitStringToIntArray(actBean.t_drop_show_id);
        CommonItem listItem;
        for (int i = 0; i < items.Length; ++i)
        {
            listItem = CommonItem.CreateInstance();
            listItem.Init(items[i]);
            listItem.SetIconScale(0.6f, 0.6f);
            listItem.AddPopupEvent();
            listItem.RefreshView();
            m_DiaoLuoList.AddChild(listItem);
        }
    }
    public override void Dispose()
    {
        actBean = null;
        base.Dispose();
    }
}
