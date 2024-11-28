//主线的超级扫荡
using UI_SaoDangJieSuan;
using Data.Beans;
using Message.Dungeon;
using System.Collections.Generic;
using UnityEngine;
using System;

using FairyGUI;

public class eliteSupperSaoDangWnd : BaseWindow
{
    private UI_eliteSupperSaoDangWnd m_window;
    //private UI_eliteChapterCell m_chapterCell;
    //private UI_eliteGuanQiaCell m_guaQiaCell;
    //private int lastBigIndex = 0;
    //private int lastSmallInex = 0;
    private int m_sweepTotalCount = 3; //当日可扫荡的总次数

    private Dictionary<int, int> m_curSelectActDic = new Dictionary<int, int>();
    private List<ChapterInfo> m_characterInfos;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_eliteSupperSaoDangWnd>();
        m_window.m_btnSaoDangSet.onChanged.Add(_OnBtnSaoDangSetChange);
        m_window.m_btnOneKeySaoDang.onClick.Add(_OnOneKeySaoDangClick);
        m_window.m_btnClose.onClick.Add(Close);
        _InitList();
        InitView();
    }

    private void _InitList()
    {
        m_window.m_mainList.SetVirtual();
        m_window.m_mainList.itemProvider = _ItemProvider;
        m_window.m_mainList.itemRenderer = _ItemRender;
    }


    private string _ItemProvider(int index)
    {
        return UI_eliteChapterCell.URL;
    }

    private void _ItemRender(int index, GObject obj)
    {
        UI_eliteChapterCell chapterCell = obj as UI_eliteChapterCell;
        if (chapterCell == null)
            return;

        if (index < 0 || index >= m_characterInfos.Count)
            return;

        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(m_characterInfos[index].chapterId);
        if (chapterBean == null)
            return;

        chapterCell.m_txtZhangJie.text = string.Format("第{0}章", m_characterInfos[index].chapterId % 100);
        chapterCell.m_txtZhangJieName.text = chapterBean.t_name_id;

        chapterCell.m_CharpterList.RemoveChildren(0, -1, true);
        List<ActInfo> actInfos = m_characterInfos[index].actInfos;
        for (int i = 0; i < actInfos.Count; i++)
        {
            if(LevelService.Singleton.CanSaoDang(actInfos[i].actId) == false)
                continue;
            UI_eliteGuanQiaCell guaQiaCell = _GetGuanQiaCellInfo(actInfos[i].actId);
            if (guaQiaCell != null)
                chapterCell.m_CharpterList.AddChild(guaQiaCell);
        }      

    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_comsumePower.visible = false;
        _RefreshChapterList();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ActDataUpdate, _RefreshWnd);

    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ActDataUpdate, _RefreshWnd);
    }

    private void _RefreshWnd(GameEvent evt)
    {
        _RefreshChapterList();
    }

    private void _RefreshChapterList()
    {

        m_characterInfos = LevelService.Singleton.GetCanSaoDangChapterList(LevelModel.Elite);
        m_window.m_mainList.numItems = m_characterInfos.Count;
    }

    private UI_eliteGuanQiaCell _GetGuanQiaCellInfo(int actId)
    {
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actId);
        if (actBean == null)
        {
            Debug.Log("不存在的关卡ID" + actId);
            return null;
        }
        ActInfo info = LevelService.Singleton.GetActInfoByID(actId);

        UI_eliteGuanQiaCell cell = UI_eliteGuanQiaCell.CreateInstance();
        UIGloader.SetUrl(cell.m_imgBoss, actBean.t_monster_icon);
        UIGloader.SetUrl(cell.m_gropSelect.m_imgBoss, actBean.t_monster_icon);

        cell.m_gropSelect.m_btnSelect.selected = m_curSelectActDic.ContainsKey(actId);

        cell.m_gropSelect.m_btnSelect.onChanged.Clear();
        cell.m_gropSelect.m_btnSelect.onChanged.Add(() => {
            if (cell.m_gropSelect.m_btnSelect.selected)
            {
                if (!m_curSelectActDic.ContainsKey(actId))
                {   
                    m_curSelectActDic.Add(actId, actId);
                }
            }
            else
            {
                if (m_curSelectActDic.ContainsKey(actId))
                {
                    m_curSelectActDic.Remove(actId);
                }
            }
        });

        cell.m_txtBossName.text = actBean.t_name_id;
        cell.m_gropSelect.visible = m_window.m_btnSaoDangSet.selected;
        cell.m_imgBoss.visible = !m_window.m_btnSaoDangSet.selected;

        int remainCount = m_sweepTotalCount - info.attackNum;
        cell.m_btnRest.visible = remainCount <= 0;
        cell.m_btnSaoDang3.visible = remainCount > 0;

        cell.m_btnSaoDang3.onClick.Clear();
        cell.m_btnRest.onClick.Clear();
        cell.m_btnSaoDang3.onClick.Add(() => {
            if (LevelService.Singleton.CheckCanDo(actId, remainCount))
            {
                LevelService.Singleton.SingleActSweep(actId, remainCount);
            }
             
        });
        cell.m_btnRest.onClick.Add(() => { _OnRestCountClick(actId); });

        return cell;

    }

    //关卡重置次数点击
    private void _OnRestCountClick(int actId)
    {
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(19025);
        if (gBean == null)
        {
            Debug.LogError("不存在的全局表ID"+ 19025);
            return;
        }

        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(actId);
        if (actInfo == null)
        {
            Debug.LogError("关卡ID异常" + actId);
            return;
        }

        if (actInfo.refreshNum >= gBean.t_int_param)
        {
            TipWindow.Singleton.ShowTip("重置次数不足!");
            return;
        }

        int comsumeNum = 0;
        t_globalBean gComsumeBean = ConfigBean.GetBean<t_globalBean, int>(19026);
        if (gComsumeBean != null)
        {
            int[] resetComsumes = GTools.splitStringToIntArray(gComsumeBean.t_string_param, '+');
            if (actInfo.refreshNum >= resetComsumes.Length)
                comsumeNum = resetComsumes[resetComsumes.Length - 1];
            else
                comsumeNum = resetComsumes[actInfo.refreshNum];

        }

        
        AgainConfirmWindow.Singleton.TipOneButton("重置", string.Format("次数不足，是否花费{0}钻石重置？", comsumeNum), () => { LevelService.Singleton.ReqResetAttackNum(actId); }, null, false);


    }

    //扫荡设置改变
    private void _OnBtnSaoDangSetChange()
    {
        if (m_window.m_btnSaoDangSet.selected)
        {
            //准备选中多个
            //m_curSelectActDic.Clear();
            m_window.m_btnOneKeySaoDang.grayed = true;
            m_window.m_btnOneKeySaoDang.enabled = false;
        }
        else
        {
            m_window.m_btnOneKeySaoDang.grayed = false;
            m_window.m_btnOneKeySaoDang.enabled = true;
        }

        m_window.m_comsumePower.visible = m_curSelectActDic.Count > 0;
        if (m_curSelectActDic.Count > 0)
        {
            int totalPower = 0;
            foreach (var info in m_curSelectActDic)
            {
                t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(info.Key);
                ActInfo actInfo = LevelService.Singleton.GetActInfoByID(info.Key);
                if (bean != null)
                {
                    totalPower += bean.t_comsumePower * (m_sweepTotalCount - actInfo.attackNum);
                }
            }

            m_window.m_comsumePower.m_txtDes.text = string.Format("完成扫荡需要消耗{0}体力", totalPower);
        }

        
        _RefreshChapterList();
    }


    //扫荡点击
    private void _OnOneKeySaoDangClick()
    {
        int totalNum = 0;
        List<SweepReqInfo> sweepInfos = new List<SweepReqInfo>();
        foreach (var actInfo in m_curSelectActDic)
        {
            t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(actInfo.Key);
            ActInfo into = LevelService.Singleton.GetActInfoByID(actInfo.Key);
            int canSweepCount = m_sweepTotalCount - into.attackNum;
            if (canSweepCount <= 0)
                continue;

            if (bean != null)
            {
                totalNum += bean.t_comsumePower * canSweepCount;
            }

            SweepReqInfo sweepInfo = new SweepReqInfo();
            sweepInfo.actId = actInfo.Key;
            sweepInfo.num = canSweepCount;
            sweepInfos.Add(sweepInfo);
        }

        if (sweepInfos.Count > 0)
        {
            if (RoleService.Singleton.RoleInfo.roleInfo.energy < totalNum)
            {
                //体力不足
                RoleService.Singleton.BuyEnergy();
                return;
            }
            LevelService.Singleton.ReqSweepAct(sweepInfos);
            m_window.m_comsumePower.visible = false;
        }
        else
            TipWindow.Singleton.ShowTip("请选中可扫荡的关卡");
    }
}
