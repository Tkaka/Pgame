

public class ChangeSoulBuff : BaseBuff
{

    private SoulType soulType;

    private SoulType oldSoulType;

    public override void ParseParam(int effectId, TwoParam<LNumber, LNumber> param)
    {
        base.ParseParam(effectId, param);
        if (BuffBean != null)
        {
            if (System.Enum.IsDefined(typeof(SoulType), BuffBean.t_buff_param))
                soulType = (SoulType)BuffBean.t_buff_param;
            else
                Logger.err("转魂参数错误");
        }
    }

    public override void ApplyEffect(bool fromTurnStart)
    {
        base.ApplyEffect(fromTurnStart);
        if (!IsEffect)
        {
            oldSoulType = Owner.SoulType;
            Owner.SoulType = soulType;
            Logger.log(oldSoulType + "--转魂生效--" + soulType);
        }
    }

    public override void OnRemove(bool fromTurnStart)
    {
        base.OnRemove(fromTurnStart);
        Owner.SoulType = oldSoulType;
    }

}

