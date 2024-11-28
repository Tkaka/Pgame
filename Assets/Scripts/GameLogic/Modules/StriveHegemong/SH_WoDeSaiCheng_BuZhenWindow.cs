using UI_StriveHegemong;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class SH_WoDeSaiCheng_BuZhenWindow : BaseWindow
{
    
    private UI_SH_WoDeSaiCheng_BuZhenWindow window;
    private List<int> shangzhen;
    private List<int> newshangzhen;//新的上阵列表
    private Dictionary<Vector2, int> postion = new Dictionary<Vector2, int>();
    /// <summary>
    /// 拖动的下标
    /// </summary>
    private int dragIndex;
    private Vector2 dragPos;
    private Vector2 changeItemPos;

    UI_SH_BM_ListItem changeItem;

    private List<Rect> rectList = new List<Rect>();

    public override void OnOpen()
    {
        window = getUiWindow<UI_SH_WoDeSaiCheng_BuZhenWindow>();
        newshangzhen = new List<int>();
        AddEvent();
        InitView();
        InitRectList();
    }
    private void AddEvent()
    {
        window.m_TuiChu.onClick.Add(OnCloseBtn);
        window.m_BaoCun.onClick.Add(OnBaoCun);
    }

    private void InitRectList()
    {
        int count = newshangzhen.Count;
        SH_ZR_SaiCheng zhenrong;
        Rect rect;
        Vector2 startPos = window.m_ChongWuList.position;
        for (int i = 0; i < count; i++)
        {
            zhenrong = window.m_ChongWuList.GetChildAt(i) as SH_ZR_SaiCheng;
            rect = new Rect(new Vector2(startPos.x + zhenrong.x, startPos.y + zhenrong.y), zhenrong.size);
            rectList.Add(rect);
        }
    }
    public override void InitView()
    {
        shangzhen = StriveHegemongService.Singleton.shangzhenList;
        for (int i = 0; i < shangzhen.Count; ++i)
        {
            newshangzhen.Add(shangzhen[i]);
        }
        RefreshView();
    }
    public override void RefreshView()
    {
        SH_ZR_SaiCheng zhenrong;
        for (int i = 0; i < newshangzhen.Count; ++i)
        {
            zhenrong = SH_ZR_SaiCheng.CreateInstance();
            zhenrong.petID = newshangzhen[i];
            zhenrong.xiabiao = i;
            zhenrong.InitView(i);
            zhenrong.m_juese.onDragStart.Add(onDragStart);
            zhenrong.m_juese.onDragMove.Add(onDragMove);
            zhenrong.m_juese.onDragEnd.Add(onDragEnd);
            window.m_ChongWuList.AddChild(zhenrong);
        }
        window.m_ChongWuList.EnsureBoundsCorrect();
        for (int i = 0; i < window.m_ChongWuList.numChildren; ++i)
        {
            zhenrong = (SH_ZR_SaiCheng)window.m_ChongWuList.GetChildAt(i);

        }
    }
    //拖动开始
    private void onDragStart(EventContext context)
    {
        UI_SH_BM_ListItem zhenrongiten = context.initiator as UI_SH_BM_ListItem;
        if (zhenrongiten != null)
        {
            SH_ZR_SaiCheng zhenrong = zhenrongiten.parent as SH_ZR_SaiCheng;
            dragIndex = zhenrong.xiabiao;
            dragPos = zhenrong.LocalToGlobal(zhenrongiten.position);
        }
    }
    //拖动中
    private void onDragMove(EventContext context)
    {

        // 遍历位置，如果不是自己的位置，那么就将那个item换到之前的位置
        Vector2 pos = Stage.inst.touchPosition;
        pos = GRoot.inst.GlobalToLocal(pos);
        //得到停留的位置下标
        int count = rectList.Count;
        for (int i = 0; i < count; i++)
        {
            bool isInPolygon = rectList[i].Contains(pos);
            if (isInPolygon)
            {

                if (changeItem != null)
                {
                    changeItem.position = changeItem.parent.GlobalToLocal(changeItemPos);
                    ChangeData();
                }
                if (i != dragIndex)
                {
                    changeItem = (window.m_ChongWuList.GetChildAt(i) as SH_ZR_SaiCheng).m_juese;
                    changeItemPos = changeItem.parent.LocalToGlobal(changeItem.position);
                    changeItem.position = changeItem.parent.GlobalToLocal(dragPos);

                    ChangeData();
                }
                else
                    changeItem = null;
            }
        }
    }
    //拖动结束
    private void onDragEnd(EventContext context)
    {
        UI_SH_BM_ListItem zhenrongiten = context.initiator as UI_SH_BM_ListItem;
        // 判断当前停留的位置是否是本身位置，不是就交换，是就不变
        Vector2 pos = Stage.inst.touchPosition;
        pos = GRoot.inst.GlobalToLocal(pos);
        pos = LimitPos(pos);
        //得到停留的位置下标
        int count = rectList.Count;
        bool isInPolygon = false;
        for (int i = 0; i < count; i++)
        {
            isInPolygon = rectList[i].Contains(pos);
            if (isInPolygon)
            {
                if (i == dragIndex)
                {
                    if (changeItem != null)
                    {
                        changeItem.position = changeItem.parent.GlobalToLocal(changeItemPos);
                    }
                    zhenrongiten.position = zhenrongiten.parent.GlobalToLocal(dragPos);
                }
                else
                {
                    changeItem.position = changeItem.parent.GlobalToLocal(dragPos);
                    zhenrongiten.position = zhenrongiten.parent.GlobalToLocal(changeItemPos);
                    ChangeChild();
                }

                break;
            }
        }
        if (isInPolygon == false)
        {
            zhenrongiten.position = zhenrongiten.parent.GlobalToLocal(dragPos);
        }
        changeItem = null;
    }
    /// <summary>
    /// 交换子类
    /// </summary>
    private void ChangeChild()
    {
        SH_ZR_SaiCheng changeParentItem = changeItem.parent as SH_ZR_SaiCheng;
        SH_ZR_SaiCheng dragParentItem = window.m_ChongWuList.GetChildAt(dragIndex) as SH_ZR_SaiCheng;
        UI_SH_BM_ListItem dragItem = dragParentItem.m_juese;

        dragParentItem.m_juese = changeItem;
        dragParentItem.AddChild(changeItem);
        changeItem.position = changeItem.parent.GlobalToLocal(dragPos);

        changeParentItem.m_juese = dragItem;
        changeParentItem.AddChild(dragItem);
        dragItem.position = dragItem.parent.GlobalToLocal(changeItemPos);

        newshangzhen[changeParentItem.xiabiao] = changeParentItem.petID;
        newshangzhen[dragParentItem.xiabiao] = dragParentItem.petID;
    }
    /// <summary>
    /// 交换数据
    /// </summary>
    private void ChangeData()
    {
        SH_ZR_SaiCheng parentItem1 = changeItem.parent as SH_ZR_SaiCheng;
        SH_ZR_SaiCheng parentItem2 = window.m_ChongWuList.GetChildAt(dragIndex) as SH_ZR_SaiCheng;

        int petID = parentItem1.petID;

        parentItem1.petID = parentItem2.petID;
        parentItem1.RefreshView();

        parentItem2.petID = petID;
        parentItem2.RefreshView();
    }


    private Vector2 LimitPos(Vector2 pos)
    {

        return pos;
    }
    private void OnBaoCun()
    {
        StriveHegemongService.Singleton.OnReqSetTeam(newshangzhen);
        OnCloseBtn();
    }
    protected override void OnCloseBtn()
    {
        window = null;
        shangzhen = null;
        newshangzhen = null;
        postion = null;
        changeItem = null;
        rectList = null;
        base.OnCloseBtn();
    }
}
