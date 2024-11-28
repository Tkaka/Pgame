using UI_SaoDangJieSuan;
using Data.Beans;
using Message.Pet;
using Message.Dungeon;
using System.Collections.Generic;
using UnityEngine;
using Message.Bag;
using System.Collections;
using DG.Tweening;
using FairyGUI;

public class SaoDangJieSuanWindow : BaseWindow
{
    private UI_SaoDangJieSuanWindow window;

    Tweener tweener;
    private int bestItemId = 0;   //最好道具ID
    private int bestItemNum = 0;  //最好道具数量
    
    private float m_showtime = 0.7f;
    private bool isVoking = false;

    private bool m_isStartShow = false;
    private int m_curShowCellInex = 0;
    private int m_totalCellNum;   //格子的总数量

    private List<BaseReward> m_awards = new List<BaseReward>();

    private struct BaseReward
    {
        public Award award; 
        public int actId;  //关卡ID
        public int count;  //第几战
    }

    private long coroutineID = 0;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_SaoDangJieSuanWindow>();
        _InitList();
        InitView();

    }

    private void _InitList()
    {
        window.m_MainList.SetVirtual();
        window.m_MainList.itemProvider = _ItemProvider;
        window.m_MainList.itemRenderer = _ItemRender;
    }

    private string _ItemProvider(int index)
    {
        TwoParam<List<SweepItem>, List<ItemInfo>> param = Info.param as TwoParam<List<SweepItem>, List<ItemInfo>>;
        if (param == null)
             return null;

        if (index < 0 || index >= m_totalCellNum)
            return null;

        if (index < m_awards.Count)
            return UI_rewardResultCell.URL;

        if (index == m_awards.Count)
        {
            return UI_SaoDangCompleteCell.URL;
        }  
        else if (index == m_awards.Count + 1)
        {
            return UI_rewardExtraCell.URL;
        }
        return null;
    }

    private void _ItemRender(int index, GObject obj)
    {
        TwoParam<List<SweepItem>, List<ItemInfo>> param = Info.param as TwoParam<List<SweepItem>, List<ItemInfo>>;
        if (param == null)
            return;

        if (index < 0 || index >= m_totalCellNum)
            return;

        if (index < m_awards.Count)
            _ShowBaseReward(m_awards[index], obj);

        //基本奖励刚完就是额外奖励
        if (index == m_awards.Count)
        {
            //扫荡成功
            //(obj as UI_SaoDangCompleteCell).m_anim.Play();
        } 
        else if (index == m_awards.Count + 1)
        {
            //额外奖励
            _ShowExtraReward(param.value2, obj);
        }

        if (m_isStartShow)
        {
            obj.alpha = (index <= m_curShowCellInex) ? 1 : 0;
        }
        else
        {
            obj.alpha = 0;
        }
    }

    private void _ShowBaseReward(BaseReward info, GObject obj)
    {
        UI_rewardResultCell rewardCell = obj as UI_rewardResultCell;
        if (rewardCell == null)
            return;

        int actId = info.actId;
        Award awards = info.award;

        if (actId != 0)
        {
            t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actId);
            if (actBean != null)
            {
                rewardCell.m_txtFightNumber.text = string.Format("{0}第{1}战", actBean.t_name_id, info.count + 1);
            }
        }
        else
        {
            rewardCell.m_txtFightNumber.text = string.Format("第{0}战", info.count + 1);
            window.m_btnContinue.visible = false;
        }

        int exp = 0;
        int coin = 0;
        List<ItemInfo> items;
        LevelService.Singleton.FilterItem(awards.items, out exp, out coin, out items);
        rewardCell.m_expAndCoin.m_JingYanNumber.text = exp.ToString();
        rewardCell.m_expAndCoin.m_JinBiNumber.text = coin.ToString();


        if (items.Count == 0)
        {
            rewardCell.m_rewardGroup.visible = false;
            rewardCell.m_txtNoReward.visible = true;
        }
        else
        {
            rewardCell.m_rewardGroup.visible = true;
            rewardCell.m_txtNoReward.visible = false;

            rewardCell.m_List.RemoveChildren(0, -1, true);
            for (int j = 0; j < items.Count; ++j)
            {
                //if (m_isStartShow == false)
                //{
                //    //第一次刷
                //    _SetBestItemAndNum(items[j].id, items[j].num);
                //}
                 
                rewardCell.m_List.AddChild(AddDiaoLuoList(items[j].id, items[j].num));

            }
        }
    }
    

    private void _ShowExtraReward(List<ItemInfo> infos , GObject obj)
    {
        //有额外获得的道具
        UI_rewardExtraCell extraReward = obj as UI_rewardExtraCell;
        if (extraReward == null)
            return;
        //额外奖励
        if (infos.Count > 0)
        {

            extraReward.m_List.RemoveChildren(0, -1, true);
            for (int i = 0; i < infos.Count; i++)
            {

                extraReward.m_List.AddChild(AddDiaoLuoList(infos[i].id, infos[i].num));

            }
        }
    }

    public override void InitView()
    {
        base.InitView();
        window.m_QueDing.onClick.Add(Close);
        window.m_Close.onClick.Add(GuanBi);
        window.m_btnFastQueDing.onClick.Add(Close);
        window.m_btnContinue.onClick.Add(_OnContinueClick);
        window.m_MainList.onClick.Add(_OnListClick);
        window.m_bestItem.visible = false;

        //传入扫荡次数
        _SaoDangList();
         

    }

    private void _ShowBestItem()
    {
        if (bestItemId == 0)
            return;

        //最好道具获得情况
        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(bestItemId);
        if (bean != null)
        {
            window.m_bestItem.visible = true;

            CommonItem commonItem = window.m_bestItem.m_itemIcon as CommonItem;
            commonItem.itemId = bestItemId;
            commonItem.isShowNum = false;
            commonItem.RefreshView();

            window.m_bestItem.m_txtGetName.text = bean.t_name;
            window.m_bestItem.m_txtGetName.color = UIUtils.GetColorByQuality(UIUtils.GetDefaultItemQuality(bestItemId));
            window.m_bestItem.m_txtGetNum.text = bestItemNum.ToString();

        }

    }

    //IEnumerator AddCell()
    //{
    //    isVoking = true;
    //    TwoParam<List<SweepItem>, List<ItemInfo>> param = Info.param as TwoParam<List<SweepItem>, List<ItemInfo>>;
    //    if (param == null)
    //        yield return null;

    //    //基本奖励
    //    int iRealCount = 0;  //真实扫荡次数与请求扫荡次数差值
    //    int iTotalReqCount = 0;
    //    for (int i = 0; i < param.value1.Count; i++)
    //    {
    //        iRealCount += param.value1[i].award.Count;
    //        iTotalReqCount += param.value1[i].reqNum;

    //        int actId = param.value1[i].actId;
    //        List<Award> awards = param.value1[i].award;
    //        for (int count = 0; count < awards.Count; ++count)
    //        {
    //            UI_rewardResultCell rewardCell = UI_rewardResultCell.CreateInstance();

    //            if (actId != 0)
    //            {
    //                t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actId);
    //                if (actBean != null)
    //                {
    //                    rewardCell.m_txtFightNumber.text = string.Format("{0}第{1}战", actBean.t_name_id, count + 1);
    //                }
    //            }
    //            else
    //            {
    //                rewardCell.m_txtFightNumber.text = string.Format("第{0}战", count + 1);
    //                window.m_btnContinue.visible = false;
    //            }
               
    //            int exp = 0;
    //            int coin = 0;
    //            List<ItemInfo> items;
    //            LevelService.Singleton.FilterItem(awards[count].items, out exp, out coin, out items);
    //            rewardCell.m_expAndCoin.m_JingYanNumber.text = exp.ToString();
    //            rewardCell.m_expAndCoin.m_JinBiNumber.text = coin.ToString();


    //            if (items.Count == 0)
    //            {
    //                rewardCell.m_rewardGroup.visible = false;
    //                rewardCell.m_txtNoReward.visible = true;
    //            }
    //            else
    //            {
    //                rewardCell.m_rewardGroup.visible = true;
    //                rewardCell.m_txtNoReward.visible = false;

    //                for (int j = 0; j < items.Count; ++j)
    //                {
    //                    _SetBestItemAndNum(items[j].id, items[j].num);
    //                    rewardCell.m_List.AddChild(AddDiaoLuoList(items[j].id, items[j].num));

    //                }
    //            }

    //            window.m_MainList.AddChild(rewardCell);
    //            TweenShow(rewardCell);
    //            yield return new WaitForSeconds(m_showtime);
    //        }
    //    }

    //    //扫荡显示完成图标
    //    //UI_WanChenPic wancheng = UI_WanChenPic.CreateInstance();
         
    //    //window.m_MainList.AddChild(wancheng);
    //    //TweenShow(wancheng);
    //    //yield return new WaitForSeconds(m_showtime);

    //    //额外奖励
    //    List<ItemInfo> extraItems = param.value2;
    //    if (extraItems.Count > 0)
    //    {
    //        //有额外获得的道具
    //        UI_rewardExtraCell extraReward = UI_rewardExtraCell.CreateInstance();
    //        for (int i = 0; i < extraItems.Count; i++)
    //        {

    //            extraReward.m_List.AddChild(AddDiaoLuoList(extraItems[i].id, extraItems[i].num));

    //        }

    //        window.m_MainList.AddChild(extraReward);
    //        TweenShow(extraReward);
    //        yield return new WaitForSeconds(m_showtime);

    //    }

    //    if (iTotalReqCount > iRealCount)
    //    {
    //        //请求的扫荡由于体力不足未能全部扫荡
    //        UI_txtNoReward txtCell = UI_txtNoReward.CreateInstance();
    //        txtCell.m_txtNoReward.text = string.Format(" 体力不足，扫荡{0}次", iRealCount);
    //        window.m_MainList.AddChild(txtCell);
    //        TweenShow(txtCell);
    //    }

    //    _ShowBestItem();
    //    isVoking = false;
    //    yield return new WaitForSeconds(m_showtime);
    //}

    //渐现格子
    private void TweenShow(GObject obj, int i)
    {
        if (tweener != null && tweener.IsActive())
            tweener.Kill();
        obj.alpha = 0;
        tweener = DOTween.To(() => obj.alpha, alpha => obj.alpha = alpha, 1, m_showtime).OnComplete(() => { window.m_MainList.ScrollToView(i, true); });
    }
    private void _SaoDangList()
    {

        m_isStartShow = false;
        TwoParam<List<SweepItem>, List<ItemInfo>> param = Info.param as TwoParam<List<SweepItem>, List<ItemInfo>>;
        if (param == null)
            return;

        m_curShowCellInex = 0;
        m_awards.Clear();
        for (int i = 0; i < param.value1.Count; i++)
        {
            SweepItem sweepItem = param.value1[i];
            for (int j = 0; j < sweepItem.award.Count; j++)
            {
                BaseReward reward = new BaseReward();
                reward.award = sweepItem.award[j];
                _SetAwawrd(reward.award);
                //保存当前奖励的关卡ID
                reward.actId = sweepItem.actId;
                reward.count = j;
                m_awards.Add(reward);
            }

        }

        //+ 扫荡成功 + 额外奖励的那次

        m_totalCellNum = m_awards.Count + 1 + 1;
        window.m_MainList.numItems = m_totalCellNum;
        window.m_MainList.ScrollToView(0);

        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
        coroutineID = CoroutineManager.Singleton.startCoroutine(_TweenShowList());


        window.m_btnFastSaoDang.visible = true;
        window.m_QueDing.visible = false;

        _ShowBestItem();
    }

    private void _SetAwawrd(Award award)
    {
        for (int i = 0; i < award.items.Count; i++)
        {
            _SetBestItemAndNum(award.items[i].id, award.items[i].num);
        }
    }

    private IEnumerator _TweenShowList()
    {
        m_isStartShow = true;
        isVoking = true;
        window.m_MainList.scrollPane.touchEffect = false;
        for (int i = 0; i < window.m_MainList.numItems; i++)
        {
            m_curShowCellInex = i;
            GObject obj = window.m_MainList.GetChildAt(window.m_MainList.ItemIndexToChildIndex(i));
            if ((obj as UI_SaoDangCompleteCell) == null)
            {
                TweenShow(obj, i);
            }
            else
            {
                obj.alpha = 1;
                (obj as UI_SaoDangCompleteCell).m_anim.Play();
            }
            
            yield return new WaitForSeconds(m_showtime);
        }

        window.m_MainList.scrollPane.touchEffect = true;
        isVoking = false;
       // _ShowBestItem();
        yield return new WaitForSeconds(m_showtime);
    }


    //传入掉落物品ID,读表填充数据，返回一个    UI_ChongWuSuiPianItem对象出去，由SaoDangList入表
    private CommonItem AddDiaoLuoList(int pass, int num = 1)
    {
        CommonItem cell = CommonItem.CreateInstance();
        cell.itemId = pass;
        cell.itemNum = num;
        cell.isShowNum = true;
        cell.SetIconScale(0.7f, 0.7f);
        cell.AddPopupEvent();
        cell.RefreshView();
        return cell;
    }



    //设置最好的道具ID和数量
    private void _SetBestItemAndNum(int itemId, int itemNum)
    {
        if (itemId < 0)
            return;

        t_itemBean curItem = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (curItem == null || curItem.t_type != 4)
        {
            //不是升品道具类型
            return;
        }

        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(100200);
        if (gBean!= null && gBean.t_int_param == itemId)
        {
            //不处理的道具
            return;
        }

        if (bestItemId == 0)
        {
            bestItemId = itemId;
            bestItemNum = itemNum;
            return;
        }

        if (itemId == bestItemId)
        {
            bestItemNum += itemNum;
            return;
        }

        t_itemBean lastItem = ConfigBean.GetBean<t_itemBean, int>(bestItemId);
         
        if (lastItem != null && curItem != null)
        {
            int curQuality = 0;
            int lastQuality = 0;
            int.TryParse(curItem.t_quality, out curQuality);
            int.TryParse(lastItem.t_quality, out lastQuality);
            if (curQuality > lastQuality)
            {
                bestItemId = itemId;
                bestItemNum = itemNum;
            }
        }

    }

    //继续点击
    private void _OnContinueClick()
    {
        TwoParam<List<SweepItem>, List<ItemInfo>> param = Info.param as TwoParam<List<SweepItem>, List<ItemInfo>>;
        if (param == null)
            return;

        List<SweepReqInfo> sweeps = new List<SweepReqInfo>();    
        for (int i = 0; i < param.value1.Count; i++)
        {
            SweepReqInfo sweepInfo = new SweepReqInfo();
            sweepInfo.actId = param.value1[i].actId;
            sweepInfo.num = param.value1[i].reqNum;
            sweeps.Add(sweepInfo);
        }

        if (CheckCanDo(sweeps))
        {
            LevelService.Singleton.ReqSweepAct(sweeps);
        }
 
        Close();

    }

    public bool CheckCanDo(List<SweepReqInfo> sweeps)
    {
        int needEnergy = 0;
        for (int i = 0; i < sweeps.Count; i++)
        {
            SweepReqInfo sweep = sweeps[i];
            t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(sweep.actId);
            if (actBean == null)
                continue;
            needEnergy += actBean.t_comsumePower * sweep.num;
        }
 

        int curEnergy = RoleService.Singleton.RoleInfo.roleInfo.energy;
        if (curEnergy < needEnergy)
        {
            //体力不足
            RoleService.Singleton.BuyEnergy();
            return false;
        }

        return true;
    }

    private void _OnListClick()
    {
        if(isVoking)
            m_showtime = 0.2f;
    }

    public  void GuanBi()
    {
        //清空表内数据
        if (isVoking)
        {
            m_showtime = 0.2f;
        }
        else { Close(); }
         
    }

    protected override void OnClose()
    {
        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
        base.OnClose();
        m_awards.Clear();

    }
}
