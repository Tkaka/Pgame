
using Data.Beans;
/// <summary>
/// 斩灭buff (必须保证只有一个)
/// </summary>
public class BeHeadBuff : BaseBuff
{

    /// <summary>
    /// 条件百分比
    /// </summary>
    public LNumber PerVal { private set; get; }

    /// <summary>
    /// 加成百分比
    /// </summary>
    public LNumber AddVal { private set; get; }

    /// <summary>
    /// 大于还是小于
    /// </summary>
    public CompareSymbol Compare { private set; get; }

    public override void ParseParam(int effectId, TwoParam<LNumber, LNumber> param)
    {
        base.ParseParam(effectId, param);
        if (param != null)
        {
            PerVal = param.value1;
            AddVal = param.value2;
            //Compare = (CompareSymbol)param.value6;
        }
    }

}