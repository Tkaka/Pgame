
/// <summary>
/// 标记类buff （麻痹 眩晕 免疫控制  免疫debuff）
/// </summary>
public class MarkBuff : BaseBuff
{
    /// <summary>
    /// 标记属性
    /// </summary>
    private PropertyType propertyType;

    /// <summary>
    /// 值（对于麻痹 或者 眩晕 即大于零即生效）
    /// </summary>
    private LNumber value = 1;

    public override void ParseParam(int effectId, TwoParam<LNumber, LNumber> param)
    {
        base.ParseParam(effectId, param);
        if (param != null)
        {
            if (System.Enum.IsDefined(typeof(PropertyType), BuffBean.t_property_id))
                propertyType = (PropertyType)BuffBean.t_property_id;
            else
                Logger.err("标记buff效果属性参数错误：" + BuffBean.t_property_id);
            //优先级参数
            value = param.value1;
            if (value < 1)
                value = 1;
        }
    }

    public override void ApplyEffect(bool fromTurnStart)
    {
        base.ApplyEffect(fromTurnStart);
        if (Owner != null)
        {
            Owner.SetProperty(propertyType, value);
        }
    }

    public override void OnRemove(bool fromTurnStart)
    {
        if (Owner != null)
        {
            Owner.SetProperty(propertyType, 0);
            //Logger.log("移除buff后的属性: " + Owner.getProperty(propertyType));
        }
        base.OnRemove(fromTurnStart);
    }


}



