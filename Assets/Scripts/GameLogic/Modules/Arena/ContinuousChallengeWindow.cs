using UI_Arena;
using Message.Arena;
using System.Collections.Generic;
using UnityEngine;
using Data.Beans;
using FairyGUI;
using Message.Role;

public class ContinuousChallengeWindow : BaseWindow
{
    private UI_ContinuousChallengeWindow m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ContinuousChallengeWindow>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOk.onClick.Add(Close);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        _ShowTitleInfo();
        _ShowResultInfo();
        _ShowPriceInfo();
    }

    //显示标题信息
    private void _ShowTitleInfo()
    {
        ResContinueArena msg = Info.param as ResContinueArena;
        if (msg == null)
            return;

        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();

        //m_window.m_imgEnemyIcon.url = msg.model + ""; // 设置头像框
        //m_window.m_imgMyIcon.url = "主角头像框";


        m_window.m_txtMyName.text = roleInfo.roleName;
        m_window.m_txtMyFightPower.text = roleInfo.fightPower + "";
        m_window.m_txtEnemyName.text = msg.name;
        m_window.m_txtEnemyFightPower.text = msg.fightPower + "";

    }

    private void _ShowResultInfo()
    {
        ResContinueArena msg = Info.param as ResContinueArena;
        if (msg == null)
            return;

        List<ArenaResult> infos = msg.result;
        for (int i = 0; i < infos.Count; i++)
        {
            UI_challengeResultCell cell = UI_challengeResultCell.CreateInstance();
            _OnCellShow(cell, infos[i]);
            m_window.m_mainList.AddChild(cell);
        }

    }

    //显示再战5次价格信息
    private void _ShowPriceInfo()
    {
        int challengeTimes = 5;
        int consumeNum = 0;
        int remainCount = ArenaService.Singleton.GetChanllangeCount();
        if (remainCount >= challengeTimes)
        {
            m_window.m_price.visible = false;
        }
        else
        {
            m_window.m_price.visible = true;
            int needBuyCount = challengeTimes - remainCount;
             
            int buyNum = ArenaService.Singleton.GetBuyedNum();
            int []arrBuyNum = GTools.splitStringToIntArray(ConfigBean.GetBean<t_globalBean, int>(17013).t_string_param);
            for (int i = 0; i < needBuyCount; i++)
            {
                if (buyNum >= arrBuyNum.Length)
                    consumeNum += arrBuyNum[arrBuyNum.Length - 1];
                else
                    consumeNum += arrBuyNum[buyNum];

                buyNum++;
            }

            m_window.m_txtNum.text = consumeNum + "";

        }

        m_window.m_btnChallenge5.onClick.Add(() =>
        {
            ResContinueArena msg = Info.param as ResContinueArena;
            if (msg == null)
                return;

            if (remainCount < challengeTimes)
            {
                string strDes = string.Format("是否花费{0}钻石购买{1}次挑战次数, 并直接挑战？", consumeNum, challengeTimes - remainCount);
 
                CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, "兑换次数", strDes, () => {
                    ArenaService.Singleton.ReqArena(msg.roleId, challengeTimes, msg.rank, 2);
                });
            }
            else
            {
                ArenaService.Singleton.ReqArena(msg.roleId, challengeTimes, msg.rank);
            }
 
        });
    }

    private void _OnCellShow(UI_challengeResultCell obj, ArenaResult info)
    {
        obj.m_imgFailed.visible = info.state == 2 ? true : false;
        obj.m_imgSucess.visible = info.state == 1 ? true : false;
        string strDes = "";
        if (info.state == 1)
            strDes = string.Format("我方在竞技中获胜,竞技场积分+{0}", info.core);
        else
            strDes = string.Format("我方在竞技中失败,竞技场积分+{0}", info.core);

        obj.m_txtDes.text = strDes;
        obj.m_txtHuiHe.text = info.round + "";
    }

    protected override void OnClose()
    {
        base.OnClose();

    }

}