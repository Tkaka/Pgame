using Data.Beans;
using FairyGUI;
using UI_JinHuaLian;
using UnityEngine;

public class JinHuaLianWindow : BaseWindow
{
    t_petBean petBean;
    UI_JinHuaLianWindow window;
    private int[] xingtai;
    private int index;
    public override void OnOpen()
    {
        window = getUiWindow<UI_JinHuaLianWindow>();
        InitView();
    }
    public override void InitView()
    {
        if (Info.param == null)
        {
            Logger.err("未传参无法打开窗口");
            OnCloseBtn();
        }
        window.m_ColseBtn.onClick.Add(OnCloseBtn);
        window.m_qiehuanBtn.onClick.Add(OnQieHuan);
        int petId = (int)Info.param;
        petBean = ConfigBean.GetBean<t_petBean,int>(petId);
        if (petBean == null)
        {
            Logger.err("传参错误无法打开窗口");
            OnCloseBtn();
        }
        if (string.IsNullOrEmpty(petBean.t_star_xingtai))
        {
            Logger.err("星级对应形态为空，无法显示进化链" + petBean.t_id);
            return;
        }
        xingtai = GTools.splitStringToIntArray(petBean.t_star_xingtai);
        OnFillData();
    }
    private void OnFillData()
    {
        JinHuaDiZuo listitem;
        int lenth = xingtai.Length;
        if (lenth > 3)
        {
            window.m_qiehuanBtn.visible = true;
            lenth = 4;
        }
        else
        {
            window.m_qiehuanBtn.visible = false;
            lenth = xingtai.Length;
        }
        for (int i = 0; i < lenth; ++i)
        {
            listitem = JinHuaDiZuo.CreateInstance();
            window.m_XingTaiList.AddChild(listitem);
            listitem.Init(petBean.t_id, i);
        }
        index = lenth - 1;
    }
    private void OnQieHuan()
    {
        int lenth = xingtai.Length;
        JinHuaDiZuo listitem = window.m_XingTaiList.GetChildAt(window.m_XingTaiList.numItems - 1) as JinHuaDiZuo;
        if (xingtai.Length - 1 > index)
        {
            index += 1;
        }
        else
        {
            index = 2;
        }
        if (listitem != null)
        {
            listitem.OnChange(index);
        }
    }
    protected override void OnCloseBtn()
    {
        window.m_XingTaiList.RemoveChildren(0, -1, true);
        window = null;
        base.OnCloseBtn();
    }
}
