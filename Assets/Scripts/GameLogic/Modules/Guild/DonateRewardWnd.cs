using Message.Guild;
using UI_Guild;
using Data.Beans;
using UnityEngine;
using System.Collections.Generic;


public class DonateRewardWnd : BaseWindow
{
    private UI_DonateRewardWnd m_window;
    private DonateInfo m_donateInfo;
    private ResDonate m_msg;
    private int m_maxDonateNum = ConfigBean.GetBean<t_globalBean, int>(1601013).t_int_param;
    //private int m_curDonateNum = 0; 
    private Dictionary<int, string> m_donateNameDic = new Dictionary<int, string>();
    private Dictionary<int, string> m_donateIconDic = new Dictionary<int, string>();

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_DonateRewardWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        TwoParam<DonateInfo, ResDonate> twoParam = Info.param as TwoParam<DonateInfo, ResDonate>;
        if (twoParam == null)
            return;
        m_donateInfo = twoParam.value1;
        m_msg = twoParam.value2;
        _Init();

        InitView();
    }

    private void _Init()
    {
        if (m_msg.bigType == (int)GuildService.EDonateBigType.Guild)
            return;

        t_donate_typeBean donateTypeBean = ConfigBean.GetBean<t_donate_typeBean, int>(m_msg.bigType);
        if (donateTypeBean == null)
            return;

        int[] arrName = GTools.splitStringToIntArray(donateTypeBean.t_detail_name, '+');
        string[] arrIcon = GTools.splitString(donateTypeBean.t_detail_icon, '+');
        int[] arrId = GTools.splitStringToIntArray(donateTypeBean.t_content, '+');

        for (int i = 0; i < arrId.Length; i++)
        {
            int id = arrId[i];
            if (i < arrName.Length)
            {
                m_donateNameDic.Add(id, UIUtils.GetStrByLanguageID(arrName[i]));
            }

            if (arrIcon != null && i < arrIcon.Length)
            {
                m_donateIconDic.Add(id, arrIcon[i]);
            }
        }

    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildDonateInfo, _OnGuildDonateInfoRefresh);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildDonateInfo, _OnGuildDonateInfoRefresh);
    }

    public override void InitView()
    {
        base.InitView();
        _ShowDonateInfo();
        _ShowDonateList();
    }


    private void _OnGuildDonateInfoRefresh(GameEvent evt)
    {
        ResDonate msg = evt.Data as ResDonate;
        if (msg == null)
            return;
        
        if(msg.bigType != m_msg.bigType)
            return;

        m_msg = msg;
        for (int i = 0; i < m_msg.donateInfos.Count; i++)
        {
            if (m_msg.donateInfos[i].target == m_donateInfo.target)
            {
                m_donateInfo = m_msg.donateInfos[i];
                break;
            }
        }

        InitView();
    }

    private void _ShowDonateInfo()
    {
        if (m_donateInfo.target == (int)GuildService.EDonateType.GuildExp)
        {
            //公会经验
            GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
            t_iconBean iconBean = ConfigBean.GetBean<t_iconBean, int>(guildInfo.badge);
            if (iconBean != null)
            {
                UIGloader.SetUrl(m_window.m_imgBadge.m_imgIcon, iconBean.t_icon);
            }

            m_window.m_txtType.text = "社团经验";
            t_guildBean guildBean = ConfigBean.GetBean<t_guildBean, int>(m_donateInfo.level);
            if (guildBean != null && guildBean.t_exp != 0)
            {
                m_window.m_totalPorgressBar.value = (1.0f * m_donateInfo.exp / guildBean.t_exp) * 100;
                m_window.m_totalPorgressBar.m_txtProgress.text = string.Format("{0}/{1}", m_donateInfo.exp, guildBean.t_exp);
                if (m_donateInfo.exp >= guildBean.t_exp)
                {
                    m_window.m_totalPorgressBar.m_txtProgress.text = "Max";
                }
            }
        }
        else
        {
            int donateId = m_donateInfo.target * 1000 + m_donateInfo.level;
            t_donateBean donateBean = ConfigBean.GetBean<t_donateBean, int>(donateId);
            if (donateBean == null)
                return;

            if (m_donateIconDic.ContainsKey(m_donateInfo.target))
            {
                UIGloader.SetUrl(m_window.m_imgBadge.m_imgIcon, m_donateIconDic[m_donateInfo.target]);
            }
            if (m_donateNameDic.ContainsKey(m_donateInfo.target))
            {
                m_window.m_txtType.text = m_donateNameDic[m_donateInfo.target];
            }
 
            if (donateBean.t_exp != 0)
            {
                m_window.m_totalPorgressBar.value = (1.0f * m_donateInfo.exp / donateBean.t_exp) * 100;
                m_window.m_totalPorgressBar.m_txtProgress.text = string.Format("{0}/{1}", m_donateInfo.exp, donateBean.t_exp);
                if (m_donateInfo.exp >= donateBean.t_exp)
                {
                    m_window.m_totalPorgressBar.m_txtProgress.text = "Max";
                }
            }
 
        }

        m_window.m_txtGuildLevel.text = string.Format("{0}级", m_donateInfo.level);
        m_window.m_txtDonateCount.text = string.Format("捐献次数:{0}", m_maxDonateNum - m_msg.num);

    }


    //显示捐献列表
    private void _ShowDonateList()
    {
        m_window.m_donateList.RemoveChildren(0, -1, true);
        int maxExp = 0;
        if (m_donateInfo.target == (int)GuildService.EDonateType.GuildExp)
        {
            t_guildBean guildBean = ConfigBean.GetBean<t_guildBean, int>(m_donateInfo.level);
            if(guildBean != null)
                maxExp = guildBean.t_exp;
        }
        else
        {
            int donateId = m_donateInfo.target * 1000 + m_donateInfo.level;
            t_donateBean donateBean = ConfigBean.GetBean<t_donateBean, int>(donateId);
            if (donateBean != null)
                maxExp = donateBean.t_exp;
        }

        List<t_donate_rewardBean> donateRewardList = ConfigBean.GetBeanList<t_donate_rewardBean>();
        for (int i = 0; i < donateRewardList.Count; i++)
        {
            t_donate_rewardBean rewardBean = donateRewardList[i];
            if (RoleService.Singleton.GetRoleInfo().level < rewardBean.t_openCondition)
                continue;

            UI_objDonateRewardCell cell = UI_objDonateRewardCell.CreateInstance();
            cell.m_btnGroup.visible = false;
            cell.m_btnDonate.visible = true;
            cell.m_btnDonate.grayed = m_msg.num >= m_maxDonateNum || m_donateInfo.exp >= maxExp;

            UIGloader.SetUrl(cell.m_imgBg, rewardBean.t_bgIocn);

            int[] arrConsumeItem = GTools.splitStringToIntArray(rewardBean.t_comsume_item, '+');
            if (arrConsumeItem != null && arrConsumeItem.Length >= 2)
            {
                UIGloader.SetUrl(cell.m_imgCoin, UIUtils.GetItemIcon(arrConsumeItem[0]));
                cell.m_txtCoinNum.text = arrConsumeItem[1] + "";
            }

            int []arrRewardNum = GTools.splitStringToIntArray(rewardBean.t_reward, '+');
            if (arrRewardNum != null && arrRewardNum.Length >= 2)
            {
                cell.m_txtExp.text = arrRewardNum[0] + "";
                cell.m_txtGuildCoin.text = arrRewardNum[1] + "";
            }

            m_window.m_donateList.AddChild(cell);
            cell.m_btnDonate.onClick.Add(() => {
                if (m_msg.num >= m_maxDonateNum)
                {
                    TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(61603003));
                    return;
                }
                if (m_donateInfo.exp >= maxExp)
                {
                    TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(61601041));
                    return;
                }
                GuildService.Singleton.ReqDonate(rewardBean.t_id, m_donateInfo.target, false, m_msg.bigType);
            });
            cell.m_btnDonate2.onClick.Add(()=> { });
            cell.m_btnDonateAll.onClick.Add(() => { });
        }
    }


    protected override void OnClose()
    {
        base.OnClose();
    }
}