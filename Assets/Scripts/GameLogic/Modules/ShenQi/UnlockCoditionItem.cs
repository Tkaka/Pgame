using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_ShenQi;
using FairyGUI;
using Data.Beans;

public class UnlockCoditionItem : UI_unlockCoditionItem {

    public string condition;
    public int conditionID;
    public int conditionState;

    public new static UnlockCoditionItem CreateInstance()
    {
        return (UnlockCoditionItem)UIPackage.CreateObject("UI_ShenQi", "unlockCoditionItem");
    }
    public void RefreshView()
    {
        ShenQiConditionType conditionType = ShenQiConditionType.ChongWuGeShu;
        int value1 = -1;
        int value2 = -1;
        string[] conditionArr = null;
        if(!string.IsNullOrEmpty(condition))
            conditionArr = condition.Split('+');

        int length = 0;
        if (conditionArr != null)
        {
            length = conditionArr.Length;
            conditionType = (ShenQiConditionType)int.Parse(conditionArr[0]);
            if (length == 2)
                value1 = int.Parse(conditionArr[1]);
            else if(length == 3)
            {
                value1 = int.Parse(conditionArr[1]);
                value2 = int.Parse(conditionArr[2]);
            }
        }
        string conditionStr = UIUtils.GetStrByLanguageID(conditionID);
        if (length == 2)
        {
            if (conditionType == ShenQiConditionType.WanChengGuanQia)
            {
                t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(value1);
                if (actBean != null)
                {
                    t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(actBean.t_chapter_id);
                    if (chapterBean != null)
                    {
                        string levelName = string.Format(":{0}{1}", chapterBean.t_name_id, actBean.t_name_id);
                        m_conditionLabel.text = string.Format(conditionStr, levelName);
                    }
                }
            }
            else
                m_conditionLabel.text = string.Format(conditionStr, value1);
        }
        else if (length == 3)
        {
            if(conditionType == ShenQiConditionType.ChongWuPinZhi)
            {
                string colorStr = UIUtils.GetColorName(value1);
                m_conditionLabel.text = string.Format(conditionStr, colorStr, value2);
            }
            else
                m_conditionLabel.text = string.Format(conditionStr, value1, value2);
        }

        if (conditionState == -1)
        {
            // 完成
            m_reachIcon.visible = true;
            m_progressLabel.visible = false;
            m_lockIcon.visible = false;
            m_colorIcon.visible = true;
            m_conditionLabel.color = Color.green;
        }
        else
        {
            // 没完成
            m_reachIcon.visible = false;
            m_progressLabel.visible = true;
            m_lockIcon.visible = true;
            m_colorIcon.visible = false;
            m_conditionLabel.color = Color.white;

            int maxPrograssNum = 0;
            if (length == 2)
                maxPrograssNum = value1;
            else if (length == 3)
                maxPrograssNum = value2;

            if (conditionType == ShenQiConditionType.WanChengGuanQia)
                maxPrograssNum = 1;

            m_progressLabel.text = string.Format("{0}/{1}", conditionState, maxPrograssNum);
        }
    }
}
