
using FairyGUI;
using UnityEngine;
using UI_Battle;

public class ComboTip
{

    public ComboTip(Vector3 pos, ComboType comboType)
    {
        FightManager.ComboCount++;
//        Debuger.Err("comboadd -- " + FightManager.ComboAdd.raw);
        FightManager.ComboAdd += GetAddVal(comboType);
//        Debuger.Err("comboadd af -- " + FightManager.ComboAdd.raw);
        BattleWindMgr.CurrentBtlWin.PlayComboAni();

        if (comboType == ComboType.Normal)
        {
            UI_ComboEvaluation_white view = UI_ComboEvaluation_white.CreateInstance();
            view.SetXY(pos.x - view.actualWidth / 2, pos.y - view.actualHeight / 2);
            view.m_GO.Play(() =>
            {
                view.Dispose();
            });
            WinMgr.Singleton.NoticeLayer.AddChild(view);
            BattleStatistics.Singleton.normalCount++;
        }
        else if (comboType == ComboType.NotBad)
        {
            UI_ComboEvaluation_blue view = UI_ComboEvaluation_blue.CreateInstance();
            view.SetXY(pos.x - view.actualWidth / 2, pos.y - view.actualHeight / 2);
            view.m_GO.Play(() =>
            {
                view.Dispose();
            });
            WinMgr.Singleton.NoticeLayer.AddChild(view);
            BattleStatistics.Singleton.notBadCount++;
        }
        else if (comboType == ComboType.Good)
        {
            UI_ComboEvaluation_zi view = UI_ComboEvaluation_zi.CreateInstance();
            view.SetXY(pos.x - view.actualWidth / 2, pos.y - view.actualHeight / 2);
            view.m_GO.Play(() =>
            {
                view.Dispose();
            });
            WinMgr.Singleton.NoticeLayer.AddChild(view);
            BattleStatistics.Singleton.goodCount++;
        }
        else if (comboType == ComboType.Perfect)
        {
            GameObject obj = FightManager.R.LoadGo("eff_ui_comboevaluation_prefect");
            GoWrapper go = new GoWrapper(obj);
            GGraph gg = new GGraph();
            gg.SetXY(pos.x, pos.y);
            gg.SetNativeObject(go);
            WinMgr.Singleton.NoticeLayer.AddChild(gg);
            CoroutineManager.Singleton.delayedCall(2.0f, () =>
            {
                gg.Dispose();
            });
            BattleStatistics.Singleton.perfectCount++;
        }
    }

    private LNumber GetAddVal(ComboType type)
    {
        if(FightManager.Singleton.IsReplay)
            return LNumber.Create(0, 0);


        switch (type)
        {
            case ComboType.Normal:
                return 0;
            case ComboType.NotBad:
                return LNumber.Create(0, 1 * 10000 / 10);// 1/(LNumber)10;
            case ComboType.Good:
                return LNumber.Create(0, 15 * 10000 / 100);//15/(LNumber)100;
            case ComboType.Perfect:
                return LNumber.Create(0, 20 * 10000 / 10);//2/(LNumber)10;
        }
        Logger.err("无法识别的Combo类型");
        return 0;
    }


    private string GetImgName(ComboType type)
    {
        switch (type)
        {
            case ComboType.Normal:
                return "yiban";
            case ComboType.NotBad:
                return "bucuo";
            case ComboType.Good:
                return "henbang";
            case ComboType.Perfect:
                return "wanmei";
        }
        Logger.err("无法识别的Combo类型");
        return "";
    }


}