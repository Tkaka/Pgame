using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Data.Beans;

public class FloorTipItem : UI_floorTipItem {

    public int floor;

	public new static FloorTipItem CreateInstance()
    {
        return UI_floorTipItem.CreateInstance() as FloorTipItem;
    }

    public void InitView()
    {
        t_trialBean trialBean = ConfigBean.GetBean<t_trialBean, int>(floor);
        if (trialBean != null)
        {
            string typeLabel = "";
            switch (trialBean.t_type)
            {
                case 1:
                    typeLabel += "怪物";
                    break;
                case 2:
                    typeLabel += "宝箱";
                    break;
                case 3:
                    typeLabel += "属性";
                    break;
                default:
                    break;
            }

            m_typeLabel.text = typeLabel;
        }

        m_floorNum.text = floor + "";
    }
}
