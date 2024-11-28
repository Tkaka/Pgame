using UI_PetParticulars;
using Data.Beans;

public class JiNengListItem : UI_JiNengListItem
{
    private t_skillBean skillBean;
    private int type;
    private int level;
    public new static JiNengListItem CreateInstance()
    {
        return (JiNengListItem)UI_JiNengListItem.CreateInstance();
    }
    public void Init(int skillId,int xiabiao,int dengji, bool isUse = false)
    {
        skillBean = ConfigBean.GetBean<t_skillBean,int>(skillId);
        if (skillBean == null)
        {
            Logger.err("JiNengListItem:Init:技能表没有这个技能，无法读取" + skillId);
            return;
        }
        type = xiabiao;
        level = dengji;
        m_name.text = skillBean.t_name;
        if (xiabiao == 1)
        {
            if (isUse == false)
            {
                isUse = true;
                level = 1;
            }
        }
        if (isUse)
        {
            OnYiJieSuo();
        }
        else
        {
            OnWeiJieSuo();
        }
        m_XiangQingBtn.onClick.Add(OnChaKanXiangQing);
    }
    private void OnYiJieSuo()
    {
        m_mengban.visible = false;
        UIGloader.SetUrl(m_icon,skillBean.t_icon);
        m_suo.visible = false;
        string miaoshu = "等级：{0}";
        m_jiesuoxingji.text = string.Format(miaoshu, level.ToString());
    }
    private void OnWeiJieSuo()
    {
        m_mengban.visible = true;
        UIGloader.SetUrl(m_icon,skillBean.t_icon);
        m_suo.visible = true;
        string miaoshu = "宠物{0}星解锁";
        m_jiesuoxingji.text = string.Format(miaoshu,(type + 1).ToString());
    }
    private void OnChaKanXiangQing()
    {
        TwoParam<int, int> twoParam = new TwoParam<int, int>();
        twoParam.value1 = skillBean.t_id;
        twoParam.value2 = level;
        WinInfo info = new WinInfo();
        info.param = twoParam;
        WinMgr.Singleton.Open<SkillDetailWindow>(info, UILayer.Popup);
    }
    public override void Dispose()
    {
        skillBean = null;
        base.Dispose();
    }
}
