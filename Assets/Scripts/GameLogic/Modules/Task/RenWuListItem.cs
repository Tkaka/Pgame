using UI_TaskSystem;
using Data.Beans;
using Message.Task;
using System;
/// <summary>
/// 任务列表元件
/// </summary>
/// 
public enum TaskType
{
    Denglu = 1,//登录
    TiLiZengSong = 2,//体力赠送
    GuanKaWnaCheng = 3,//关卡完成
    JingJiChang = 4,//竞技场挑战次数
    ZongJiShiLian = 5,//终极试炼
    JinBiFuBen = 6,//金币副本挑战次数
    ChongWuShengJi = 7,//宠物升级
    ChongWuShengPin = 8,//宠物升品
    ChongWuShengXing = 9,//宠物升星
    JiuBaChouKa = 10,//酒吧抽卡次数
    MeiRiDianJin = 11,//每日点金次数
    TiLiGouMai = 12,//体力购买
    SheTuanJuanXian = 13,//社团捐献
    KeLongZuDui = 14,//克隆组队
    MeiRiXiaoFei = 15,//每日消费钻石
    PuTongGuanKa = 16,//普通关卡通关次数
    JingYingGuanKa = 17,//精英关卡通关次数
    XunLianJiaDengJi = 18,//训练家等级
    ChongWuShouJi = 19,//宠物收集
    LeiChong = 20,//累计充值
}
public class RenWuListItem : UI_RenWuListItem
{
    private t_taskBean taskBean;
    private  TaskInfo taskInfo;
    public int taskId;
    private DoActionInterval doAction;
    private int time;//倒计时总时间
    public new static RenWuListItem CreateInstance()
    {
        return (RenWuListItem)UI_RenWuListItem.CreateInstance();
    }

