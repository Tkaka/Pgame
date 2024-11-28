using FairyGUI;
using UI_Battle;

public class EvaluateTip 
{

    private UI_EvaluateTip view;

    public EvaluateTip(UI_EvaluateTip view)
    {
        this.view = view;
    }

    public void Show(long hurt)
    {
        view.visible = true;
        view.m_hurtTxt.text = hurt + "";
        UIGloader.SetUrl(view.m_levelImg, GetEvaluate());
        Timers.inst.Remove(Hide);
        Timers.inst.Add(FightManager.turnGap, 1, Hide);
    }

    private string GetEvaluate()
    {
        int num = FightManager.Singleton.Grid.AliveNum(ActorCamp.CampFriend);
        if (num > 0)
        {
            float average = (FightManager.ComboAdd -1) / num;
            if (average < 0.1f)
            {
                return "ui://" + WinEnum.UI_Battle + "/yiban";
            }
            else if (average >= 0.1f && average < 0.15f)
            {
                return "ui://" + WinEnum.UI_Battle + "/henhao";
            }
            else if (average >= 0.15f && average < 0.2f)
            {
                return "ui://" + WinEnum.UI_Battle + "/henwanmei";
            }
            else if (average >= 0.2f)
            {
                return "ui://" + WinEnum.UI_Battle + "/feichangwanmei";
            }
        }
        return "ui://" + WinEnum.UI_Battle + "/yiban";
    }

    public void Hide(object param = null)
    {
        view.visible = false;
    }

}
