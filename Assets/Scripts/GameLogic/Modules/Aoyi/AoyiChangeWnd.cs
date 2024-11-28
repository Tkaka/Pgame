using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;

public class AoyiChangeWnd : BaseWindow
{
    public enum ESortType
    {
        ByQuility,     //按品质
        ByDic,         //按指令
        ByEffect,      //按效果
    }

    private int m_selectPetId;          //宠物ID
    private AoyiService.EStonePage m_page;
    private Dictionary<int, StoneInfoExtra> m_selectStoneDic = new Dictionary<int, StoneInfoExtra>();      //选中的奥义链

    private List<StoneInfoExtra> m_aoyiBagList = new List<StoneInfoExtra>();          //奥义背包列表
    private int m_onePageMaxNum = 24;                                       //一页背包的最大数量 
    private int m_curPage = 1;
    private int m_maxPage = 1;

    private Dictionary<int, Vector2[]> m_equipGridsPos = new Dictionary<int, Vector2[]>();

    private ChangeAyCell m_dragingCell;
    private Vector2 m_dragStartPos;          //拖拽前的位置


    private UI_AoyiChangeWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_AoyiChangeWnd>();
        _Init();
        _BindEvent();

        _ShowCurAoyiInfo();
        _ShowAyBagList();
        //m_window.onTouchEnd.Add(() => { Debug.Log("ddddddddddddddddd" + Stage.inst.touchPosition); });
    }

    private void _BindEvent()
    {
        UI_commonTop commonTop = m_window.m_commonTop as UI_commonTop;
        if (commonTop != null)
        {
            commonTop.m_closeBtn.onClick.Add(OnCloseBtn);
        }

        m_window.m_btnSkill.onClick.Add(_OnSkillClick);
        m_window.m_btnLeft.onClick.Add(_OnLeftClick);
        m_window.m_btnRight.onClick.Add(_OnRightClick);
        m_window.m_btnPut.onClick.Add(_OnOneKePutClick);
        m_window.m_btnUnEquip.onClick.Add(_OnOenKeyUnPutClick);
        m_window.m_dropDown.onChanged.Add(_OnDropDownChanged);
        m_window.m_aoyiBagList.scrollPane.onScroll.Add(_OnScroll);
    }

    private void _Init()
    {
        TwoParam<int, AoyiService.EStonePage> param = Info.param as TwoParam<int, AoyiService.EStonePage>;
        if (param == null)
        {
            Debug.LogError("=============>>参数异常");
            Close();
            return;
        }

        m_selectPetId = param.value1;
        m_page = param.value2;
        int curPageMaxNum = AoyiService.Singleton.GetPageStoneGridNum(m_page);

        List<StoneInfo> stoneInfos = new List<StoneInfo>();
        StonePage page = AoyiService.Singleton.GetPetPageStoneInfos(m_selectPetId, m_page);
        if (page != null)
            stoneInfos = page.stones;

        for (int i = 0; i < curPageMaxNum; i++)
        {
            int gridId = i + 1;
            StoneInfoExtra stoneInfo = null;
            for (int index = 0; index < stoneInfos.Count; index++)
            {
                if (stoneInfos[index].id == gridId)
                {
                    stoneInfo = new StoneInfoExtra(stoneInfos[index], true, 2);
                    break;
                }
            }

            m_selectStoneDic.Add(gridId, stoneInfo);
        }

        m_window.m_aoyiBagList.SetVirtual();
        m_window.m_aoyiBagList.itemProvider = _ItemProvider;
        m_window.m_aoyiBagList.itemRenderer = _ItemRender;
        m_window.m_aoyiBagList.scrollPane.touchEffect = false;
 
    }


    private string _ItemProvider(int index)
    {
        return ChangeAyCell.URL;
    }

    private void _ItemRender(int index, GObject obj)
    {
        ChangeAyCell cell = obj as ChangeAyCell;
        if (cell == null)
            return;

        if (index < 0 || index >= m_aoyiBagList.Count)
            return;

        //Debug.Log("-------------------------------->>>" + index + "    " + m_aoyiBagList[index].stoneInfo.itemId);
        StoneInfoExtra stoneInfo = m_aoyiBagList[index];

        cell.RefreshView(stoneInfo, stoneInfo.isUsing);
        if (stoneInfo.isUsing)
        {
            cell.touchable = false;
        }
        else
        {
            cell.touchable = true;
            _RegisterCellDrag(cell);
        }
        
    }

    //显示奥义的背包列表信息
    private void _ShowAyBagList()
    {
        m_aoyiBagList.Clear();
        List<StoneInfo> stoneInfos = AoyiService.Singleton.GetBagList();
        for (int i = 0; i < stoneInfos.Count; i++)
        {
            //在背包中移除是将所有职位0
            if (stoneInfos[i].itemId == 0)
                continue;
            StoneInfoExtra stoneInfoExtra = new StoneInfoExtra(stoneInfos[i], false, 1);
            m_aoyiBagList.Add(stoneInfoExtra);
        }



        foreach (var info in m_selectStoneDic)
        {
            StoneInfoExtra stoneInfo = info.Value;
            if (stoneInfo != null)
            {
                stoneInfo.isUsing = true;
                m_aoyiBagList.Add(stoneInfo);
            }
 
        }

        m_aoyiBagList.Sort(_SortCompareTo);

        m_window.m_aoyiBagList.numItems = m_aoyiBagList.Count;
        m_maxPage = Mathf.CeilToInt(m_aoyiBagList.Count / (m_onePageMaxNum * 1.0f));
        m_maxPage = m_maxPage == 0 ? 1 : m_maxPage;
        m_window.m_txtPage.text = string.Format("{0}/{1}", m_curPage, m_maxPage);
        m_window.m_btnLeft.visible = false;
        m_window.m_btnRight.visible = m_maxPage > 1;
    }

    //显示当前组合的奥义信息
    private void _ShowCurAoyiInfo()
    {
        int lightNum = 0;
        List<StoneInfo> stoneInfos = new List<StoneInfo>();
        for (int i = 1; i <= m_selectStoneDic.Count; i++)
        {
            if (m_selectStoneDic.ContainsKey(i) && m_selectStoneDic[i] != null)
                stoneInfos.Add(m_selectStoneDic[i].stoneInfo);
            //else
            //    Debug.Log("不存在的格子部位" + i);
        }

        int aoyiId = AoyiService.Singleton.GetActiveAoyiId(m_selectPetId, stoneInfos);
        if (aoyiId == -1)
        {
            //未激活奥义
            m_window.m_txtAoyiName.grayed = true;
            m_window.m_txtAoyiName.text = "未激活任何奥义";
        }
        else
        {
            int quility = AoyiService.Singleton.GetLowQuilityInStones(aoyiId, stoneInfos);
            t_aoyi_zuheBean zuheBean = ConfigBean.GetBean<t_aoyi_zuheBean, int>(aoyiId);
            if (zuheBean != null)
            {
                m_window.m_txtAoyiName.text = zuheBean.t_name;
                int[] arrDic = GTools.splitStringToIntArray(zuheBean.t_group, '+');
                if (arrDic != null)
                    lightNum = arrDic.Length;
            }
            m_window.m_txtAoyiName.grayed = false;
            m_window.m_txtAoyiName.color = UIUtils.GetColorValueByQuility(quility);
         
        }

        //奥义方向图标列表
        AoyiIconList iconList = m_window.m_iconList as AoyiIconList;
        if (iconList != null)
        {
            iconList.RefreshView(stoneInfos, lightNum);
        }


        //显示组合的奥义链列表
        m_window.m_aoyiGroupList.RemoveChildren(0, -1, true);

        for (int i = 1; i <= m_selectStoneDic.Count; i++)
        {
            ChangeAyCell cell = ChangeAyCell.CreateInstance();
            cell.RefreshView(m_selectStoneDic[i], false, (int)m_page * 10 + i, i);
            m_window.m_aoyiGroupList.AddChild(cell);

             
            _RegisterCellDrag(cell);
             
            //cell.onRollOver.Add(() => { _OnTouchEnd(cell); });
        }

        m_window.m_aoyiGroupList.EnsureBoundsCorrect();
        for (int i = 0; i < m_window.m_aoyiGroupList.numItems; i++)
        {
            _InitEquipGirdPos(i, m_window.m_aoyiGroupList.GetChildAt(i));
        }

    }

    private void _InitEquipGirdPos(int partId, GObject cell)
    {
        if (m_equipGridsPos.ContainsKey(partId))
            return;

         
        Vector2 pos = cell.TransformPoint(Vector2.zero, GRoot.inst);
        //Debug.Log("==================坐标" + pos);
         
        Vector2[] arrPos = new Vector2[4];
        arrPos[0].Set(pos.x, pos.y);                  //左上
        arrPos[1].Set(pos.x + cell.width, pos.y + cell.height);   //右下

        //Debug.Log("------------------------>>>四点位置" + arrPos[0] + "    " + arrPos[1] + "     " + arrPos[2] + "      " + arrPos[3]);
        m_equipGridsPos.Add(partId, arrPos);
    }

    private void _RegisterCellDrag(ChangeAyCell cell)
    {
        cell.m_toucher.draggable = false;
        cell.m_toucher.onDragStart.Clear();
        cell.m_toucher.onDragMove.Clear();
        cell.m_toucher.onDragEnd.Clear();
        if (cell.stoneExtraInfo != null)
        {
            cell.m_toucher.draggable = true;
    
            cell.m_toucher.onDragStart.Add(() => { _OnDragStart(cell); });
            cell.m_toucher.onDragMove.Add(() => { _OnDragMove(cell); });
            cell.m_toucher.onDragEnd.Add(() => { _OnDragEnd(cell); });
        }
 
    }

    private void _OnDragStart(ChangeAyCell cell)
    {
        if (m_dragingCell == null)
        {
            m_dragingCell = ChangeAyCell.CreateInstance();
            m_window.AddChild(m_dragingCell);
        }

        //克隆一个出来移动
        m_dragingCell.RefreshView(cell.stoneExtraInfo);
        m_dragingCell.visible = true;
        m_dragStartPos = cell.m_toucher.position;
        //Vector3 pos = GRoot.inst.LocalToGlobal(cell.m_toucher.position);
        //m_dragingCell.LocalToRoot = pos;
        //m_dragingCell = 
    }

    private void _OnDragMove(ChangeAyCell cell)
    {
        if (m_dragingCell != null)
        {
            Vector2 pos = cell.m_toucher.TransformPoint(Vector2.zero, m_window);
            m_dragingCell.SetXY(pos.x, pos.y);
        }
    }

    private void _OnDragEnd(ChangeAyCell cell)
    {
        //Debug.Log("拖拽结束");
     
        cell.m_toucher.position = m_dragStartPos;

        if (m_dragingCell != null)
        {
            m_dragingCell.visible = false;
        }

        Vector2 pos = Stage.inst.touchPosition;
        //pos = GRoot.inst.GlobalToLocal(pos);
        //Debug.Log("---------------------->>" + GRoot.inst.GlobalToLocal(pos));
        //Debug.Log("拖拽的终点位置" + pos);
        if (cell.stoneExtraInfo.isUsing && cell.partID > 0)
        {
            //拖拽的格子是装备上的格子


            int partId = _GetTouchEquipGrid(pos);
            if (partId == -1)
            {
                //没拖到装备格子上为卸下
                foreach (var info in m_selectStoneDic)
                {
                    if (info.Value != null && info.Value.stoneInfo == cell.stoneExtraInfo.stoneInfo)
                    {
                        //在选中列表中移除
                        m_selectStoneDic[info.Key] = null;
                        cell.stoneExtraInfo.isUsing = false;
                        break;
                    }
                }
            }
            else
            {
                if (AoyiService.Singleton.EquipGridIsUnLock(m_page, partId) == false)
                {
                    TipWindow.Singleton.ShowTip("当前部位未解锁");
                    return;
                }

                //拖到了装备格子上
                if (partId == cell.partID)
                {
                    //自己拖到自己上不动
                    return;
                }
                else
                {
                    if (m_selectStoneDic[partId] == null)
                    {
                        m_selectStoneDic[partId] = cell.stoneExtraInfo;
                        m_selectStoneDic[cell.partID] = null;
                    }
                    else
                    {
                        //交换位置
                        _SwapStoneInDic(cell.stoneExtraInfo, m_selectStoneDic[partId]);
                    }

                }
            }
        }
        else
        {
            //拖的是背包中的格子
            int partId = _GetTouchEquipGrid(pos);
            if (partId == -1)
                return;

            if (AoyiService.Singleton.EquipGridIsUnLock(m_page, partId) == false)
            {
                TipWindow.Singleton.ShowTip("当前部位未解锁");
                return;
            }

            if (GetConflictPartId(cell.stoneExtraInfo.stoneInfo) > 0)
            {
                //有冲突
                TipWindow.Singleton.ShowTip("已装备相同类型的奥义石");
                return;
            }

            m_selectStoneDic[partId] = cell.stoneExtraInfo;
            //m_selectStoneDic[cell.partID] = null;
            cell.stoneExtraInfo.isUsing = true;
        }

        _ShowCurAoyiInfo();
        m_window.m_aoyiBagList.RefreshVirtualList();

    }

 

    private int _GetTouchEquipGrid(Vector2 pos)
    { 
        int part = -1;
        foreach (var info in m_equipGridsPos)
        {
            if (_IsInRect(pos, info.Value))//GTools.IsInPolygon(pos, info.Value))
            {
                part = info.Key + 1;
                break;
            }
        }

        //Debug.Log("=====================>>>>>拖拽到的部位" + part);
        return part;
    }

    //交换两个石头在装备格子上的位置
    private void _SwapStoneInDic(StoneInfoExtra stoneA, StoneInfoExtra stoneB)
    {
        int indexA = -1;
        int indexB = -1;

        foreach (var info in m_selectStoneDic)
        {
            if (info.Value == null)
                continue;

            if (stoneA.stoneInfo == info.Value.stoneInfo)
                indexA = info.Key;

            if (stoneB.stoneInfo == info.Value.stoneInfo)
                indexB = info.Key;
        }

        if (indexA != -1 && indexB != -1)
        {
            m_selectStoneDic[indexA] = stoneB;
            m_selectStoneDic[indexB] = stoneA;
        }
    }

    //获得奥义石冲突的部位(-1为没冲突)
    private int GetConflictPartId(StoneInfo stoneInfo)
    {
        int part = -1;
        t_aoyiBean aBean = ConfigBean.GetBean<t_aoyiBean, int>(stoneInfo.itemId);
        if (aBean == null)
            return part;

        int targetType = aBean.t_dic * 10 + aBean.t_type;

        foreach (var info in m_selectStoneDic)
        {
            if (info.Value == null)
                continue;

            t_aoyiBean bean = ConfigBean.GetBean<t_aoyiBean, int>(info.Value.stoneInfo.itemId);
            if (bean == null)
                continue;

            int type = bean.t_dic * 10 + bean.t_type;
            if (type == targetType || (type < 0 && targetType < 0))
            {
                part = info.Key;
            }
        }

        return part;
    }



    //石头排序
    private int _SortCompareTo(StoneInfoExtra a, StoneInfoExtra b)
    {
        if (m_window.m_dropDown.selectedIndex == (int)ESortType.ByDic)
            return AoyiService.Singleton.SortByDic(a.stoneInfo, b.stoneInfo);
        else if (m_window.m_dropDown.selectedIndex == (int)ESortType.ByEffect)
            return AoyiService.Singleton.SortByType(a.stoneInfo, b.stoneInfo);
        else if (m_window.m_dropDown.selectedIndex == (int)ESortType.ByQuility)
            return AoyiService.Singleton.SortByQulity(a.stoneInfo, b.stoneInfo);

        return -1;
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
    }


    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
    }


    //排序规则改变
    private void _OnDropDownChanged()
    {



        m_aoyiBagList.Sort(_SortCompareTo);

        m_window.m_aoyiBagList.numItems = m_aoyiBagList.Count;
    }


    //奥义列表点击
    private void _OnSkillClick()
    {
        WinMgr.Singleton.Open<AoyiRewardWnd>(WinInfo.Create(false, null, false, m_selectPetId), UILayer.Popup);
    }

    private void _OnLeftClick()
    {
        if (m_curPage > 1)
        {
            m_curPage--;
            m_window.m_txtPage.text = string.Format("{0}/{1}", m_curPage, m_maxPage);
            m_window.m_aoyiBagList.ScrollToView((m_curPage - 1) * m_onePageMaxNum, true, true);
        }
    }

    private void _OnRightClick()
    {
        if (m_curPage < m_maxPage)
        {
            m_curPage++;
            m_window.m_txtPage.text = string.Format("{0}/{1}", m_curPage, m_maxPage);
            m_window.m_aoyiBagList.ScrollToView((m_curPage - 1) * m_onePageMaxNum, true, true);
        }
    }

    private void _OnScroll()
    {
        m_window.m_btnLeft.visible = false;
        m_window.m_btnRight.visible = false;
        if (!(m_window.m_aoyiBagList.scrollPane.posX == 0))
        {
            m_window.m_btnLeft.visible = true;
        }

        if (!m_window.m_aoyiBagList.scrollPane.isRightMost)
        {
            m_window.m_btnRight.visible = true;
        }
    }

    private void _OnOneKePutClick()
    {
        ThreeParam<int, AoyiService.EStonePage, Action<object>> param = new ThreeParam<int, AoyiService.EStonePage, Action<object>>();
        param.value1 = m_selectPetId;
        param.value2 = m_page;
        param.value3 = _OneKeyPutCallBack;
        WinMgr.Singleton.Open<OneKeyPlaceWnd>(WinInfo.Create(false, null, false, param), UILayer.Popup);
    }

    //一键放置的回调
    private void _OneKeyPutCallBack(object param)
    {
        Dictionary<int, StoneInfoExtra> dic = param as Dictionary<int, StoneInfoExtra>;
        if (dic == null)
            return;

        //先卸下已装上的
        for (int i = 1; i <= m_selectStoneDic.Count; i++)
        {
            if (m_selectStoneDic.ContainsKey(i) && m_selectStoneDic[i] != null)
            {
                m_selectStoneDic[i].isUsing = false;
                m_selectStoneDic[i] = null;
            }
        }

        foreach (var info in dic)
        {
            StoneInfoExtra stoneExtra = null;
            if (info.Value != null)
            {
                stoneExtra = _GetStonExtraByStoneInfo(info.Value.stoneInfo);
            }  

            if(stoneExtra != null)
                stoneExtra.isUsing = true;

            if (m_selectStoneDic[info.Key] == null)
            {
                //当前没有装备
                m_selectStoneDic[info.Key] = stoneExtra;
            }
            else
            {
                m_selectStoneDic[info.Key].isUsing = false;
                m_selectStoneDic[info.Key] = stoneExtra;
            }
        }

        _ShowCurAoyiInfo();
        m_window.m_aoyiBagList.RefreshVirtualList();
    }

    public StoneInfoExtra _GetStonExtraByStoneInfo(StoneInfo stoneInfo)
    {
        for (int i = 0; i < m_aoyiBagList.Count; i++)
        {
            if (m_aoyiBagList[i].stoneInfo == stoneInfo)
                return m_aoyiBagList[i];
        }

        return null;
    }

    private void _OnOenKeyUnPutClick()
    {
        //卸下所有的
        for (int i = 1; i <= m_selectStoneDic.Count; i++)
        {
            if (m_selectStoneDic.ContainsKey(i) && m_selectStoneDic[i] != null)
            {
                m_selectStoneDic[i].isUsing = false;
                m_selectStoneDic[i] = null;
            }
        }

        _ShowCurAoyiInfo();
        m_window.m_aoyiBagList.RefreshVirtualList();
    }

    private void _SendToEquip()
    {
        List<EquipInfo> equipInfos = new List<EquipInfo>();
        int curEquipNum = 0;
        StonePage page = AoyiService.Singleton.GetPetPageStoneInfos(m_selectPetId, m_page);
        if (page != null)
            curEquipNum = page.stones.Count;

        bool isNeedSend = false;
        foreach (var info in m_selectStoneDic)
        {
            if (info.Value == null)
                continue;

            if (isNeedSend == false)
            {
                isNeedSend = AoyiService.Singleton.EquipPartIsChange(m_selectPetId, m_page, info.Value, info.Key);
                
            }

            EquipInfo equipInfo = new EquipInfo();
            equipInfo.equipId = info.Key;
            equipInfo.gridId = info.Value.stoneInfo.id;
            equipInfo.source = info.Value.type;
            equipInfos.Add(equipInfo);
        }

        if(isNeedSend || curEquipNum != equipInfos.Count)
            AoyiService.Singleton.ReqEquip(m_selectPetId, m_page, equipInfos);
    }


    //是否在矩形中
    private bool _IsInRect(Vector2 checkPoint, Vector2 [] polygonPoints)
    {
        Vector2 start = polygonPoints[0];
        Vector2 end = polygonPoints[1];
        if (checkPoint.x > start.x && checkPoint.x < end.x && checkPoint.y > start.y && checkPoint.y < end.y)
            return true;
        return false;
    }

    protected override void OnClose()
    {
        base.OnClose();
        _SendToEquip();
    }


}