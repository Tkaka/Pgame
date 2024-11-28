using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBattle;
using Data.Beans;
using Message.Pet;
using FairyGUI;

public class GuildBattleBuZhengWindow : BaseWindow {

    UI_GuildBattleBuZhengWindow window;
    private List<Message.Pet.PetInfo> petInfoList = new List<Message.Pet.PetInfo>();
    public GuildBattlePetItem dragPetItem;
    /// <summary>
    /// 拖拽下面宠物列表是替代的Item
    /// </summary>
    public GuildBattlePetItem cloneDragPetItem;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBattleBuZhengWindow>();

        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_ctrl.onChanged.Add(OnCtrlChanged);
        
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        
        // 初始化拖拽代替的Item
        InitClonePetItem();
        InitPetList();

        int weekDay = GuildBattleService.Singleton.GetGuildBattleType();
        int index = weekDay == 0 ? 0 : weekDay - 1;
        if (window.m_ctrl.selectedIndex == index)
        {
            OnCtrlChanged();
        }
        else
            window.m_ctrl.selectedIndex = index;

    }

    private void InitClonePetItem()
    {
        cloneDragPetItem = GuildBattlePetItem.CreateInstance();
        cloneDragPetItem.isShanZhen = false;
        cloneDragPetItem.visible = false;
        cloneDragPetItem.SetPivot(0.5f, 0.5f, true);
        cloneDragPetItem.InitItem(this, true);
        cloneDragPetItem.sortingOrder = int.MaxValue;
        GRoot.inst.AddChild(cloneDragPetItem);
    }

    private void InitPetList()
    {
        window.m_petList.SetVirtual();
        window.m_petList.itemRenderer = RendPetListItem;
    }

    private void OnCtrlChanged()
    {
        int weekDay = window.m_ctrl.selectedIndex + 1;
        t_guild_battle_timeBean guildBattleTimeBean = ConfigBean.GetBean<t_guild_battle_timeBean, int>(weekDay);
        if (guildBattleTimeBean != null)
        {
            // 刷新可上阵的宠物列表
            RefreshShanZhenPetList(guildBattleTimeBean.t_pet_type);
            // 刷新阵容列表
            RefreshZhenRongList(guildBattleTimeBean.t_formation ,guildBattleTimeBean.t_num);
        }
    }

    private void RefreshShanZhenPetList(int limitType)
    {
        RefreshPetData(limitType);
        window.m_petList.numItems = petInfoList.Count;
    }

    private void RendPetListItem(int index, GObject obj)
    {
        GuildBattlePetItem item = obj as GuildBattlePetItem;
        item.petID = petInfoList[index].petId;
        item.isShanZhen = false;
        item.InitItem(this);
        item.RefreshItem();
    }

    private void RefreshPetData(int limitType)
    {
        petInfoList.Clear();
        petInfoList.AddRange(PetService.Singleton.GetPetInfos());
        petInfoList.Sort(SortPetInfoList);
        if (limitType != 0)
        {
            t_petBean petBean = null;
            int count = petInfoList.Count;
            for (int i = count - 1; i < count; i++)
            {
                petBean = ConfigBean.GetBean<t_petBean, int>(petInfoList[i].petId);
                if (petBean.t_type != limitType)
                    petInfoList.RemoveAt(i);
            }
        }
    }

    private void RefreshZhenRongList(int teamCount, int petCount)
    {
        GuildBattleZhenRongItem zhenRongItem = null;
        window.m_buZhenList.RemoveChildren(0, -1, true);
        for (int i = 0; i < teamCount; i++)
        {
            zhenRongItem = GuildBattleZhenRongItem.CreateInstance();
            // 测试
            List<int> petList = new List<int>();
            for (int j = 0; j < 6; j++)
            {
                petList.Add(0);
            }

            zhenRongItem.RefreshView(petCount, petList, this);
            window.m_buZhenList.AddChild(zhenRongItem);
        }
    }
    /// <summary>
    /// 改变宠物列表
    /// </summary>
    /// <param name="petID"></param>
    /// <param name="isAdd">是添加还是移除</param>
    public void ChangePetList(int petID, bool isAdd)
    {
        bool isChange = false;
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petID);
        if (petInfo != null)
        {
            if (isAdd)
            {
                if (!petInfoList.Contains(petInfo))
                {
                    petInfoList.Add(petInfo);
                    isChange = true;
                }
            }
            else
            {
                if (petInfoList.Contains(petInfo))
                {
                    petInfoList.Remove(petInfo);
                    isChange = true;
                }
            }
        }

        if (isChange)
        {
            // 刷新宠物列表
            petInfoList.Sort(SortPetInfoList);
            window.m_petList.numItems = petInfoList.Count;
            //window.m_petList.RefreshVirtualList();
        }
    }

    private int SortPetInfoList(PetInfo petInfoA, PetInfo petInfoB)
    {
        if (petInfoA == null)
            return 1;
        if (petInfoB == null)
            return -1;

        long fightPowerA = PetService.Singleton.GetPetFightPower(petInfoA.petId);
        long fightPowerB = PetService.Singleton.GetPetFightPower(petInfoB.petId);

        return -fightPowerA.CompareTo(fightPowerB);
    }
}
