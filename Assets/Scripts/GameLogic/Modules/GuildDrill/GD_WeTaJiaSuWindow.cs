using FairyGUI;
using Message.Guild;
using Message.Pet;
using System.Collections.Generic;
using UI_GuildDrill;

public class GD_WeTaJiaSuWindow : BaseWindow
{
    //加载链表
    //
    private UI_GD_WeTaJiaSuWindow window;
    private GD_GameDataManger manger;
    private ResQuickRoleInfo roleInfo;
    private long roleId;
    private List<PetInfo> petInfos;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_GD_WeTaJiaSuWindow>();
        AddKeyEvent();
        if (Info.param == null)
        {
            Logger.err("未传入角色id，无法加载");
            return;
        }
        roleId = (long)Info.param;
        GuildService.Singleton.ReqQuickRoleInfo(roleId);
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnGuildDrillJiaSuPetId,OnJiaSuBtn);
        GED.ED.addListener(EventID.OnGuildDrillRolePets, OnRolePets);
        GED.ED.addListener(EventID.OnGuildDrillTaRenJiaSuFanHui, OnTaRenJiaSuFanHui);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnGuildDrillJiaSuPetId,OnJiaSuBtn);
        GED.ED.removeListener(EventID.OnGuildDrillRolePets, OnRolePets);
        GED.ED.removeListener(EventID.OnGuildDrillTaRenJiaSuFanHui, OnTaRenJiaSuFanHui);
    }
    private void AddKeyEvent()
    {
        window.m_close.onClick.Add(OnCloseBtn);
        window.m_petList.SetVirtual();
        window.m_petList.itemRenderer = RenderListItem;
       
    }
    private void OnRolePets(GameEvent evt)
    {
        roleInfo = evt.Data as ResQuickRoleInfo;
        if (roleInfo == null)
        {
            Logger.err("类型转换失败无法打开窗口");
            return;
        }
        
        InitView();
    }
    public override void InitView()
    {
        base.InitView();
        // GD_XunLianWei xunlianwei;
        petInfos = new List<PetInfo>();
        for (int i = 0; i < roleInfo.role.pets.Count; ++i)
        {
            PetInfo info = new PetInfo();
            info.petId = roleInfo.role.pets[i].petId;
            info.basInfo.color = roleInfo.role.pets[i].color;
            info.basInfo.level = roleInfo.role.pets[i].level;
            info.basInfo.expRemain = roleInfo.role.pets[i].exp;
            petInfos.Add(info);
        }
        window.m_petList.numItems = roleInfo.role.pets.Count;
        window.m_petList.RefreshVirtualList();
    }
    private void RenderListItem(int index, GObject obj)
    {
        GD_XunLianWei list_Item = obj as GD_XunLianWei;
        list_Item.JiaSuInit(petInfos[index],index + 1);
    }
    private void OnJiaSuBtn(GameEvent evt)
    {
        GuildService.Singleton.ReqQuickRole(roleId,(int)evt.Data);
    }
    //为他人加速返回信息
    private void OnTaRenJiaSuFanHui(GameEvent evt)
    {
        ResQuickRole msg = evt.Data as ResQuickRole;
        TipWindow.Singleton.ShowTip("你成功为该宠物加速" + msg.pet.petId);
    }
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        window = null;
        base.OnCloseBtn();
    }
}
