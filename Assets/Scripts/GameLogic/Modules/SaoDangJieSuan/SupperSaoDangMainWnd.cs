//主线的超级扫荡
using UI_SaoDangJieSuan;
using Data.Beans;
using Message.Dungeon;
using System.Collections.Generic;
using UnityEngine;

using FairyGUI;

public class SupperSaoDangWindow : BaseWindow
{
    private UI_SupperSaoDangWindow m_window;

    private UI_GuanKaCell m_guaQiaCell;

    List<ChapterInfo> m_chapterInfoList;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SupperSaoDangWindow>();
        m_window.m_popupView.m_btnClose.onClick.Add(Close);
        m_window.m_mask.onClick.Add(Close);
        _InitList();
        InitView();
        PlayPopupAnim(m_window.m_mask, m_window.m_popupView);
    }

    private void _InitList()
    {
        m_window.m_popupView.m_mainList.SetVirtual();
        m_window.m_popupView.m_mainList.itemProvider = _CharpterCellProvider;
        m_window.m_popupView.m_mainList.itemRenderer = _CharpterCellRender;
    }

    private string _CharpterCellProvider(int index)
    {
        return UI_CharpterCell.URL;
    }


    private void _CharpterCellRender(int index, GObject obj)
    {
        if (index < 0 || index >= m_chapterInfoList.Count)
            return;

        UI_CharpterCell chapterCell = obj as UI_CharpterCell;
        if (chapterCell == null)
            return;

        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(m_chapterInfoList[index].chapterId);
        if (chapterBean == null)
            return;

        chapterCell.m_txtZhangJie.text = string.Format("第{0}章", m_chapterInfoList[index].chapterId % 100);
        chapterCell.m_txtZhangJieName.text = chapterBean.t_name_id;

        chapterCell.m_CharpterList.RemoveChildren(0, -1, true);
        ChapterInfo chapterInfo = m_chapterInfoList[index];
        for (int i = 0; i < chapterInfo.actInfos.Count; i++)
        {
            ActInfo actInfo = chapterInfo.actInfos[i];
            if (LevelService.Singleton.CanSaoDang(actInfo.actId) == false)
                continue;
            m_guaQiaCell = _GetGuanQiaCellInfo(actInfo.actId);
            chapterCell.m_CharpterList.AddChild(m_guaQiaCell);
        }

    }


    public override void InitView()
    {
        base.InitView();
        List<ChapterInfo> chapterInfoList = LevelService.Singleton.GetCanSaoDangChapterList(LevelModel.Main);
        m_chapterInfoList = chapterInfoList;
        m_chapterInfoList.Sort((x,y)=> x.chapterId.CompareTo(y.chapterId));
        m_window.m_popupView.m_mainList.numItems = m_chapterInfoList.Count;
    }

    private UI_GuanKaCell _GetGuanQiaCellInfo(int actId)
    {
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actId);
        if (actBean == null)
        {
            Debug.Log("不存在的关卡ID" + actId);
            return null;
        }
        UI_GuanKaCell cell = UI_GuanKaCell.CreateInstance();
        //UIGloader.SetUrl(cell.m_imgBoss, actBean.t_monster_icon);
        cell.m_txtGuanQiaName.text = actBean.t_name_id;
        cell.m_btnSaoDang10.visible = LevelService.Singleton.CheckCanSweep10(actId);
        cell.m_btnSaoDang50.visible = LevelService.Singleton.CheckCanSweep50(actId);

        cell.m_btnSaoDang10.onClick.Clear();
        cell.m_btnSaoDang10.onClick.Add((param) =>
        {
            if (LevelService.Singleton.CheckCanDo(actId, 10, true))
            {
                LevelService.Singleton.SingleActSweep(actId, 10);
            }
        });

        cell.m_btnSaoDang50.onClick.Clear();
        cell.m_btnSaoDang50.onClick.Add((param) => 
        {
            if (LevelService.Singleton.CheckCanDo(actId, 50, true))
            {
                LevelService.Singleton.SingleActSweep(actId, 50);
            }
         });

        cell.m_RewardList.RemoveChildren(0, -1, true);
        List<int> itemList = GTools.splitStringToIntList(actBean.t_drop_show_id);
        for (int i = 0; i < itemList.Count; i++)
        {
            CommonItem itemCell = CommonItem.CreateInstance();
            itemCell.Init(itemList[i], 0);
            itemCell.SetIconScale(0.5f, 0.5f);
            itemCell.RefreshView();
            cell.m_RewardList.AddChild(itemCell);
        }
        //cell.m_RewardList.autoResizeItem = true;

        return cell;

    }
}