    public void Init(int taskid, int index = 0)
    {
        taskBean = ConfigBean.GetBean<t_taskBean,int>(taskid);
        taskInfo = TaskService.Singleton.GetTaskInfo(taskid);
        if (taskBean == null)
        {
            Logger.err("RenWuListItem:Init:未能在任务表中找到对应任务id的任务------" + taskid);
            return;
        }
        if (taskInfo == null)
        {
            Logger.err("RenWuListItem:Init:未能在服务器发来的任务表中找到对应任务id的任务------" + taskid + "---" + index);
            return;
        }
        m_QianWang.onClick.Add(OnQianWang);
        m_WanCheng.onClick.Add(OnDrawAward);
        taskId = taskid;
        FillData();
    }
    private void FillData()
    {
        //加载所有需要加载的图片
        OnIcon();
        //是否可领取
        OnWhetherGet();
        //奖励加载
        OnLoadAward();
        //文本加载
        OnLoadText();
        //剩余时间加载
        OnTime();
    }
    private void OnIcon()
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(taskBean.t_icon);
        if (itemBean != null)
        {
            UIGloader.SetUrl(m_RenWuBeiJing, UIUtils.GetIocnBorderByQuility(int.Parse(itemBean.t_quality)));
            UIGloader.SetUrl(m_RenWuIcon, UIUtils.GetItemIcon(itemBean.t_id));
            string iconname = OnGetIcon(taskBean.t_diban);
            UIGloader.SetUrl(m_BeiJing, iconname);
        }
    }
    /// <summary>
    /// 前往按键
    /// 如果列表上添加领取时间不行，就直接在item上添加事件
    /// </summary>
    private void OnQianWang()
    {
        int[,] tiaojian = UIUtils.splitStringTotwodimensionArry(taskBean.t_finish_condition);
        if (tiaojian == null)
        {
            Logger.err("RenWuListItem:OnQiangHua:完成条件为空，无法前往指定界面！------" + taskBean.t_id);
        }
        switch (tiaojian[0,0])
        {
            case (int)TaskType.GuanKaWnaCheng:
                {
                    LevelService.Singleton.LevelModel = LevelModel.Main;
                    WinMgr.Singleton.Open<LevelMainWindow>(null,UILayer.Popup);
                } break;
            case (int)TaskType.JingJiChang:
                {
                    //s TipWindow.Singleton.ShowTip("跳转到竞技场");
                    WinMgr.Singleton.Open<ArenaMainWindow>(null, UILayer.Popup);
                } break;
            case (int)TaskType.ZongJiShiLian:
                {
                    TipWindow.Singleton.ShowTip("跳转到终极试炼");
                    //发请求，收到回复后打开终极试炼的窗口
                    //WinMgr.Singleton.Open<UltemateTrainMainWindow>(new WinInfo(),UILayer.Popup);
                    GED.ED.dispatchEvent(EventID.OnTaskType, TaskType.ZongJiShiLian);
                    UltemateTrainService.Singleton.ReqUltemateTrialInfo();
                }
                break;
            case (int)TaskType.JinBiFuBen:
                {
                    //TipWindow.Singleton.ShowTip("跳转到金币副本");
                    //打开挑战窗口
                    WinMgr.Singleton.Open<TiaoZhanWindow>(new WinInfo(),UILayer.Popup);
                } break;
            case (int)TaskType.ChongWuShengJi:
                {
                    WinInfo info = new WinInfo();
                    TwoParam<int, StrengthType> twoParam = new TwoParam<int, StrengthType>();
                    twoParam.value1 = 0;
                    twoParam.value2 = StrengthType.ShengJi;
                    info.param = twoParam;
                    WinMgr.Singleton.Open<StrengthWindow>(info, UILayer.Popup);
                }
                break;
            case (int)TaskType.ChongWuShengPin:
                {
                    WinInfo info = new WinInfo();
                    TwoParam<int, StrengthType> twoParam = new TwoParam<int, StrengthType>();
                    twoParam.value1 = 0;
                    twoParam.value2 = StrengthType.ShengPing;
                    info.param = twoParam;
                    WinMgr.Singleton.Open<StrengthWindow>(info, UILayer.Popup);
                }
                break;
            case (int)TaskType.ChongWuShengXing:
                {
                    WinInfo info = new WinInfo();
                    TwoParam<int, StrengthType> twoParam = new TwoParam<int, StrengthType>();
                    twoParam.value1 = 0;
                    twoParam.value2 = StrengthType.StarUp;
                    info.param = twoParam;
                    info.isFullResize = true;
                    WinMgr.Singleton.Open<StrengthWindow>(info, UILayer.Popup);
                }
                break;
            case (int)TaskType.JiuBaChouKa:
                {
                   // TipWindow.Singleton.ShowTip("跳转到酒吧抽卡");
                    WinMgr.Singleton.Open<DrawCardWindow>(new WinInfo(),UILayer.Popup);
                } break;
            case (int)TaskType.MeiRiDianJin:
                {
                    TipWindow.Singleton.ShowTip("跳转到每日点金");
                } break;
            case (int)TaskType.TiLiGouMai:
                {
                    //TipWindow.Singleton.ShowTip("跳转到体力购买");
                    RoleService.Singleton.BuyEnergy();
                }
                break;
            case (int)TaskType.SheTuanJuanXian:
                {
                    TipWindow.Singleton.ShowTip("跳转到社团捐献");
                    GED.ED.dispatchEvent(EventID.OnTaskType,TaskType.SheTuanJuanXian);
                    if (RoleService.Singleton.GetRoleInfo().guildId <= 0)
                    {
                        WinMgr.Singleton.Open<JoinGuildMainWnd>();
                        TipWindow.Singleton.ShowTip("未加入社团");
                    }
                    else
                    {
                        GuildService.Singleton.ReqGuildInfo();
                    }
                } break;
            case (int)TaskType.KeLongZuDui:
                {
                    TipWindow.Singleton.ShowTip("跳转到克隆组队讨伐");
                    CloneTeamFightService.Singleton.ReqTeamFightInfo();
                } break;
            case (int)TaskType.PuTongGuanKa:
                {
                    LevelService.Singleton.LevelModel = LevelModel.Main;
                    WinMgr.Singleton.Open<LevelMainWindow>(null, UILayer.Popup);
                } break;
            case (int)TaskType.JingYingGuanKa:
                {
                    LevelService.Singleton.LevelModel = LevelModel.Elite;
                    WinMgr.Singleton.Open<LevelMainWindow>(null, UILayer.Popup);
                } break;
            case (int)TaskType.LeiChong:
                {
                    //TipWindow.Singleton.ShowTip("跳转到充值窗口");
                    VipWndMgr.Singleton.OpenWnd(EVipWndType.Recharge);
                    WinMgr.Singleton.Open<VipMainWnd>(null, UILayer.Popup);
                } break;
            case 21:
                {
                    WinInfo info = new WinInfo();
                    TwoParam<int, StrengthType> twoParam = new TwoParam<int, StrengthType>();
                    twoParam.value1 = 0;
                    twoParam.value2 = StrengthType.ShengPing;
                    info.param = twoParam;
                    WinMgr.Singleton.Open<StrengthWindow>(info, UILayer.Popup);
                }
                break;
        }
    }
    private void OnLoadText()
    {
        if (string.IsNullOrEmpty(taskBean.t_name))
            Logger.err("RenWuListItem:OnLoadText:未获得任务名字对应数据！------" + taskBean.t_id);
        else
            m_Name.text = taskBean.t_name;
        if (string.IsNullOrEmpty(taskBean.t_desc) || string.IsNullOrEmpty(taskBean.t_desc_number))
        {
            if (!(string.IsNullOrEmpty(taskBean.t_desc)))
                m_Content.text = taskBean.t_desc;
            else
                Logger.err("RenWuListItem:OnLoadText:未获得任务内容对应数据！------" + taskBean.t_id);
        }
        else
        {
            string[] desc_namber = GTools.splitString(taskBean.t_desc_number);
            string neirong = UIUtils.onXiaoGuo(taskBean.t_desc,desc_namber);
            m_Content.text = neirong;
        }
        if (string.IsNullOrEmpty(taskBean.t_finish_condition))
        {
            Logger.err("RenWuListItem:OnLoadText:未获得任务进度对应数据！------" + taskBean.t_id);
        }
        else
        {
            if (taskInfo.state == 1 || (taskInfo.schedule.Count == 0) && (string.IsNullOrEmpty(taskBean.t_finish_condition)))
            {
                m_AccomplishNumber.visible = false;
            }
            else
            {
                int[,] finish = UIUtils.splitStringTotwodimensionArry(taskBean.t_finish_condition);
                string accomplish = "";
                int line = finish.GetUpperBound(0) + 1;
                if (finish[0, 0] == (int)TaskType.GuanKaWnaCheng)
                {
                    accomplish = 0 + "/" + 1;
                }
                else
                {
                    //判断进度显示条件
                    if (taskInfo.schedule.Count > 1)
                    { accomplish = taskInfo.schedule[1].value + "/" + taskInfo.schedule[1].target; }
                    else
                     accomplish = taskInfo.schedule[0].value + "/" + taskInfo.schedule[0].target;
                }
                m_AccomplishNumber.text = accomplish;
            }
        }
    }
    private void OnWhetherGet()
    {
        m_WanChenIcon.visible = false;
        //是否可领取
        if (taskInfo.state == 1)
        {
            m_YiWanCheng.visible = true;
            m_YiWanCheng.onClick.Add(OnLingQu);
            m_QianWang.visible = false;
        }
        else if (taskInfo.state == 0)
        {
            m_YiWanCheng.visible = false;
            int[,] tiaojian = UIUtils.splitStringTotwodimensionArry(taskBean.t_finish_condition);
            if (tiaojian == null)
                return;
            if (tiaojian[0, 0] != 1 && tiaojian[0, 0] != 15
                && tiaojian[0, 0] != 18 && tiaojian[0, 0] != 19)
            {
                m_QianWang.visible = true;
            }
            else
                m_QianWang.visible = false;
        }
        else if (taskInfo.state == 3)
        {
            m_QianWang.visible = false;
            m_YiWanCheng.visible = false;
            m_WanChenIcon.visible = true;
        }
    }
    private void OnLoadAward()
    {
        if (string.IsNullOrEmpty(taskBean.t_reward))
        {
            Logger.err("RenwuListItem:OnLoadAward:任务表奖励字段为空-----" + taskBean.t_id);
            return;
        }
        string[] icon = new string[2];
        if (string.IsNullOrEmpty(taskBean.t_reward_icon))
        {
            Logger.err("RenwuListItem:OnLoadAward:任务表奖励图片字段为空-----" + taskBean.t_reward_icon);
            icon[0] = " ";
            icon[1] = " ";
        }
        else
        {
            icon = GTools.splitString(taskBean.t_reward_icon, ';');
        }
        int[,] award = UIUtils.splitStringTotwodimensionArry(taskBean.t_reward);
        //获得奖励道具
        t_itemBean awardA = ConfigBean.GetBean<t_itemBean,int>(award[0,0]);
        t_itemBean awardB = null;
        if ((award.GetUpperBound(0) + 1) == 2)
        {
            awardB = ConfigBean.GetBean<t_itemBean, int>(award[1, 0]);
        }
        if (awardA == null)
        {
            Logger.err("RenwuListItem:OnLoadAward:在道具表中到不到对应道具----" + award[0, 0]);
        }
        else
        {
            if (taskBean.t_color_condition == 0 || taskBean.t_color_condition > 3)
                Logger.err("RenwuListItem:OnLoadAward:在道具表中到不到对应道具----" + awardA.t_id);
            else
            {
               
                if (taskBean.t_color_condition == 1)
                {

                    string iconA = UIUtils.GetItemBorder(award[0, 0]);
                    m_OneAward.text = icon[0] + "X" + award[0, 1].ToString();
                    if ((award.GetUpperBound(0) + 1) == 2)
                    {
                        if (awardB == null)
                            Logger.err("RenwuListItem:OnLoadAward:在道具表中到不到对应道具----" + award[0, 1]);
                        else
                        {
                            if (string.IsNullOrEmpty(awardB.t_icon))
                                Logger.err("RenwuListItem:OnLoadAward:在道具表中到不到对应道具----" + award[1, 1]);
                            else
                            {
                                string iconB = UIUtils.GetItemBorder(award[1, 0]);
                                if(icon.Length > 1)
                                    m_TwoAward.text = icon[1] + "X" + award[1, 1].ToString();
                            }
                        }
                    }
                }
                else if (taskBean.t_color_condition == 2)
                {
                    //奖励2是文字变色处理
                    string iconA = UIUtils.GetItemBorder(award[0, 0]);
                    m_OneAward.text = icon[0] + "X" + award[0, 1].ToString();
                    if ((award.GetUpperBound(0) + 1) == 2)
                    {
                        if (awardB == null)
                            Logger.err("RenwuListItem:OnLoadAward:在道具表中到不到对应道具----" + award[0, 1]);
                        else
                        {
                            if (string.IsNullOrEmpty(awardB.t_quality))
                                Logger.err("RenwuListItem:OnLoadAward:在道具表中到不到对应道具----" + award[1, 1]);
                            else
                            {
                                m_TwoAward.text = awardB.t_name;
                                m_TwoAward.text += "X" + award[1, 1];
                                m_TwoAward.color = UIUtils.GetItemColor(awardB.t_id);
                            }
                        }
                    }
                }
                else if (taskBean.t_color_condition == 3)
                {
                    //奖励1和奖励2都是文字变色处理
                    if (string.IsNullOrEmpty(awardA.t_quality))
                    {
                        Logger.err("RenWuListItm:OnLoadAward():道具品质字段为空" + awardA.t_id);
                    }
                    else
                    {
                        m_OneAward.text = awardA.t_name;
                        m_OneAward.text += "X" + award[0, 1];
                        m_OneAward.color = UIUtils.GetItemColor(awardA.t_id);
                    }
                    if ((award.GetUpperBound(0) + 1) == 2)
                    {
                        if (awardB != null)
                        {
                            if (string.IsNullOrEmpty(awardB.t_quality))
                            {
                                Logger.err("RenWuListItm:OnLoadAward():道具品质字段为空" + awardB.t_id);
                            }
                            else
                            {
                                m_TwoAward.text = awardB.t_name;
                                m_TwoAward.text += "X" + award[1, 1];
                                m_TwoAward.color = UIUtils.GetItemColor(awardB.t_id);
                            }
                        }
                    }
                }
            }
        }
       
    }
    private void OnTime()
    {
        m_DaoJiShi.visible = false;

        if (taskInfo.hasEndTime())
        {
            m_QianWang.visible = false;
            if (!(string.IsNullOrEmpty(taskBean.t_time)))
            {
                //启动协程
                m_DaoJiShi.visible = true;
                long endtime = taskInfo.endTime;//毫秒时间
                endtime /= 1000;
                long currentTime = TimeUtils.currentServerDateTime().Ticks;//当前客户端时间
                DateTime dt = new DateTime(1970, 1, 1);
                TimeSpan ts = new TimeSpan(currentTime - dt.Ticks);
                //当前客户端时间
                int seconds = (int)ts.TotalSeconds;
                time = (int)(endtime - seconds);
                if (doAction == null)
                {
                    doAction = new DoActionInterval();
                    doAction.doAction(1, OnDaoJiShi, null, true);
                }
            }
        }
    }
    private void OnDaoJiShi(object obj)
    {
        if (time < 0)
        {
            //到时间
            m_DaoJiShi.visible = false;
            doAction.kill();
            doAction = null;
        }
        else
        {
            int hour = time / (60 * 60);
            int minute = (time % (60 * 60)) / 60;
            int scend = (time % (60 * 60)) % 60;
            string shi = hour.ToString();
            string fen = minute.ToString();
            string miao = scend.ToString();
            if (hour < 10)
            {
                shi = "0" + shi;
            }
            if (minute < 10)
            {
                fen = "0" + fen;
            }
            if (scend < 10)
            {
                miao = "0" + miao;
            }
            m_Time.text = "剩余时间" + shi + "时" + fen + "分" + miao + "秒";
        }
        time--;
    }
    private void OnLingQu()
    {
        TaskService.Singleton.OnReward(taskBean.t_id);
    }
    public override void Dispose()
    {
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        taskBean = null;
        base.Dispose();
    }
    private void OnDrawAward()
    {
        TipWindow.Singleton.ShowTip("该关卡尚未完成");
    }
    private string OnGetIcon(int type)
    {
        m_XingYunJiaoBiao.visible = false;
        m_VipJiaoBiao.visible = false;
        switch (type)
        {
            case 0: return "ui://UI_TaskSystem/rwdiban01";
            case 1:return "ui://UI_TaskSystem/rwdiban04";
            case 2: m_XingYunJiaoBiao.visible = true; return "ui://UI_TaskSystem/rwdiban02";
            case 3: m_VipJiaoBiao.visible = true; return "ui://UI_TaskSystem/rwdiban03";
            default: return null;
        }
    }
}