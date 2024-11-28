using System;
using FairyGUI;
using System.Collections.Generic;

public abstract class TabPage
{

    public int index;

    public abstract void OnHide();

    public abstract void OnShow();

    public abstract void OnClose();

    public abstract void RefreshView(bool isNet = false);

    public bool isShowing = false;
}

public class UITable
{
    private struct FuncLock
    {
        public int funcId;
        public GButton btn;
    }

    private bool lockChange;
    private Controller ctrl;
    private System.Action<int> changeCb;
    private List<TabPage> pages = new List<TabPage>();
    private Dictionary<int, FuncLock> lockList = new Dictionary<int, FuncLock>();
    private Dictionary<GComponent, int> btnAnimlist = new Dictionary<GComponent, int>();
    private bool isClickBtn;
    private DoActionInterval doAction;

    public void Init(Controller controller, Action<int> cb, params TabPage[] tabs)
    {
        if(controller == null)
        {
            Debuger.Err("controller为空");
            return;
        }
        pages.Clear();
        for(int i=0; i<tabs.Length; ++i)
            pages.Add(tabs[i]);

        changeCb = cb;
        ctrl = controller;
        ctrl.onChanged.Add(onTabChange);
    }
    
    public void AddFuncLock(int idx, int funcId, GButton btn)
    {
        lockList[idx] = new FuncLock { funcId = funcId, btn = btn };
        FuncService.Singleton.SetFuncLock(btn, funcId);
        btn.onChanged.Clear();
        btn.onChanged.Add(onBtnChange);
    }

    public void AddBtnAnim(params GButton[] btns)
    {
        int count = btns.Length;
        GButton btn = null;
        for (int i = 0; i < count; i++)
        {
            btn = btns[i];
            btn.onTouchBegin.Add(OnBtnTouchBegain);
            btn.onTouchEnd.Add(OnBtnTouchEnd);
            btn.onRollOut.Add(OnBtnTouchEnd);

            btnAnimlist.Add(btn, i);
        }
    }

    private void onBtnChange()
    {
        if (ctrl == null)
            return;

        if (!lockList.ContainsKey(ctrl.selectedIndex))
            return;

        var func = lockList[ctrl.selectedIndex];
        if (FuncService.Singleton.IsFuncOpen(func.funcId))
            return;
        
        lockChange = true;
        ctrl.selectedIndex = ctrl.previsousIndex;
        lockChange = false;
    }

    private void OnBtnTouchBegain(EventContext context)
    {
        GComponent sender = context.sender as GComponent;
        if (btnAnimlist.ContainsKey(sender))
        {
            int index = btnAnimlist[sender];
            if (index != ctrl.selectedIndex && IsOpenFunction(index))
            {
                sender.SetPivot(0.5f, 0.5f);
                sender.TweenScale(new UnityEngine.Vector2(0.85f, 0.85f), 0.1f);
                isClickBtn = true;
            }
        }
    }

    private void OnBtnTouchEnd(EventContext context)
    {
        if (isClickBtn)
        {
            GComponent sender = context.sender as GComponent;
            sender.TweenScale(new UnityEngine.Vector2(1.1f, 1.1f), 0.1f);
            if (doAction != null && doAction.IsRunning)
                doAction.kill();
            doAction = new DoActionInterval();
            doAction.doActionWithTimes(0.1f, OnTweenEnd, sender);

            isClickBtn = false;
        }
    }

    private void OnTweenEnd(object obj)
    {
        GComponent sender = obj as GComponent;
        sender.TweenScale(new UnityEngine.Vector2(1f, 1f), 0.1f);
    }

    private void onTabChange()
    {
        //如果业签有模型，必须回调刷新模型层级
        if (changeCb != null)
            changeCb(ctrl.selectedIndex);

        if (lockChange)
            return;

        if (ctrl == null)
        {
            Debuger.Err("controller为空");
            return;
        }

        //if(lockList.ContainsKey(ctrl.selectedIndex))
        //{
        //    var func = lockList[ctrl.selectedIndex];
        //    if (!FuncService.Singleton.TipFuncNotOpen(func.funcId))
        //        return;
        //}

        if (!IsOpenFunction(ctrl.selectedIndex))
            return;

        for (int i = 0; i < ctrl.pageCount; ++i)
        {
            if (ctrl.selectedIndex != i)
            {
                if (pages.Count > i && pages[i] != null && pages[i].isShowing)
                {
                    pages[i].OnHide();
                    pages[i].isShowing = false;
                }
            }
            else
            {
                if (pages.Count > i && pages[i] != null && !pages[i].isShowing)
                {
                    pages[i].OnShow();
                    pages[i].isShowing = true;
                }
            }
        }
    }

    private bool IsOpenFunction(int index)
    {
        if (lockList.ContainsKey(index))
        {
            var func = lockList[index];
            return FuncService.Singleton.TipFuncNotOpen(func.funcId);
        }

        return true;
    }
    
    public void Refresh(bool isNet = false)
    {
        if (ctrl == null)
        {
            Debuger.Err("controller为空");
            return;
        }

        for (int i = 0; i < pages.Count; ++i)
        {
            if (i == ctrl.selectedIndex)
            {
                if (pages[i] != null)
                    pages[i].RefreshView(isNet);
            }
        }
    }

    public void Close()
    {
        for (int i = 0; i < pages.Count; ++i)
        {
            if (pages[i] != null)
                pages[i].OnClose();
        }

        pages.Clear();
        if (ctrl != null)
            ctrl.onChanged.Clear();

        if (btnAnimlist != null)
            btnAnimlist.Clear();

        if (doAction != null && doAction.IsRunning)
            doAction.kill();
    }
}