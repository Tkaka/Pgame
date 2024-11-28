using Data.Beans;
using Message.Bag;
using Message.Dungeon;
using UI_Battle;
using Message.Fight;

public class BattleAwardWindow : BaseWindow
{

    private UI_BattleAwardWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_BattleAwardWindow>();
        window.onClick.Add(OnCloseBtn);
        MissionResult res = BattleService.Singleton.FightRes;
        if (res != null && RoleService.Singleton.RoleInfo.roleInfo != null)
        {
            window.m_lvTxt.text = RoleService.Singleton.RoleInfo.roleInfo.level + "";
        }
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(19017);
        if (gBean != null)
            window.m_exp.text = gBean.t_int_param + "";
        ShowStars();
        ShowItems();

        //window.m_rightBg1.x += 1f;
       // window.m_rightBg2.x += 1f;
    }

    private void ShowStars()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean == null)
            return;

        if (bean.t_act_type == 1)
        {
            //主线关卡
            window.m_jingYingStarList.visible = false;
            window.m_normalStarList.visible = true;
            NomalStarList nomalStarList = window.m_normalStarList as NomalStarList;
            if (nomalStarList != null)
            {
                nomalStarList.OnOpen();
            }
        }
        else
        {
            window.m_jingYingStarList.visible = true;
            window.m_normalStarList.visible = false;
            JingYingStarList jingYingStarList = window.m_jingYingStarList as JingYingStarList;
            if (jingYingStarList != null)
            {
                jingYingStarList.OnOpen();
            }

        }
    }


    /// <summary>
    /// 显示所获得的道具
    /// </summary>
    private void ShowItems()
    {
        MissionResult res = BattleService.Singleton.FightRes;
        if (res != null && res.awards != null && res.awards.items != null)
        {
            
            CommonItem commonItem = null;
            foreach (ItemInfo item in res.awards.items)
            {
                t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(item.id);
                if (bean != null)
                {
                    if (bean.t_type == (int)ItemType.Gold)
                    {
                        window.m_gold.text = item.num + "";
                        continue;
                    }

                    commonItem = CommonItem.CreateInstance();
                    commonItem.Init(item.id, item.num, true);
                    commonItem.RefreshView();
                    //UI_BattleItemIcon itemIcon = UI_BattleItemIcon.CreateInstance();
                    //if (bean.t_type < 0)
                    //{
                    //    //itemIcon.m_borderLoader.url = UIUtils.GetItemBorderByQuality(UIUtils.GetDaiBiQulity(item.num, item.id));
                    //    //itemIcon.m_iconLoader.url = UIUtils.GetItemIcon(item.id, item.num);
                    //    UIGloader.SetUrl(itemIcon.m_borderLoader, UIUtils.GetItemBorderByQuality(UIUtils.GetDaiBiQulity(item.num, item.id)));
                    //    UIGloader.SetUrl(itemIcon.m_iconLoader, UIUtils.GetItemIcon(item.id, item.num));
                    //}
                    //else
                    //{
                    //    //itemIcon.m_borderLoader.url = UIUtils.GetItemBorder(item.id, item.num);
                    //    //itemIcon.m_iconLoader.url = UIUtils.GetItemIcon(item.id, item.num);
                    //    UIGloader.SetUrl(itemIcon.m_borderLoader, UIUtils.GetItemBorder(item.id, item.num));
                    //    UIGloader.SetUrl(itemIcon.m_iconLoader, UIUtils.GetItemIcon(item.id, item.num));
                    //}
                    //itemIcon.m_numTxt.text = item.num + "";
                    commonItem.SetScale(0.75f, 0.75f);
                    window.m_itemList.AddChild(commonItem);
                }
            }
            if (commonItem != null)
            {
                window.m_itemList.columnGap -= (int)(commonItem.width * 0.25f);
                window.m_unGetTipLabel.visible = false;
                window.m_itemList.visible = true;
            }
            else
            {
                window.m_unGetTipLabel.visible = true;
                window.m_itemList.visible = false;
            }
        }
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
        BattleService.Singleton.QuitBattle();
    }

}
