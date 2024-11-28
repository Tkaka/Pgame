using UI_Achievement;

public class AM_StartListItem : UI_AM_StartListItem
{
    public new static AM_StartListItem CreateInstance()
    {
        return (AM_StartListItem)UI_AM_StartListItem.CreateInstance();
    }
    public void Init(bool xianshi)
    {
        if (xianshi)
        {
            m_conclude.visible = true;
            m_NoConclude.visible = false;
        }
        else
        {
            m_conclude.visible = false;
            m_NoConclude.visible = true;
        }
    }
}
