using UI_GuillRedEnvelope;

public class GRE_GuiZeWindow : BaseWindow
{
    UI_GRE_GuiZeWindow window;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_GRE_GuiZeWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        InitView();
    }
    public override void InitView()
    {
        base.InitView();
        GRE_GuiZeListitem listitem;
        for (int i = 0; i < 8; ++i)
        {
            listitem = GRE_GuiZeListitem.CreateInstance();
            listitem.Init(71602100 + i);
            window.m_guizeList.AddChild(listitem);
        }
    }
    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
