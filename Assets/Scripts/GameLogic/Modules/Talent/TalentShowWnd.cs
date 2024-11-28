using Data.Beans;
using FairyGUI;
using UI_Talent;
using Message.Talent;
using Message.Role;
using System.Collections.Generic;
using System;
using UnityEngine;


public class TalentShowWnd : BaseWindow
{
    private UI_TalentShowWnd m_window;
    private int m_talentId;                   //天赋ID

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_TalentShowWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        //m_window.m_btnLevelUp.onClick.Add(_OnLevelUpClick);
        m_window.m_imgBg.onClick.Add(Close);
        m_talentId = (int)Info.param;
        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.TalentLevelUp, _OnTalentLevelUp);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.TalentLevelUp, _OnTalentLevelUp);
    }

    private void _OnTalentLevelUp(GameEvent evt)
    {
        TwoParam<int, int> param = evt.Data as TwoParam<int, int>;
        if (param == null || param.value2 != m_talentId)
        {
            return;
        }

        InitView();
    }


    public override void InitView()
    {
        base.InitView();

        t_talentBean talentBean = ConfigBean.GetBean<t_talentBean, int>(m_talentId);
        if (talentBean == null)
            return;

        int talentLevel = TalentService.Singleton.GetTalentLevel(m_talentId);
        //m_window.m_imgIcon.url = PathEnum.Icons + talentBean.t_icon;
        UIGloader.SetUrl(m_window.m_imgIcon, talentBean.t_icon);
        m_window.m_imgIcon.grayed = talentLevel < 0;
        m_window.m_txtTalentName.text = talentBean.t_name;
        m_window.m_txtTalentLevel.text = talentLevel < 0 ? "未开启" : string.Format("{0}/{1}", talentLevel, talentBean.t_level_max);
        m_window.m_txtDes.text = _GetTalentDes() ;


        if (talentLevel < 0)
        {
            //未解锁
            m_window.m_openConditionGroup.visible = true;
            m_window.m_maxLevelGroup.visible = false;
            m_window.m_levelUpGroup.visible = false;

            int page = m_talentId / 100;
            TalentPage info = TalentService.Singleton.GetTalentPageInfo(page);
            if (info == null)
            {
                //天赋页都没解锁
                m_window.m_txtUnLockDes.text = UIUtils.GetStrByLanguageID(70802506);
            }
            else
            {
                _ShowUnLockDes(talentBean);
            }
        }
        else
        {
            m_window.m_openConditionGroup.visible = false;

            if (talentLevel == talentBean.t_level_max)
            {
                //已满级
                m_window.m_maxLevelGroup.visible = true;
                m_window.m_levelUpGroup.visible = false;
            }
            else
            {
                m_window.m_maxLevelGroup.visible = false;
                m_window.m_levelUpGroup.visible = true;
                long talentNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.TALENT);
                long coinNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.GOLD);
                int needMoney = _GetCurLevelUpNeedMoney(talentLevel);
                int needTalent = _GetCurLevelUpNeedTalent(talentLevel);
                m_window.m_txtCoinNum.text = needMoney + "";
                m_window.m_txtTalentNum.text = needTalent + "";
                m_window.m_txtCoinNum.color = coinNum >= needMoney ? Color.white : Color.red;
                m_window.m_txtTalentNum.color = talentNum >= needTalent ? Color.white : Color.red;
                m_window.m_btnLevelUp.grayed = (coinNum < needMoney || talentNum < needTalent);

                m_window.m_btnLevelUp.onClick.Clear();
                m_window.m_btnLevelUp.onClick.Add(() =>
                {
                    TalentService.Singleton.ReqLevel(m_talentId);
                });
            }                     
        }

    }

    //当前等级升级需要的金币
    private int _GetCurLevelUpNeedMoney(int level)
    {
        t_talentBean talentBean = ConfigBean.GetBean<t_talentBean, int>(m_talentId);
        if (talentBean == null)
            return -1;

        int[] arr = GTools.splitStringToIntArray(talentBean.t_money, '+');
        if (arr != null && level < arr.Length)
        {
            return arr[level];
        }

        return -1;
    }


    //当前等级升级需要的天赋点
    private int _GetCurLevelUpNeedTalent(int level)
    {
        t_talentBean talentBean = ConfigBean.GetBean<t_talentBean, int>(m_talentId);
        if (talentBean == null)
            return -1;

        int[] arr = GTools.splitStringToIntArray(talentBean.t_point, '+');
        if (arr != null && level < arr.Length)
        {
            return arr[level];
        }

        return -1;
    }

    //显示解锁描述
    private void _ShowUnLockDes(t_talentBean talentBean)
    {
        t_talentBean preTalentBean = ConfigBean.GetBean<t_talentBean, int>(talentBean.t_befor);
        int preTalentLevel = TalentService.Singleton.GetTalentLevel(talentBean.t_befor);
        preTalentLevel = preTalentLevel == -1 ? 0 : preTalentLevel;
        int roleLevel = RoleService.Singleton.GetRoleInfo().level;

        if (preTalentBean != null)
        {
            string strLevel = "";
            if (roleLevel >= talentBean.t_open_level)
            {
                strLevel = string.Format(UIUtils.GetStrByLanguageID(70802507), talentBean.t_open_level);
            }
            else
            {
                strLevel = string.Format(UIUtils.GetStrByLanguageID(70802508), talentBean.t_open_level, roleLevel, talentBean.t_open_level);
            }

            string strTalentLevel = "";
            if (preTalentLevel >= talentBean.t_befor_point)
            {
                strTalentLevel = string.Format(UIUtils.GetStrByLanguageID(70802509), preTalentBean.t_name, talentBean.t_befor_point);
            }
            else
            {
                strTalentLevel = string.Format(UIUtils.GetStrByLanguageID(70802510), preTalentBean.t_name, talentBean.t_befor_point, preTalentLevel, talentBean.t_befor_point);
            }

            if (talentBean.t_befor_point == 0)
                strTalentLevel = "";

            m_window.m_txtUnLockDes.text = string.Format("{0}\n{1}", strLevel, strTalentLevel);
                
        }
    }

    //获得天赋描述
    private string _GetTalentDes()
    {
        t_talentBean talentBean = ConfigBean.GetBean<t_talentBean, int>(m_talentId);
        if (talentBean == null)
            return "";

        string []propertyArr = GTools.splitString(talentBean.t_property, ';');
        int[] conditionArr = GTools.splitStringToIntArray(talentBean.t_condition, ';');
        if (propertyArr.Length != conditionArr.Length)
        {
            Debug.LogError("天赋所加属性与加成范围数量不一致");
            return "";
        }

        int curTalentLevel = TalentService.Singleton.GetTalentLevel(m_talentId);
        if (curTalentLevel <= 0)
            curTalentLevel = 1;

        string strTotal = "";
        for (int i = 0; i < propertyArr.Length; i++)
        {

            int[] iArr = GTools.splitStringToIntArray(propertyArr[i], '+');
            if (iArr.Length < 3)
                continue;
            int condition = conditionArr[i];
            int pos = condition / 100;
            int type = (condition % 100) / 10;
            int aren = condition % 10;

            string strPos = "";
            if (pos == 1)
                strPos = UIUtils.GetStrByLanguageID(70802511);
            else if (pos == 2)
                strPos = UIUtils.GetStrByLanguageID(70802512);

            string strType = "";
            if (type == 1)
                strType = UIUtils.GetStrByLanguageID(70802513);
            else if (type == 2)
                strType = UIUtils.GetStrByLanguageID(70802514);
            else if (type == 3)
                strType = UIUtils.GetStrByLanguageID(70802515);

            string strArena = "";
            if (aren == 1)
                strArena = UIUtils.GetStrByLanguageID(70802516);
            else if (aren == 2)
                strArena = UIUtils.GetStrByLanguageID(70802517);
            else if (aren == 3)
                strArena = UIUtils.GetStrByLanguageID(70802518);

            string propertyName = ConfigBean.GetBean<t_attr_nameBean, int>(iArr[0]).t_name_id;
            string addValue = "";
            if (iArr[1] == 2)
            {
                addValue = "+" + (iArr[2] * curTalentLevel);
            }
            else if (iArr[1] == 3)
            {
                addValue = "+" + ((iArr[2] * curTalentLevel) / 100.0f) + "%";
            }
            else
            {
                Debug.Log("不存在的属性类型" + iArr[1]);
            }

            string strDes = strArena + strPos + strType + UIUtils.GetStrByLanguageID(70802519) + propertyName + addValue;
            strTotal = string.Format("\n{0}\n{1}", strTotal, strDes);
        }

        return strTotal;
    }

    protected override void OnClose()
    {
        base.OnClose();
    }

}