/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
*/

using UnityEngine;

public class Notice : BaseDownloader
{
    private static Notice instance;
    public static Notice Singleton
    {
        get
        {
            if(instance == null)
                instance = new Notice();
            return instance;
        }
    }

    //公告内容
    public string Content { get; private set; }

    public override void Download()
    {
        //UnityWebLoader.Singleton.Download(getDownloadUrl(), onLoadCmp, onLoadUpdate, mVersion, true);
        WWWLoader.Singleton.Download(getDownloadUrl(), onLoadCmp, onLoadUpdate, mVersion, loadCache, false);
    }

    protected override void onLoadCmp(string path, bool success, byte[] data)
    {
        base.onLoadCmp(path, success, data);
        if(!Loaded)
            return;

        if(success && data != null)
        {
            Content = System.Text.Encoding.UTF8.GetString(data);
        } else
        {
            Debuger.Err("notice 下载失败");
            //CoroutineManager.Singleton.delayedCall(10, ReDownload);
        }

        if(mCallback != null)
            mCallback();

        GED.ED.dispatchEvent(EventID.NoticeLoaded);
    }
}