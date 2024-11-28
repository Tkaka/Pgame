using UI_DrawCard;
using Data.Beans;
using System.Collections.Generic;
using FairyGUI;

public class KeHuoDeWindow : BaseWindow
{
    private UI_KeHuoDeWindow window;
    private List<t_drawitemBean> drawitemBeans;
    private List<List<int>> attkList; //攻
    private List<List<int>> defenseList;//防
    private List<List<int>> skillList;//技
    private List<List<int>> currPetList;
    private List<List<int>> petlist;
    private List<List<int>> itemList = new List<List<int>>();
    private int type;//0为有分割线，1为没有分割线，2为加载道具类型
    private int[] itemIndexs = new int[3];//下标数组，用于虚拟列表加载时的下标
    private int[] levels = new int[3];//解锁等级


    public override void OnOpen()
    {
        window = getUiWindow<UI_KeHuoDeWindow>();

        drawitemBeans = ConfigBean.GetBeanList<t_drawitemBean>();
        attkList = new List<List<int>>();
        defenseList = new List<List<int>>();
        skillList = new List<List<int>>();
        petlist = new List<List<int>>();
        int moshi = (int)Info.param;
        OnLodaList(moshi);
        AddKeyEvent();
        OnFillPetList();
        InitView();
    }
    public override void InitView()
    {
        type = 0;
        window.m_ChongWujiangLi.onChanged.Add(onChanged);
        window.m_JiangLiList.m_JiangLiList.sourceWidth = (int)window.width;
        window.m_JiangLiList.m_JiangLiList.SetVirtual();
        window.m_JiangLiList.m_JiangLiList.itemRenderer = OnRenderListItem;
        window.m_JiangLiList.m_JiangLiList.itemProvider = OnItemProvider;
        onChanged();
        int moshi = (int)Info.param;
        if (moshi == 1)
        {
            OnJinBiJiangLiLiist();
        }
        else if (moshi == 2)
        {
            OnZuanShiJiangLiList();
        }
    }
    private void AddKeyEvent()
    {
        window.m_CloseBtn.onClick.Add(Close);
    }
    private void OnZuanShiJiangLiList()
    {
        window.m_ZuanShiJiangLi.visible = true;
        window.m_JinBiJiangLi.visible = false;
        type = 0;
        window.m_JiangLiList.m_JiangLiList.RefreshVirtualList();
    }
    private void OnJinBiJiangLiLiist()
    {
        window.m_ZuanShiJiangLi.visible = false;
        window.m_JinBiJiangLi.visible = true;
        type = 2;
        window.m_JiangLiList.m_JiangLiList.RefreshVirtualList();
    }
    private void OnLodaList(int moshi)
    {
        if (moshi == 1)
        {
            for (int i = 0; i < drawitemBeans.Count; ++i)
            {
                if (drawitemBeans[i].t_type == 1)
                {
                    OnItemList(drawitemBeans[i]);
                }
            }
        }
        else if (moshi == 2)
        {
            for (int i = 0; i < drawitemBeans.Count; ++i)
            {
                if (drawitemBeans[i].t_type == 2)
                {
                    OnPetList(drawitemBeans[i]);
                    OnItemList(drawitemBeans[i]);
                }
            }
        }
    }
    /// <summary>
    /// 控制器改变
    /// </summary>
    public void onChanged()
    {
        window.m_JiangLiList.m_JiangLiList.ScrollToView(0);
        if (window.m_ChongWujiangLi.selectedIndex == 0)
        {
            window.m_JiangLiList.m_JiangLiList.numItems = window.m_JiangLiList.m_JiangLiList.numItems = petlist.Count + levels.Length;
            type = 0;
        }
        else if (window.m_ChongWujiangLi.selectedIndex == 1)
        {
            currPetList = attkList;
            window.m_JiangLiList.m_JiangLiList.numItems = currPetList.Count;
            type = 1;
        }
        else if (window.m_ChongWujiangLi.selectedIndex == 2)
        {
            currPetList = defenseList;
            window.m_JiangLiList.m_JiangLiList.numItems = currPetList.Count;
            type = 1;
        }
        else if (window.m_ChongWujiangLi.selectedIndex == 3)
        {
            currPetList = skillList;
            window.m_JiangLiList.m_JiangLiList.numItems = currPetList.Count;
            type = 1;
        }
        else if (window.m_ChongWujiangLi.selectedIndex == 4)
        {
            currPetList = itemList;
            window.m_JiangLiList.m_JiangLiList.numItems = currPetList.Count;
            type = 2;
        }
        window.m_JiangLiList.m_JiangLiList.RefreshVirtualList();
    }
    /// <summary>
    /// 列表渲染
    /// </summary>
    /// <param name="index"></param>
    /// <param name="obj"></param>
    private void OnRenderListItem(int index, GObject obj)
    {
        if (type == 0)
        {
            if (index == 0)
            {
                JieSuoDengJiFenGeXian listitem = obj as JieSuoDengJiFenGeXian;
                listitem.Init(levels[0]);
            }
            else if (index == itemIndexs[0] + 1)
            {
                JieSuoDengJiFenGeXian listitem = obj as JieSuoDengJiFenGeXian;
                listitem.Init(levels[1]);
            }
            else if (index == itemIndexs[1] + 2)
            {
                JieSuoDengJiFenGeXian listitem = obj as JieSuoDengJiFenGeXian;
                listitem.Init(levels[2]);
            }
            else if (index > 0 && index < itemIndexs[0] + 1)
            {
                GD_ChongWuItem listitem = obj as GD_ChongWuItem;
                listitem.Init(petlist[index - 1]);
            }
            else if (index > itemIndexs[0] + 1 && index < itemIndexs[1] + 2)
            {
                GD_ChongWuItem listitem = obj as GD_ChongWuItem;
                listitem.Init(petlist[index - 2]);
            }
            else if (index > itemIndexs[1] + 2)
            {
                GD_ChongWuItem listitem = obj as GD_ChongWuItem;
                listitem.Init(petlist[index - 3]);
            }
        }
        else if (type == 1)
        {
            GD_ChongWuItem listItem = obj as GD_ChongWuItem;
            listItem.Init(currPetList[index]);
        }
        else if (type == 2)
        {
            GD_ChongWuItem listItem = obj as GD_ChongWuItem;
            listItem.OnItemInit(itemList[index]);
        }
    }
    //item提供
    private string OnItemProvider(int index)
    {
        if (type == 0)
        {
            if (index == 0 || index == itemIndexs[0] + 1 || index == itemIndexs[1] + 2)
            {
                return UI_JieSuoDengJiFenGeXian.URL;
            }
            else
                return UI_GD_ChongWuItem.URL;
        }
        else if (type == 1)
            return UI_GD_ChongWuItem.URL;
        else if (type == 2)
            return UI_GD_ChongWuItem.URL;
        else
            return null;

    }
    protected override void OnClose()
    {
        window.m_JiangLiList.m_JiangLiList.RemoveChildren(0, -1, true);
        itemList = null;
        attkList = null;
        defenseList = null;
        skillList = null;
        if (window != null)
            window = null;
        base.OnClose();
    }
    /// <summary>
    /// 获得宠物分类
    /// </summary>
    /// <param name="drawitemBean"></param>
    private void OnPetList(t_drawitemBean drawitemBean)
    {
        t_petBean petBean;
        int[] pets = GTools.splitStringToIntArray(drawitemBean.t_pet_list);
        int num = 5;//得到每行可放的宠物数量
        List<int> attkids;
        int attkList_lenth = -1;
        List<int> defids;
        int defList_lenth = -1;
        List<int> skillids;
        int skillList_lenth = -1;
        if (attkList.Count == 0)
            attkids = new List<int>();
        else
        {
            attkids = attkList[attkList.Count - 1];
            attkList_lenth = attkList.Count;
        }
        if (defenseList.Count == 0)
            defids = new List<int>();
        else
        {
            defids = defenseList[defenseList.Count - 1];
            defList_lenth = defenseList.Count;
        }
        if (skillList.Count == 0)
            skillids = new List<int>();
        else
        {
            skillids = skillList[skillList.Count - 1];
            skillList_lenth = skillList.Count;
        }
        for (int i = 0; i < pets.Length; ++i)
        {
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(pets[i]);
            if (itemBean != null)
            {
                if (!(string.IsNullOrEmpty(itemBean.t_value)))
                {
                    string[] pet = GTools.splitString(itemBean.t_value, ';');
                    int petid = int.Parse(pet[0]);
                    petBean = ConfigBean.GetBean<t_petBean, int>(petid);
                    if (petBean == null)
                    {
                        Logger.err("KeHuoDeWindow:JiaZaiZuanSHiDaoJu:宠物表没有此id---" + petid);
                        continue;
                    }
                    if (petBean.t_type == 1)
                    {
                        if (attkids.Count % num == 0)
                        {
                            if (attkids.Count != 0)
                            {
                                attkList.Add(attkids);
                                attkids = new List<int>();
                                attkids.Add(pets[i]);
                            }
                            else
                                attkids.Add(pets[i]);

                        }
                        else
                        {
                            attkids.Add(pets[i]);
                        }
                    }
                    else if (petBean.t_type == 2)
                    {
                        if (defids.Count % num == 0)
                        {
                            if (defids.Count != 0)
                            {
                                defenseList.Add(defids);
                                defids = new List<int>();
                                defids.Add(pets[i]);
                            }
                            else
                                defids.Add(pets[i]);
                        }
                        else
                        {
                            defids.Add(pets[i]);
                        }
                    }
                    else if (petBean.t_type == 3)
                    {
                        if (skillids.Count % num == 0)
                        {
                            if (skillids.Count != 0)
                            {
                                skillList.Add(skillids);
                                skillids = new List<int>();
                                skillids.Add(pets[i]);
                            }
                            else
                                skillids.Add(pets[i]);
                        }
                        else
                            skillids.Add(pets[i]);
                    }
                }
            }
        }
        if (attkList.Count != attkList_lenth)
            attkList.Add(attkids);
        if (defList_lenth != defenseList.Count)
            defenseList.Add(defids);
        if (skillList_lenth != skillList.Count)
            skillList.Add(skillids);
        return;
    }
    /// <summary>
    /// 获得所有道具
    /// </summary>
    /// <param name="drawitemBean"></param>
    /// <returns></returns>
    private void OnItemList(t_drawitemBean drawitemBean)
    {
        t_itemBean itemBean;
        if (string.IsNullOrEmpty(drawitemBean.t_item_list))
        {
            Logger.err("KeHuoDeWindow:JiaZaiZuanSHiDaoJu:未从抽卡表获得对应数据，请检查抽卡表id" + drawitemBean.t_id + "的道具奖励列表字段");
            return;
        }
        int num = 5;//得到每行可放的宠物数量
        int[] daoju = GTools.splitStringToIntArray(drawitemBean.t_item_list);
        List<int> itemid = new List<int>();
        for (int i = 0; i < daoju.Length; ++i)
        {
            itemBean = ConfigBean.GetBean<t_itemBean, int>(daoju[i]);
            if (itemBean == null)
            {
                Logger.err("KeHuoDeWindow:JiaZaiZuanSHiDaoJu:道具表没有此id---" + drawitemBean.t_id);
                continue;
            }
            for (int j = 0; j < daoju.Length; ++j)
            {
                if (j % num == 0)
                {
                    itemid = new List<int>();
                }
                itemid.Add(daoju[j]);
                if (j % num == num - 1)
                {
                    itemList.Add(itemid);
                }
                if (j == daoju.Length - 1)
                {
                    itemList.Add(itemid);
                }
            }
        }
        return;
    }
    /// <summary>
    /// 填充宠物列表
    /// </summary>
    private void OnFillPetList()
    {
        List<int> drawId = new List<int>();
        for (int i = 0; i < drawitemBeans.Count; ++i)
        {
            if (drawitemBeans[i].t_type == 2)
            {
                drawId.Add(drawitemBeans[i].t_id);
            }
        }
        drawId.Sort();
        for (int i = 0; i < drawId.Count; ++i)
        {
            t_drawitemBean bean = ConfigBean.GetBean<t_drawitemBean, int>(drawId[i]);
            if (bean != null)
            {
                int num = 5;//得到每行可放的宠物数量
                if (string.IsNullOrEmpty(bean.t_pet_list))
                {
                    Logger.err("宠物列表字段为空，无法获得对应数据");
                    continue;
                }
                int[] petids = GTools.splitStringToIntArray(bean.t_pet_list);
                List<int> petid = new List<int>();
                for (int j = 0; j < petids.Length; ++j)
                {
                    if (j % num == 0)
                    {
                        petid = new List<int>();
                    }
                    petid.Add(petids[j]);
                    if (j % num == num - 1)
                    {
                        petlist.Add(petid);
                    }
                    if (j == petids.Length - 1)
                    {
                        petlist.Add(petid);
                    }
                }
                itemIndexs[i] = petlist.Count;
                //获得解锁等级
                if (string.IsNullOrEmpty(bean.t_limit))
                {
                    Logger.err("未能获得等级解锁区间，解锁等级显示将不准确");
                }
                else
                {
                    int[] levle = GTools.splitStringToIntArray(bean.t_limit);
                    levels[i] = levle[0];
                }
            }
        }
    }
}
