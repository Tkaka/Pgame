using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;


public class ResolveFastSelectWnd : BaseWindow
{
    private Action<object> m_callBack;
    private Dictionary<int, bool> m_quilityDic = new Dictionary<int, bool>();
    private UI_ResolveFastSelectWnd m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ResolveFastSelectWnd>();
        m_window.m_close.onClick.Add(Close);
        m_window.m_centerYes.onClick.Add(_OnOkClick);

        m_window.m_checkBai.onChanged.Add(()=> { _QuilitySelect(0, m_window.m_checkBai.selected); });
        m_window.m_checkLv.onChanged.Add(() => { _QuilitySelect(1, m_window.m_checkLv.selected); });
        m_window.m_checkLan.onChanged.Add(() => { _QuilitySelect(2, m_window.m_checkLan.selected); });
        m_window.m_checkZi.onChanged.Add(() => { _QuilitySelect(3, m_window.m_checkZi.selected); });

        OneParam<Action<object>> param = Info.param as OneParam<Action<object>>;
        if (param == null)
        {
            Close();
            return;
        }
        m_callBack = param.value;
    }


    private void _QuilitySelect(int quility, bool isSelect)
    {
        if (m_quilityDic.ContainsKey(quility))
        {
            m_quilityDic[quility] = isSelect;
        }
        else
        {
            m_quilityDic.Add(quility, isSelect);
        }
    }

    private void _OnOkClick()
    {
        if (m_callBack != null)
            m_callBack(m_quilityDic);

        Close();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }

}