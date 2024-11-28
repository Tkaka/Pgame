using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBattle;
using FairyGUI;

public class GuildBattlePetItem : UI_guildBattlePetItem
{

    public int petID;
    /// <summary>
    /// 是否上阵
    /// </summary>
    public bool isShanZhen;
    /// <summary>
    /// 禁止的位置
    /// </summary>
    public Vector2 stillPosition;
    public FairyGUI.GComponent parentObj;
    /// <summary>
    /// 占领的物体
    /// </summary>
    public GuildBattlePetItem seizeObj;
    public GuildBattleBuZhengWindow parentWindow;

    public new static GuildBattlePetItem CreateInstance()
    {
        return UI_guildBattlePetItem.CreateInstance() as GuildBattlePetItem;
    }

    public void InitItem(GuildBattleBuZhengWindow parentWindow, bool isCanDrag = false)
    {
        this.parentWindow = parentWindow;
        // 添加拖拽事件
        this.draggable = true;
        this.onDragStart.Clear();
        this.onDragMove.Clear();
        this.onDragEnd.Clear();
        this.onRollOut.Clear();
        this.onRollOver.Clear();

        this.onDragStart.Add(OnDragStart);
        if(isCanDrag)
        {
            this.onDragMove.Add(OnDragMove);
            this.onDragEnd.Add(OnDragEnd);
        }
    }
    public void RefreshItem()
    {
        parentObj = this.parent;
        stillPosition = this.position;

        if (petID == 0)
        {
            m_petItem.visible = false;
            this.m_bg.visible = true;
            //this.visible = false;
        }
        else
        {
            m_petItem.visible = true;
            this.m_bg.visible = false;
            PetItem petItem = m_petItem as PetItem;
            petItem.petID = petID;
            petItem.RefreshItem(0, PetItemType.Normal, false);
        }
    }
    private void OnDragStart(EventContext context)
    {

        if (petID == 0)
            return;

        context.PreventDefault();

        parentWindow.cloneDragPetItem.petID = petID;
        parentWindow.cloneDragPetItem.RefreshItem();
        parentWindow.cloneDragPetItem.visible = true;
        parentWindow.cloneDragPetItem.position = GRoot.inst.GlobalToLocal(Stage.inst.touchPosition);
        parentWindow.cloneDragPetItem.StartDrag();
        parentWindow.cloneDragPetItem.touchable = false;
        parentWindow.cloneDragPetItem.isShanZhen = isShanZhen;

        parentWindow.dragPetItem = this;

        if (isShanZhen)
        {
            parentWindow.cloneDragPetItem.alpha = 1;
            petID = 0;
            RefreshItem();
        }
        else
            parentWindow.cloneDragPetItem.alpha = 0.5f;

    }
    private void OnDragMove(EventContext context)
    {
        if (isShanZhen)
        {
            GuildBattlePetItem touchObj = GRoot.inst.touchTarget as GuildBattlePetItem;
            if (touchObj != null && touchObj.isShanZhen == true)
            {
                bool isChange = false;
                // 上阵的在拖动过程中碰到上阵的，数据互换
                if (parentWindow.dragPetItem.seizeObj != null)
                {
                    if (parentWindow.dragPetItem.seizeObj != touchObj)
                    {
                        // 还原之前交换的, 在交换
                        parentWindow.dragPetItem.seizeObj.petID = parentWindow.dragPetItem.petID;
                        parentWindow.dragPetItem.seizeObj.RefreshItem();
                        // 赋值新的
                        //parentWindow.dragPetItem.petID = touchObj.petID;
                        //parentWindow.dragPetItem.RefreshItem();

                        //touchObj.petID = 0;
                        //touchObj.RefreshItem();

                        //parentWindow.dragPetItem.seizeObj = touchObj;
                        isChange = true;
                    }
                }
                else
                {
                    // 没有占领的Item，交换，占领的置空
                    //parentWindow.dragPetItem.petID = touchObj.petID;
                    //parentWindow.dragPetItem.RefreshItem();

                    //touchObj.petID = 0;
                    //touchObj.RefreshItem();

                    //parentWindow.dragPetItem.seizeObj = touchObj;
                    isChange = true;
                }

                if (isChange)
                {
                    parentWindow.dragPetItem.petID = touchObj.petID;
                    parentWindow.dragPetItem.RefreshItem();

                    touchObj.petID = 0;
                    touchObj.RefreshItem();

                    parentWindow.dragPetItem.seizeObj = touchObj;
                }
            }
        }
    }

    private void OnDragEnd()
    {
        // 处理item的变化,上阵的Item是不会改变的，只是会改变里面的数据
        // 下面的Pet item会动态增减
        GuildBattlePetItem touchObj = GRoot.inst.touchTarget as GuildBattlePetItem;
        bool isChange = false;
        if (touchObj != null)
        {
            if (isShanZhen)
            {
                if (touchObj.isShanZhen)
                {
                    // 两个都是上阵的，判断阵容是否满了,只是改变各自的数据
                    GuildBattleZhenRongItem zhenRongItem = touchObj.parentObj.parent as GuildBattleZhenRongItem;
                    int tempID = touchObj.petID;
                    if (zhenRongItem != null)
                    {
                        isChange = zhenRongItem.ChangePetList(touchObj, petID);
                    }

                    if(isChange == false)
                    {
                        // 如果没交换，数据还原
                        touchObj.petID = parentWindow.dragPetItem.seizeObj.petID;
                        touchObj.RefreshItem();

                        parentWindow.dragPetItem.petID = petID;
                        parentWindow.dragPetItem.RefreshItem();
                    }
                }
                else
                {
                    //上阵的拖到没上阵的地方,拖动的置空，下面列表增加
                    parentWindow.ChangePetList(petID, true);
                }
            }
            else
            {
                if (touchObj.isShanZhen)
                {
                    // 没上阵的，拖到上阵的位置
                    // 是否可上阵，是的话，替换，下面列表移除一个，否的话，下面列表不变
                    GuildBattleZhenRongItem zhenRongItem = touchObj.parentObj.parent as GuildBattleZhenRongItem;
                    if (zhenRongItem != null)
                    {
                        int tempID = touchObj.petID;
                        isChange = zhenRongItem.ChangePetList(touchObj, petID);
                        if (isChange)
                        {
                            // 移除之前的
                            parentWindow.ChangePetList(petID, false);
                            // 添加新的
                            parentWindow.ChangePetList(tempID, true);
                        }
                    }
                }
            }
        }
        else
        {
            if (isShanZhen)
            {
                parentWindow.ChangePetList(petID, true);
                
            }
        }

        this.touchable = true;
        this.visible = false;
        parentWindow.dragPetItem.seizeObj = null;

        if (isShanZhen)
        {
            // 刷新数据
            GuildBattleZhenRongItem zhenRongItem = parentWindow.dragPetItem.parentObj.parent as GuildBattleZhenRongItem;
            if (zhenRongItem != null)
            {
                zhenRongItem.ChangeData();
            }
        }
    }
}
