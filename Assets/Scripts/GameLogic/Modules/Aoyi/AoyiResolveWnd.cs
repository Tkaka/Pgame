using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;

public class AoyiResolveWnd : BaseWindow
{
    private UI_AoyiResolveWnd m_window;
    private Dictionary<int, bool> m_selectResolveGrids = new Dictionary<int, bool>();    //分解的背包格子

    private List<StoneInfo> m_stones;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_AoyiResolveWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnFastSelect.onClick.Add(_OnFastSelectClick);
        m_window.m_btnOk.onClick.Add(_OnOkClick);
        _InitList();
        _ShowResolveList();
    }


    private void _InitList()
    {
        m_window.m_mainList.SetVirtual();
        m_window.m_mainList.itemProvider = _ItemProvider;
        m_window.m_mainList.itemRenderer = _ItemRender;
    }

    private string _ItemProvider(int index)
    {
        return UI_objResolveCell.URL;
    }

    private void _ItemRender(int index, GObject obj)
    {
        UI_objResolveCell cell = obj as UI_objResolveCell;
        if (cell == null)
            return;

        if (index < 0 || index >= m_stones.Count)
            return;

        StoneInfo stoneInfo = m_stones[index];

        //cell.m_txtName.text = ConfigBean.GetBean<t_aoyiBean, int>(stoneInfo.itemId).t_name;
        List<AoyiService.PropertyInfo> propertyInfos = AoyiService.Singleton.GetStoneAddPropertyInfo(stoneInfo.itemId, stoneInfo.bigLevel * 10 + stoneInfo.minLevel);

        //属性
        string strPropertyDes = "";
        for (int i = 0; i < propertyInfos.Count; i++)
        {
            AoyiService.PropertyInfo propertyInfo = propertyInfos[i];
            t_attr_nameBean propertyBean = ConfigBean.GetBean<t_attr_nameBean, int>(propertyInfo.propertyId);
            if (propertyBean == null)
                continue;

             
            if (propertyBean.t_value_type == 0)
            {
                strPropertyDes += string.Format("{0} +{1}\n", propertyBean.t_name_id, (propertyInfo.propertyValue * 0.01) + "%");
            }
            else
            {
                strPropertyDes += string.Format("{0} +{1}\n", propertyBean.t_name_id, propertyInfo.propertyValue);
            }
        }

        if (!m_selectResolveGrids.ContainsKey(stoneInfo.id))
        {
            //默认设为不选中
            m_selectResolveGrids.Add(stoneInfo.id, false);
        }

        cell.m_txtProperty.text = strPropertyDes;
        cell.m_checkBox.onChanged.Clear();
        cell.m_checkBox.onChanged.Add(() => 
        {
            if (m_selectResolveGrids.ContainsKey(stoneInfo.id))
            {
                m_selectResolveGrids[stoneInfo.id] = cell.m_checkBox.selected;
            }
            else
            {
                m_selectResolveGrids.Add(stoneInfo.id, cell.m_checkBox.selected);
            }

            _ShowCanBackInfo();
        });


      
        cell.m_checkBox.selected = m_selectResolveGrids[stoneInfo.id];
        AoyiCommonItem aoyiCell = cell.m_icon as AoyiCommonItem;
        if (aoyiCell != null)
            aoyiCell.RefreshView(stoneInfo.itemId, stoneInfo.bigLevel * 10 + stoneInfo.minLevel);

    }



    private void _ShowResolveList()
    {
        List<StoneInfo> stoneInfos = new List<StoneInfo>();
        List<StoneInfo> bagList = AoyiService.Singleton.GetBagList();

        for (int i = 0; i < bagList.Count; i++)
        {
            if (bagList[i].itemId == 0)
                continue;
            stoneInfos.Add(bagList[i]);
        }

        m_stones = stoneInfos;
        m_window.m_mainList.numItems = m_stones.Count;
        _ShowCanBackInfo();

    }

    public override void AddEventListener()
    {
        base.AddEventListener();
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
    }


    private void _OnFastSelectClick()
    {
        OneParam<Action<object>> param = new OneParam<Action<object>>();
        param.value = _FastCallBack;
        WinMgr.Singleton.Open<ResolveFastSelectWnd>(WinInfo.Create(false, null, false, param), UILayer.Popup);
    }

    private void _FastCallBack(object param)
    {
        Dictionary<int, bool> quilityDic = param as Dictionary<int, bool>;
        if (quilityDic == null)
            return;

        List<StoneInfo> stoneList = AoyiService.Singleton.GetBagList();
        for (int i = 0; i < stoneList.Count; i++)
        {
            StoneInfo stoneInfo = stoneList[i];
            //道具ID石0的是被移除的部位
            if (stoneInfo.itemId == 0)
                continue;

            int quility = UIUtils.GetDefaultItemQuality(stoneInfo.itemId);
            if (quilityDic.ContainsKey(quility))
            {
                if (m_selectResolveGrids.ContainsKey(stoneInfo.id))
                {
                    m_selectResolveGrids[stoneInfo.id] = quilityDic[quility];
                }
                else
                {
                    m_selectResolveGrids.Add(stoneInfo.id, quilityDic[quility]);
                }
            }
            else
            {
                //没有该品质也是没有选中
                if (m_selectResolveGrids.ContainsKey(stoneInfo.id))
                {
                    m_selectResolveGrids[stoneInfo.id] = false;
                }
                else
                {
                    m_selectResolveGrids.Add(stoneInfo.id, false);
                }
            }
             
        }
        m_window.m_mainList.RefreshVirtualList();

        _ShowCanBackInfo();
    }


    //显示分解返还道具信息
    private void _ShowCanBackInfo()
    {
        int stoneNum = 0;
        int aoyiNum = 0;
        int coinNum = 0;
        _GetCurSelectStoneInfo(out stoneNum, out aoyiNum, out coinNum);

        m_window.m_txtSelectNum.text = string.Format("当前选中{0}个奥义石", stoneNum);
        if (stoneNum == 0)
        {
            m_window.m_txtGetDes.visible = false;
        }
        else
        {
            m_window.m_txtGetDes.visible = true;
            m_window.m_txtGetDes.text = string.Format("熔炼可获得：{0} {1}  {2} {3}", UIUtils.GetItemName(-11), aoyiNum, UIUtils.GetItemName(-1), coinNum);
        }
 
    }
    private void _GetCurSelectStoneInfo(out int stoneNum, out int aoyiNum, out int coinNum)
    {
        stoneNum = 0;
        aoyiNum = 0;
        coinNum = 0;

        foreach (var info in m_selectResolveGrids)
        {
            if (info.Value == false)
                continue;

            StoneInfo stoneInfo = AoyiService.Singleton.GetStoneInfoByGridId(info.Key);
            if (stoneInfo == null || stoneInfo.itemId == 0)
            {
                Debug.LogError("背包中对应的格子ID不存在石头" + info.Key);
                continue;
            }

            stoneNum++;
            int aoyi = 0;
            int coin = 0;
            _GetSingleStoneCanBack(stoneInfo, out aoyi, out coin);
            aoyiNum += aoyi;
            coinNum += coin;
        }
    }

    //单个奥义石分解能返还的道具数量
    private void _GetSingleStoneCanBack(StoneInfo stoneInfo, out int aoyiNum, out int coinNum)
    {
        aoyiNum = 0;
        coinNum = 0;
        int quility = UIUtils.GetDefaultItemQuality(stoneInfo.itemId);

        for (int i = 0; i < stoneInfo.bigLevel; i++)
        {
            t_aoyi_level_consumeBean bean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(i * 10 + quility);
            aoyiNum += 10 * bean.t_level_consume;
            coinNum += bean.t_break_consume;
        }

        //还需加上当前超出的小等级消耗
        t_aoyi_level_consumeBean curBean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(stoneInfo.bigLevel * 10 + quility);
        if (curBean != null)
        {
            aoyiNum += curBean.t_level_consume * stoneInfo.minLevel;
        }

        //还要加上分解原本该获得的奥义精华数量
        t_aoyi_resolveBean resolveBean = ConfigBean.GetBean<t_aoyi_resolveBean, int>(quility);
        if (resolveBean != null)
        {
            int[] arrItem = GTools.splitStringToIntArray(resolveBean.t_resolve, '+');
            if (arrItem != null && arrItem.Length >= 2)
            {
                aoyiNum += arrItem[1];
            }

            //金币需要按比率返还
            coinNum = (int)(coinNum * resolveBean.t_gold_back * 0.0001);
        }

    }

    private void _OnOkClick()
    {
        List<int> grids = new List<int>();
        foreach (var info in m_selectResolveGrids)
        {
            if (info.Value == true)
            {
                grids.Add(info.Key);
            }
        }

        AoyiService.Singleton.ReqResolve(grids);
        Close();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}