using FairyGUI;
using Data.Beans;
using System.Collections.Generic;
using DG.Tweening;


public class WinInfo
{
    //是否是该模块的入口窗口
    public bool isEntry = false;
    //父级窗口名字
    public string parentName;
    //是否是全屏窗口
    public bool isFullScreen = true;       
    //是否全屏适配
    public bool isFullResize = true;
    //是否有绑定窗口类
    public bool hasClass = true;
    //窗口参数
    public object param;

    public static WinInfo Create(bool isEntry=false, string parentName = null, bool isFullScreen=true, object param =null, bool isFullResize = true, bool hasClass = true)
    {
        WinInfo info = new WinInfo();
        info.isEntry = isEntry;
        info.parentName = parentName;
        info.isFullScreen = isFullScreen;
        info.isFullResize = isFullResize;
        info.hasClass = hasClass;
        info.param = param;
        return info;
    }

}

/// <summary>
/// TODO:处理子父级窗口
/// </summary>
public class BaseWindow : UIResPack
{
    public BaseWindow() : base(null)
    {
#if UNITY_EDITOR
        this.resOwnerObj = this;
#endif
    }

    //子窗口 
    //protected Dictionary<int, BaseWindow> childMap = new Dictionary<int, BaseWindow>();

    //父窗口 
    //public BaseWindow parent;

    public WinInfo Info { private set; get; }

    //是否打开
    public bool IsOpen { get; protected set; }

    //窗口包名字
    public string packageName { get; protected set; }

    //窗口实例化名字
    public string winName { get; protected set; }

    public GComponent view { get; protected set; }

    public UILayer layer { get; private set; }

    public T getUiWindow<T>() where T:GComponent
    {
        return view as T;
    }


    private Dictionary<string, List<GObject>> m_redDotObjMap;
    private DoActionInterval doAction;

    public void Create(string packageName, string winName, GComponent view, WinInfo info, UILayer winLayer)
    {
        view.name = GetType().Name;

        this.packageName = packageName;
        this.winName = winName;
        this.view = view;
        this.Info = info;
        layer = winLayer;
        //drawcall优化(已经放到层级去设置)
        //this.view.fairyBatching = true;
        IsOpen = true;
        m_redDotObjMap = new Dictionary<string, List<GObject>>();
    }

    public virtual void OnOpen()
    {
        AddEventListener();
    }

 
    public virtual void AddEventListener()
    {
        GED.ED.addListener(EventID.HongDianChange, _OnRedDotRefreshEvent);
    }

