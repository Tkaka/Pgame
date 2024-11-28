using UI_Common;
using System;

public class SingleBtnCofirmWindow : BaseWindow
{

    private UI_SingleBtnCofirmWindow m_window;
    private Action fun;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SingleBtnCofirmWindow>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnComfirm.onClick.Add(_OnComfirmClick);
        _ShowTxt();
    }


    private void _ShowTxt()
    {
        ThreeParam<string, string, Action> threeParam = Info.param as ThreeParam<string, string, Action>;
        m_window.m_txtTitle.text = threeParam.value1;
        m_window.m_txtDescribe.text = threeParam.value2;
        fun = threeParam.value3;
    }

    private void _OnComfirmClick()
    {
        if (fun != null)
            fun();
        Close();
    }

}


