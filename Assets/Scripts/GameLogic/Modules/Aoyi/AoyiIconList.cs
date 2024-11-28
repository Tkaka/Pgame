using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;

public class AoyiIconList : UI_ayIocnList
{

    private Dictionary<int, string> m_iconDic;
    public AoyiIconList()
    {
        if (m_iconDic == null)
        {
            m_iconDic = new Dictionary<int, string>();
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
                        m_iconDic.Add(id, arrIcon[1]);
                    }
                }
            }

        }
 
    }

    public void RefreshView(int petId, AoyiService.EStonePage page, int lightNum = 0, int lightPart = -1)
    {

        List<StoneInfo> stoneInfos = null;
        StonePage stonePage = AoyiService.Singleton.GetPetPageStoneInfos(petId, page);
        if (stonePage != null)
            stoneInfos = stonePage.stones;

        RefreshView(stoneInfos, lightNum, lightPart);
    }

    public void RefreshView(List<StoneInfo> stoneInfos, int lightNum, int lightPart = -1)
    {
        m_iconList.RemoveChildren(0, -1, true);

        if (stoneInfos == null)
            return;


        for (int i = 0; i < stoneInfos.Count; i++)
        {
            StoneInfo stoneInfo = stoneInfos[i];
            t_aoyiBean aoyiBean = ConfigBean.GetBean<t_aoyiBean, int>(stoneInfo.itemId);
            if (aoyiBean == null)
                continue;

            UI_AYiIconCell cell = UI_AYiIconCell.CreateInstance();
            if (m_iconDic.ContainsKey(aoyiBean.t_dic))
            {
                UIGloader.SetUrl(cell.m_imgIcon, m_iconDic[aoyiBean.t_dic]);
            }
            

            cell.m_imgIcon.color = lightNum <= i ? Color.gray : Color.yellow;

            if (stoneInfo.id == lightPart)
            {
                cell.m_imgIcon.color = Color.yellow;

            }

            m_iconList.AddChild(cell);

        }
    }

    public void RefreshView(List<int> dicList, bool isLight)
    {
        m_iconList.RemoveChildren(0, -1, true);
        for (int i = 0; i < dicList.Count; i++)
        {
            UI_AYiIconCell cell = UI_AYiIconCell.CreateInstance();
            int dic = dicList[i];
            if (m_iconDic.ContainsKey(dic))
            {
                UIGloader.SetUrl(cell.m_imgIcon, m_iconDic[dic]);
            }

            cell.m_imgIcon.color = isLight ? Color.yellow : Color.black;

            m_iconList.AddChild(cell);
        }
    }

    public void ClearChildren()
    {
        m_iconList.RemoveChildren(0, -1, true);
    }

}