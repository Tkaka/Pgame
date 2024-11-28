using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuideManager : SingletonTemplate<GuideManager>
{
    private long clickCheckTimer;
    private bool enabled = false;
    private Queue<int> blockList = new Queue<int>();
    private Dictionary<int, Guide> guideList = new Dictionary<int, Guide>();

    public void Initialize()
    {
        enabled = true;
        guideList.Clear();
        blockList.Clear();
        addListener(); 
        clickCheckTimer = CoroutineManager.Singleton.startCoroutine(checkClickEvt());
    }

    public void Uninitialize()
    {
        enabled = false;
        guideList.Clear();
        blockList.Clear();
        removeListener();
        CoroutineManager.Singleton.stopCoroutine(clickCheckTimer);
    }

    private void addListener()
    {
        GED.GuideED.addListener((int)GuideEventID.GuideStepFinish, onEvtBack);
        GED.GuideED.addListener((int)GuideEventID.ClickGuideBtn, onEvtBack);
        GED.GuideED.addListener((int)GuideEventID.ClickScreen, onEvtBack);
        GED.GuideED.addListener((int)GuideEventID.CloseWnd, onEvtBack);
        GED.GuideED.addListener((int)GuideEventID.OpenWnd, onEvtBack);
        GED.GuideED.addListener((int)GuideEventID.SpawnMonster, onEvtBack);

        GED.GuideED.addListener((int)GuideEventID.GuideFinish, onGuideFinish);
        GED.GuideED.addListener((int)GuideEventID.GuideTriggerCheck, outEvtCheck);
    }

    private void outEvtCheck(GameEvent evt)
    {
        checkTigger(GuideEventID.Invalid, null);
    }

    private void onGuideFinish(GameEvent evt)
    {
        int id = (int)evt.Data;
        if (guideList.ContainsKey(id))
            guideList.Remove(id);
    }

    private void removeListener()
    {
        GED.GuideED.removeListener((int)GuideEventID.GuideStepFinish, onEvtBack);
        GED.GuideED.removeListener((int)GuideEventID.ClickGuideBtn, onEvtBack);
        GED.GuideED.removeListener((int)GuideEventID.ClickScreen, onEvtBack);
        GED.GuideED.removeListener((int)GuideEventID.CloseWnd, onEvtBack);
        GED.GuideED.removeListener((int)GuideEventID.OpenWnd, onEvtBack);
        GED.GuideED.removeListener((int)GuideEventID.SpawnMonster, onEvtBack);

        GED.GuideED.removeListener((int)GuideEventID.GuideFinish, onGuideFinish);
        GED.GuideED.removeListener((int)GuideEventID.GuideTriggerCheck, outEvtCheck);
    }

    private void onEvtBack(GameEvent evt)
    {
        checkTigger((GuideEventID)evt.EventId, evt.Data != null ? evt.Data.ToString() : null);
    }

    private IEnumerator checkClickEvt()
    {
        while(true)
        {
            yield return null;
            if(Input.GetMouseButtonUp(0))
                GED.GuideED.dispatchEvent((int)GuideEventID.ClickScreen, null);
        }
    }

    private void checkTigger(GuideEventID evt, string param)
    {
        var list = new List<int>(guideList.Keys);
        for(int i=0; i<list.Count; ++i)
        {
            if (guideList.ContainsKey(list[i]))
                guideList[list[i]].CheckTrigger(evt, param);
        }

        var guideConf = ConfigBean.GetBeanList<Data.Beans.t_guideBean>();
        for (int i = 0, len = guideConf.Count; i < len; ++i)
        {
            int id = guideConf[i].t_id;
            if (guideList.ContainsKey(id))
                continue;
            if (GuideService.Singleton.IsGuideFinish(id))
                continue;

            if (GuideConditions.CheckTrigger(false, id, guideConf[i].t_trigger, evt, param))
            {
                if (guideConf[i].t_block == 1 && hasBlockGuideDoing())
                {
                    blockList.Enqueue(id);
                }
                else
                {
                    guideList[id] = new Guide(id);
                    guideList[id].Begin();
                }
            }
        }

        if(!hasBlockGuideDoing() && blockList.Count > 0)
        {
            int id = blockList.Dequeue();
            guideList[id] = new Guide(id);
            guideList[id].Begin();
        }
    }

    private bool hasBlockGuideDoing()
    {
        var enu = guideList.GetEnumerator();
        while(enu.MoveNext())
        {
            var bean = ConfigBean.GetBean<Data.Beans.t_guideBean, int>(enu.Current.Key);
            if (bean.t_block == 1)
                return true;
        }
        enu.Dispose();
        return false;
    }
}