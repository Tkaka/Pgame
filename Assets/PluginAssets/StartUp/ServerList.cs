/*
 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
 */
using UnityEngine;

public class ServerList : BaseDownloader
{
    private static ServerList instance;
    public static ServerList Singleton
    {
        get
        {
            if(instance == null)
                instance = new ServerList();
            return instance;
        }
    }

    public string CloseDesc { get; private set; }

    public ServerGroup[] ServerGroups { get; private set; }
    
    public string Content { get; private set; }

    private const string Last_Server_Key = "Last_Server_key";
    private Server curServer;
    public Server CurrentServer
    {
        get
        {
            if(curServer == null)
            {
                //PlayerPrefs.DeleteKey(Last_Server_Key);
                int serverID = PlayerPrefs.GetInt(Last_Server_Key, -123456);
                if(serverID == -123)
                    curServer = GetRandomServer();
                else
                    curServer = GetServer(serverID);

                if (curServer == null)
                    curServer = GetRandomServer();
            }
            return curServer;
        }
        set
        {
            curServer = value;
            if(value == null)
            {
                Debuger.Err("当前服务器被设置为空");
                PlayerPrefs.DeleteKey(Last_Server_Key);
                PlayerPrefs.Save();
                return;
            }
            PlayerPrefs.SetInt(Last_Server_Key, value.id);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// 获取服务器信息
    /// </summary>
    public Server GetServer(int serverID)
    {
        if (ServerGroups == null)
            return null;
        for (int i = 0, len = ServerGroups.Length; i < len; ++i)
        {
            var arr = ServerGroups[i].servers;
            for (int j = 0, num = arr.Length; j < num; ++j)
            {
                if (arr[j].id == serverID)
                    return arr[j];
            }
        }
        return null;
    }

    public ServerGroup GetServerGroup(int serverID)
    {
        if (ServerGroups == null)
            return null;
        for (int i = 0, len = ServerGroups.Length; i < len; ++i)
        {
            var arr = ServerGroups[i].servers;
            for (int j = 0, num = arr.Length; j < num; ++j)
            {
                if (arr[j].id == serverID)
                    return ServerGroups[i];
            }
        }
        return null;
    }

    private Server GetRandomServer()
    {
        if (ServerGroups == null)
            return null;

        int result = 0;
        int serverCount = 0;
        int randomTotal = 0;
        for (int i = 0, len = ServerGroups.Length; i < len; ++i)
        {
            var arr = ServerGroups[i].servers;
            for (int j = 0, num = arr.Length; j < num; ++j)
            {
                serverCount++;
                randomTotal += arr[j].random;
            }
        }

        if(randomTotal <= 0)
        {
            result = UnityEngine.Random.Range(0, serverCount);
            for (int i = 0, len = ServerGroups.Length; i < len; ++i)
            {
                var arr = ServerGroups[i].servers;
                for (int j = 0, num = arr.Length; j < num; ++j)
                {
                    result--;
                    if (result < 0)
                        return arr[j];
                }
            }
        }else
        {
            result = UnityEngine.Random.Range(0, randomTotal);
            for (int i = 0, len = ServerGroups.Length; i < len; ++i)
            {
                var arr = ServerGroups[i].servers;
                for (int j = 0, num = arr.Length; j < num; ++j)
                {
                    result -= arr[j].random;
                    if (result < 0)
                        return arr[j];
                }
            }
        }

        if (ServerGroups.Length > 0 && ServerGroups[0].servers.Length > 0)
            return ServerGroups[0].servers[0];
        return null;
    }

    public override void Download()
    {
        //UnityWebLoader.Singleton.Download(getDownloadUrl(), onLoadCmp, onLoadUpdate, mVersion, true);
        WWWLoader.Singleton.Download(getDownloadUrl(), onLoadCmp, onLoadUpdate, mVersion, loadCache, true);
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
            Debuger.Err("服务器列表下载失败，重新下载 3秒");
            CoroutineManager.Singleton.delayedCall(3, ReDownload);
        }

        if(!string.IsNullOrEmpty(Content))
        {
            SimpleJSON.JSONNode json = SimpleJSON.JSONClass.Parse(Content);
            CloseDesc = json["closeDesc"];
            SimpleJSON.JSONArray arr = json["servers"].AsArray;
            ServerGroups = new ServerGroup[arr.Count];
            for (int i=0, len = arr.Count; i<len; ++i)
            {
                ServerGroups[i] = new ServerGroup();

                var group = arr[i]["servers"];
                int num = group.Count;
                ServerGroups[i].servers = new Server[num];
                ServerGroups[i].id = arr[i]["id"].AsInt;
                ServerGroups[i].name = arr[i]["groupName"];
                for (int j = 0; j < num; ++j)
                {
                    Server server = new Server();
                    ServerGroups[i].servers[j] = server;
                    server.id = group[j]["id"].AsInt;
                    server.ip = group[j]["ip"].Value;
                    server.port = group[j]["port"].AsInt;
                    server.name = group[j]["name"].Value;
                    server.random = group[j]["random"].AsInt;
                    server.state = group[j]["state"].AsInt;
                }
            }
        }

        if(mCallback != null)
            mCallback();
        GED.ED.dispatchEvent(EventID.ServerListLoaded);
    }
    
    public class Server
    {
        public int id;
        public string name;
        public string ip;
        public int port;
        public int state;
        public int random;
    }

    public class ServerGroup
    {
        public int id;
        public string name;
        public Server[] servers;
    }
}