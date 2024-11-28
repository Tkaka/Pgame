using Message.Pet;
using UI_Strength;
using Data.Beans;
using Message.Role;
using System.Collections.Generic;

public class JinHuaPanel : TabPage
{

    private StrengthWindow owner;

    private UI_JinHuaPanel panel;

    private PetInfo petInfo;

    private t_petBean petBean;
    private bool isClickSX = false;

    private List<FairyGUI.GLoader> starList = new List<FairyGUI.GLoader>();
    /// <summary>
    /// 需要的碎片数量
    /// </summary>
    private int needFragNum;

    public JinHuaPanel(StrengthWindow owner)
    {
        this.owner = owner;
        panel = owner.Window.m_jinhuaPanel;
        panel.m_fragBtn.onClick.Add(OnFragBtn);
        panel.m_jinhuaBtn.onClick.Add(OnJinHuaBtn);
        panel.m_addBtn.onClick.Add(OnAddBtn);

        InitStarList();
    }

    private void InitStarList()
    {
        starList.Add(panel.m_star1);
        starList.Add(panel.m_star2);
        starList.Add(panel.m_star3);
        starList.Add(panel.m_star4);
        starList.Add(panel.m_star5);
        starList.Add(panel.m_star6);
        starList.Add(panel.m_star7);
    }

