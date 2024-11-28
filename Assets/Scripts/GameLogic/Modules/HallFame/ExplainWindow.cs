using UI_HallFame;

public class ExplainWindow : BaseWindow
{
    UI_ExplainWindow window;
    private int[] guizeId = { };
    public override void OnOpen()
    {
        window = getUiWindow<UI_ExplainWindow>();
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
    }
    public override void InitView()
    {
        ExplainListItem explainListItem;
        window.m_GuiZeList.RemoveChildren(0,-1,true);
        for (int i = 0; i < guizeId.Length; ++i)
        {
            explainListItem = ExplainListItem.CreateInstance();
            explainListItem.Init(guizeId[i]);
            window.m_GuiZeList.AddChild(explainListItem);
        }
        base.InitView();
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }
}
