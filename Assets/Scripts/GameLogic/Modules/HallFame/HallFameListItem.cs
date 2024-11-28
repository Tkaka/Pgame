using Data.Beans;
using FairyGUI;
using UI_HallFame;
public class HallFameListItem : UI_HallFameListItem
{
    public new static HallFameListItem CreateInstance()
    {
        return (HallFameListItem)UI_HallFameListItem.CreateInstance();
    }

    public void Init(t_hof_teamBean fameBean)
    {
        m_TeamList.onClickItem.Add(OnXiangQing);
        if (string.IsNullOrEmpty(fameBean.t_pets))
        {
            Logger.err("HallFameListItem:Init:t_hall_of_fameBean战队数据表没有宠物组成数据" + fameBean.t_id);
            return;
        }
        m_TeamList.RemoveChildren(0,-1,true);
        int[] petids = GTools.splitStringToIntArray(fameBean.t_pets);
        TeamListItem listItem;
        for (int i = 0; i < petids.Length; ++i)
        {
            listItem = TeamListItem.CreateInstance();
            listItem.Init(petids[i]);
            m_TeamList.AddChild(listItem);
        }
    }
    private void OnXiangQing(EventContext evt)
    {
        TeamListItem listItem = (TeamListItem)evt.data;
        int petid = listItem.GetPetid();
        if (petid == 0)
            TipWindow.Singleton.ShowTip("该宠物即将到来，敬请期待（非语言包提示语言）");
        else
        {
            WinInfo info = new WinInfo();
            info.param = petid;
            WinMgr.Singleton.Open<TeamWindow>(info,UILayer.Popup);
        }
    }
}
