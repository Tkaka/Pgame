//Í¨ÓÃÌáÊ¾¹ÜÀíÀà
using System;

public enum TipsType 
{
    SingleButton = 1,   //µ¥°´Å¥
    DoubleButton = 2,   //Ë«°´Å¥
}

public class CommonTipsManager
{   
    private static CommonTipsManager m_instance;
    public static CommonTipsManager GetInstance()
    {
        if (m_instance == null)
            m_instance = new CommonTipsManager();

        return m_instance;
    }

    public void ShowTips(TipsType type, string strTitle = "", string strDes = "", Action callBack = null)
    {
        switch (type)
        {
            case TipsType.SingleButton:
                //ThreeParam<string, string, Action> threeParam = new ThreeParam<string, string, Action>();
                //threeParam.value1 = strTitle;
                //threeParam.value2 = strDes;
                //threeParam.value3 = () => {
                //    if (callBack != null)
                //    {
                //        callBack(callBack);
                //    }
                //};
                 
                //WinMgr.Singleton.Open<SingleBtnCofirmWindow>(WinInfo.Create(false, null, false, threeParam), UILayer.Popup);
                AgainConfirmWindow.Singleton.TipOneButton(strDes, callBack, null , false);
                break;
            case TipsType.DoubleButton:
                AgainConfirmWindow.Singleton.ShowTip(strDes, callBack);
                break;
        }
    }

}