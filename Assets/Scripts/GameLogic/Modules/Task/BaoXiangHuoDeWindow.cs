using Data.Beans;
using Message.Task;
using UI_TaskSystem;


public class BaoXiangHuoDeWindow : BaseWindow
{
    UI_BaoXiangHuoDeWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
    }
    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_BaoXiangHuoDeWindow>();
        window.m_CloseBtn.onClick.Add(CloseBtn);
        window.m_QueDing.onClick.Add(CloseBtn);
        FillData();
    }
    private void FillData()
    {
        
    }
    public void CloseBtn()
    {
        if (window != null)
            window = null;
        Close();
    }
}