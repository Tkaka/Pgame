
using UI_Arena;
using Message.Role;
using Message.Arena;
using UnityEngine;
using FairyGUI;
using Data.Beans;
using System.Collections.Generic;


public class ArenaMainWindow : BaseWindow
{
    private UI_ArenaMainWindow m_window;

    private int m_totalCount = 5;   //挑战总次数
    private int m_myRank;           //我的排名
    private int m_fastChallangeOpenLevel = 60;   //快速挑战开启等级
    private int[] m_arrBuyNum;                   //购买次数需要消耗钻石

    private static UIResPack m_resPacker;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ArenaMainWindow>();
        m_resPacker = new UIResPack(this);
        _BindEvent();
        ArenaService.Singleton.ReqAreanInfo();

    }

    private void _BindEvent()
    {
        m_window.m_btnSet.onClick.Add(_OnSetClick);
        m_window.m_btnRank.onClick.Add(_OnRankClick);
        m_window.m_btnShop.onClick.Add(_OnShopClick);
        m_window.m_btnReward.onClick.Add(_OnRewardClick);
        m_window.m_btnZhanBao.onClick.Add(_OnZhanBaoClick);
        m_window.m_btnScore.onClick.Add(_OnScoreClick);
        m_window.m_btnRule.onClick.Add(_OnRuleClick);
        m_window.m_btnLeft.onClick.Add(_OnLeftClick);
        m_window.m_btnRight.onClick.Add(_OnRightClick);
        m_window.m_ComsumeItem.m_btnAddCount.onClick.Add(_OnAddCountClick);
        m_window.m_buyCount.onClick.Add(_OnBuyCountClick);
        m_window.m_btnHuanYiPi.m_btnHuan.onClick.Add(_OnHuanClick);
        m_window.m_Reset.m_btnReset.onClick.Add(_OnResetClick);
        m_window.m_btnOneKeyMoBai.onClick.Add(_OnOneKeyMoBaiClick);
        m_window.m_List.m_mainList.scrollPane.onScroll.Add(_OnScroll);
        ((UI_Common.UI_commonTop)m_window.m_TaiTou).m_closeBtn.onClick.Add(Close);
        m_window.m_btnClose.onClick.Add(Close);

    }

    public override void InitView()
    {
        base.InitView();
        _RegisterRedDot("mainArean/ArenaPage/RankReward", m_window.m_btnReward.m_imgRewardRed);
        _RegisterRedDot("mainArean/ArenaPage/ScoreReward", m_window.m_btnScore.m_imgScoreRed);

        m_window.m_List.m_mainList.SetVirtual();
        m_window.m_List.m_mainList.itemProvider = _CellProvider;
        m_window.m_List.m_mainList.itemRenderer = _OnCellShow;

        _ShowTimesInfo();
        _ShowPlayersInfo();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ArenaInfoRefresh, _OnRefreshInfo);
        GED.ED.addListener(EventID.ClearCoolTime, _OnClearCoolTimes);
        GED.ED.addListener(EventID.PlayersChange, _PlayerListRefresh);
        GED.ED.addListener(EventID.PlayerChangeCount, _OnHuanYiPiCountChange);
        GED.ED.addListener(EventID.ChallengeCountChange, _OnChallengeCountChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ArenaInfoRefresh, _OnRefreshInfo);
        GED.ED.removeListener(EventID.ClearCoolTime, _OnClearCoolTimes);
        GED.ED.removeListener(EventID.PlayersChange, _PlayerListRefresh);
        GED.ED.removeListener(EventID.PlayerChangeCount, _OnHuanYiPiCountChange);
        GED.ED.removeListener(EventID.ChallengeCountChange, _OnChallengeCountChange);
    }

    private void _OnRefreshInfo(GameEvent evt)
    {
        InitView();
    }

    //冷却时间清除
    private void _OnClearCoolTimes(GameEvent evt)
    {
        _ShowTimesInfo();
    }

    private void _OnHuanYiPiCountChange(GameEvent evt)
    {
        _ShowTimesInfo();
    }

    //玩家列表刷新
    private void _PlayerListRefresh(GameEvent evt)
    {
        _ShowPlayersInfo();
    }


    //挑战次数改变
    private void _OnChallengeCountChange(GameEvent evt)
    {
        _ShowTimesInfo();
    }

    private string _CellProvider(int index)
    {
        return UI_PlayerCell.URL;
    }

    //显示玩家列表信息
    private void _ShowPlayersInfo()
    {


        _ShowRoleInfo();
        List<PlayerInfo> top10List = ArenaService.Singleton.GetTop10PlayersInfo();
        List<PlayerInfo> lastFives = ArenaService.Singleton.GetLastFivePlayerInfo();

        bool isGray = true;

        for (int i = 0; i < top10List.Count; i++)
        {
            if (top10List[i].canWorship)
            {
                isGray = false;
                break;
            }

        }

        _ShowOneKeyBtnState(isGray);
        m_window.m_List.m_mainList.numItems = top10List.Count + lastFives.Count;
        _OnScroll();
    }

    private void _OnCellShow(int index, GObject gObject)
    {
        UI_PlayerCell obj = gObject as UI_PlayerCell;
        List<PlayerInfo> top10List = ArenaService.Singleton.GetTop10PlayersInfo();
        List<PlayerInfo> lastFives = ArenaService.Singleton.GetLastFivePlayerInfo();
        PlayerInfo info;
        if (index < top10List.Count)
            info = top10List[index];
        else
            info = lastFives[index - top10List.Count];

        if (info == null || obj == null)
            return;

        obj.m_txtRank.visible = info.rank <= 10;
        obj.m_txtRank.text = info.rank + "";

        //obj.m_bg.m_bgLoader.url = "";
        //obj.m_modle.url = "";

        obj.m_txtName.text = info.name;
        obj.m_txtFightPower.text = info.fightPower + "";

        obj.m_txtMoBaiOrRank.text = info.rank <= 10 ? "被膜拜" : "排名";
        obj.m_txtCount.text = info.rank <= 10 ? (info.worshipNum + "") : (info.rank + "");

         
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        if (roleInfo.roleId == info.roleId)
        {
            obj.m_mySelf.visible = true;
            obj.m_btnMoBai.visible = false;
            obj.m_btnSingleChallange.visible = false;
            obj.m_challange50.visible = false;
        }
        else
        {
            obj.m_mySelf.visible = false;
            if (info.rank <= 10)
            {
                //前10
                if (info.canWorship)
                {
                    obj.m_btnMoBai.visible = true;
                    obj.m_btnSingleChallange.visible = false;
                    obj.m_challange50.visible = false;

                }
                else
                {
                    obj.m_btnMoBai.visible = false;
                    if (roleInfo.level >= m_fastChallangeOpenLevel && m_myRank < info.rank)
                    {
                        obj.m_challange50.visible = true;
                        obj.m_btnSingleChallange.visible = false;

                    }
                    else
                    {
                        obj.m_challange50.visible = false;
                        obj.m_btnSingleChallange.visible = true;
                        obj.m_btnSingleChallange.grayed = m_myRank > 20;
                    }
                    
               
                }

            }
            else
            {
                obj.m_btnMoBai.visible = false;
                if (roleInfo.level >= m_fastChallangeOpenLevel && m_myRank < info.rank)
                {
                    obj.m_challange50.visible = true;
                    obj.m_btnSingleChallange.visible = false;

                }
                else
                {
                    obj.m_challange50.visible = false;
                    obj.m_btnSingleChallange.visible = true;
                    obj.m_btnSingleChallange.grayed = false;
                }
            }
        }

        obj.m_btnMoBai.onClick.Clear();
        obj.m_btnMoBai.onClick.Add(() => { ArenaService.Singleton.ReqWorship(false, info.roleId); });

        obj.m_btnSingleChallange.onClick.Clear();
        obj.m_btnSingleChallange.onClick.Add(() => {
            if (info.rank <= 10 && m_myRank > 20)
            {
                TipWindow.Singleton.ShowTip("排名提升至前20才可挑战该玩家");
            }
            else
            {
                _CheckToSendChanllange(info.roleId, 1, info.rank);
            }
            
        });

        obj.m_challange50.m_btnChallange.onClick.Clear();
        obj.m_challange50.m_btnChallenge50.onClick.Clear();
        obj.m_imgClick.onClick.Clear();
        obj.m_challange50.m_btnChallange.onClick.Add(() => { _CheckToSendChanllange(info.roleId, 1, info.rank); });
        obj.m_challange50.m_btnChallenge50.onClick.Add(() => { _CheckToSendChanllange(info.roleId, 5, info.rank); });
        obj.m_imgClick.onClick.Add(() => { ArenaService.Singleton.ReqSeeOther(info.roleId); });
        _LoadModel(101, obj.m_modle);

    }

    //显示一键膜拜按钮的状态
    private void _ShowOneKeyBtnState(bool isGray)
    {

        //List<PlayerInfo> top10Infos = ArenaService.Singleton.GetTop10PlayersInfo();
        //for (int i = 0; i < top10Infos.Count; i++)
        //{
        //    if (top10Infos[i].canWorship)
        //    {
        //        isGray = false;
        //        break;
        //    }
        //}

        m_window.m_btnOneKeyMoBai.grayed = isGray;
        m_window.m_btnOneKeyMoBai.enabled = !isGray;

    }

    //显示主角信息
    private void _ShowRoleInfo()
    {
        m_myRank = ArenaService.Singleton.GetRoleRank();
        m_window.m_myRankInfo.m_txtRank.text = m_myRank + "";
        m_window.m_myRankInfo.m_txtFightPower.text = RoleService.Singleton.GetRoleInfo().fightPower + "";
    }


    //显示次数相关信息
    private void _ShowTimesInfo()
    {
        int remainCount = ArenaService.Singleton.GetChanllangeCount();
        //int remainCount = challangedCount > m_totalCount ? 0 : (m_totalCount - challangedCount);
        m_window.m_txtCount.text = string.Format("{0}/{1}", remainCount, m_totalCount);
        m_window.m_txtCount.color = remainCount > 0 ? Color.green : Color.red;

        //m_window.m_Reset.visible = false;
        //m_window.m_btnHuanYiPi.visible = false;
        //m_window.m_ComsumeItem.visible = false;
        //m_window.m_buyCount.visible = false;

        long remainTime = -TimeUtils.CalculateDelta(ArenaService.Singleton.GetCoolTime());
        if (remainTime > 0)
        {
            //有冷却
            m_window.m_Reset.visible = true;
            m_window.m_buyCount.visible = false;
            m_window.m_ComsumeItem.visible = false;
            m_window.m_btnHuanYiPi.visible = false;

            //m_window.m_Reset.m_txtRemainTime.text = 
            Timers.inst.Remove(_CountDownTime);
            Timers.inst.Add(1, (int)Mathf.Ceil(remainTime / 1000), _CountDownTime);

            int resetCount = ArenaService.Singleton.GetChangeCount();
            m_window.m_Reset.m_txtNum.text = ConfigBean.GetBean<t_globalBean, int>(17011).t_int_param + "";
           
        }
        else
        {
            m_window.m_Reset.visible = false;

            if (remainCount > 0)
            {
                m_window.m_btnHuanYiPi.visible = true;

                //有挑战次数
                m_window.m_buyCount.visible = false;
                m_window.m_ComsumeItem.visible = false;

                int changCount = ArenaService.Singleton.GetChangeCount();
                int[] arr = GTools.splitStringToIntArray(ConfigBean.GetBean<t_globalBean, int>(17012).t_string_param);
                int consumeNum = 0;
                if (changCount > arr.Length)
                {
                    consumeNum = arr[arr.Length - 1];
                }           
                else
                {
                    consumeNum = changCount == 0 ? 0 : arr[changCount - 1];
                }

                m_window.m_btnHuanYiPi.m_imgZhuanShi.visible = consumeNum > 0;
                m_window.m_btnHuanYiPi.m_txtNum.text = consumeNum > 0 ? (consumeNum + "") : "免费";


            }
            else
            {
                m_window.m_btnHuanYiPi.visible = false;
                int itemId = ConfigBean.GetBean<t_globalBean, int>(17020).t_int_param;
                int resetItemNum = BagService.Singleton.GetItemNum(itemId);
                if (resetItemNum > 0)
                {
                    //有重置券
                    m_window.m_ComsumeItem.visible = true;
                    m_window.m_buyCount.visible = false;

                    t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemId);
                    if (itemBean != null)
                    {
                        //设置道具图标
                    }

                    m_window.m_ComsumeItem.m_txtCount.text = string.Format("{0}/{1}", resetItemNum, 1);

                }
                else
                {
                    m_window.m_buyCount.visible = true;
                    m_window.m_ComsumeItem.visible = false;
                    int buyedCount = ArenaService.Singleton.GetBuyedNum();
                    int[] arr = GTools.splitStringToIntArray(ConfigBean.GetBean<t_globalBean, int>(17013).t_string_param);
                    int consumeNum = 0;
                    if (buyedCount >= arr.Length)
                    {
                        consumeNum = arr[arr.Length - 1];
                    }
                    else
                    {
                        consumeNum = arr[buyedCount];
                    }

                    m_window.m_buyCount.m_txtNum.text = consumeNum + "";
                }

            }

        }
    }

    //冷却倒计时
    public void _CountDownTime(object param)
    {
        long remainTime = -TimeUtils.CalculateDelta(ArenaService.Singleton.GetCoolTime());
        if (remainTime <= 0)
        {
            Timers.inst.Remove(_CountDownTime);
            m_window.m_Reset.m_txtRemainTime.text = "00:00";
            _ShowTimesInfo();
            return;
        }
        string time = TimeUtils.FormatTime2((int)Mathf.Ceil(remainTime / 1000));
        m_window.m_Reset.m_txtRemainTime.text = time;
    }


    //检测发送挑战
    private void _CheckToSendChanllange(long roleId, int times, int rank)
    {
        int remainCount = ArenaService.Singleton.GetChanllangeCount();
        int consumeItem = -1;  //1为券 2为钻石 -1不消耗
 
        if (remainCount >= times)
        {
            ArenaService.Singleton.ReqArena(roleId, times, rank, consumeItem);
            return;
        }

        if (times == 1)
        {
            int itemId = ConfigBean.GetBean<t_globalBean, int>(17020).t_int_param;
            int resetItemNum = BagService.Singleton.GetItemNum(itemId);
            consumeItem = resetItemNum > 0 ? 1 : 2;
        }
        else
        {
            consumeItem = 2;
        }

        string strDes = "";
        if (consumeItem == 1)
        {
            //用券只能挑战单次
            strDes = "是否消耗一个挑战券兑换一次挑战次数并直接进入战斗?";
        }
        else
        {
            if (m_arrBuyNum == null)
            {
                m_arrBuyNum = GTools.splitStringToIntArray(ConfigBean.GetBean<t_globalBean, int>(17013).t_string_param);
            }

            int consumeNum = 0;
            int buyNum = ArenaService.Singleton.GetBuyedNum();
            times = times - remainCount;
            for (int i = 0; i < times; i++)
            {
                if (buyNum >= m_arrBuyNum.Length)
                    consumeNum += m_arrBuyNum[m_arrBuyNum.Length - 1];
                else
                    consumeNum += m_arrBuyNum[buyNum];

                buyNum++;
            }

            strDes = string.Format("是否花费{0}钻石购买{1}次挑战次数, 并直接挑战？", consumeNum, times);
        }

        CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, "兑换次数", strDes, () => { ArenaService.Singleton.ReqArena(roleId, times, rank, consumeItem); });
         
    }

    //加载模型
    private void _LoadModel(int petId, GGraph showObj)
    {


        Vector3 postion = new Vector3(50, 0, 600);

        m_resPacker.CacheWrapper(showObj);
        var wrapper = new GoWrapper();
        showObj.SetNativeObject(wrapper);
        var actor = m_resPacker.NewActorUI(petId, ActorType.Pet, wrapper, true);
        actor.SetTransform(new Vector3(0,0, 50), 100, new Vector3(0, 90, 0));
    }


    //设置头像点击
    private void _OnSetClick()
    {
        WinMgr.Singleton.Open<IconSelectWindow>(null, UILayer.Popup);
    }

    //排行榜点击
    private void _OnRankClick()
    {
        ArenaService.Singleton.ReqRank();
        //WinMgr.Singleton.Open<RankWindow>();
         
    }

    private void _OnShopClick()
    {
        //Debug.Log("------------->>>打开商店");
        WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null, false, 2), UILayer.Popup);
    }

    private void _OnRewardClick()
    {
        WinMgr.Singleton.Open<RewardWindow>(null, UILayer.Popup);
    }

    private void _OnZhanBaoClick()
    {

    }

    private void _OnScoreClick()
    {
        WinMgr.Singleton.Open<ScoreRewardWindow>(null, UILayer.Popup);
    }

    private void _OnRuleClick()
    {
        WinMgr.Singleton.Open<ArenaRuleWindow>(null, UILayer.Popup);
    }


    //左滑点击
    private void _OnLeftClick()
    {
        int index = m_window.m_List.m_mainList.GetFirstChildInView();
        int totalNum = m_window.m_List.m_mainList.numItems;
        int moveToInex = index - 5 < 0  ? 0 : (index - 5);

        m_window.m_List.m_mainList.ScrollToView(moveToInex, true, true);
        //m_window.m_btnLeft.visible = moveToInex == 0 ? false : true;
        //m_window.m_btnLeft.visible = m_window.m_mainList.GetChildAt(0).visible;
        //m_window.m_btnRight.visible = m_window.m_mainList.GetChildAt(totalNum - 1).visible;
    }

    //右滑点击
    private void _OnRightClick()
    {
        int index = m_window.m_List.m_mainList.GetFirstChildInView();
        int totalNum = m_window.m_List.m_mainList.numItems;
        int moveToInex = index + 5 > (totalNum - 1) ? (totalNum - 1) : (index + 5);

        m_window.m_List.m_mainList.ScrollToView(moveToInex, true, true);
        //m_window.m_btnRight.visible = moveToInex == (totalNum - 1) ? false : true;
        //m_window.m_btnLeft.visible = m_window.m_mainList.GetChildAt(0).visible;
        //m_window.m_btnRight.visible = m_window.m_mainList.GetChildAt(totalNum - 1).visible;
    }

    //消耗券购买次数
    private void _OnAddCountClick()
    {
        ArenaService.Singleton.ReqBuyNum(1);
    }

    //购买次数
    private void _OnBuyCountClick()
    {
        ArenaService.Singleton.ReqBuyNum(2);
    }

    //换一批
    private void _OnHuanClick()
    {
        ArenaService.Singleton.ReqChangeTarget();
    }

    private void _OnResetClick()
    {

        ArenaService.Singleton.ReqClearCoolTime();
    }


    //一键膜拜
    private void _OnOneKeyMoBaiClick()
    {
        ArenaService.Singleton.ReqWorship(true);
    }

    private void _OnScroll()
    {

        m_window.m_btnLeft.visible = false;
        m_window.m_btnRight.visible = false;
        if (!(m_window.m_List.m_mainList.scrollPane.posX == 0))
        {
            m_window.m_btnLeft.visible = true;
        }

        if (!m_window.m_List.m_mainList.scrollPane.isRightMost)
        {
            m_window.m_btnRight.visible = true;
        }

    }

    protected override void OnClose()
    {
        base.OnClose();
        Timers.inst.Remove(_CountDownTime);
        if (m_resPacker != null)
        {
            m_resPacker.ReleaseAllRes();
            m_resPacker = null;
        }
 
        m_arrBuyNum = null;
    }
}