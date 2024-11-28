/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.24
*/

public class StartupManager
{
    private static StartupManager instance;
    public static StartupManager Singleton
    {
        get
        {
            if(instance == null)
                instance = new StartupManager();
            return instance;
        }
    }

    private System.Action callBack;
    /// <summary>
    /// 启动更新流程
    /// </summary>
    /// <param name="callback">回调</param>
    public void Start(System.Action callback)
    {
        callBack = callback;
        LocalConfig.Singleton.Init();
        if(LocalConfig.Singleton.IsNewApp)
        {
            //新app需要清理之前的强更资源
            PathUtil.ClearForce();
            ForceManager.Singleton.Clear();
        }

        UnityEngine.Caching.ClearCache();
        WWWLoader.Singleton.UseCache = false;
        VersionConfig.Singleton.SetUrlList(LocalConfig.Singleton.ConfigUrlList);
        VersionConfig.Singleton.CheckUpdate(updateApp, false);
    }

    private void updateApp()
    {
        //app
        ApplicationUpdater.Singleton.Reset(VersionConfig.Singleton.AppVersion);
        ApplicationUpdater.Singleton.SetUrlList(VersionConfig.Singleton.AppUrlList);

        //强更
        ForceFileList.Singleton.Reset(VersionConfig.Singleton.ForceResVersion);
        ForceFileList.Singleton.SetUrlList(VersionConfig.Singleton.ForceResUrlList);
        ForceFileList.Singleton.SetDownloadUrl(VersionConfig.Singleton.ForceDownloadUrlList);

        //偷跑
        //BackFileList.Singleton.Reset(VersionConfig.Singleton.BackResVersion);
        //BackFileList.Singleton.SetUrlList(VersionConfig.Singleton.BackResUrlList);
        //BackFileList.Singleton.SetDownloadUrl(VersionConfig.Singleton.BackDownloadUrlList);

        //服务器列表
        ServerList.Singleton.Reset(VersionConfig.Singleton.ServerListVersion);
        ServerList.Singleton.SetUrlList(VersionConfig.Singleton.ServerListUrlList);

        //公告
        Notice.Singleton.Reset(VersionConfig.Singleton.NoticeVersion);
        Notice.Singleton.SetUrlList(VersionConfig.Singleton.NoticeUrlList);

        //更新app
        ApplicationUpdater.Singleton.CheckUpdate(updateForce, false);
    }

    private void updateForce()
    {
        //更新强更文件
        ForceFileList.Singleton.CheckUpdate(LocalConfig.Singleton.LocalForceResVersion, startEnd);
    }

    private void startEnd()
    {
        LocalConfig.Singleton.SaveToLocal(VersionConfig.Singleton.ConfigUrlList, VersionConfig.Singleton.ForceResVersion);
        ResDepManager.Singleton.LoadDeps();
        ConfigManager.Singleton.LoadBeans();

        Data.Containers.GameDataManager.Instance.loadAll();

        ServerList.Singleton.CheckUpdate(null);
        Notice.Singleton.CheckUpdate(null);
        //BackFileList.Singleton.CheckUpdate(BackFileList.Singleton.BeginBackDownLoad);
        if(callBack != null)
            callBack();
    }
}