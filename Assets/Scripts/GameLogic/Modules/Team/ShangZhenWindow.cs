using UI_BuZhen;
using Data.Beans;
using System.Collections.Generic;
using Message.Pet;
using Message.Challenge;

/// <summary>
/// 选择方式
/// </summary>
public enum ShangZhenSelectType
{
    Default = 1,         // 默认（选择上阵信息）
    HuiNu = 2,           // 怒气回复选择
    HuiXue = 3,          // 生命回复选择
    FuHuo = 4,           // 复活
    CloneChangePet = 5,  // 克隆组队战更换宠物
}

public class ShangZhenWindow : BaseWindow
{
    private UI_ShangZhenWindow window;
    public int oldPetId;
    private ShangZhenSelectType type;

    List<PetInfo> petInfos = new List<PetInfo>();

    public override void OnOpen()
    {
        base.OnOpen();
        InitView();
    }


    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_ShangZhenWindow>();
        window.m_Close.onClick.Add(CloseBtn);
        TwoParam<int, ShangZhenSelectType> param = Info.param as TwoParam<int, ShangZhenSelectType>;
        oldPetId = param.value1;
        type = param.value2;

        ((UI_Common.UI_commonTop)window.m_taitou).m_title.text = "宠物选择";
        ShowList();
    }
    private void ShowList()
    {
        petInfos.Clear();
        petInfos.AddRange(PetService.Singleton.GetPetInfos());
        FilterPetInfo(petInfos);

        window.m_ShanZhenList.SetVirtual();
        window.m_ShanZhenList.itemRenderer = ListItemRender;
        window.m_ShanZhenList.numItems = petInfos.Count;

        //PetInfo petInfo = null;
        //ShangZhenListItem item = null;
        //int len = petInfos.Count;
        //if (petInfos != null)
        //{
        //    for (int i = 0; i < len; ++i)
        //    {
                
        //        window.m_ShanZhenList.AddChild(item);
        //    }
        //}
    }

    private void ListItemRender(int index, FairyGUI.GObject obj)
    {
        PetInfo petInfo = petInfos[index];
        ShangZhenListItem item = obj as ShangZhenListItem;

        item.petid = petInfo.petId;
        item.isEnterID = petInfo.petId == oldPetId;
        item.packageName = WinEnum.UI_BuZhen;
        item.winName = winName;
        if (PetService.Singleton.ShangZhenList(petInfo.petId))
        {
            item.InitView(true, type);
        }
        else
        {
            item.InitView(false, type);
        }
    }

    private void FilterPetInfo(List<PetInfo> petInfoList)
    {
        PetInfo petInfo = null;
        int count = petInfoList.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            petInfo = petInfoList[i];
            TrialPetStatus status = UltemateTrainService.Singleton.GetTrialPetStatue(petInfo.petId);
            switch (type)
            {
                case ShangZhenSelectType.Default:
                    break;
                case ShangZhenSelectType.HuiNu:
                    if (status != null && status.anger >= 1000)
                        petInfoList.RemoveAt(i);
                    break;
                case ShangZhenSelectType.HuiXue:
                    if (status == null || status.hpLoss <= 0)
                        petInfoList.RemoveAt(i);
                    break;
                case ShangZhenSelectType.FuHuo:
                    if (status.dead == 0)
                        petInfoList.RemoveAt(i);
                    break;
                default:
                    break;
            }
        }
    }
    public void CloseBtn()
    {
        GameEvent evt = GameEventFactory.create();
        evt.EventId = (int)EventID.OnShangZhenChongWuId;
        TwoParam<int, int> param = new TwoParam<int, int>();
        param.value1 = oldPetId;
        param.value2 = 0;
        evt.Data = param;
        GED.ED.dispatchEvent(evt);
        Close();
    }

}
