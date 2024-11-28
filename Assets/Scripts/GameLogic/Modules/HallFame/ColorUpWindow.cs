using Data.Beans;
using Message.Bag;
using Message.Team;
using System.Collections.Generic;
using UI_HallFame;
public class ColorUpWindow : BaseWindow
{
    private UI_ColorUpWindow window;
    private List<ItemInfo> itemInfos;
    private t_hof_level_upBean levelupBean;
    private t_petBean petBean;
    public override void OnOpen()
    {
        window = getUiWindow<UI_ColorUpWindow>();
        window.m_HF_CloseBtn.onClick.Add(OnCloseBtn);
        window.m_HF_queding.onClick.Add(OnCloseBtn);
        AddEventListener();
        InitView();
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
    }
    public override void InitView()
    {
        base.InitView();
        if (Info.param == null)
        {
            Logger.err("ColorUpWindow:InitView传参有误！");
            return;
        }
        TwoParam<HofItem, List<ItemInfo>> twoParam = Info.param as TwoParam<HofItem, List<ItemInfo>>;
        if (twoParam == null)
        {
            Logger.err("ColorUPWindow:InitView:传入奖品列表参数有误！");
            return;
        }
        levelupBean = ConfigBean.GetBean<t_hof_level_upBean,int>(twoParam.value1.color);
        if (levelupBean == null)
        {
            Logger.err("ColorUpWindow:InitView从服务器获得的品阶有误！");
            return;
        }
        petBean = ConfigBean.GetBean<t_petBean,int>(twoParam.value1.petId);
        if (petBean == null)
        {
            Logger.err("ColorUpWindow:InitView未能从宠物表获得数据！");
            return;
        }
        itemInfos = twoParam.value2;
        FllData();
    }
    private void FllData()
    {
        UIGloader.SetUrl(window.m_HF_haoganduIcon,levelupBean.t_icon);
        string miaoshu = "为了表示友情，{0}送给你以下礼品";
        string name = UIUtils.GetPetName(petBean);
        window.m_miaoshu.text = string.Format(miaoshu,name);
        
        TeamListItem teamList;
        window.m_ItemList.RemoveChildren(0,-1,true);
        for (int i = 0; i < itemInfos.Count; ++i)
        {
            teamList = TeamListItem.CreateInstance();
            teamList.Init(itemInfos[i].id,true,itemInfos[i].num);
            window.m_ItemList.AddChild(teamList);
        }
    }
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        window = null;
        base.OnCloseBtn();
    }
}
