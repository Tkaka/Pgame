using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_MainCity;

public class FuncTipWindow : BaseWindow
{
    private UI_FuncTipWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_FuncTipWindow>();

        window.m_t2.Play();

        int curFunc = FuncService.Singleton.curTipFuncID;
        var bean = ConfigBean.GetBean<Data.Beans.t_moduleBean, int>(curFunc);
        if (bean == null)
        {
            Close();
            return;
        }

        window.m_tip.m_name.text = bean.t_name;
        window.m_tip.m_desc1.text = bean.t_desc1;
        window.m_tip.m_desc2.text = string.Format(bean.t_desc2, FuncService.Singleton.GetFuncCondition(curFunc));

        window.m_tip.m_close.onClick.Add(Close);
    }
}
