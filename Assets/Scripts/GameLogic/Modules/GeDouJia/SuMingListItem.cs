using UI_GeDouJia;
using Data.Beans;

public class SuMingListItem : UI_SuMingListItem
{
    t_fetterBean fetterBean;
    public new static SuMingListItem CreateInstance()
    {
        return (SuMingListItem)UI_SuMingListItem.CreateInstance();
    }
    public void Init(int fetterId)
    {
        fetterBean = ConfigBean.GetBean<t_fetterBean,int>(fetterId);
        if (fetterBean == null)
        {
            Logger.err("SuMingListItem:Init:未在羁绊表找到对应羁绊id-----" + fetterBean.t_id);
            return;
        }
        if (string.IsNullOrEmpty(fetterBean.t_name))
            Logger.err("SuMingListItem:Init:羁绊表羁绊名字为空-----" + fetterBean.t_id);
        else
            m_Name.text = fetterBean.t_name;
        if (string.IsNullOrEmpty(fetterBean.t_content))
        {
            Logger.err("SuMingListItem:Init:羁绊表内羁绊条件字段不合法------" + fetterBean.t_id);
            return;
        }
        int[] conditions = GTools.splitStringToIntArray(fetterBean.t_condition);
        SuMingPetItem petItem = null;
        m_PetIconList.RemoveChildren(0,-1,true);
        for (int i = 0; i < conditions.Length; ++i)
        {
            petItem = SuMingPetItem.CreateInstance();
            if (i == conditions.Length - 1)
                petItem.Init(conditions[i], false);
            else
                petItem.Init(conditions[i], true);
            m_PetIconList.AddChild(petItem);
        }
        if (string.IsNullOrEmpty(fetterBean.t_content) || string.IsNullOrEmpty(fetterBean.t_content_result))
            Logger.err("SuMingListItem:Init:羁绊表羁绊效果字段为空-----" + fetterBean.t_id);
        else
        {
            string xiaoguo = fetterBean.t_content_result;
            if (string.IsNullOrEmpty(fetterBean.t_propety_type))
                Logger.err("SuMingListItem:Init:羁绊表羁绊类型字段为空-------" + fetterBean.t_id);
            else
            {
                int[] shuzhi = GTools.splitStringToIntArray(fetterBean.t_propety_type);
                xiaoguo += ((float)(shuzhi[1]) / 10000 * 100).ToString() + "%";
            }
            m_MiaoShu.text = xiaoguo;
        }

    }
    public override void Dispose()
    {
        base.Dispose();
    }

}

