
using UI_Arena;
using Message.Role;
using Message.Arena;
using UnityEngine;
using FairyGUI;
using Data.Beans;
using System.Collections.Generic;

public class ArenaRuleWindow : BaseWindow
{
    private UI_ArenaRuleWindow m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ArenaRuleWindow>();
        m_window.m_btnClose.onClick.Add(Close);
        InitView();


    }

    public override void InitView()
    {
        base.InitView();

        UI_ruleCell cell1 = UI_ruleCell.CreateInstance();
        cell1.m_txtTitle.text = "战斗规则";
        m_window.m_txtList.AddChild(cell1);

        string strDes = ""; 
        t_languageBean bean  = ConfigBean.GetBean<t_languageBean, int>(61701019);
        if (bean != null)
        {
            strDes = bean.t_content;
        }
        m_window.m_txtList.AddChild(_GetTxtCell(strDes));

        UI_ruleCell cell2 = UI_ruleCell.CreateInstance();         
        cell2.m_txtTitle.text = "最高排名奖励";
        m_window.m_txtList.AddChild(cell2);

        bean = ConfigBean.GetBean<t_languageBean, int>(61701020);
        if (bean != null)
        {
            strDes = bean.t_content;
        }
        m_window.m_txtList.AddChild(_GetTxtCell(strDes));

    }

    private GObject _GetTxtCell(string str)
    {

        GTextField aTextField = new GTextField();
        aTextField.SetSize(100, 100);
        aTextField.text = str;

        TextFormat tf = aTextField.textFormat;
        tf.color = Color.white;
        tf.size = 20;
        aTextField.textFormat = tf;

        return aTextField;
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}