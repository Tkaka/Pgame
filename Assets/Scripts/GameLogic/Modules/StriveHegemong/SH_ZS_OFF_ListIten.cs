using UI_StriveHegemong;

public class SH_ZS_OFF_ListIten : UI_SH_ZS_OFF_ListIten
{
    public new static SH_ZS_OFF_ListIten CreateInstance()
    {
        return (SH_ZS_OFF_ListIten)UI_SH_ZS_OFF_ListIten.CreateInstance();
    }
    public void Init(string text)
    {
        m_xianshi.text = text;
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
