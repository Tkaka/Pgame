using Data.Beans;
using System.Collections.Generic;
using UnityEngine;

public enum BuffShowType
{ 
    None = 0,                 //没有叠加显示
    Turn = 1,                  //轮播
    AlwaysShow = 2,       //一直显示
    UIAlwaysShow = 3,    //一直显示&UI
}

public class BuffEftCtrl
{
    private Dictionary<string, int> buffEftDic;

    public Actor Owner { private set; get; }

    /// <summary>
    /// 循环显示列表
    /// </summary>
    private List<GameObject> turnList;

    /// <summary>
    /// 常显列表
    /// </summary>
    private List<GameObject> alwaysList;

    /// <summary>
    /// 循环显示定时器
    /// </summary>
    private SimpleInterval mInterval;

    public BuffEftCtrl(Actor owner)
    {
        this.Owner = owner;
        buffEftDic = new Dictionary<string, int>();
        turnList = new List<GameObject>();
        alwaysList = new List<GameObject>();
        mInterval = new SimpleInterval();
        mInterval.DoAction(1, TurnShow);
    }

    private int curIndex = 0;
    /// <summary>
    /// 循环显示  
    /// </summary>
    public void TurnShow()
    {
        if (turnList != null && turnList.Count > 0)
        {
            if (curIndex >= turnList.Count)
                curIndex = 0;
            for (int i = 0; i < turnList.Count; i++)
            {
                if(curIndex == i)
                    turnList[i].SetActive(true);
                else
                    turnList[i].SetActive(false);
            }
        }
    }

    public void AddBuffEft(t_buffBean bean)
    {
        if (bean == null)
            return;
        string prefab = bean.t_prefab;
        if (!buffEftDic.ContainsKey(prefab))
        {
            buffEftDic.Add(prefab, 1);
            GameObject go = FightManager.R.LoadGo(prefab);
            if (go != null)
            {
                go.transform.SetParent(Owner.TransformExt, false);
                if (bean.t_show_type == (int)BuffShowType.Turn
                    || bean.t_show_type == (int)BuffShowType.None)
                    turnList.Add(go);
                else if (bean.t_show_type == (int)BuffShowType.AlwaysShow
                    || bean.t_show_type == (int)BuffShowType.UIAlwaysShow)
                    alwaysList.Add(go);
                else
                    Logger.err("AddBuffEft:无法识别的buff显示类型");
            }
        }
        else
        {
            buffEftDic[prefab] += 1;
        }
    }

    public void RemoveBuffEft(t_buffBean bean)
    {
        if (bean == null)
            return;
        string prefab = bean.t_prefab;
        if (buffEftDic.ContainsKey(prefab))
        {
            buffEftDic[prefab] -= 1;
            if (buffEftDic[prefab] <= 0)
            {
                buffEftDic.Remove(prefab);
                if (bean.t_show_type == (int)BuffShowType.Turn
                    || bean.t_show_type == (int)BuffShowType.None)
                    Remove(turnList, prefab);
                else if (bean.t_show_type == (int)BuffShowType.AlwaysShow
                    || bean.t_show_type == (int)BuffShowType.UIAlwaysShow)
                    Remove(alwaysList, prefab);
                else
                    Logger.err("RemoveBuffEft:无法识别的buff显示类型");
            }
        }
    }

    private void Remove(List<GameObject> list, string name)
    {
        if (list == null || list.Count <= 0 || string.IsNullOrEmpty(name))
            return;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].name == name)
            {
                GameObject go = list[i];
                list.RemoveAt(i);
                if (go != null)
                    Object.Destroy(go);
                break;
            }
        }
    }

    public void Clear()
    {
        if (mInterval != null)
            mInterval.Kill();
        for (int i = 0; i < turnList.Count; i++)
            Object.Destroy(turnList[i]);
        for (int i = 0; i < alwaysList.Count; i++)
            Object.Destroy(alwaysList[i]);
        turnList.Clear();
        alwaysList.Clear();
    }

}
