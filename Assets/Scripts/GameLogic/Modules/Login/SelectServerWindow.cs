using FairyGUI;
using System.Collections.Generic;
using UI_Login;
using UnityEngine;

public class SelectServerWindow : BaseWindow
{

    private UI_SelectServerWindow window;

    private GList serverList;
    private GList groupList;
    private int selectGroup = 0;

    public static bool IsTrustUser
    {
        get
        {
            int val = PlayerPrefs.GetInt(TrustUserKey, -10);
            return val > 0;
        }
    }
    public const string TrustUserKey = "Im_Trust_User";
    private static bool trustAdded = false;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_SelectServerWindow>();
        InitView();

        //白名单处理
        if (IsTrustUser)
            Debug.Log("你是一个白名单用户");
        if (!trustAdded)
        {
            trustAdded = true;
            Debuger.AddTrigger("1,4,1,4,1,4,3", (s)=>{
                if(WinMgr.Singleton.IsWindOpen("UI_Login.SelectServerWindow"))
                {
                    int value = PlayerPrefs.GetInt(TrustUserKey, -10);
                    if (value > 0)
                    {
                        Debug.Log("已切出白名单，成为正常设备");
                        PlayerPrefs.SetInt(TrustUserKey, -10);
                    }
                    else
                    {
                        Debug.Log("你发现一个秘密，已切换为白名单");
                        PlayerPrefs.SetInt(TrustUserKey, 10);
                    }
                }
            });
        }
    }

    public override void InitView()
    {
        base.InitView();
        window.m_closeBtn.onClick.Add(OnCloseBtn);

        groupList = window.m_groupList;
        groupList.RemoveChildren();
        serverList = window.m_serverList;

        var groups = ServerList.Singleton.ServerGroups;
        if (groups != null && groups.Length > 0)
        {
            ServerGroupItem groupItem = null;
            for(int i=0, len = groups.Length; i < len; ++i)
            {
                groupItem = new ServerGroupItem();
                groupItem.GroupItem.m_groupName.text = groups[i].name;
                groupItem.groupId = i;
                groupItem.groupName = groups[i].name;
                groupList.AddChild(groupItem);
            }
            groupList.EnsureBoundsCorrect();
            groupList.onClickItem.Add(OnServerGroupClick);

            selectGroup = 0;
            selectGroup = System.Convert.ToInt32(PlayerLocalData.GetData(PlayerLocalData.key_Last_Server_GroupID, selectGroup));
            CreateServerList();
        }

        /*ServerList dataList = GameConfig.ServerList;
        if (dataList != null && dataList.GroupServer != null && dataList.GroupServer.Length > 0)
        {
            ServerGroupItem groupItem = null;
            for (int i = 0; i < dataList.GroupServer.Length; i++)
            {
                groupItem = new ServerGroupItem();
                groupItem.GroupItem.m_groupName.text = dataList.GroupServer[i].Name;
                groupItem.groupId = i;
                groupItem.groupName = dataList.GroupServer[i].Name;
                groupList.AddChild(groupItem);
            }
            groupList.EnsureBoundsCorrect();
            groupList.onClickItem.Add(OnServerGroupClick);

            selectGroup = 0;
            selectGroup = System.Convert.ToInt32(PlayerLocalData.GetData(PlayerLocalData.key_Last_Server_GroupID, selectGroup));
            CreateServerList();
        }*/
    }

    private void OnServerGroupClick(EventContext context)
    {
        ServerGroupItem item = (ServerGroupItem)context.data;
        if (item.groupId != selectGroup)
        {
            selectGroup = item.groupId;
            CreateServerList();
        }
    }

    private void CreateServerList()
    {
        //serverList.RemoveChildren();
        serverList.RemoveChildren(0, -1, true);
        //serverList.RemoveChildrenToPool(0, -1);
        //ServerList dataList = GameConfig.ServerList;
        ServerListItem serverItem = null;
        var serverListArr = ServerList.Singleton.ServerGroups[selectGroup].servers;
        //List<ServerListEntry> serverListArr = dataList.GroupServer[selectGroup].serverList;
        for (int i = 0; i < serverListArr.Length; i++)
        {
            serverItem = new ServerListItem();
            serverItem.ServerItem.m_serverName.text = serverListArr[i].name;
            serverItem.serverId = serverListArr[i].id;
            serverItem.serverName = serverListArr[i].name;
            serverList.AddChild(serverItem);
        }
        serverList.onClickItem.Add(OnServerItemClick);
    }

    private void OnServerItemClick(EventContext context)
    {
        ServerListItem serverItem = (ServerListItem)context.data; ;
        //PlayerLocalData.SetData(PlayerLocalData.key_Last_Server_ID, serverItem.serverId);
        //PlayerLocalData.SetData(PlayerLocalData.key_Last_Server_GroupID, selectGroup);
        //GameConfig.ServerList.ResetCurServer(serverItem.serverId);
        ServerList.Singleton.CurrentServer = ServerList.Singleton.GetServer(serverItem.serverId);
        Close();
    }

    protected override void OnClose()
    {
        base.OnClose();
        GED.ED.dispatchEvent(EventID.SelectServerWindowClose);
    }

}
