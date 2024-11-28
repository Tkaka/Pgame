using FairyGUI;
using UI_MainCity;

public class GMWindow : BaseWindow
{

    private UI_GMWindow window;
    private string[] items;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_GMWindow>();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window.m_okBtn.onClick.Add(OnOkBtn);
        window.m_cancelBtn.onClick.Add(OnCloseBtn);
        items = window.m_dropDown.items;
    }

    private void OnOkBtn()
    {
        string[] arrStrInput = GTools.splitString(window.m_txtCmdInput.text, ' ');
        if (arrStrInput == null || arrStrInput.Length < 1)
        {
            int id = window.m_dropDown.selectedIndex;
            string param = window.m_cmdTxt.text;
            if (id < items.Length && !string.IsNullOrEmpty(param))
            {
                GMService.Singleton.ReqGm(items[id], param);
            }
            else
            {
                Logger.err("GMWindow:OnOkBtn:命令为空");
            }
        }
        else
        {
            string strParam = window.m_txtCmdInput.text;
            strParam = strParam.Replace(string.Format("{0} ", arrStrInput[0]), "");
            GMService.Singleton.ReqGm(arrStrInput[0], strParam);
        }
  
 

 
    }

}
