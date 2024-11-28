using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;

public class AddAoyiCell : UI_addAyCell
{
    private int m_petId;
    private AoyiService.EStonePage m_page;
    private int m_gridId;

    public new static AddAoyiCell CreateInstance()
    {
        return UI_addAyCell.CreateInstance() as AddAoyiCell;
    }


    public void RefreshView(int petId, AoyiService.EStonePage page, int gridId)
    {
        m_petId = petId;
        m_page = page;
        m_gridId = gridId;

        t_aoyi_pageBean pageBean = ConfigBean.GetBean<t_aoyi_pageBean, int>((int)page * 10 + gridId);
        if (pageBean == null)
            return;

        bool isUnLock = RoleService.Singleton.GetRoleInfo().level >= pageBean.t_level_limit;
        StoneInfo stoneInfo = null;
        if (!isUnLock)
        {
            //未解锁
            this.m_itemIcon.visible = false;
            this.m_txtUnLockDes.visible = true;
            this.m_imgAdd.visible = false;
            this.m_txtUnLockDes.text = string.Format("{0}级解锁", pageBean.t_level_limit);
        }
        else
        {
            this.m_txtUnLockDes.visible = false;
            this.m_imgAdd.visible = true;
            stoneInfo = AoyiService.Singleton.GetPetStoneInfo(petId, page, gridId);
            if (stoneInfo == null)
            {
                //未装备
                this.m_objAdd.visible = true;
                this.m_itemIcon.visible = false;
                //红点
            }
            else
            {
                this.m_objAdd.visible = false;
                this.m_itemIcon.visible = true;
                AoyiCommonItem commonItem = this.m_itemIcon as AoyiCommonItem;
                if (commonItem != null)
                {
                    commonItem.RefreshView(stoneInfo.itemId, stoneInfo.bigLevel * 10 + stoneInfo.minLevel);
                    commonItem.SelectToggle(false);
                }
            }
        }

        this.onClick.Clear();
        this.onClick.Add(() =>
        {
            if (stoneInfo == null)
            {
                if (isUnLock)
                {
                    TwoParam<int, AoyiService.EStonePage> param = new TwoParam<int, AoyiService.EStonePage>();
                    param.value1 = m_petId;
                    param.value2 = page;
                    WinMgr.Singleton.Open<AoyiChangeWnd>(WinInfo.Create(false, null, true, param), UILayer.Popup);
                }
                else
                {
                    TipWindow.Singleton.ShowTip(string.Format("该槽位{0}级解锁，请努力提升训练家等级!", pageBean.t_level_limit));
                }
            }
            else
            {
                ThreeParam<int, AoyiService.EStonePage, StoneInfo> threeParam = new ThreeParam<int, AoyiService.EStonePage, StoneInfo>();
                threeParam.value1 = m_petId;
                threeParam.value2 = page;
                threeParam.value3 = stoneInfo;
                WinMgr.Singleton.Open<AoyiStrengthWnd>(WinInfo.Create(false, null, false, threeParam), UILayer.Popup);

                AoyiCommonItem item = m_itemIcon as AoyiCommonItem;
                if (item != null)
                    item.SelectToggle(true);
            }

        });

    }

    public void SelectToggle(bool isToggle)
    {
        AoyiCommonItem item = m_itemIcon as AoyiCommonItem;
        if(item != null)
            item.SelectToggle(isToggle);
    }

}