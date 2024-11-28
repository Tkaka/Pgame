using UI_GuillRedEnvelope;
class GER_WoDeHuoDe : UI_GER_WoDeHuoDe
{
    public new static GER_WoDeHuoDe CreateInstance()
    {
        return (GER_WoDeHuoDe)UI_GER_WoDeHuoDe.CreateInstance();
    }
    public void Init(string miaoshu)
    {
        m_miaoshu.text = miaoshu;
    }
}
