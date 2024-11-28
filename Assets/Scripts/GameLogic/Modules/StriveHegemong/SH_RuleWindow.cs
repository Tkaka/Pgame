using System.Collections.Generic;
using UI_StriveHegemong;
using Data.Beans;

public class SH_RuleWindow : BaseWindow
{
    UI_SH_RuleWindow window;
    public override void OnOpen()
    {
        window = getUiWindow<UI_SH_RuleWindow>();
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        InitView();
    }
    /// <summary>
    /// 1、加载链表
    /// 2、排序
    /// 3、加载
    /// </summary>
    public override void InitView()
    {
        List<t_ruleBean> guize = ConfigBean.GetBeanList<t_ruleBean>();
        
        //记录类型
        int type = -1;
        //类型不同时加载分割线元件，同时更新类型
        SH_GZ_FenGeXian fengexian;//分割线
        SH_GZ_ListItem item;//纯语言item
        SH_GZ_Time time;//时间段显示
        SH_GZ_Jiangli jiangpin;//奖品展示
        //加载元件
        for (int i = 0; i < guize.Count; ++i)
        {
            if (type != guize[i].t_type)
            {
                //加载分割线
                fengexian = SH_GZ_FenGeXian.CreateInstance();
                fengexian.Init(guize[i].t_desc);
                window.m_GuiZeList.AddChild(fengexian);
                type = guize[i].t_type;
            }
            else if (guize[i].t_type == 5)
            {
                time = SH_GZ_Time.CreateInstance();
                time.Init(guize[i].t_desc);
                window.m_GuiZeList.AddChild(time);
            }
            else
            {
                item = SH_GZ_ListItem.CreateInstance();
                item.Init(guize[i].t_desc);
                window.m_GuiZeList.AddChild(item);
            }
        }
        OnJiangPinZhanShi();
    }
    private void OnJiangPinZhanShi()
    {
        List<t_rank_rewardBean> jiangpin = ConfigBean.GetBeanList<t_rank_rewardBean>();
        SH_GZ_Jiangli jiangli;
        for (int i = 0; i < jiangpin.Count; ++i)
        {
            jiangli = SH_GZ_Jiangli.CreateInstance();
            jiangli.Init(jiangpin[i]);
            window.m_GuiZeList.AddChild(jiangli);
        }
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }
}
