using System.Collections.Generic;
using UI_DrawCard;
using FairyGUI;
public class GD_ChongWuItem : UI_GD_ChongWuItem
{
    private List<int> pets;
    public new static GD_ChongWuItem CreateInstance()
    {
        return (GD_ChongWuItem)UI_GD_ChongWuItem.CreateInstance();
    }
    public void Init(List<int> petid)
    {
        pets = petid;
        OnFill();
    }
    private void OnFill()
    {
        //var objs = m_petList.GetChildren();
        //for (int i = 0; i < objs.Length; ++i)
        //    (objs[i] as DaoJuListItem).CacheModel();

        m_petList.RemoveChildrenToPool();
        DaoJuListItem listItem;
        for (int i = 0; i < pets.Count; ++i)
        {
            listItem = m_petList.GetFromPool(UI_DaoJuListItem.URL) as DaoJuListItem;
            m_petList.AddChild(listItem);
            listItem.Init(pets[i]);
        }
    }

    public void OnItemInit(List<int> itemids)
    {
        m_petList.RemoveChildren(0,-1,true);
        JiangPingListItem listItem;
        for (int i = 0; i < itemids.Count; ++i)
        {
            listItem = JiangPingListItem.CreateInstance();
            listItem.Init(itemids[i]);
            m_petList.AddChild(listItem);
        }
    }

    public override void Dispose()
    {
        m_petList.RemoveChildren(0,-1,true);
        base.Dispose();
    }
}