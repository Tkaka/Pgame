using FairyGUI;
using UnityEngine;

public class UIEffectWrapper
{

    private GGraph cont;

    private GoWrapper gow;

    public void SetEffect(string path, Vector3? pos = null, float recycleDelay=1.0f)
    {
        GameObject go = FightManager.R.LoadGo(path);
        if (go == null)
            return;
        BaseEffectMono comp = go.GetComponent<BaseEffectMono>();
        if (comp != null)
        {
            if (comp.RecycleDelay != -1)
            {
                recycleDelay = comp.RecycleDelay;
            }
            CoroutineManager.Singleton.delayedCall(recycleDelay, Dispose);
        }

        gow = new GoWrapper(go);
        gow.alpha = 0;
        cont = new GGraph();
        cont.alpha = 0;
        cont.SetNativeObject(gow);
        if (!pos.HasValue)
        {
            Vector3 p = Vector3.zero;
            p.x = GRoot.inst.root.width * 0.5f;
            p.y = GRoot.inst.root.height * 0.5f;
            cont.position = p;
        }
        else
        {
            cont.position = pos.Value;
        }
        WinMgr.Singleton.NoticeLayer.AddChild(cont);
    }

    public void Dispose()
    {
        if(gow != null)
            gow.Dispose();
        if(cont != null)
            cont.Dispose();
    }

}
