using Data.Beans;
using UI_VIP;
public class VipPageCell : UI_vipPageCell
{
    private int m_fixHight = 13;

    public new static VipPageCell CreateInstance()
    {
        return UI_vipPageCell.CreateInstance() as VipPageCell;
    }

    public void RefreshView(int vipLevel)
    {
        m_desList.RemoveChildren(0, -1, true);
        m_rewardList.RemoveChildren(0, -1, true);
        t_vipBean vipBean = ConfigBean.GetBean<t_vipBean, int>(vipLevel);
        if (vipBean == null)
            return;


        m_txtRewardDes.text = string.Format("贵族{0}超值礼包包含以下内容:", vipLevel);
        m_txtOriginalPrice.text = vipBean.t_original_price + "";
        m_txtCurPrice.text = vipBean.t_cur_price + "";

        int[] strDesInfos = GTools.splitStringToIntArray(vipBean.t_vip_des, '+');
        int[] lanParam = GTools.splitStringToIntArray(vipBean.t_vip_des_param, '+');
        if (strDesInfos != null && strDesInfos.Length > 0 && lanParam != null && strDesInfos.Length == lanParam.Length)
        {
            for (int i = 0; i < strDesInfos.Length; i++)
            {
                UI_vipDesCell desCell = UI_vipDesCell.CreateInstance();
                desCell.m_txtDes.text = string.Format(UIUtils.GetStrByLanguageID(strDesInfos[i]), lanParam[i]);
                desCell.height = desCell.m_txtDes.height + m_fixHight;
                m_desList.AddChild(desCell);
            }
        }

        string[] itemInfos = GTools.splitString(vipBean.t_giftBag, ';');
        if (itemInfos != null && itemInfos.Length > 0)
        {
            for (int i = 0; i < itemInfos.Length; i++)
            {
                int[] itemInfo = GTools.splitStringToIntArray(itemInfos[i], '+');
                if (itemInfo == null || itemInfo.Length == 0)
                    continue;

                CommonItem commonItem = CommonItem.CreateInstance();
                commonItem.Init(itemInfo[0], itemInfo[1], true);
                commonItem.SetIconScale(0.7f, 0.7f);
                commonItem.RefreshView();

                m_rewardList.AddChild(commonItem);
            }
        }

        m_btnBuy.visible = !VipService.Singleton.GetVipGiftBagState(vipLevel);
        m_objBuyed.visible = VipService.Singleton.GetVipGiftBagState(vipLevel);

        m_btnBuy.grayed = vipLevel > VipService.Singleton.VipLevel;
        m_btnBuy.onClick.Clear();
        m_btnBuy.onClick.Add(()=> {
            if (vipLevel > VipService.Singleton.VipLevel)
            {
                TipWindow.Singleton.ShowTip("贵族等级不足!");
                return;
            }

            VipService.Singleton.ReqBuyVipGiftBag(vipLevel);
        });

    }


    public override void Dispose()
    {
        base.Dispose();
    }

}