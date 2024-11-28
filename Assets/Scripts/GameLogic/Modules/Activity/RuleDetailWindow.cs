using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;

public class RuleDetailWindow : BaseWindow {

    UI_RuleDetailWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_RuleDetailWindow>();
        window.m_popupView.m_forkBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);

        InitView();
        PlayPopupAnim(window.m_mask, window.m_popupView);
    }

    public override void InitView()
    {
        base.InitView();

        int openTimeID = 0;
        int ruleID1 = 0;
        int ruleID2 = 0;
        int ruleID3 = 0;
        int typeID = 0;

        switch (ChallegeService.Singleton.activityType)
        {
            case ActivityType.Gold:
                openTimeID = 71802006;
                ruleID1 = 71802008;
                ruleID2 = 71802009;
                ruleID3 = 71802010;
                typeID = 71802004;
                break;
            case ActivityType.Exp:
                openTimeID = 71802006;
                ruleID1 = 71802017;
                ruleID2 = 71802018;
                typeID = 71802019;
                break;
            case ActivityType.NvGeDouJia:
                openTimeID = 71802011;
                ruleID1 = 71802012;
                ruleID2 = 71802013;
                ruleID3 = 71802014;
                typeID = 71802020;
                break;
            case ActivityType.HuanXiang:
                openTimeID = 71802015;
                ruleID1 = 71802012;
                ruleID2 = 71802016;
                ruleID3 = 71802014;
                typeID = 71802021;
                break;
            default:
                break;
        }
        string openTimeStr = "";
        string ruleStr1 = "";
        string ruleStr2 = "";
        string ruleStr3 = "";
        if(openTimeID != 0)
            openTimeStr = UIUtils.GetStrByLanguageID(openTimeID);
        if (ruleID1 != 0)
            ruleStr1 = UIUtils.GetStrByLanguageID(ruleID1);
        if (ruleID2 != 0)
            ruleStr2 = UIUtils.GetStrByLanguageID(ruleID2);
        if (ruleID3 != 0)
            ruleStr3 = UIUtils.GetStrByLanguageID(ruleID3);

        window.m_popupView.m_specialRuleLabel.text = string.Format("{0}\n{1}\n{2}", ruleStr1, ruleStr2, ruleStr3);
        window.m_popupView.m_openTimeLabel.text = openTimeStr;

        window.m_popupView.m_typeLabel.text = UIUtils.GetStrByLanguageID(typeID);
        window.m_popupView.m_openTimeTipLabel.text = UIUtils.GetStrByLanguageID(71802005);
        window.m_popupView.m_specialRuleTipLabel.text = UIUtils.GetStrByLanguageID(71802007);
        
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
