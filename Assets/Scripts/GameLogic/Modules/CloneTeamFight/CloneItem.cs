using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;
using Data.Beans;
using FairyGUI;

public class CloneItem : UI_cloneItem {

    public int petID;
    public int index;

    CloneMainWindow parentUI;
    ResPack resPack;
    public new static CloneItem CreateInstance()
    {
        return UI_cloneItem.CreateInstance() as CloneItem;
    }

    public void InitView(CloneMainWindow parentUI)
    {
        this.parentUI = parentUI;

        m_normalCreateTeamBtn.onClick.Add(OnCreateTeamBtnClick);
        m_noramlQucikJoinBtn.onClick.Add(OnQucikJoinBtnClick);
        m_cardCreateTeamBtn.onClick.Add(OnCreateTeamBtnClick);
        m_cardQucikJoinBtn.onClick.Add(OnQucikJoinBtnClick);

        InitBaseInfo();
        InitModel();
        RefreshView(false);
    }

    private void InitBaseInfo()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if (petBean != null)
            m_nameLabel.text = string.Format("克隆人{0}", UIUtils.GetPetName(petBean));

        m_indexNum.text = string.Format("0{0}", index);
    }

    private void InitModel()
    {
        string modelName = UIUtils.GetPetStartModel(petID);
        resPack = new ResPack(this);
        GameObject model = resPack.LoadGo(modelName);
        model.transform.localPosition = new Vector3(0, 0, 500);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        model.transform.localScale = new Vector3(100, 100, 100);

        m_modelPos.SetNativeObject(new GoWrapper(model));
        model.setLayer("UIActor");
    }

    public void RefreshView(bool isSelect)
    {
        
        if (isSelect)
        {
            m_bubbleGroup.visible = true;
            m_bubbleLabel.text = "玩就送屠龙宝刀";

            if (IsHaveThisPet())
            {
                m_normalBtnGroup.visible = true;
                m_noramlQucikJoinBtn.grayed = false;
                m_normalCreateTeamBtn.grayed = false;
                m_cardBtnGroup.visible = false;
            }
            else
            {
                if(IsHaveInviteCard())
                {
                    m_normalBtnGroup.visible = false;
                    m_cardBtnGroup.visible = true;
                }
                else
                {
                    m_normalBtnGroup.visible = true;
                    m_cardBtnGroup.visible = false;
                    m_noramlQucikJoinBtn.grayed = true;
                    m_normalCreateTeamBtn.grayed = true;
                }
            }
        }
        else
        {
            m_bubbleGroup.visible = false;
            m_normalBtnGroup.visible = false;
            m_cardBtnGroup.visible = false;
        }
    }

    private void OnCreateTeamBtnClick()
    {
        if (IsHaveThisPet())
        {
            // 请求创建队伍
            CloneTeamFightService.Singleton.ReqTeamFightEnterTeam(false, petID);
        }
        else
        {
            if (IsHaveInviteCard())
            {
                // 请求创建队伍
                CloneTeamFightService.Singleton.ReqTeamFightEnterTeam(false, petID);
            }
            else
            {
                // 71803006   还没有对应格斗家，不能组建讨伐队伍
                TipWindow.Singleton.ShowTip(71803006);
            }
        }

        parentUI.curSelectType = CloneTeamJoinType.CreatTeam;
    }

    private void OnQucikJoinBtnClick()
    {
        if (IsHaveThisPet())
        {
            // 请求创建队伍
            CloneTeamFightService.Singleton.ReqTeamFightEnterTeam(true, petID);
        }
        else
        {
            if (IsHaveInviteCard())
            {
                // 请求创建队伍
                CloneTeamFightService.Singleton.ReqTeamFightEnterTeam(true, petID);
            }
            else
            {
                // 71803007 需要对应拥有格斗家才能组建讨伐队伍
                TipWindow.Singleton.ShowTip(71803007);
            }
        }

        parentUI.curSelectType = CloneTeamJoinType.QuickJoin;
    }

    #region 数据处理 *****************************************************************************************************
    /// <summary>
    /// 是否有这个宠物
    /// </summary>
    /// <returns></returns>
    private bool IsHaveThisPet()
    {
        Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetByID(petID);
        return petInfo != null;
    }
    /// <summary>
    /// 是否拥有邀请卡
    /// </summary>
    /// <returns></returns>
    private bool IsHaveInviteCard()
    {
        int num = parentUI.GetCloneInviteCardNum();
        return num > 0;
    }
    #endregion;
    public override void Dispose()
    {
        resPack.ReleaseAllRes();
        resPack = null;

        base.Dispose();
    }
}
