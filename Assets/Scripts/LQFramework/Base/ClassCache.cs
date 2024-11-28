/* 
 * Coder：Zhou XiQuan
 * Time ：2017.09.19
*/

public abstract class IClassCache
{
    public virtual bool doCache { get { return false; } set { Logger.wrn(GetType() +  "doCache do not allow modify"); } }

    public static bool a = false;
    public virtual void FakeCtr(IParam param)
    {

    }

    public virtual void FakeDtr()
    {

    }
}

public abstract class IParam : IClassCache
{
    public override bool doCache { get; set; }
}

public class OneParam<T> : IParam
{
    public T value;
}

public class TwoParam<T1, T2> : IParam
{
    public T1 value1;
    public T2 value2;
}

public class ThreeParam<T1, T2, T3> : IParam
{
    public T1 value1;
    public T2 value2;
    public T3 value3;
}

public class FourParam<T1, T2, T3, T4> : IParam
{
    public T1 value1;
    public T2 value2;
    public T3 value3;
    public T4 value4;
}

public class FiveParam<T1, T2, T3, T4, T5> : IParam
{
    public T1 value1;
    public T2 value2;
    public T3 value3;
    public T4 value4;
    public T5 value5;
}

public class SixParam<T1, T2, T3, T4, T5, T6> : IParam
{
    public T1 value1;
    public T2 value2;
    public T3 value3;
    public T4 value4;
    public T5 value5;
    public T6 value6;
}