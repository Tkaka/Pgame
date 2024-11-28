using FairyGUI;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using UI_Common;

public enum UILayer
{
    HUD = 0,          //场景自带UI
    HUD1 = 1,          //场景自带UI
    Popup,             //所有从HUD中弹出的窗口
    //Effect,              //伤害数字之类
    TopHUD,          //场景自带UI
    Guide,              //新手引导
    Loading,            //loading
    Notice,             //提示层（滚动条之类）
    //Loading,        //loading界面 在最上层 
    //Guide,           //引导层 
    //God,             //最顶层
}

//TODO:实现分层
public class WinMgr : SingletonTemplate<WinMgr>
{
    public const int DesignResolutionW = 1280;
    public const int DesignResolutionH = 720;

    //private Dictionary<long, BaseWindow> windows = new Dictionary<long, BaseWindow>();

    private Dictionary<string, BaseWindow> windows = new Dictionary<string, BaseWindow>();

    private Dictionary<Type, string> win_pkg_pair = new Dictionary<Type, string>();

    private Dictionary<string, BaseWindow> updateWindows = new Dictionary<string, BaseWindow>();

    private Dictionary<string, bool> dontCloseWndMap = new Dictionary<string, bool>();

    public GComponent HudLayer { get; private set; }

    public GComponent Hud1Layer { get; private set; }

    public GComponent PopupLayer { get; private set; }           // 弹窗层

    public GComponent NoticeLayer { get; private set; }

    public GComponent TopHudLayer { get; private set; }

    public GComponent GuideLayer { get; private set; }

    private Dictionary<UILayer, GComponent> layerDic = new Dictionary<UILayer, GComponent>();

    private int fullScreenWindNum = 0;
    private int mainCameraCulling = 0;
    private Camera cullingCamera;

