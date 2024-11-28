using UI_DrawCard;

public class JieSuoDengJiFenGeXian : UI_JieSuoDengJiFenGeXian
{
    public new static JieSuoDengJiFenGeXian CreateInstance()
    {
        return (JieSuoDengJiFenGeXian)UI_JieSuoDengJiFenGeXian.CreateInstance();
    }

    public void Init(int num)
    {
        string str = "{0}级解锁以下宠物";
        string miaoshu = string.Format(str,num.ToString());
        m_JieSuo.text = miaoshu;
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
