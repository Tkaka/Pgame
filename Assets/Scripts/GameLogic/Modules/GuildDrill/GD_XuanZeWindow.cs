using System.Collections.Generic;
using UI_GuildDrill;
using Message.Pet;
using FairyGUI;

public class GD_XuanZeWindow : BaseWindow
{
    private UI_GD_XuanZeWindow window;
    private GD_GameDataManger manger;
    private int index;
    private List<PetInfo> pets;
    private List<PetInfo> weishang;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_GD_XuanZeWindow>();
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        if (Info.param == null)
        {
            Logger.err("GD_XuanZeWindow:OnOpen:未传入数据引用，无法加载选择窗口");
            return;
        }

        TwoParam<GD_GameDataManger, int> twoParam = new TwoParam<GD_GameDataManger, int>();
        twoParam = Info.param as TwoParam<GD_GameDataManger, int>;
        if (twoParam == null)
        {
            Logger.err("GD_XuanZeWindow:OnOpen:传入数据类型不正确无法加载选择窗口");
            return;
        }
        manger = twoParam.value1;
        index = twoParam.value2;
        pets = PetService.Singleton.GetPetInfos();
        if (pets == null)
        {
            Logger.err("GD_XuanZeWindow:OnOpen:未能获得宠物数据，无法加载窗口");
            return;
        }

        AddKeyEvent();
        
        InitView();
    }
    private void AddKeyEvent()
    {
        GED.ED.addListener(EventID.OnGuildDrillChangePet, OnChangPet);
        window.m_petlist.SetVirtual();
        window.m_petlist.itemRenderer = OnLieBiaoXuanRan;
    }
    public override void InitView()
    {
        base.InitView();
        int number = 0;
        for (int i = 0; i < manger.mydrill.pets.Count; ++i)
        {
            if (manger.mydrill.pets[i].petId != -1)
                number++;
        }
        OnList();
        window.m_petlist.numItems = pets.Count - number;
        if (window.m_petlist.numItems == 0)
        {
            TipWindow.Singleton.ShowTip("没有可训练的宠物！！！（非）" );
            OnCloseBtn();
        }
        else
         window.m_petlist.RefreshVirtualList();
    }
    private void OnLieBiaoXuanRan(int index,GObject obj)
    {
        GD_XuanZeListItem listItem = obj as GD_XuanZeListItem;
        listItem.Init(weishang[index]);
    }
    private void OnChangPet(GameEvent evt)
    {
        int petId = (int)evt.Data;
        int xiabiao = index;
        GuildService.Singleton.ReqSetExpPet(xiabiao, petId);
        OnCloseBtn();
    }
    private void OnList()
    {
        bool tianjia;
        weishang = new List<PetInfo>();
        for (int i = 0; i < pets.Count; ++i)
        {
            tianjia = true;
            for (int j = 0; j < manger.mydrill.pets.Count; ++j)
            {
                if (pets[i].petId == manger.mydrill.pets[j].petId)
                {
                    tianjia = false;
                    break;
                }
            }
            if (tianjia)
            {
                weishang.Add(pets[i]);
            }
        }
    }
    protected override void OnCloseBtn()
    {
        GED.ED.removeListener(EventID.OnGuildDrillChangePet, OnChangPet);
        window = null;
        manger = null;
        pets = null;
        weishang = null;
        base.OnCloseBtn();
    }
}
