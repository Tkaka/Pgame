using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using Data.Beans;

public class CommonHeadIcon : UI_commonHeadIcon {
    private bool isGet = false;
    System.Action<object> funcCall;
    private object param;
    public int headID;
    public new static CommonHeadIcon CreateInstance()
    {
        return UI_commonHeadIcon.CreateInstance() as CommonHeadIcon;
    }

    public void Init(int headID, System.Action<object> funcCall = null, bool isGet = true, object param = null)
    {
        this.isGet = isGet;
        this.funcCall = funcCall;
        this.param = param;
        this.headID = headID;
        m_toucher.onClick.Add(OnClickItem);
        RefreshView();
    }

    public void RefreshView()
    {
        this.grayed = !isGet;
        t_headBean headBean = ConfigBean.GetBean<t_headBean, int>(headID);
        UIGloader.SetUrl(m_headIcon, headBean.t_icon);
    }

    private void OnClickItem()
    {
        if (funcCall != null)
        {
            t_headBean headBean = ConfigBean.GetBean<t_headBean, int>(headID);
            param = param == null ? headBean : param;
            funcCall(param);
        }
    }
}
