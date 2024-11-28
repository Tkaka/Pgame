using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;

public class OneKeyPlaceWnd :BaseWindow
{
    private UI_OneKeyPlaceWnd m_window;
    private int m_curPetId;
    private AoyiService.EStonePage m_page;
    private Action<object> m_callBack;

    private Dictionary<int, StoneInfoExtra> m_stoneDic = new Dictionary<int, StoneInfoExtra>();
    private List<int> m_aoyiList = new List<int>();
    private Dictionary<int, string> m_dicIconDic = new Dictionary<int, string>();    //æ–¹å‘å›¾æ ‡å­—å…¸
    //private Dictionary<int, int> m_xiangQiangDic = new Dictionary<int, int>();          //å½“å‰å® ç‰©å·²æ¿€æ´»çš„å¥¥ä¹‰
    private int m_curPageActiveId;                           //å½“å‰é¡µå·²æ¿€æ´»çš„ID

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_OneKeyPlaceWnd>();
        m_window.m_btnClose.onClick.Add(Close);

        _Init();
        _ShowAoyiList();
    }

   
    private void _Init()
    {
        m_window.m_mainList.SetVirtual();
        m_window.m_mainList.itemProvider = _ItemProvider;
        m_window.m_mainList.itemRenderer = _ItemRender;

        ThreeParam<int, AoyiService.EStonePage, Action<object>> threeParam = Info.param as ThreeParam<int, AoyiService.EStonePage, Action<object>>;
        if (threeParam == null)
        {
            Close();
            return;
        }

        m_curPetId = threeParam.value1;
        m_page = threeParam.value2;
        m_callBack = threeParam.value3;

        List<StoneInfo> stoneInfos = AoyiService.Singleton.GetBagList();
        for (int i = 0; i < stoneInfos.Count; i++)
        {
            if (stoneInfos[i].itemId == 0)
                continue;
            _AddToDic(stoneInfos[i], 1, false);
        }

        StonePage pageStone = AoyiService.Singleton.GetPetPageStoneInfos(m_curPetId, m_page);
        if (pageStone != null)
        {
            for (int i = 0; i < pageStone.stones.Count; i++)
            {
                _AddToDic(pageStone.stones[i], 2, true);
            }
        }
 

        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(210101);
        if (gBean != null)
        {
            string[] arrStr = GTools.splitString(gBean.t_string_param, ';');
            for (int i = 0; i < arrStr.Length; i++)
            {
                string[] arrIcon = GTools.splitString(arrStr[i], '+');
                if (arrIcon.Length >= 2)
                {
                    int id = 0;
                    int.TryParse(arrIcon[0], out id);
                    m_dicIconDic.Add(id, arrIcon[1]);
                }
            }
        }

        //åˆå§‹åŒ–å·²é•¶åµŒçš„å¥¥ä¹‰é“¾
        //for (int i = (int)AoyiService.EStonePage.Primiry; i <= (int)AoyiService.EStonePage.Ultima; i++)
        //{
        //    int activeId = AoyiService.Singleton.GetActiveAoyiId(m_curPetId, (AoyiService.EStonePage)i);
        //    if (activeId != -1)
        //    {
        //        if (!m_xiangQiangDic.ContainsKey(activeId))
        //        {
        //            m_xiangQiangDic.Add(activeId, activeId);
        //        }
        //    }
        //}
        m_curPageActiveId = AoyiService.Singleton.GetActiveAoyiId(m_curPetId, m_page);
    }

    private void _AddToDic(StoneInfo stoneInfo, int type, bool isUsing)
    {
        //åªä¿å­˜æ¯ä¸ªç±»åž‹çš„å“è´¨æœ€é«˜çš„çŸ³å¤´
        int dic = ConfigBean.GetBean<t_aoyiBean, int>(stoneInfo.itemId).t_dic;
        if (m_stoneDic.ContainsKey(dic))
        {
            int quilityA = UIUtils.GetDefaultItemQuality(stoneInfo.itemId);
            int quilityB = UIUtils.GetDefaultItemQuality(m_stoneDic[dic].stoneInfo.itemId);
            if (quilityA > quilityB)
            {
                StoneInfoExtra stoneInfoExtra = new StoneInfoExtra(stoneInfo, isUsing, type);
                m_stoneDic[dic] = stoneInfoExtra;
            }
        }
        else
        {
            StoneInfoExtra stoneInfoExtra = new StoneInfoExtra(stoneInfo, isUsing, type);
            m_stoneDic.Add(dic, stoneInfoExtra);
        }
    }

    private void _ShowAoyiList()
    {
        m_aoyiList.Clear();
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(m_curPetId);
        if (petBean == null)
            return;

        List<int> ayList = GTools.splitStringToIntList(petBean.t_aoyi, '+');
        for (int i = 0; i < ayList.Count; i++)
        {
            //è¶…è¿‡å½“å‰é¡µæ ¼å­æ•°é‡çš„ç»„åˆéƒ½ä¸è¦
            t_aoyi_zuheBean zuheBean = ConfigBean.GetBean<t_aoyi_zuheBean, int>(ayList[i]);
            if (zuheBean == null)
                continue;

            int[] arrDic = GTools.splitStringToIntArray(zuheBean.t_group, '+');
            if (arrDic == null || arrDic.Length == 0)
                continue;

            if (arrDic.Length > AoyiService.Singleton.GetPageStoneGridNum(m_page))
            {
                continue;
            }

            m_aoyiList.Add(ayList[i]);

        }

        m_aoyiList.Sort(_SortFun);
        m_window.m_mainList.numItems = m_aoyiList.Count;
    }

    private int _SortFun(int a, int b)
    {
        //è¿˜æ²¡æœ‰ç»™æŽ’åºè§„åˆ™
        int[] arrDicA = GTools.splitStringToIntArray(ConfigBean.GetBean<t_aoyi_zuheBean, int>(a).t_group, '+');
        int[] arrDicB = GTools.splitStringToIntArray(ConfigBean.GetBean<t_aoyi_zuheBean, int>(b).t_group, '+');

        if (arrDicA.Length == arrDicB.Length)
            return -(_GetHaveStoneNum(arrDicA) - _GetHaveStoneNum(arrDicB));

        return arrDicA.Length - arrDicB.Length;
    }


    //èŽ·å¾—æ‹¥æœ‰çš„æ–¹å‘çš„æ•°é‡
    private int _GetHaveStoneNum(int[] arrDic)
    {
        int haveNum = 0;
        for (int i = 0; i < arrDic.Length; i++)
        {
            if (m_stoneDic.ContainsKey(arrDic[i]))
            {
                haveNum++;
            }
        }

        return haveNum;
    }

    private string _ItemProvider(int index)
    {
        return UI_objCommondCell.URL;
    }

    private void _ItemRender(int index, GObject obj)
    {
        if (index < 0 || index >= m_aoyiList.Count)
            return;

        UI_objCommondCell cell = obj as UI_objCommondCell;
        if (cell == null)
            return;

        t_aoyi_zuheBean zuheBean = ConfigBean.GetBean<t_aoyi_zuheBean, int>(m_aoyiList[index]);
        if (zuheBean == null)
            return;


        int[] arrDic = GTools.splitStringToIntArray(zuheBean.t_group, '+');
        if (arrDic == null || arrDic.Length == 0)
            return;

        int canPutNum = 0;
        cell.m_gridList.RemoveChildren(0, -1, true);
        for (int i = 0; i < arrDic.Length; i++)
        {
            PlaceAyCell ayCell = PlaceAyCell.CreateInstance();
            string icon = "";
            StoneInfo stoneInfo = null;
            if (m_dicIconDic.ContainsKey(arrDic[i]))
            {
                icon = m_dicIconDic[arrDic[i]];
            }

            if (m_stoneDic.ContainsKey(arrDic[i]))
            {
                stoneInfo = m_stoneDic[arrDic[i]].stoneInfo;
                canPutNum++;
            }
            ayCell.RefreshView(stoneInfo, icon);

            cell.m_gridList.AddChild(ayCell);
        }

        cell.m_btnOneKeyPlace.grayed = canPutNum == 0;
        cell.m_btnOneKeyPlace.onClick.Clear();
        cell.m_btnOneKeyPlace.onClick.Add(()=> {
            if (canPutNum == 0)
            {
                TipWindow.Singleton.ShowTip("当前没有闲置的奥义石!");
                return;
            }

            Dictionary<int, StoneInfoExtra> dic = new Dictionary<int, StoneInfoExtra>();
            for (int i = 0; i < arrDic.Length; i++)
            {
                StoneInfoExtra stoneInfo = null;
                if (m_stoneDic.ContainsKey(arrDic[i]))
                {
                    stoneInfo = m_stoneDic[arrDic[i]];
                    stoneInfo.isUsing = true;

                }

                dic.Add(i + 1, stoneInfo);
            }

            if(m_callBack!= null)
                m_callBack(dic);
            Close();
        });

        cell.m_objXiangQian.visible = m_aoyiList[index] == m_curPageActiveId;
        cell.m_txtAoyiName.text = zuheBean.t_name;
    }



    protected override void OnClose()
    {
        base.OnClose();
    }

}