using Data.Beans;
using Message.Pet;
using Message.Rank;
using UI_PetParticulars;
using FairyGUI;

public class ZhanHunMianBan : UI_ZhanHunMianBan
{
    private int petid;
    public new static ZhanHunMianBan CreateInstance()
    {
        return (ZhanHunMianBan)UI_ZhanHunMianBan.CreateInstance();
    }
    public void Init(int petId,int type = 0)
    {
        petid = petId;
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        m_ZhanHunList.onClickItem.Add(OnOpenDetailsWindow);
        if (petBean == null)
        {
            Logger.err("宠物id有误");
            return;
        }
        if (string.IsNullOrEmpty(petBean.t_soul_detail_type))
        {
            Logger.err("未能获得战魂数据" + petBean.t_id);
            return;
        }
        if (type != 0)
        {
            OnPaiHangBang();
        }
        else
        {
            FillData();
        }
    }
    private void FillData()
    {
        ZhanHunListItem item;
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petid);
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petid);
        int[] zhanhun = GTools.splitStringToIntArray(petBean.t_soul_detail_type);
        m_ZhanHunList.RemoveChildren(0, -1, true);
        if (petInfo != null)
        {

            for (int i = 0; i < zhanhun.Length; ++i)
            {
                item = ZhanHunListItem.CreateInstance();
                if (PetService.Singleton.ZhanHunIsUnlock(i, petBean.t_id))
                {
                    item.Init(zhanhun[i], petInfo.soulInfo.souls[i].level);
                }
                else
                {
                    item.Init(zhanhun[i], -1);
                }
                m_ZhanHunList.AddChild(item);
            }
        }
        else
        {
            for (int i = 0; i < zhanhun.Length; ++i)
            {
                item = ZhanHunListItem.CreateInstance();
                item.Init(zhanhun[i], -1);
                m_ZhanHunList.AddChild(item);
            }
        }
    }
    //排行榜宠物信息
    private void OnPaiHangBang()
    {
        Petdata petdata = TopService.Singleton.GetPetdata();
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petid);
        if (petBean != null)
        {
            if (!(string.IsNullOrEmpty(petBean.t_soul_detail_type)))
            {
                int[] soulids = GTools.splitStringToIntArray(petBean.t_soul_detail_type);
                ZhanHunListItem hunItem;
                for (int i = 0; i < soulids.Length; ++i)
                {
                    hunItem = ZhanHunListItem.CreateInstance();
                    bool shiyong = false;
                    for (int j = 0; j < petdata.title.souls.Count; ++j)
                    {
                        if (i == petdata.title.souls[j].index)
                        {
                            hunItem.Init(soulids[i], petdata.title.souls[j].level);
                            shiyong = true;
                            break;
                        }
                    }
                    if (!shiyong)
                    {
                        hunItem.Init(soulids[i], -1);
                    }
                    m_ZhanHunList.AddChild(hunItem);
                }
            }
        }
    }
    private void OnOpenDetailsWindow(EventContext context)
    {
        ZhanHunListItem listItem = context.data as ZhanHunListItem;
        if (listItem != null)
        {
            TwoParam<int, int> twoParam = new TwoParam<int, int>();
            twoParam.value1 = listItem.zhanhunId;
            twoParam.value2 = listItem.level;
            WinMgr.Singleton.Open<ZhanHUnXiangQingWindow>(WinInfo.Create(false,null,false,twoParam),UILayer.Popup);
        }
    }
}