    public void Init()
    {
        UIObjectFactory.SetLoaderExtension(typeof(UIGloader));
        GRoot.inst.SetContentScaleFactor(DesignResolutionW, DesignResolutionH, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
        
        var values = Enum.GetValues(typeof(UILayer)) as UILayer[];
        for (int i = 0, len = values.Length; i < len; ++i)
        {
            var layer = new GComponent();
            layer.name = values[i].ToString();
            layer.gameObjectName = values[i].ToString();

            layer.fairyBatching = true;
            layer.sortingOrder = i * 100;
            layer.SetSize(GRoot.inst.width, GRoot.inst.height);
            layer.AddRelation(GRoot.inst, RelationType.Size);
            GRoot.inst.AddChild(layer);
            layerDic.Add(values[i], layer);
            switch (values[i])
            {
                case UILayer.HUD:
                    HudLayer = layer;
                    break;
                case UILayer.HUD1:
                    Hud1Layer = layer;
                    break;
                case UILayer.TopHUD:
                    TopHudLayer = layer;
                    break;
                case UILayer.Popup:
                    PopupLayer = layer;
                    break;
                case UILayer.Notice:
                    NoticeLayer= layer;
                    break;
                case UILayer.Guide:
                    GuideLayer = layer;
                    break;
            }
        }

        new BasicWindowRegisterCmd().Excute();
        dontCloseWndMap.Add(Open<TipWindow>(WinInfo.Create(), UILayer.Notice), true);
        dontCloseWndMap.Add(Open<ConnectingWindow>(WinInfo.Create(), UILayer.Notice), true);
        dontCloseWndMap.Add(Open<AgainConfirmWindow>(WinInfo.Create(), UILayer.Notice), true);
    }

    public void RegisterPackage(string packageName, Type winType)
    {
        if (!win_pkg_pair.ContainsKey(winType))
        {
            win_pkg_pair.Add(winType, packageName);
        }
        else
        {
            Logger.err("重复注册窗口");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="packageName"></param>
    /// <param name="winName"></param>
    /// <param name="isFull"></param>
    /// <param name="hasClass">表示该窗口是否在FairyGUI内导出了代码</param>
    /// <returns></returns>
    public string Open<T>(WinInfo winInfo=null, UILayer layer=UILayer.HUD) where T : BaseWindow, new()
    {
        Type winType = typeof(T);
        string pkgName;
        if (win_pkg_pair.ContainsKey(winType))
        {
            pkgName = win_pkg_pair[winType];
        }
        else
        {
            Logger.err("未注册的窗口类型：" + winType);
            return null;
        }

        //加载资源
        if (UIPackage.GetByName(pkgName) == null)
        {
            AddPackage(pkgName);
        }

        //添加资源计数
        UIResMgr.Singleton.AddPkgRef(pkgName);
        
        if (winInfo == null)
            winInfo = WinInfo.Create();

        //Type uiWinType = Type.GetType(pkgName + ".UI_" + winType.Name, true);
        string winName = pkgName + "." + winType.Name;
        //TOOD:打开多个相同类型窗口
        //TODO:处理窗口顶替逻辑
        WinInfo info = new WinInfo();
        T win = Create<T>(pkgName, winType, winInfo, layer);
        if (win == null)
        {
            Logger.err("WinMgr:Open window failed");
            return null;
        }
        windows[winName] = win;
        win.OnOpen();

        GED.GuideED.dispatchEvent((int)GuideEventID.OpenWnd, winName);

        //全屏窗口
        if(win.Info.isFullScreen && win.layer == UILayer.Popup)
        {
            fullScreenWindNum++;
            if (fullScreenWindNum == 1)
            {
                //隐藏主相机
                cullingCamera = Camera.main;
                mainCameraCulling = cullingCamera.cullingMask;
                Camera.main.cullingMask = 0;
                Hud1Layer.visible = false;
                HudLayer.visible = false;
            }
        }
        return winName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="packageName"></param>
    /// <param name="instanceName">用作打开多个相同窗口</param>
    /// <param name="isFull"></param>
    /// <returns></returns>
    private T Create<T>(string pkgName, Type winType, WinInfo info, UILayer layer) where T : BaseWindow, new()
    {
        T win = new T();

        GComponent view = null;
        Type instanceType = Type.GetType("UI_" + winType.Name, false);
        if(instanceType == null)
            instanceType = Type.GetType(pkgName + ".UI_" + winType.Name, false);

        if (instanceType != null)
        {
            try
            {
                view = UIPackage.CreateObject(pkgName, winType.Name, instanceType).asCom;
            }
            catch (Exception e)
            {
                Logger.err("WinMgr.Create:GetType failed", e.ToString());
                return null;
            }
        }
        else
        {
            view = UIPackage.CreateObject(pkgName, winType.Name).asCom;
        }

        if (view == null)
        {
            Logger.err("WinMgr.Create:初始化窗口失败");
            return null;
        }

        GComponent parent = null;
        if (layerDic.ContainsKey(layer))
        {
            parent = layerDic[layer];
        }
        else
        {
            parent = HudLayer;
            Logger.err("WinMgr:Create:Unknow Layer:" + layer);
        }

        parent.AddChild(view);
        if (info.isFullResize)
        {
            view.SetSize(GRoot.inst.width, GRoot.inst.height);
            view.AddRelation(GRoot.inst, RelationType.Size);
        }
        win.Create(pkgName, pkgName + "." + winType.Name, view, info, layer);

        return win;
    }

    public void Close(string winName, float delay = 0)
    {
        if (string.IsNullOrEmpty(winName))
            return;
        if (windows.ContainsKey(winName) && windows[winName].IsOpen)
        {
            //从更新中移除
            //RemoveUpdate(winName);
            windows[winName].Close(delay);
        }
    }

    public void RemoveRef(string winName)
    {
        if (windows.ContainsKey(winName))
        {
            var window = windows[winName];
            windows.Remove(winName);

            //减少资源计数
            string pkgName = null;
            if (win_pkg_pair.ContainsKey(window.GetType()))
                pkgName = win_pkg_pair[window.GetType()];
            UIResMgr.Singleton.RemovePkgRef(pkgName);
            if (window.layer == UILayer.Popup)
                UIResMgr.Singleton.ReleaseNoUseRes();

            if (window.Info.isFullScreen && window.layer == UILayer.Popup)
            {
                fullScreenWindNum--;
                if (fullScreenWindNum <= 0)
                {
                    //显示主相机
                    var camera = Camera.main;
                    if (cullingCamera == camera && camera.cullingMask == 0)
                        camera.cullingMask = mainCameraCulling;

                    Hud1Layer.visible = true;
                    HudLayer.visible = true;
                }
            }
            GED.GuideED.dispatchEvent((int)GuideEventID.CloseWnd, winName);
        }
    }

    /*public void AddToUpdate(string winName)
    {
        BaseWindow win = GetWindow<BaseWindow>(winName);
        if (win != null)
            updateWindows[winName] = win;
    }

    public void RemoveUpdate(string winName)
    {
        if (updateWindows.ContainsKey(winName))
        {
            updateWindows.Remove(winName);
        }
    }

    public void Update()
    {
        foreach (KeyValuePair<string, BaseWindow> keyVal in updateWindows)
        {
            keyVal.Value.OnUpdate();
        }
    }*/

    public T GetWindow<T>(string winName) where T : BaseWindow
    {
        if (windows.ContainsKey(winName))
        {
            return windows[winName] as T;
        }
        return null;
    }

    public bool IsWindOpen(string winName)
    {
        return windows.ContainsKey(winName);
    }
    
    public Vector3 WorldToScreen(Vector3 worldPos, float offsetY = 0)
    {
        worldPos.y += offsetY;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        screenPos.y = Screen.height - screenPos.y; //convert to Stage coordinates system
        return GRoot.inst.GlobalToLocal(screenPos);
    }

    public void CloseAll()
    {
        for (int a = windows.Count - 1; a >= 0; --a)
        {
            KeyValuePair<string, BaseWindow> kv = windows.ElementAt(a);
            if (dontCloseWndMap.ContainsKey(kv.Key))
                continue;
            kv.Value.Close();
        }
    }

    public void Uninit()
    {
        for (int a = windows.Count - 1; a >= 0; --a)
        {
            KeyValuePair<string, BaseWindow> kv = windows.ElementAt(a);
            kv.Value.Close();
        }
        GRoot.inst.RemoveChildren(0, -1, true);
    }

    public void HideAll()
    {
        for (int a = windows.Count - 1; a >= 0; --a)
        {
            KeyValuePair<string, BaseWindow> kv = windows.ElementAt(a);

            kv.Value.ToogleVisible(false);
        }
    }

    public void ShowAll()
    {
        for (int a = windows.Count - 1; a >= 0; --a)
        {
            KeyValuePair<string, BaseWindow> kv = windows.ElementAt(a);

            kv.Value.ToogleVisible(true);
        }
    }

    public static void AddPackage(string packageName)
    {
        if (UIPackage.GetByName(packageName) == null)
        {
            //UIPackage.AddPackage(WinEnum.BasePath + packageName);
            UIPackage.AddPackage(WinEnum.BasePath + packageName, UIResMgr.Singleton.UILoadDelegate);
        }
    }
    
    /// <summary>
    /// Removes the package res
    /// </summary> 
    public static void RemovePackage(string packageName)
    {
        if (string.IsNullOrEmpty(packageName))
        {
            Logger.log("RemovePackage, packageName is null");
            return;
        }
        if (UIPackage.GetByName(packageName) != null)
            UIPackage.RemovePackage(packageName, true);
        UIResMgr.Singleton.RemoveUIPackage(packageName);
    }
}
