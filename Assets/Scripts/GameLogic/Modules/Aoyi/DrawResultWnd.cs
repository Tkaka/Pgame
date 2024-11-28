using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_AoYi;
using Message.Profound;
using Data.Beans;
using Message.Bag;
using System.Collections.Generic;

public class DrawResultWnd : BaseWindow
{
    private UI_DrawResultWnd m_window;
    private ResAoyiDraw m_msg;
    private DoActionInterval m_timer;
    private int m_curShowedNum = 0;   //å½“å‰å·²æ˜¾ç¤ºçš„é“å…·æ•°é‡

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_DrawResultWnd>();
        m_window.m_btnOk.onClick.Add(Close);
        m_msg = Info.param as ResAoyiDraw;
        if (m_msg == null)
            return;

        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_objGroup.visible = false;
        m_window.m_txtTitle.text = string.Format("获得道具:");
        _ShowRewardInfo();

    }

    private void _ShowRewardInfo()
    {
        if (m_timer != null)
        {
            m_timer.kill();
            m_timer = null;
        }

        m_timer = new DoActionInterval();
        m_timer.doAction(0.3f, (param) =>
        {
            if (m_curShowedNum >= m_msg.itemInfos.Count)
            {
                m_timer.kill();
                m_timer = null;
                _ShowCoinInfo();
                return;
            }
            else
            {
                GObject obj = _OnItemCellShow(m_msg.itemInfos[m_curShowedNum]);

                m_window.m_rewardList.AddChild(obj);
                m_window.m_rewardList.ScrollToView(m_window.m_rewardList.numChildren - 1, true, true);
            }

            m_curShowedNum++;
        });
    }

    private GObject _OnItemCellShow(ItemInfo itemInfo)
    {

        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemInfo.id);
        //if (itemBean.t_tab == (int)ItemCategory.AoyiStone)
        //{
        //    //UIPackage.AddPackage(WinEnum.BasePath + WinEnum.UI_AoYi);
        //    AoyiCommonItem stoneItem = AoyiCommonItem.CreateInstance();
        //    stoneItem.RefreshView(itemInfo.id, 0, false);
        //    stoneItem.SetNum(itemInfo.num);
        //    return stoneItem;
        //}
        //else
        //{
            CommonItem commonItem = CommonItem.CreateInstance();
            commonItem.itemId = itemInfo.id;
            commonItem.itemNum = itemInfo.num;
            commonItem.isShowNum = true;
            commonItem.RefreshView(true);
            return commonItem;
        //}

    }

    private void _ShowCoinInfo()
    {
        m_window.m_objGroup.visible = true;
 

        DrawCountInfo info = AoyiService.Singleton.GetAoyiDrawCountInfo((AoyiService.EAyDrawType)m_msg.id);
        if (info == null)
        {
            Debug.LogError("æœåŠ¡å™¨æœªå‘æ¬¡æ•°ä¿¡æ¯");
            return;
        }

        t_aoyi_drawBean drawBean = ConfigBean.GetBean<t_aoyi_drawBean, int>(m_msg.id);
        if (drawBean == null)
            return;

        int haveDaiChouNum = BagService.Singleton.GetItemNum(drawBean.t_replace_id);
        if (haveDaiChouNum > 0)
        {
            UIGloader.SetUrl(m_window.m_imgComsume, UIUtils.GetItemIcon(drawBean.t_replace_id));
            m_window.m_txtNum.text = string.Format("{0}/{1}", drawBean.t_replace_num, haveDaiChouNum);
        }
        else
        {
            UIGloader.SetUrl(m_window.m_imgComsume, UIUtils.GetItemIcon(drawBean.t_cost_id));
            m_window.m_txtNum.text = drawBean.t_cost_num + "";
        }

        if (m_msg.itemInfos.Count > 1)
        {
            m_window.m_btnOpen1.visible = false;
            m_window.m_btnOpen10.visible = true;
            m_window.m_btnOpen10.onClick.Add(() => 
            {
                if (drawBean.t_buy_num == info.drawCount)
                {
                    TipWindow.Singleton.ShowTip("次数不足");
                    return;
                }


                long haveNum = RoleService.Singleton.GetCurrencyNum(drawBean.t_cost_id);
                if (haveDaiChouNum <= 0 && haveNum < drawBean.t_cost_num)
                {
                    string des = string.Format("{0}不足", ConfigBean.GetBean<t_itemBean, int>(drawBean.t_cost_id).t_name);
                    TipWindow.Singleton.ShowTip(des);
                    return;
                }
                AoyiService.Singleton.ReqAoyiDraw(m_msg.id);

                Close();
            });

        }
        else
        {
            m_window.m_btnOpen1.visible = true;
            m_window.m_btnOpen10.visible = false;
            m_window.m_btnOpen1.onClick.Add(() =>
            {
                if (drawBean.t_buy_num == info.drawCount)
                {
                    TipWindow.Singleton.ShowTip("抽取次数不足");
                    return;
                }

                long haveNum = RoleService.Singleton.GetCurrencyNum(drawBean.t_cost_id);
                if (haveDaiChouNum < 10 && haveNum < drawBean.t_cost_num)
                {
                    string des = string.Format("{0}不足", ConfigBean.GetBean<t_itemBean, int>(drawBean.t_cost_id).t_name);
                    TipWindow.Singleton.ShowTip(des);
                    return;
                }

                AoyiService.Singleton.ReqAoyiDraw(m_msg.id);
                Close();
            });
        }

    }


    protected override void OnClose()
    {
        base.OnClose();
        m_msg = null;
        if (m_timer != null)
        {
            m_timer.kill();
            m_timer = null;
        }
    }
}