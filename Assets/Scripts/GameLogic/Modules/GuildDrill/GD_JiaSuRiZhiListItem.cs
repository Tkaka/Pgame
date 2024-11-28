using UI_GuildDrill;

public class GD_JiaSuRiZhiListItem : UI_GD_JiaSuRiZhiListItem
{
    public new static GD_JiaSuRiZhiListItem CreateInstance()
    {
        return (GD_JiaSuRiZhiListItem)UI_GD_JiaSuRiZhiListItem.CreateInstance();
    }
    public void Init(string text)
    {
        m_string.text = text;
        height = m_string.height;
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
