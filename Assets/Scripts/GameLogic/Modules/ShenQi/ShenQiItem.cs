using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_ShenQi;
using FairyGUI;
using Message.Team;
using Data.Beans;

public class ShenQiItem :  UI_shenQiItem{

    public int shenQiID;
    private ShenQiMainWidow parentUI;

    public new static ShenQiItem CreateInstance()
    {
        return (ShenQiItem)UIPackage.CreateObject("UI_ShenQi", "shenQiItem");
    }

    public void Init(ShenQiMainWidow parentUI)
    {
        this.parentUI = parentUI;
        m_toucher.onClick.Add(OnClickItem);

        InitShenQiIcon();
        RefreshView();
    }

    private void InitShenQiIcon()
    {
        t_artifactBean artifactBean = ConfigBean.GetBean<t_artifactBean, int>(shenQiID);
        if (artifactBean != null)
        {
            UIGloader.SetUrl(m_shenQiIconLoader, artifactBean.t_icon);
        }
    }

    public void RefreshView()
    {
        bool isSelcted = IsSelected();
        bool isUnlock = IsUnlock();
        bool isNextUnlock = IsNextUnlock();

        m_showBoardIcon.visible = isUnlock || isNextUnlock;
        m_selectIcon.visible = isSelcted;
        m_wenHaoIcon.visible = !(isUnlock || isNextUnlock);
        m_lockIcon.visible = isNextUnlock;
        m_shenQiIconLoader.visible = isUnlock || isNextUnlock;
    }

    private bool IsSelected()
    {
        return shenQiID == ShenQiService.Singleton.curShenQiID;
    }

    private bool IsUnlock()
    {
        Artifact artifact = ShenQiService.Singleton.GetShenQiInfoByID(shenQiID);
        return artifact != null;
    }

    private bool IsNextUnlock()
    {
        ResArtifactInfo artifactInfo = ShenQiService.Singleton.artifactInfo;
        if (artifactInfo != null)
        {
            return artifactInfo.artifactId == shenQiID;
        }

        return false;
    }

    private void OnClickItem()
    {
        bool isUnlock = IsUnlock();
        bool isNextUnlock = IsNextUnlock();
        if (isUnlock || isNextUnlock)
        {
            ShenQiService.Singleton.curShenQiID = shenQiID;
            parentUI.OnChangeShenQiItem();
        }
        else
        {
            // TODO: 语言ID
            TipWindow.Singleton.ShowTip("请解锁上一个神器!");
        }
    }
}
