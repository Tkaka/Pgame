using Data.Beans;
using UI_PetParticulars;
using Message.Pet;
using System.Collections.Generic;

public class ZhanHUnXiangQingWindow : BaseWindow
{
    UI_ZhanHUnXiangQingWindow window;
    private int souldid;
    private int level;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ZhanHUnXiangQingWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        if (Info.param == null)
        {
            Logger.err("未传参无法显示数据");
            return;
        }
        TwoParam<int, int> twoParam = Info.param as TwoParam<int,int>;
        if (twoParam == null)
        {
            Logger.err("传参类型不正确");
            return;
        }
        souldid = twoParam.value1;
        level = twoParam.value2;
        InitView();
    }
    public override void InitView()
    {
        t_pet_soulBean soulBean = ConfigBean.GetBean<t_pet_soulBean,int>(souldid);
        if (soulBean != null)
        {
            UIGloader.SetUrl(window.m_toixaing, soulBean.t_icon);
            window.m_name.text = soulBean.t_nameLanguageID;
            List<float> valueList = GetZhanHunDesValueList(soulBean.t_id, level);
            switch (valueList.Count)
            {
                case 1:
                    window.m_MiaoShu.text = string.Format(soulBean.t_descriptID, valueList[0]);
                    break;
                case 2:
                    window.m_MiaoShu.text = string.Format(soulBean.t_descriptID, valueList[0], valueList[1]);
                    break;
                case 3:
                    window.m_MiaoShu.text = string.Format(soulBean.t_descriptID, valueList[0], valueList[1], valueList[2]);
                    break;
                case 4:
                    window.m_MiaoShu.text = string.Format(soulBean.t_descriptID, valueList[0], valueList[1], valueList[2], valueList[3]);
                    break;
                default:
                    break;
            }
            OnTexeHeight();
            if (level > 0)
            {
                window.m_level.text = "等级：" + level;
            }
            else
            {
                window.m_toixaing.grayed = true;
                window.m_level.text = "未解锁";
            }
        }
        else
        {
            Logger.err("战魂表没有对应id" + souldid);
        }
    }
    public List<float> GetZhanHunDesValueList(int zhanHunID, int level)
    {
        if (level <= 0)
        {
            level = 1;
        }
        List<float> valueList = new List<float>();
        t_pet_soulBean petSoulBean = ConfigBean.GetBean<t_pet_soulBean, int>(zhanHunID);
        if (petSoulBean != null)
        {
            int value = 0;
            if (petSoulBean.t_initValue1 != 0)
            {
                if (petSoulBean.t_initValue1 == -1)
                    valueList.Add(petSoulBean.t_initValue1);
                else
                {
                    value = petSoulBean.t_initValue1 + (level - 1) * petSoulBean.t_gropValue1;
                    valueList.Add(value);
                }
            }

            if (petSoulBean.t_initValue2 != 0)
            {
                if (petSoulBean.t_initValue2 == -1)
                    valueList.Add(petSoulBean.t_initValue2);
                else
                {
                    value = petSoulBean.t_initValue2 + (level - 1) * petSoulBean.t_groupValue2;
                    valueList.Add(value);
                }
            }

            if (petSoulBean.t_initValue3 != 0)
            {
                if (petSoulBean.t_initValue3 == -1)
                    valueList.Add(petSoulBean.t_initValue3);
                else
                {
                    value = petSoulBean.t_initValue3 + (level - 1) * petSoulBean.t_groupValue3;
                    valueList.Add(value);
                }
            }

            if (petSoulBean.t_initValue4 != 0)
            {
                if (petSoulBean.t_initValue4 == -1)
                    valueList.Add(petSoulBean.t_initValue4);
                else
                {
                    value = petSoulBean.t_initValue4 + (level - 1) * petSoulBean.t_groupValue4;
                    valueList.Add(value);
                }
            }
        }
        return valueList;
    }
    private void OnTexeHeight()
    {
        window.m_miaoshubeijing.height = window.m_MiaoShu.height + 20;
        window.m_beijing.height = (window.m_miaoshubeijing.y - window.m_beijing.y) + window.m_miaoshubeijing.height + 20;
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }
}
