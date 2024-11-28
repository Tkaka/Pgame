
public enum DispelType
{
    Unknown = 0,   //未知
    Ctrl = 1,           //控制
    Sub = 2,          //减益
    Add = 3,          //增益
    All = 4,            //全部清除
}

public class DispelBuff : BaseBuff
{

    private int priority;

    //驱散类型(当类型为驱散时,1=驱散控制;2=驱散负面3=驱散正面)
    private DispelType dispelType = DispelType.Unknown;

    public override void ParseParam(int effectId, TwoParam<LNumber, LNumber> param)
    {
        base.ParseParam(effectId, param);
        if (BuffBean != null)
        {
            priority = BuffBean.t_buff_param;
            if (System.Enum.IsDefined(typeof(DispelType), BuffBean.t_buff_param2))
                dispelType = (DispelType)BuffBean.t_buff_param2;
            else
                Logger.err("驱散参数错误" + BuffBean.t_buff_param2);
        }
    }

    public override void ApplyEffect(bool fromTurnStart)
    {
        base.ApplyEffect(fromTurnStart);
        if (Owner != null && !IsEffect)
        {
            Owner.BuffMgr.Dispel(priority, dispelType, fromTurnStart);
        }
    }
    


}