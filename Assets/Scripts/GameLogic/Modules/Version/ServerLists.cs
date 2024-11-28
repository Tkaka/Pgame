/*
using SimpleJSON;
using System;
using System.Collections.Generic;

// 服务器列表结构
public class ServerListEntry
{
    public int ServerId { get; set; }
    public String ServerName { get; set; }
    public string ServerUrl { get; set; }
    public int Port { get; set; }
    public int ServerStatus { get; set; }
	public string Desc{ get; set; }
	public int Random{ get; set; }
}

/// 服务器列表分组结构
public class ServerGroupListEntry
{
	public int Id{ get; set; }
	public String Name{ get; set; }
	public String List{ get; set; }
	public int Status{ get; set; }

	public List<ServerListEntry> serverList;
	public void ParseList(ServerList owner)
	{
		serverList = new List<ServerListEntry>();
		string[] idList = List.Split( ',' );
		foreach( string id in idList )
		{
			ServerListEntry sle = owner.GetServerByID(int.Parse(id));
			if( sle != null )
				serverList.Add( sle );
		}
	}
}

/// <summary>
/// 服务器列表
/// </summary>
public class ServerList
{

    public string Version;

    // app版本号
    public string AppVersion { get; set; }

    // 资源版本号
    public string ResourceVersion { get; set; }

	//登陆白名单
	public List<string> TrustList { get; set; }
    
	// 后台激活码
	public string Back{ get; set; }

    // 服务器列表
    public ServerListEntry[] ServerLists { get; set; }

	// 隐藏服务器分组
	public ServerGroupListEntry[] GroupMaintainServer{ get; set; }
	// 服务器分组 正式
	public ServerGroupListEntry[] GroupServer{ get; set; }
    // 资源服务器
    public ServerUrlEntry[] ResourceServer { get; set; }
    
    // 公告服务器
    public ServerUrlEntry[] NoticeUrls { get; set; }

    // 资源更新地址 
    public ServerUrlEntry[] UpdateUrls { get; set; }

    // 索引服务器最新地址
    public ServerUrlEntry[] IndexServer { get; set; }

    //是否通过审核
    public bool IsPassed = false;

    private static Random mRandom = new Random();
    
    
    // 资源Url
    public string getResourceUrl()
    {
        return ServerUrlEntry.getRandomOneUrl(ResourceServer);
    }

    // Notice Url
    public string getNoticeUrl()
    {
        return ServerUrlEntry.getRandomOneUrl(NoticeUrls);
    }

    // Update Url
    public string getUpdateUrl()
    {
        return ServerUrlEntry.getRandomOneUrl(UpdateUrls);
    }

    private ServerListEntry mCServer;
    /// 当前服务器.
    public ServerListEntry CurrentServer
    {
        get
        {
            if (mCServer == null)
            {
                mCServer = new ServerListEntry();
                int serverID = int.Parse(PlayerLocalData.GetData(PlayerLocalData.key_Last_Server_ID, -123).ToString());
                if (serverID == -123)
                {
                    // TODO 没有选服务器，本地也没有保存登陆信息
					mCServer = GetServerByIndex( getRandomIndex() );
                }
                else
                {
                    // 没有选择服务器，读本地
                    mCServer = GetServerByID(serverID);
                }
                if (mCServer == null)
                {
                    mCServer = GetServerByIndex( getRandomIndex() );
                }
            }
            return mCServer;
        }
        set
        {
            mCServer = value;
        }
    }

    public void ResetCurServer(int serverId)
    {
        mCServer = GetServerByID(serverId);
    }


    public static ServerList Load(string text)
    {
        try
        {
            ServerList list = new ServerList();
            JSONNode node = SimpleJSON.JSON.Parse(text);
            list.IndexServer = ServerUrlEntry.parseServerUrlEntry(node, "IndexServer");
            list.Version = node["Version"].Value;

            //服务器列表
            ServerListEntry[] entryArr = null;
            SimpleJSON.JSONArray serverListNode = node["ServerLists"].AsArray;
            if (serverListNode != null && serverListNode.Count > 0)
            {
                entryArr = new ServerListEntry[serverListNode.Count];
                ServerListEntry tempEntry = null;
                SimpleJSON.JSONNode entryNode;
                for (int i = 0; i < serverListNode.Count; i++)
                {
                    tempEntry = new ServerListEntry();
                    entryNode = serverListNode[i];
                    if (entryNode["Id"] != null)
                        tempEntry.ServerId = entryNode["Id"].AsInt;
                    else
                        tempEntry.ServerId = entryNode["ServerId"].AsInt;
                    if (entryNode["Url"] != null)
                        tempEntry.ServerUrl = entryNode["Url"].Value;
                    else
                        tempEntry.ServerUrl = entryNode["ServerUrl"].Value;
                    if (entryNode["Name"] != null)
                        tempEntry.ServerName = entryNode["Name"].Value;
                    else
                        tempEntry.ServerName = entryNode["ServerName"].Value;
                    if (entryNode["Status"] != null)
                        tempEntry.ServerStatus = entryNode["Status"].AsInt;
                    else
                        tempEntry.ServerStatus = entryNode["ServerStatus"].AsInt;

                    tempEntry.Port = entryNode["Port"].AsInt;
                    tempEntry.Desc = entryNode["Desc"].Value;
                    tempEntry.Random = entryNode["Random"].AsInt;
                    entryArr[i] = tempEntry;
                }
            }
            else
            {
                entryArr = new ServerListEntry[0];
            }
            list.ServerLists = entryArr;

            // 服务器分组信息
            List<ServerGroupListEntry> groupsList = new List<ServerGroupListEntry>();
            List<ServerGroupListEntry> maintainList = new List<ServerGroupListEntry>();
            ServerGroupListEntry sgle = null;
            SimpleJSON.JSONArray groupNode = node["GroupLists"].AsArray;
            if (groupNode != null && groupNode.Count > 0)
            {
                SimpleJSON.JSONNode entryNode;
                for (int i = 0; i < groupNode.Count; ++i)
                {
                    entryNode = groupNode[i];
                    sgle = new ServerGroupListEntry();
                    sgle.Status = entryNode["Status"].AsInt;
                    // 状态为1才显示该分组
                    sgle.Name = entryNode["Name"].Value;
                    sgle.List = entryNode["List"].Value;
                    sgle.Id = entryNode["Id"].AsInt;
                    sgle.ParseList(list);
                    if (sgle.Status == 1)
                        groupsList.Add(sgle);
                    else
                        maintainList.Add(sgle);
                }
            }

            list.GroupServer = groupsList.ToArray();
            list.GroupMaintainServer = maintainList.ToArray();

            //白名单
            List<string> trustList = new List<string>();
            SimpleJSON.JSONArray trustListNode = node["TrustList"].AsArray;
            if (trustListNode != null && trustListNode.Count > 0)
            {
                SimpleJSON.JSONNode nameNode;
                for (int i = 0; i < trustListNode.Count; i++)
                {
                    nameNode = trustListNode[i];
                    trustList.Add(nameNode["name"].Value);
                }
            }
            list.TrustList = trustList;


            list.ResourceServer = ServerUrlEntry.parseServerUrlEntry(node, "ResourceServer");
            list.NoticeUrls = ServerUrlEntry.parseServerUrlEntry(node, "NoticeUrl");
            list.UpdateUrls = ServerUrlEntry.parseServerUrlEntry(node, "UpdateUrl");
            list.IndexServer = ServerUrlEntry.parseServerUrlEntry(node, "IndexServer");


            return list;
        }
        catch (Exception ex)
        {
            Logger.err("text =" + text + " Exception: " + ex.ToString());
            return null;
        }
    }

	private int getRandomIndex()
	{
		int ret = 0;
		int totalPro = 0;
		int count = ServerLists.Length; 
		foreach( ServerListEntry sle in GameConfig.ServerList.ServerLists)
			totalPro += sle.Random;

		if( totalPro <= 0 )
		{
			System.Random random = new Random();
			ret = random.Next( 0, count );
		}else
		{
			System.Random random = new Random();
			int rand = random.Next( 0, totalPro );
			int pro = 0;
			for( int i=0; i<count; ++i )
			{
				ServerListEntry sle = GameConfig.ServerList.ServerLists[i];
				pro += sle.Random;
				if( rand < pro )
				{
					ret = i;
					break;
				}
			}
		}
		return ret;
	}

    /// 通过id在列表中查找对应的服务器.
    public ServerListEntry GetServerByID(int id)
    {
        foreach (ServerListEntry se in ServerLists)
        {
            if (se.ServerId == id)
            {
                return se;
            }
        }
        return null;
    }

    public ServerListEntry GetServerByIndex(int index)
    {
		if (ServerLists.Length > index )
		{
        	return ServerLists[index];
		}
		else
		{
			ServerListEntry ret = new ServerListEntry();
			ret.ServerStatus = 0;
			ret.Desc = "无效的服务器";//"无效的服务器";
			ret.ServerName = "服务器1";//"服务器1";
			return ret;
		}
    }

}
*/
