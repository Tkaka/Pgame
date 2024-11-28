using UI_Login;
using FairyGUI;
using FairyGUI.Utils;
using System.Collections.Generic;
using System.Linq;

public class ServerListItem : UI_ServerListItem
{

    public UI_ServerListItem ServerItem { private set; get; }

    public int serverId;

    public string serverName;

    public ServerListItem()
    {
        ServerItem = CreateInstance();
        AddChild(ServerItem);
        SetSize(ServerItem.width, ServerItem.height);
        //this.packageItem = ServerItem.packageItem;
        //string str = packageItem.componentData.GetAttribute("customData");
        //Logger.err(str);
        //packageItem.componentData.printAttris();
    }

}

