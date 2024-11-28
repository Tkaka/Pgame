//精英关卡的准备界面
using UI_Level;
using UI_Common;
using FairyGUI;
using Message.Role;
using Message.Dungeon;
using Data.Beans;
using System.Collections.Generic;
using UnityEngine;

public class eliteGuanQiaWindow : BaseWindow
{
    private UI_eliteGuanQiaWindow m_window;
    private List<GImage> m_imgList = new List<GImage>();
    private int m_actId;             //关卡ID
    private int m_starLevel;           //星级
    private int m_maxCount = 3;            //关卡最大次数 

    private TopRoleInfo topRoleInfo;

    public override void OnOpen()
    {
        base.OnOpen();         
        m_window = getUiWindow<UI_eliteGuanQiaWindow>();
        
        _InitInfo();
        _BindEvent();
        InitView();
        _ShowMoneyInfo();
    }

    private void _BindEvent()
    {
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnSaoDang.onClick.Add(_OnSaoDangClick);
        m_window.m_btnChallange.onClick.Add(_OnChallangeClick);
    }

    private void _InitInfo()
    {
        m_imgList.Clear();
        m_imgList.Add(m_window.m_imgStar.m_star1);
        m_imgList.Add(m_window.m_imgStar.m_star2);
        m_imgList.Add(m_window.m_imgStar.m_star3);

        OneParam<int> param = Info.param as OneParam<int>;
        m_actId = param.value;
        m_starLevel = LevelService.Singleton.GetActStarInfoById(m_actId);
    }

    public override void InitView()
    {
        base.InitView();
        _ShowGuanQiaInfo();
    }


    private void _ShowMoneyInfo()
    {
        //topRoleInfo = new TopRoleInfo((UI_TopRoleInfo)m_window.m_roleInfo);
    }

    private void _ShowGuanQiaInfo()
    {
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(m_actId);
        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(m_actId);
        if (actBean == null || actInfo == null)
        {
            Debug.Log("不存在的关卡ID" + m_actId);
            return;
        }

        m_window.m_txtGuaKaName.text = actBean.t_name_id;
        m_window.m_txtPower.text = actBean.t_comsumePower + "";
        m_window.m_txtCount.text = string.Format("{0}/{1}", m_maxCount - actInfo.attackNum, m_maxCount);
        m_window.m_btnSaoDang.visible = actInfo.star > 0;

        //星级
        for (int i = 0; i < m_imgList.Count; i++)
        {
            m_imgList[i].visible = m_starLevel >= i + 1;
        }

        List<int> itemList = GTools.splitStringToIntList(actBean.t_drop_show_id);
        for (int i = 0; i < itemList.Count; i++)
        {
            CommonItem itemCell = CommonItem.CreateInstance();
            itemCell.isShowNum = false;
            itemCell.itemId = itemList[i];
            itemCell.RefreshView();
            m_window.m_rewardList.AddChild(itemCell);
        }
        
        

    }

    private void _OnSaoDangClick()
    {
        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(m_actId);
        if (actInfo.attackNum >= m_maxCount)
        {
            TipWindow.Singleton.ShowTip("关卡挑战次数不足");
            return;
        }
        if (LevelService.Singleton.CheckCanDo(m_actId, 1))
        {
            LevelService.Singleton.SingleActSweep(m_actId, 1);
        }
            
    }

    private void _OnChallangeClick()
    {
        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(m_actId);
        if (actInfo.attackNum >= m_maxCount)
        {
            TipWindow.Singleton.ShowTip("关卡挑战次数不足");
            return;
        }
        if (LevelService.Singleton.CheckCanDo(m_actId, 1, false))
        {
            LevelService.Singleton.ReqFightStart(m_actId);
            Close();
        }
         
    }

    protected override void OnClose()
    {
        base.OnClose();
        //topRoleInfo.OnWindowClose();
    }
}


