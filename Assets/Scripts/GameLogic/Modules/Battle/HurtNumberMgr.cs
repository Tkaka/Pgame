using System.Collections.Generic;
using FairyGUI;

public class HurtNumberMgr : SingletonTemplate<HurtNumberMgr>
{
    public string hurtFont1;
    public string hurtFont2;
    public string hpFont;
    public string criticalSign;

    public GComponent view { get; private set; }

    private readonly Stack<HurtNumberFairy> _componentPool = new Stack<HurtNumberFairy>();

    private Queue<HurtNumberFairy> queue = new Queue<HurtNumberFairy>();

    public HurtNumberMgr()
    {
        hurtFont1 = "ui://UI_Battle/HurtNumber0";
        hurtFont2 = "ui://UI_Battle/HurtNumber1";
        hpFont = "ui://UI_Battle/HpNumber";
        criticalSign = "ui://UI_Battle/critical";

        view = new GComponent();
        WinMgr.Singleton.TopHudLayer.AddChild(view);
    }

    public void Emit(Actor owner, NumberType type, long hurt, bool critical=false)
    {
        //if (owner.isDead())
        //    return;
        if (owner.IsDestoryed)
            return;
        if(owner.monoBehavior.headBar == null)
            Logger.err("EmitManager:Emit:headBar is null");

        HurtNumberFairy ec;
        if (_componentPool.Count > 0)
            ec = _componentPool.Pop();
        else
            ec = new HurtNumberFairy();
        ec.isRecycled = false;
        ec.SetHurt(owner, type, hurt, critical);
        ec.Start();
    }

    public void Update()
    {

    }

    public void ReturnComponent(HurtNumberFairy com)
    {
        _componentPool.Push(com);
    }

    public void Clear()
    {
        foreach (HurtNumberFairy hm in _componentPool)
        {
            hm.RemoveFromParent();
            hm.Dispose();
        }
        _componentPool.Clear();
    }

}