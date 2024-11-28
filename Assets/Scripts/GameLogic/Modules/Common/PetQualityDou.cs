using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using Data.Beans;
using FairyGUI;

public class PetQualityDou : UI_petQualityDou {

    /// <summary>
    /// 设置宠物品质
    /// </summary>
    /// <param name="color">品质</param>
	public void InitView(int color)
    {
        m_rightDou.visible = false;
        m_leftDou.visible = false;
        t_color_name_beanBean colorNameBean = ConfigBean.GetBean<t_color_name_beanBean, int>(color);
        if (colorNameBean != null)
        {
            m_leftDou.visible = false;
            m_rightDou.visible = false;
            m_centerDouList.RemoveChildren(0, -1, true);
            m_centerDouList.visible = true;
            int switchNum = colorNameBean.t_num >= 3 ? 3 : colorNameBean.t_num;
            switch (switchNum)
            {
                case 1:
                    m_centerDouList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "bigDou"));
                    break;
                case 2:
                    m_centerDouList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "bigDou"));
                    m_centerDouList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "bigDou"));
                    break;
                case 3:
                    m_centerDouList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "smallDou"));
                    m_centerDouList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "bigDou"));
                    m_centerDouList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "smallDou"));
                    break;
                default:
                    break;
            }
            // 4品及以上
            string douUrl = string.Format("dou{0}", colorNameBean.t_color);
            if (colorNameBean.t_num >= 4)
            {
                // 右边
                UIGloader.SetUrl(m_rightDou, UIUtils.GetLoaderUrl(WinEnum.UI_Common, douUrl));
                m_rightDou.visible = true;
            }

            if (colorNameBean.t_num >= 5)
            {
                // 左边
                UIGloader.SetUrl(m_leftDou, UIUtils.GetLoaderUrl(WinEnum.UI_Common, douUrl));
                m_leftDou.visible = true;
            }
        }
    }

}
