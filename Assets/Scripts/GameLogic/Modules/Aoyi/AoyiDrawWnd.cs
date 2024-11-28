using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;

public class AoyiDrawWnd : BaseWindow
{
    private UI_AoyiDrawWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_AoyiDrawWnd>();
        _BindEvent();
        m_window.m_objCoin.visible = true;
        m_window.m_objCoinDraw.visible = false;
        m_window.m_objDiamond.visible = true;
        m_window.m_objDiamondDraw.visible = false;

        _ShowCoinOneDrawInfo();
        _ShowCoinTenDrawInfo();
        _ShowDiamondOneDrawInfo();
        _ShowDiamondTenDrawInfo();

        _ShowCoinInfo();
        _ShowDiamondInfo();
    }

    private void _BindEvent()
    {
        UI_commonTop commonTop = m_window.m_commonTop as UI_commonTop;
        if (commonTop != null)
        {
            commonTop.m_closeBtn.onClick.Add(OnCloseBtn);
        }

        m_window.m_objCoin.onClick.Add(_OnJumpCoinDraw);
        m_window.m_objDiamond.onClick.Add(_OnJumpDiamondDraw);
        //m_window.m_objCoinDraw.onClick.Add(_OnJumpCoin);
        //m_window.m_objDiamondDraw.onClick.Add(_OnJumpDiamond);
    }

    //显示未翻转的金币信息界面
    private void _ShowCoinInfo()
    {
        DrawCountInfo info = AoyiService.Singleton.GetAoyiDrawCountInfo(AoyiService.EAyDrawType.CoinOne);
        if (info == null)
        {
            Debug.LogError("服务器未发次数信息");
            return;
        }

        t_aoyi_drawBean drawBean = ConfigBean.GetBean<t_aoyi_drawBean, int>((int)AoyiService.EAyDrawType.CoinOne);
        if (drawBean == null)
            return;

        if (info.freeCount > 0)
        {
            m_window.m_objCoin.m_txtWCoin.text = "免费";

        }
        else
        {
            m_window.m_objCoin.m_txtWCoin.text = drawBean.t_cost_num + "";
        }
        _RegisterRedDot("Aoyi/DrawReward/Coin", m_window.m_objCoin.m_imgWCoinRed);
    }

    //显示未翻转的钻石信息界面
    private void _ShowDiamondInfo()
    {
        DrawCountInfo info = AoyiService.Singleton.GetAoyiDrawCountInfo(AoyiService.EAyDrawType.DiamondOne);
        if (info == null)
        {
            Debug.LogError("服务器未发次数信息");
            return;
        }

        t_aoyi_drawBean drawBean = ConfigBean.GetBean<t_aoyi_drawBean, int>((int)AoyiService.EAyDrawType.DiamondOne);
        if (drawBean == null)
            return;

        UIGloader.SetUrl(m_window.m_objDiamond.m_imgWDiamond, UIUtils.GetItemIcon(drawBean.t_cost_id));
        if (info.freeCount > 0)
        {
            m_window.m_objDiamond.m_txtWDiamondNum.text = "免费";

        }
        else
        {
            m_window.m_objDiamond.m_txtWDiamondNum.text = drawBean.t_cost_num + "";
        }
        _RegisterRedDot("Aoyi/DrawReward/Diamond", m_window.m_objDiamond.m_imgWDiamondRed);
    }

    //æ˜¾ç¤ºé‡‘å¸å•æŠ½ä¿¡æ¯
    private void _ShowCoinOneDrawInfo()
    {
        DrawCountInfo info = AoyiService.Singleton.GetAoyiDrawCountInfo(AoyiService.EAyDrawType.CoinOne);
        if (info == null)
        {
            Debug.LogError("服务器未发次数信息");
            return;
        }

        t_aoyi_drawBean drawBean = ConfigBean.GetBean<t_aoyi_drawBean, int>((int)AoyiService.EAyDrawType.CoinOne);
        if (drawBean == null)
            return;

        if (info.freeCount > 0)
        {
            m_window.m_objCoinDraw.m_txtCoinFree.text = string.Format("免费次数:{0}/{1}", info.freeCount, drawBean.t_free_num);
            m_window.m_objCoinDraw.m_txtSingleCoinNum.text = "免费";
        }
        else
        {
            m_window.m_objCoinDraw.m_txtCoinFree.text = "每日凌晨5点重置";
            m_window.m_objCoinDraw.m_txtSingleCoinNum.text = drawBean.t_cost_num + "";
        }

        m_window.m_objCoinDraw.m_txtCoinRemainCount.text = string.Format("剩余购买次数:{0}/{1}", drawBean.t_buy_num - info.drawCount, drawBean.t_buy_num);
        m_window.m_objCoinDraw.m_btnCoinBuyOnce.onClick.Clear();
        m_window.m_objCoinDraw.m_btnCoinBuyOnce.onClick.Add(() => 
        {
            if (drawBean.t_buy_num == info.drawCount)
            {
                TipWindow.Singleton.ShowTip("购买次数不足");
                return;
            }

            long haveNum = RoleService.Singleton.GetCurrencyNum(drawBean.t_cost_id);
            if (info.freeCount <= 0 && haveNum < drawBean.t_cost_num)
            {
                string des = string.Format("{0}不足", ConfigBean.GetBean<t_itemBean, int>(drawBean.t_cost_id).t_name);
                TipWindow.Singleton.ShowTip(des);
                return;
            }

            AoyiService.Singleton.ReqAoyiDraw((int)AoyiService.EAyDrawType.CoinOne);
        });

        _RegisterRedDot("Aoyi/DrawReward/Coin", m_window.m_objCoinDraw.m_imgCoinSingleRed);
    }


    //æ˜¾ç¤ºé‡‘å¸åè¿žæŠ½ä¿¡æ¯
    private void _ShowCoinTenDrawInfo()
    {
        DrawCountInfo info = AoyiService.Singleton.GetAoyiDrawCountInfo(AoyiService.EAyDrawType.CoinTen);
        if (info == null)
        {
            Debug.LogError("服务器未发次数信息");
            return;
        }

        t_aoyi_drawBean drawBean = ConfigBean.GetBean<t_aoyi_drawBean, int>((int)AoyiService.EAyDrawType.CoinTen);
        if (drawBean == null)
            return;

        m_window.m_objCoinDraw.m_txtCoinTenNum.text = drawBean.t_cost_num + "";
        m_window.m_objCoinDraw.m_txtCoinTenRemainNum.text = string.Format("剩余购买次数:{0}/{1}", drawBean.t_buy_num - info.drawCount, drawBean.t_buy_num);
        m_window.m_objCoinDraw.m_btnCoinTen.onClick.Clear();
        m_window.m_objCoinDraw.m_btnCoinTen.onClick.Add(() =>
        {
            if (drawBean.t_buy_num == info.drawCount)
            {
                TipWindow.Singleton.ShowTip("购买次数不足");
                return;
            }

            long haveNum = RoleService.Singleton.GetCurrencyNum(drawBean.t_cost_id);
            if (haveNum < drawBean.t_cost_num)
            {
                string des = string.Format("{0}不足", ConfigBean.GetBean<t_itemBean, int>(drawBean.t_cost_id).t_name);
                TipWindow.Singleton.ShowTip(des);
                return;
            }

            AoyiService.Singleton.ReqAoyiDraw((int)AoyiService.EAyDrawType.CoinTen);
        });
    }

    //æ˜¾ç¤ºé’»çŸ³å•æŠ½
    private void _ShowDiamondOneDrawInfo()
    {
        DrawCountInfo info = AoyiService.Singleton.GetAoyiDrawCountInfo(AoyiService.EAyDrawType.DiamondOne);
        if (info == null)
        {
            Debug.LogError("服务器未发次数信息");
            return;
        }

        t_aoyi_drawBean drawBean = ConfigBean.GetBean<t_aoyi_drawBean, int>((int)AoyiService.EAyDrawType.DiamondOne);
        if (drawBean == null)
            return;

        if (info.freeCount > 0)
        {
           
            m_window.m_objDiamondDraw.m_txtDiamondFreeCount.text = string.Format("免费次数:{0}/{1}", info.freeCount, drawBean.t_free_num);
            m_window.m_objDiamondDraw.m_txtSingleDiamondNum.text = "免费";
            UIGloader.SetUrl(m_window.m_objDiamondDraw.m_imgSingleDiamond, UIUtils.GetItemIcon(drawBean.t_cost_id));
        }
        else
        {
            m_window.m_objDiamondDraw.m_txtDiamondFreeCount.text = "每日凌晨5点重置";

            //æ˜¯å¦æœ‰ä»£æŠ½é“å…·
            int haveDaiChouNum = BagService.Singleton.GetItemNum(drawBean.t_replace_id);
            if (haveDaiChouNum > 0)
            {
                UIGloader.SetUrl(m_window.m_objDiamondDraw.m_imgSingleDiamond, UIUtils.GetItemIcon(drawBean.t_replace_id));
                m_window.m_objDiamondDraw.m_txtSingleDiamondNum.text = string.Format("{0}/{1}", haveDaiChouNum, 1);
            }
            else
            {
                UIGloader.SetUrl(m_window.m_objDiamondDraw.m_imgSingleDiamond, UIUtils.GetItemIcon(drawBean.t_cost_id));
                m_window.m_objDiamondDraw.m_txtSingleDiamondNum.text = drawBean.t_cost_num + "";
            }
           
        }

        m_window.m_objDiamondDraw.m_txtSingleDiamondRemain.text = string.Format("剩余购买次数:{0}/{1}", drawBean.t_buy_num - info.drawCount, drawBean.t_buy_num);

        m_window.m_objDiamondDraw.m_btnSingleDiamond.onClick.Clear();
        m_window.m_objDiamondDraw.m_btnSingleDiamond.onClick.Add(()=>
        {
            if (drawBean.t_buy_num == info.drawCount)
            {
                TipWindow.Singleton.ShowTip("购买次数不足");
                return;
            }

            //æ˜¯å¦æœ‰ä»£æŠ½é“å…·
            int haveDaiChouNum = BagService.Singleton.GetItemNum(drawBean.t_replace_id);

            long haveNum = RoleService.Singleton.GetCurrencyNum(drawBean.t_cost_id);
            if (info.freeCount <= 0 && haveDaiChouNum <= 0 && haveNum < drawBean.t_cost_num)
            {
                string des = string.Format("{0}不足", ConfigBean.GetBean<t_itemBean, int>(drawBean.t_cost_id).t_name);
                TipWindow.Singleton.ShowTip(des);
                return;
            }

            AoyiService.Singleton.ReqAoyiDraw((int)AoyiService.EAyDrawType.DiamondOne);
        });

        _RegisterRedDot("Aoyi/DrawReward/Diamond", m_window.m_objDiamondDraw.m_imgDiamondRed);
    }


    //æ˜¾ç¤ºé’»çŸ³10è¿žæŠ½
    private void _ShowDiamondTenDrawInfo()
    {
        DrawCountInfo info = AoyiService.Singleton.GetAoyiDrawCountInfo(AoyiService.EAyDrawType.DiamondTen);
        if (info == null)
        {
            Debug.LogError("服务器未发次数信息");
            return;
        }

        t_aoyi_drawBean drawBean = ConfigBean.GetBean<t_aoyi_drawBean, int>((int)AoyiService.EAyDrawType.DiamondTen);
        if (drawBean == null)
            return;

        //æ˜¯å¦æœ‰ä»£æŠ½é“å…·
        int haveDaiChouNum = BagService.Singleton.GetItemNum(drawBean.t_replace_id);
        if (haveDaiChouNum > 0)
        {
            UIGloader.SetUrl(m_window.m_objDiamondDraw.m_imgDiamondTen, UIUtils.GetItemIcon(drawBean.t_replace_id));
            m_window.m_objDiamondDraw.m_txtDiamondTenNum.text = string.Format("{0}/{1}", haveDaiChouNum, 10);
        }
        else
        {
            UIGloader.SetUrl(m_window.m_objDiamondDraw.m_imgDiamondTen, UIUtils.GetItemIcon(drawBean.t_cost_id));
            m_window.m_objDiamondDraw.m_txtDiamondTenNum.text = drawBean.t_cost_num + "";
        }

        m_window.m_objDiamondDraw.m_txtDiamondTenRemain.text = string.Format("剩余购买次数:{0}/{1}", drawBean.t_buy_num - info.drawCount, drawBean.t_buy_num);
        m_window.m_objDiamondDraw.m_btnDiamondTen.onClick.Clear();
        m_window.m_objDiamondDraw.m_btnDiamondTen.onClick.Add(()=> 
        {
            if (drawBean.t_buy_num == info.drawCount)
            {
                TipWindow.Singleton.ShowTip("次数不足");
                return;
            }

            long haveNum = RoleService.Singleton.GetCurrencyNum(drawBean.t_cost_id);
            if (haveDaiChouNum < 10 && haveNum < drawBean.t_cost_num)
            {
                string des = string.Format("{0}不足", ConfigBean.GetBean<t_itemBean, int>(drawBean.t_cost_id).t_name);
                TipWindow.Singleton.ShowTip(des);
                return;
            }

            AoyiService.Singleton.ReqAoyiDraw((int)AoyiService.EAyDrawType.DiamondTen);
        });
    }



    //è·³è½¬åˆ°é‡‘å¸æŠ½å¥–
    private void _OnJumpCoinDraw()
    {
        m_window.m_objCoin.visible = false;
        m_window.m_objCoinDraw.visible = true;
    }

    //è·³è½¬åˆ°é’»çŸ³æŠ½å¥–
    private void _OnJumpDiamondDraw()
    {
        m_window.m_objDiamond.visible = false;
        m_window.m_objDiamondDraw.visible = true;
    }

    //è·³è½¬åˆ°é‡‘å¸å¤–éƒ¨ç•Œé¢
    private void _OnJumpCoin()
    {
        m_window.m_objCoin.visible = true;
        m_window.m_objCoinDraw.visible = false;
    }

    //è·³è½¬åˆ°é’»çŸ³å¤–éƒ¨ç•Œé¢
    private void _OnJumpDiamond()
    {
        m_window.m_objDiamond.visible = true;
        m_window.m_objDiamondDraw.visible = false;
    }

    private void _OnAoyiDrawRewardInfoChange(GameEvent evt)
    {
        _ShowCoinOneDrawInfo();
        _ShowCoinTenDrawInfo();
        _ShowDiamondOneDrawInfo();
        _ShowDiamondTenDrawInfo();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.AoyiDrawRewardInfoChange, _OnAoyiDrawRewardInfoChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.AoyiDrawRewardInfoChange, _OnAoyiDrawRewardInfoChange);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}
