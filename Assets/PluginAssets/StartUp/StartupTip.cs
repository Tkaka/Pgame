/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
*/

using System;
public class StartupTip
{
    private static StartupTip instance;
    public static StartupTip Singleton
    {
        get
        {
            if(instance == null)
                instance = new StartupTip();
            return instance;
        }
    }

    private void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }

    /// <summary>
    /// 提示没有网络
    /// </summary>
    /// <param name="seg">模块</param>
    /// <param name="callback">回调</param>
    public void TipNoNetwork(string seg, Action cbYes, Action cbNo = null)
    {
        if(cbNo == null) cbNo = quitGame;

        AgainConfirmWindow.Singleton.ShowTip("网络连接失败，请检查您的网络后重试", cbYes, cbNo, true);
    }

    /// <summary>
    /// 提示写文件错误
    /// </summary>
    /// <param name="seg">模块</param>
    /// <param name="callback">回调</param>
    public void TipWriteFileError(string seg, Action cbYes, Action cbNo = null)
    {
        if(cbNo == null) cbNo = quitGame;

        AgainConfirmWindow.Singleton.ShowTip("写入本地文件失败，请检查磁盘空间后重试", cbYes, cbNo, true);
    }

    /// <summary>
    /// 提示有新App更新
    /// </summary>
    /// <param name="curVer">新app版本号</param>
    /// <param name="oldVer">当前版本号</param>
    /// <param name="size">新app大小kb</param>
    /// <param name="yesCb">确定更新</param>
    /// <param name="noCb">不更新</param>
    public void TipNewAppUpdate(string curVer, string oldVer, long size, Action yesCb, Action noCb)
    {
        AgainConfirmWindow.Singleton.ShowTip(string.Format("发现新版本:{0}，当前版本号:{1}，点击更新", curVer, oldVer), yesCb, noCb, true);
    }

    /// <summary>
    /// 提示下载进度
    /// </summary>
    /// <param name="seg">模块</param>
    /// <param name="progress">进度</param>
    /// <param name="loadedLength">下载量</param>
    public void TipProgress(string seg, float progress, long loadedLength, bool hideProgress = false)
    {
        if(seg == typeof(ApplicationUpdater).Name)
        {
            //下载app（Android）
            if (progress <= 0)
                UpdateLoadingWindow.Singleton.ShowTip("正在下载新版本...");
            else
                UpdateLoadingWindow.Singleton.ShowTip(string.Format("正在下载新版本...{0}M/{1}M", loadedLength / 1024f, (loadedLength / progress) / 1204f));
            UpdateLoadingWindow.Singleton.ChangeProgress(progress);
        }
        else if(seg == typeof(ForceFileList).Name)
        {
            //下载强更资源
            if(hideProgress)
            {
                UpdateLoadingWindow.Singleton.ShowTip("正在加载资源...");
                UpdateLoadingWindow.Singleton.ChangeProgress(-1f);
            }else
            {
                if (progress <= 0)
                    UpdateLoadingWindow.Singleton.ShowTip("资源更新中...");
                else
                    UpdateLoadingWindow.Singleton.ShowTip(string.Format("资源更新中...{0}M/{1}M", loadedLength / 1024f, (loadedLength / progress) / 1024f));
            }
        } else if(seg == typeof(VersionConfig).Name)
        {
            UpdateLoadingWindow.Singleton.ShowTip("正在检测更新...");
            UpdateLoadingWindow.Singleton.ChangeProgress(-1f);
            //下载versionConfig
        }
    }

    /// <summary>
    /// 提示强更文件
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="tip">提示内容</param>
    /// <param name="size">更新文件大小KB</param>
    /// <param name="callback">更新回调</param>
    public void TipForceResUpdate(string title, string tip, long size, Action callback)
    {
        AgainConfirmWindow.Singleton.ShowTip(string.Format(tip, size / 1024), callback, callback, true);
    }
}