    private void OnAddBtn()
    {
        if (petInfo != null)
        {
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petInfo.petId);
            if (petBean != null)
            {
                int itemId = petBean.t_fragment_id;
                t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(itemId);
                if (itemBean != null)
                {
                    if (!(string.IsNullOrEmpty(itemBean.t_value)))
                    {
                        int[] num = GTools.splitStringToIntArray(itemBean.t_value);
                        TwoParam<int, int> twoParam = new TwoParam<int, int>();
                        twoParam.value1 = itemId;
                        twoParam.value2 = num[0];
                        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
                    }
                    //else
                    //{
                    //    if(!(string.IsNullOrEmpty(itemBean.t_)))
                    //}
                }
            }
        }
    }

    private void OnResBagUpdate(GameEvent evt)
    {
        RefreshView();
    }

    public override void OnHide()
    {
        GED.ED.removeListener(EventID.ResBagUpdate, OnResBagUpdate);
    }

    public override void OnShow()
    {
        GED.ED.addListener(EventID.ResBagUpdate, OnResBagUpdate);
        RefreshView();
    }

    public override void RefreshView(bool isNet = false)
    {
        petInfo = owner.strengthData.CurSelectPetInfo;
        if (petInfo != null
            && petInfo.fightInfo != null)
        {
            panel.m_fightTxt.text = petInfo.fightInfo.fightPower + "";
            panel.m_atkTxt.text = petInfo.fightInfo.atk + "";
            panel.m_defTxt.text = petInfo.fightInfo.def + "";
            panel.m_hpTxt.text = petInfo.fightInfo.hp + "";
        }
        if (petInfo != null)
        {
            petBean = ConfigBean.GetBean<t_petBean, int>(petInfo.petId);
            if (petBean != null)
            {
                int cur = BagService.Singleton.GetItemNum(petBean.t_fragment_id);
                //判断是否达到最大等级
                int maxStar = PetService.Singleton.GetPetMaxStar();
                if (petInfo.basInfo.star >= maxStar)
                {
                    panel.m_progressBar.value = 0;
                    panel.m_progressBar.max = 0;
                    panel.m_progressTxt.text = cur  + "";
                    panel.m_jinhuaBtn.grayed = true;
                    panel.m_goldTxt.text = "0";
                }
                else
                {
                    TwoParam<int, int> param = UIUtils.GetStarUpCount(petInfo.petId, petInfo.basInfo.star);
                    panel.m_progressBar.value = cur;
                    panel.m_progressBar.max = param.value2;
                    needFragNum = param.value2;
                    panel.m_progressTxt.text = cur + "/" + param.value2;
                    panel.m_goldTxt.text = param.value1 + "";
                    panel.m_jinhuaBtn.grayed = false;
                }
            }

            //升星成功
            if (isNet && isClickSX)
            {
                Logger.log("升星成功！");
                owner.OpenChild<JinHuaSuccessWindow>(WinInfo.Create(false, null, false, owner.winName));
                isClickSX = false;
            }
            RefreshStarList();
        }
    }
    /// <summary>
    /// 刷新星星的数量
    /// </summary>
    private void RefreshStarList()
    {
        if (petInfo != null)
        {
            int count = starList.Count;
            for (int i = 0; i < count; i++)
            {
                if (i < petInfo.basInfo.star)
                    UIGloader.SetUrl(starList[i], UIUtils.GetLoaderUrl(WinEnum.UI_Strength, "xing03"));
                else
                    UIGloader.SetUrl(starList[i], UIUtils.GetLoaderUrl(WinEnum.UI_Strength, "xing04"));
            }
        }
    }


    private void OnFragBtn()
    {
        if (petBean != null)
        {
            //判断是否达到最大等级
            int maxStar = PetService.Singleton.GetPetMaxStar();
            if (petInfo != null && petInfo.basInfo.star >= maxStar)
            {
                TipWindow.Singleton.ShowTip("已满星");
            }
            //if (IsFull())
            //{
            //    TipWindow.Singleton.ShowTip("碎片已满");
            //}
            else
            {
                ThreeParam<int, int, int> threeParam = new ThreeParam<int, int, int>();
                threeParam.value1 = petBean.t_fragment_id;
                threeParam.value2 = needFragNum;
                threeParam.value3 = owner.strengthData.CurSelectPetInfo.petId;
                owner.OpenChild<WanNengFragWindow>(WinInfo.Create(false, owner.winName, false, threeParam));
            }
            
        }
    }

    private void OnJinHuaBtn()
    {
        if (petInfo != null)
        {
            int maxStar = PetService.Singleton.GetPetMaxStar();
            if (petInfo.basInfo.star >= maxStar)
            {
                TipWindow.Singleton.ShowTip("宠物已达最大星级");
                return;
            }
            TwoParam<int, int> param = UIUtils.GetStarUpCount(petInfo.petId, petInfo.basInfo.star);
            Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGridInfo(petBean.t_fragment_id);
            if (gridInfo == null || gridInfo.itemInfo.num < param.value2)
            {
                //跳转到碎片来源界面
                t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petInfo.petId);
                if (petBean != null)
                {
                    ThreeParam<int, int, int> threeParam = new ThreeParam<int, int, int>();
                    threeParam.value1 = petBean.t_fragment_id;
                    threeParam.value2 = needFragNum;
                    threeParam.value3 = owner.strengthData.CurSelectPetInfo.petId;
                    owner.OpenChild<WanNengFragWindow>(WinInfo.Create(false, owner.winName, false, threeParam));
                }
            }
            else
            {
                ResRoleInfo roleInfo = RoleService.Singleton.RoleInfo;
                if (roleInfo != null)
                {
                    if (roleInfo.roleInfo.gold >= param.value1)
                    {
                        owner.strengthData.UpdateOldPetInfo();
                        PetService.Singleton.ReqPetStarUp(petInfo.petId, gridInfo.id, param.value2);
                        isClickSX = true;
                    }
                    else
                    {
                        TipWindow.Singleton.ShowTip("金币不足");
                    }
                }
            }
        }
    }
    /// <summary>
    /// 碎片是否已经满了
    /// </summary>
    /// <returns></returns>
    private bool IsFull()
    {
        int cur = BagService.Singleton.GetItemNum(petBean.t_fragment_id);
        return cur >= needFragNum;
    }

    public override void OnClose()
    {
        GED.ED.removeListener(EventID.ResBagUpdate, OnResBagUpdate);
        petInfo = null;
        petBean = null;
    }
    
}
