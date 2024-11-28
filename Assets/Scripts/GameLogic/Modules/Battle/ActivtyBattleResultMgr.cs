using Message.Challenge;
using Message.Fight;
public class ActivtyBattleResultMgr
{

    protected static ActivtyBattleResultMgr mSingleton = null;

    public static ActivtyBattleResultMgr Singleton
    {
        get
        {
            if (mSingleton == null)
            {
                mSingleton = new ActivtyBattleResultMgr();
            }
            return mSingleton;
        }
    }


    public void OpenBattleResultWnd(ResFightResultInfo msg)
    {
        if (msg.result.result == 0)
        {
            BattleWindow.Singleton.OpenChild<BattleFailedWindow>(WinInfo.Create(false, null, false));
        }
        else
        {
            WinMgr.Singleton.Open<BattleSucessWindow>(WinInfo.Create(false, null, false, msg), UILayer.Popup);
        }

    }
}