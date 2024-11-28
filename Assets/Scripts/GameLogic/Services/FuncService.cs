using FairyGUI;
using Message.Func;
using System.Collections.Generic;

public class FuncService : SingletonService<FuncService>
{
    public int curTipFuncID { get; private set; }
    private Dictionary<int, bool> funcList = new Dictionary<int, bool>();
    private Dictionary<int, GComponent> comMap = new Dictionary<int, GComponent>();
    protected override void RegisterEventListener()
    {
        GED.NED.addListener(ResFuncList.MsgId, onFuncList);
        GED.NED.addListener(ResNewFunc.MsgId, onNewFunc);
    }

    private void onFuncList(GameEvent evt)
    {
        comMap.Clear();
        funcList.Clear();
        var func = GetCurMsg<ResFuncList>(evt.EventId);
        for (int i = 0, len = func.funcList.Count; i < len; ++i)
            funcList.Add(func.funcList[i], true);

        int tipID = 0;
        var gb = ConfigBean.GetBean<Data.Beans.t_globalBean, int>(20104);
        if (gb != null)
            tipID = gb.t_int_param;

        while(IsFuncOpen(tipID))
        {
            var bean = ConfigBean.GetBean<Data.Beans.t_moduleBean, int>(tipID);
            if(bean == null)
            {
                tipID = 0;
                break;
            }
            tipID = bean.t_next_tip;
        }
        curTipFuncID = tipID;
    }

    private void onNewFunc(GameEvent evt)
    {
        var func = GetCurMsg<ResNewFunc>(evt.EventId);
        funcList[func.func] = true;
        if(comMap.ContainsKey(func.func))
        {
            var com = comMap[func.func];
            if(com.displayObject == null || com.displayObject.isDisposed)
                comMap.Remove(func.func);
            else
                SetFuncLock(com, func.func);
        }

        //抛出功能开启事件
        GED.ED.dispatchEvent(EventID.FunOpenEvent, func.func);

        if(curTipFuncID == func.func)
        {
            var bean = ConfigBean.GetBean<Data.Beans.t_moduleBean, int>(curTipFuncID);
            if (bean == null)
                curTipFuncID = 0;
            else
                curTipFuncID = bean.t_next_tip;
            GED.ED.dispatchEvent(EventID.TipFuncChange);
        }

    }
    
    public bool IsFuncOpen(int id)
    {
        return funcList.ContainsKey(id);
    }

    public void SetFuncLock(GComponent com, int funcId)
    {
        var bean = ConfigBean.GetBean<Data.Beans.t_moduleBean, int>(funcId);
        if (bean == null)
            return;

        var obj = com.GetChild("lockGroup");
        comMap[funcId] = com;
        if (bean.t_lock == 1)
        {
            //隐藏
            com.visible = shouldShowFuncEntry(bean.t_condition);
            if (obj != null)
            {
                obj.visible = false;
            }
        }
        else if (bean.t_lock == 2)
        {
            //加锁
            if (obj != null)
            {
                obj.visible = !IsFuncOpen(funcId);
            }
            else if (!IsFuncOpen(funcId))
            {
                //GObject lockGO = UIPackage.CreateObject(WinEnum.UI_Common, "lockGroup");
                //lockGO.name = "lockGroup";
                //com.AddChild(lockGO);
                //lockGO.SetXY(com.width - lockGO.width - 5, 18);
            }
        }
        else if (bean.t_lock == 3)
        {
            com.visible = IsFuncOpen(funcId);
            if (obj != null)
            {
                obj.visible = false;
            }
        }
    }

    private bool shouldShowFuncEntry(string condition)
    {
        var cons = condition.Split(';');
        for (int i = 0; i < cons.Length; ++i)
        {
            int type = -1;
            var arr = cons[i].Split('+');
            int.TryParse(arr[0], out type);
            switch (type)
            {
                case 1:
                    var gb = ConfigBean.GetBean<Data.Beans.t_globalBean, int>(20102);
                    int lvl = gb != null ? gb.t_int_param : 5;
                    var roleInfo = RoleService.Singleton.GetRoleInfo();
                    if (roleInfo == null || int.Parse(arr[1]) - roleInfo.level >= lvl)
                        return false;
                    break;
                case 2:
                    return false;
                case 3:
                    var gb3 = ConfigBean.GetBean<Data.Beans.t_globalBean, int>(20103);
                    int day = gb3 != null ? gb3.t_int_param : 10;
                    if (int.Parse(arr[1]) - RoleService.Singleton.ServerOpenDay > day)
                        return false;
                    break;
            }
        }
        return true;
    }
    
    /// <summary>
    /// 返回功能是否开启
    /// </summary>
    public bool TipFuncNotOpen(int funcId)
    {
        var bean = ConfigBean.GetBean<Data.Beans.t_moduleBean, int>(funcId);
        if (bean == null)
            return true;

        if (!IsFuncOpen(funcId))
        {
            string tip = "开放";
            var lb = ConfigBean.GetBean<Data.Beans.t_languageBean, int>(7201001);
            if (lb != null) tip = lb.t_content;

            TipWindow.Singleton.ShowTip(GetFuncCondition(funcId) + tip + bean.t_name);
            return false;
        }
        return true;
    }

    public string GetFuncCondition(int funcID)
    {
        var bean = ConfigBean.GetBean<Data.Beans.t_moduleBean, int>(funcID);
        if (bean == null)
            return "";

        string tip = "";
        var cons = bean.t_condition.Split(';');
        for (int i = 0; i < cons.Length; ++i)
        {
            int type = -1;
            var arr = cons[i].Split('+');
            int.TryParse(arr[0], out type);
            switch (type)
            {
                case 1:
                    string lan = "{0}级";
                    var lb = ConfigBean.GetBean<Data.Beans.t_languageBean, int>(7201002);
                    if (lb != null) lan = lb.t_content;
                    tip += string.Format(lan, arr[1]);
                    break;
                case 2:
                    string lan2 = "通关{0}-{1}{2}";
                    var lb2 = ConfigBean.GetBean<Data.Beans.t_languageBean, int>(7201003);
                    if (lb2 != null) lan2 = lb2.t_content;
                    int guanqia = int.Parse(arr[1]);
                    var gb = ConfigBean.GetBean<Data.Beans.t_dungeon_actBean, int>(guanqia);
                    tip += string.Format(lan2, (guanqia % 10000) / 100, (guanqia % 10), gb.t_name_id);
                    break;
                case 3:
                    string lan3 = "开服{0}天";
                    var lb3 = ConfigBean.GetBean<Data.Beans.t_languageBean, int>(7201004);
                    if (lb3 != null) lan3 = lb3.t_content;
                    tip = string.Format(lan3, arr[1]);
                    break;
            }
        }
        return tip;
    }

    public override void ClearData()
    {
        base.ClearData();
        comMap.Clear();
    }
}