    public virtual void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.HongDianChange, _OnRedDotRefreshEvent);
    }


    private void _OnRedDotRefreshEvent(GameEvent evt)
    {
        TwoParam<string, bool> param = evt.Data as TwoParam<string, bool>;
        if (m_redDotObjMap.ContainsKey(param.value1))
        {
            List<GObject> redObjList = m_redDotObjMap[param.value1];
            for (int i = 0; i < redObjList.Count; i++)
            {
                if (redObjList[i].displayObject != null && !redObjList[i].displayObject.isDisposed)
                    redObjList[i].visible = param.value2;
                //else
                //    Debuger.Err("显示对象已销毁，对应的红点事件->" + param.value1 + "  " + param.value2);
            }
        }
    }

    protected void _RegisterRedDot(string path, GObject redObj)
    {
        if (path.Equals("") || redObj == null)
        {
            return;
        }

        redObj.visible = RedDotManager.Singleton.GetRedDotValue(path);
        if (m_redDotObjMap.ContainsKey(path))
        {
            List<GObject> objList = m_redDotObjMap[path];
            for (int i = 0; i < objList.Count; i++)
            {
                if (redObj == objList[i])
                {
                    //重复注册
                    return;
                }
            }

            objList.Add(redObj);
        }
        else
        {
            List<GObject> objList = new List<GObject>();
            objList.Add(redObj);
            m_redDotObjMap.Add(path, objList);
        }
    }

    public virtual void OpenChild<T>(WinInfo info=null) where T : BaseWindow, new()
    {
        if (info == null)
            info = WinInfo.Create(false, winName);
        info.parentName = winName;
        if (info.isFullScreen)
            this.view.visible = false;
        WinMgr.Singleton.Open<T>(info, UILayer.Popup);
    }

    public virtual void InitView()
    {
        
    }

    public virtual void RefreshView()
    {

    }

    /// <summary>
    /// 需要添加到winMgr的update列表中
    /// </summary>
    public virtual void OnUpdate()
    {

    }

    public virtual void ToogleVisible(bool flag)
    {
        view.visible = flag;
    }

    public virtual void ToggleTouchable(bool flag)
    {
        view.touchable = flag;
    }
    /// <summary>
    /// 播放打开的效果
    /// </summary>
    public virtual void PlayOpenEffect()
    {
        TopRoleInfo.PlayOpenAnim();
    }
    /// <summary>
    /// 播放弹窗动画
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="popupView"></param>
    public void PlayPopupAnim(GObject mask, GObject popupView)
    {
        if(mask != null)
        {
            mask.alpha = 0;
            mask.TweenFade(1, 0.3f);
        }

        if (popupView != null)
        {
            popupView.SetPivot(0.5f, 0.5f);
            popupView.scale = UnityEngine.Vector2.zero;
            popupView.TweenScale(new UnityEngine.Vector2(1.15f,1.15f), 0.15f);

            if (doAction != null && doAction.IsRunning)
                doAction.kill();

            doAction = new DoActionInterval();
            doAction.doActionWithTimes(0.15f, OnPopupTweenEnd, popupView);
        }
    }

    private void OnPopupTweenEnd(object obj)
    {
        GObject popupView = obj as GObject;
        if(popupView != null)
        {
            popupView.TweenScale(UnityEngine.Vector2.one, 0.15f).OnComplete(OnOpenEffectEnd);
        }
    }

    private void PlayCloseEffect()
    {
        if (Info.isFullScreen == false && this.view != null)
        {
            this.view.SetScale(1, 1);
            this.view.TweenScale(UnityEngine.Vector2.zero, 0.5f);
        }
    }

    /// <summary>
    /// 打开效果结束
    /// </summary>
    protected virtual void OnOpenEffectEnd()
    {

    }

    /// <summary>
    /// 关闭效果开始
    /// </summary>
    protected virtual void OnCloseEffectBegin()
    {

    }

    protected virtual void OnCloseBtn()
    {
        Close();
    }

    private void OnReduceBtnClick()
    {

    }

    private void OnAddBtnClick()
    {

    }

    protected virtual void OnClose()
    {
        RemoveEventListener();
        m_redDotObjMap.Clear();
        if (Info != null)
        {
            if (!string.IsNullOrEmpty(Info.parentName) && Info.isFullScreen)
            {
                BaseWindow window = WinMgr.Singleton.GetWindow<BaseWindow>(Info.parentName);
                if (window != null)
                {
                    window.OnShow();
                }
            }

            PlayCloseEffect();
        }
    }
    /// <summary>
    /// 从隐藏状态变成显示状态
    /// </summary>
    public virtual void OnShow()
    {
        if (this.view != null)
            this.view.visible = true;
        PlayOpenEffect();
    }
    
    public void Close()
    {
        if (IsOpen)
        {
            if (doAction != null && doAction.IsRunning)
                doAction.kill();

            WinMgr.Singleton.RemoveRef(winName);
            OnClose();
            view.RemoveFromParent();
            view.Dispose();
            view = null;
            IsOpen = false;
            ReleaseAllRes();
        }
    }

    public virtual void Close(float delay)
    {
        if(delay <= 0)
        {
            Close();
            return;
        }

        Timers.inst.Add(delay, 1, (p)=>{
            Close();
        });
    }
}