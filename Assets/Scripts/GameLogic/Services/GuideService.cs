using System.Collections.Generic;
using Message.Guide;

public class GuideService : SingletonService<GuideService>
{
    private Dictionary<int, bool> finishList = new Dictionary<int, bool>();
    protected override void RegisterEventListener()
    {
        GED.NED.addListener(ResGuideList.MsgId, onGuideList);
    }

    private void onGuideList(GameEvent evt)
    {
        var msg = GetCurMsg<ResGuideList>(evt.EventId);
        finishList.Clear();
        for (int i = 0, len = msg.finishList.Count; i < len; ++i)
            finishList[msg.finishList[i]] = true;

        if (msg.enable)
            GuideManager.Singleton.Initialize();
    }

    public void ReqFinishGuide(int id)
    {
        finishList[id] = true;
        var msg = GetEmptyMsg<ReqFinishGuide>();
        msg.id = id;
        SendMsg(ref msg);
    }

    public bool IsGuideFinish(int id)
    {
        return finishList.ContainsKey(id);
    }

    public override void ClearData()
    {
        base.ClearData();
        finishList.Clear();
        GuideManager.Singleton.Uninitialize();
    }
}