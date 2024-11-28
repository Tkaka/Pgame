using Message.Guild;
using UI_Guild;
using Data.Beans;
using UnityEngine;
using System.Collections.Generic;

public class DonatePageWnd : BaseWindow
{
    private UI_DonatePageWnd m_window;
    private int m_bigType;              //当前捐献类型
    private ResDonate m_msg;
    private int m_maxDonateNum = ConfigBean.GetBean<t_globalBean, int>(1601013).t_int_param;
    private Dictionary<int, string> m_donateNameDic = new Dictionary<int, string>();
    private Dictionary<int, string> m_donateDesDic = new Dictionary<int, string>();
    private Dictionary<int, string> m_donateIconDic = new Dictionary<int, string>();
    private Dictionary<int, int> m_openLevelDic = new Dictionary<int, int>();
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_DonatePageWnd>();
        m_window.m_btnClose.onClick.Add(Close);

        m_bigType = (int)Info.param;


        GuildService.Singleton.ReqOpenDonate(m_bigType);
        InitView();
        _Init();
    }


    private void _Init()
    {
        t_donate_typeBean donateTypeBean = ConfigBean.GetBean<t_donate_typeBean, int>(m_bigType);
        if (donateTypeBean == null)
            return;

        int[] arrName = GTools.splitStringToIntArray(donateTypeBean.t_detail_name, '+');
        int[] arrDes = GTools.splitStringToIntArray(donateTypeBean.t_detail_desc, '+');
        string[] arrIcon = GTools.splitString(donateTypeBean.t_detail_icon, '+');
        int[] arrOpenLevel = GTools.splitStringToIntArray(donateTypeBean.t_detai_level, '+');
        int[] arrId = GTools.splitStringToIntArray(donateTypeBean.t_content, '+');

        for (int i = 0; i < arrId.Length; i++)
        {
            int id = arrId[i];
            if (i < arrName.Length)
            {
                m_donateNameDic.Add(id, UIUtils.GetStrByLanguageID(arrName[i]));
            }

            if (i < arrDes.Length)
            {
                m_donateDesDic.Add(id, UIUtils.GetStrByLanguageID(arrDes[i]));
            }

            if (i < arrOpenLevel.Length)
            {
                m_openLevelDic.Add(id, arrOpenLevel[i]);
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

    private void _OnGuildDonateInfoRefresh(GameEvent evt)
    {
        ResDonate msg = evt.Data as ResDonate;
        if (msg == null)
            return;

        m_msg = msg;
        _ShowDetailInfo();

    }

    private void _ShowPageBaseInfo()
    {
        t_donate_typeBean donateTypeBean = ConfigBean.GetBean<t_donate_typeBean, int>(m_bigType);
        if (donateTypeBean == null)
            return;

        UIGloader.SetUrl(m_window.m_imgTitle, donateTypeBean.t_title_icon);
        m_window.m_txtShuoMing.text = donateTypeBean.t_desc;
    }

    private void _ShowDetailInfo()
    {
        m_window.m_donateList.RemoveChildren(0, -1, true);
        for (int i = 0; i < m_msg.donateInfos.Count; i++)
        {
            DonateInfo donateInfo = m_msg.donateInfos[i];
            UI_objDonatePageCell cell = UI_objDonatePageCell.CreateInstance();

            int id = donateInfo.target * 1000 + donateInfo.level;
            t_donateBean donateBean = ConfigBean.GetBean<t_donateBean, int>(id);
            if (donateBean == null)
                continue;

            if (m_openLevelDic.ContainsKey(donateInfo.target))
            {
                int guildLevel = GuildService.Singleton.GetGuildInfo().level;
                cell.m_openGroup.visible = guildLevel >= m_openLevelDic[donateInfo.target];
                cell.m_objNoOpen.visible = guildLevel < m_openLevelDic[donateInfo.target];
            }

            cell.m_btnDonate.grayed = m_msg.num >= m_maxDonateNum;
            if (m_donateIconDic.ContainsKey(donateInfo.target))
            {
                UIGloader.SetUrl(cell.m_imgIcon, m_donateIconDic[donateInfo.target]);
            }
            
            if (donateBean.t_exp != 0)
            {
                cell.m_progressBar.value = (1.0f * donateInfo.exp / donateBean.t_exp) * 100;
                cell.m_progressBar.m_txtProgressBar.text = string.Format("{0}/{1}", donateInfo.exp, donateBean.t_exp);
                if (donateInfo.exp >= donateBean.t_exp)
                {
                    //已满级
                    cell.m_progressBar.m_txtProgressBar.text = "Max";
                }
            }

            if (m_donateNameDic.ContainsKey(donateInfo.target))
            {
                cell.m_txtName.text = m_donateNameDic[donateInfo.target];
            }

            if (m_donateDesDic.ContainsKey(donateInfo.target))
            {
                cell.m_txtCurLevelDes.text = string.Format(m_donateDesDic[donateInfo.target], GTools.splitStringToIntArray(donateBean.t_param, '+'));
            }

            cell.m_txtLevel.text = string.Format("Lv.{0}", donateInfo.level);

             
            if (donateInfo.exp >= donateBean.t_exp)
            {
                //已满级没有下一级
                cell.m_txtNextLevelDes.text = "Max";
            }
            else
            {
                t_donateBean nextLeveDonateBean = ConfigBean.GetBean<t_donateBean, int>(id + 1);
                if (nextLeveDonateBean != null && m_donateDesDic.ContainsKey(donateInfo.target))
                {
                    cell.m_txtNextLevelDes.text = string.Format(m_donateDesDic[donateInfo.target], GTools.splitStringToIntArray(nextLeveDonateBean.t_param, '+'));
                }
            }

            m_window.m_donateList.AddChild(cell);

            cell.m_btnDonate.onClick.Add(()=> {
                if (donateInfo.exp >= donateBean.t_exp)
                {
                    TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(61601041));
                    return;
                }

                if (m_msg.num >= m_maxDonateNum)
                {
                    TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(61603003));
                    return;
                }

                TwoParam<DonateInfo, ResDonate> twoParam = new TwoParam<DonateInfo, ResDonate>();
                twoParam.value1 = donateInfo;
                twoParam.value2 = m_msg;
                WinMgr.Singleton.Open<DonateRewardWnd>(WinInfo.Create(false, null, true, twoParam), UILayer.Popup);
            });
 

        }
    }

    public override void InitView()
    {
        base.InitView();
        _ShowPageBaseInfo();
    }

    protected override void OnClose()
    {
        base.OnClose();
        m_donateIconDic.Clear();
        m_donateNameDic.Clear();
        m_openLevelDic.Clear();
        m_donateDesDic.Clear();
    }
}