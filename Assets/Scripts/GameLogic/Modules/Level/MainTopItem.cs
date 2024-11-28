using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using Data.Beans;
using Message.Dungeon;
using DG.Tweening;

public class MainTopItem : UI_mainTopItem {

    private int actID;
    private Tweener tweener;
    private long cortineID;

    public void RefreshView(int levelID)
    {
        actID = levelID;
        RefreshInfo();
        RefreshShowBubble();
    }

    private void RefreshInfo()
    {
        m_bubble.m_bubbleLabel.text = "皮~卡~丘~";

        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actID);
        if (actBean != null)
        {
            m_nameLabel.text = actBean.t_name_id;
            UIGloader.SetUrl(m_iconLoader, actBean.t_icon);
            UIGloader.SetUrl(m_levelFramLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Level, UIUtils.GetLevelBoradIcon(actBean.t_type)));

            // 星星
            m_starList.visible = IsShowStarList();
            this.grayed = !IsShowStarList();
            ActInfo normalActInfo = LevelService.Singleton.GetActInfoByID(actID);
            if (normalActInfo != null)
            {
                UIGloader.SetUrl(m_star1, GetStarIcon(normalActInfo.star > 0));
                UIGloader.SetUrl(m_star2, GetStarIcon(normalActInfo.star > 1));
                UIGloader.SetUrl(m_star3, GetStarIcon(normalActInfo.star > 2));
            }
        }
    }

    private bool IsShowStarList()
    {
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actID);
        if (actBean == null)
        {
            return false;
        }

        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(actID);
        if (actInfo == null)
        {
            return false;
        }

        if (LevelService.Singleton.NormalRecentlyID == actID || actInfo.star > 0)
        {
            return true;
        }

        return false;
    }

    private string GetStarIcon(bool isBright)
    {
        if (isBright)
            return UIUtils.GetLoaderUrl(WinEnum.UI_Common, "xing01");
        else
            return UIUtils.GetLoaderUrl(WinEnum.UI_Common, "xing02");
    }

    private void RefreshShowBubble()
    {
        bool isShowBubble = IsShowBubble();
        //m_bubble.visible = isShowBubble;
        m_bubble.visible = false;
        return;
        if (isShowBubble == true && cortineID == -1)
        {
            cortineID = CoroutineManager.Singleton.startCoroutine(ShowBubble());
        }

        if (isShowBubble == false && cortineID != -1)
        {
            CoroutineManager.Singleton.stopCoroutine(cortineID);
            cortineID = -1;
        }
    }

    IEnumerator ShowBubble()
    {
        if (tweener != null && tweener.IsActive())
            tweener.Kill();
        tweener = null;
        // 显示5秒，然后隐藏2秒
        while (IsShowBubble())
        {
            m_bubble.scale = Vector2.zero;
            tweener = DOTween.To(() => m_bubble.scale, sccle => m_bubble.scale = scale, Vector2.one, 0.5f);
            yield return new WaitForSeconds(5);
            tweener = DOTween.To(() => m_bubble.scale, scale => m_bubble.scale = scale, Vector2.zero, 0.5f);
            yield return new WaitForSeconds(2);
        }
    }

    private bool IsShowBubble()
    {
        return LevelService.Singleton.NormalRecentlyID == actID;
    }

    public override void Dispose()
    {
        if (cortineID != -1)
            CoroutineManager.Singleton.stopCoroutine(cortineID);

        if (tweener != null && tweener.IsActive())
            tweener.Kill();
        tweener = null;

        base.Dispose();
    }
}
