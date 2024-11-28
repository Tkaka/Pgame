using UI_Login;
using FairyGUI;

public class ServerGroupItem  : GComponent
{

    public UI_GroupListItem GroupItem { private set; get; }

    public int groupId;

    public string groupName;


    public ServerGroupItem()
    {
        GroupItem = UI_GroupListItem.CreateInstance();
        AddChild(GroupItem);
        SetSize(GroupItem.width, GroupItem.height);
    }

}