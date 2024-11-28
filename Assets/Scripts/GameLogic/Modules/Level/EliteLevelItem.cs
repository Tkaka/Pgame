using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using FairyGUI;
using Data.Beans;
using Message.Dungeon;
using DG.Tweening;
using DG.Tweening.Core;

public class EliteLevelItem : UI_eliteLevelItem {

    public int actID;
    private long coroutineID;

    private MainLevel parentUI;
    private Tweener tweener;

    public new static UI_eliteLevelItem CreateInstance()
    {
        return (UI_eliteLevelItem)UIPackage.CreateObject("UI_Level", "eliteLevelItem");
    }

    private LevelDataManager LevelData
    {
        get { return parentUI.levelData; }
    }

    public void Init(MainLevel parentUI)
    {
        this.parentUI = parentUI;
        m_toucher.onClick.Remove(OnLevelItemClick);
        m_toucher.onClick.Add(OnLevelItemClick);
        coroutineID = -1;
        RefreshView();
    }

    public void RefreshView()
    {
        RefreshLevelInfo();
        ShowAnimation();
    }

    private void RefreshLevelInfo()
    {
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actID);
        if (actBean != null)
        {
            m_nameLabel.text = actBean.t_name_id;
            UIGloader.SetUrl(m_iconLoader,actBean.t_icon);
            UIGloader.SetUrl(m_levelFramLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetLevelBoradIcon(actBean.t_type)));


            // 设置星星
            ActInfo eliteActInfo = LevelService.Singleton.GetActInfoByID(actID);

            m_starList.visible = IsShowStarList();
            if (eliteActInfo != null)
            {
                UIGloader.SetUrl(m_star1, GetEliteStarIcon(eliteActInfo.star > 0));
                UIGloader.SetUrl(m_star2, GetEliteStarIcon(eliteActInfo.star > 1));
                UIGloader.SetUrl(m_star3, GetEliteStarIcon(eliteActInfo.star > 2));
            }
        }
    }

    private string GetEliteStarIcon(bool isBright)
    {
        if (isBright)
            return UIUtils.GetLoaderUrl(WinEnum.UI_Level, "jingyingxing");
        else
            return UIUtils.GetLoaderUrl(WinEnum.UI_Level, "jingyingxing2");
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

        if (actBean.t_type == (int)LevelType.EliteBoss || LevelService.Singleton.EliteRecentlyID == actID || actInfo.star > 0)
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// 显示将要攻打的关卡的上下抖动的动画
    /// </summary>
    private void ShowAnimation()
    {
        if (LevelService.Singleton.EliteRecentlyID == actID && coroutineID == -1)
        {
            coroutineID = CoroutineManager.Singleton.startCoroutine(ShowShakeAnimation());
        }

        if (LevelService.Singleton.EliteRecentlyID != actID && coroutineID != -1)
        {
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
            coroutineID = -1;
        }
    }

    IEnumerator ShowShakeAnimation()
    {
        float posOffset = 10f;
        if (tweener != null && tweener.IsActive())
        {
            tweener.Kill();
        }
        while (LevelService.Singleton.EliteRecentlyID == actID)
        {
            tweener = DOTween.To(() => this.position, pos => this.position = pos, new Vector3(this.position.x, this.position.y + posOffset, this.position.z), 0.3f);
            yield return new WaitForSeconds(0.3f);
            tweener = DOTween.To(() => this.position, pos => this.position = pos, new Vector3(this.position.x, this.position.y - posOffset, this.position.z), 0.7f);
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void OnLevelItemClick()
    {
        // 如果该关卡没有打开没反应，否则进入战斗准备界面
        if (actID <= LevelService.Singleton.EliteRecentlyID)
        {
            // 可以打
            OneParam<int> param = new OneParam<int>();
            param.value = actID;
            WinInfo winInfo = new WinInfo();
            winInfo.param = param;
            //WinMgr.Singleton.Open<GuanQiaWindow>(winInfo, UILayer.Popup);

            WinMgr.Singleton.Open<eliteGuanQiaWindow>(winInfo, UILayer.Popup);
            //BattleService.Singleton.ReqFightResult(actID, 1, 3);
        }
    }


    public override void Dispose()
    {
        base.Dispose();

        parentUI = null;
        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);

        if (tweener != null && tweener.IsActive())
            tweener.Kill();

        tweener = null;
    }
}
