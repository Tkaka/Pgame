/*
 * File Name:               XXVector2.cs
 *
 * Description:             整型基础数学库
 * Author:                  zhangwei
 * Create Date:             2016/12/30
 */

using UnityEngine;

[System.Serializable]
public struct XVector2// : IEquatable<XVector2>
{
    public XNumber x;
    public XNumber y;

    private const int FRACTION_BITS = XNumber.FRACTION_BITS;                                      // 小数位位数 10
    private const int FRACTION_RANGE = XNumber.FRACTION_RANGE;                                    // 1024 == 1000000000

    public static XVector2 zero = new XVector2(XNumber.zero, XNumber.zero);
    public static XVector2 one = new XVector2(XNumber.one, XNumber.one);
    public static XVector2 up = new XVector2(XNumber.zero, XNumber.one);
    public static XVector2 down = new XVector2(XNumber.zero, -XNumber.one);
    public static XVector2 left = new XVector2(-XNumber.one, XNumber.zero);
    public static XVector2 right = new XVector2(XNumber.one, XNumber.zero);

    public XNumber magnitude
    {
        get
        {
            XNumber result;
            long dot = (long)x.raw * x.raw + (long)y.raw * y.raw;
            result.raw = XIntMath.Sqrt_Long(dot);
            return result;
        }
    }

    public XVector2 yx
    {
        get
        {
            XVector2 result;
            result.x.raw = y.raw;
            result.y.raw = x.raw;
            return result;
        }
    }

    public XVector3 x0z
    {
        get
        {
            XVector3 result;
            result.x.raw = x.raw;
            result.y.raw = 0;
            result.z.raw = y.raw;
            return result;
        }
    }

    public XVector3 xy0
    {
        get
        {
            XVector3 result;
            result.x.raw = x.raw;
            result.y.raw = y.raw;
            result.z.raw = 0;
            return result;
        }
    }

    public XNumber sqrMagnitude
    {
        get
        {
            XNumber ret;
            ret.raw = (int)(((long)x.raw * x.raw + (long)y.raw * y.raw) >> FRACTION_BITS);
            return ret;
        }
    }

    public XVector2 normalized
    {
        get
        {
            int mag = magnitude.raw;
            if (mag == 0)
                return this;

            XVector2 ret;
            ret.x.raw = (int)((((long)x.raw << (FRACTION_BITS + 1)) / (long)mag + 1) >> 1);
            ret.y.raw = (int)((((long)y.raw << (FRACTION_BITS + 1)) / (long)mag + 1) >> 1);

            return ret;
        }
    }

    public XVector2(XNumber x)
    {
        this.x.raw = x.raw;
        this.y.raw = x.raw;
    }

    public XVector2(XNumber x, XNumber y)
    {
        this.x.raw = x.raw;
        this.y.raw = y.raw;
    }



    public static XVector2 operator +(XVector2 lhs, XVector2 rhs)
    {
        XVector2 result;
        result.x.raw = lhs.x.raw + rhs.x.raw;
        result.y.raw = lhs.y.raw + rhs.y.raw;
        return result;
    }

    public static XVector2 operator -(XVector2 lhs, XVector2 rhs)
    {
        XVector2 result;
        result.x.raw = lhs.x.raw - rhs.x.raw;
        result.y.raw = lhs.y.raw - rhs.y.raw;
        return result;
    }

    public static XVector2 operator -(XVector2 lhs)
    {
        XVector2 result;
        result.x.raw = -lhs.x.raw;
        result.y.raw = -lhs.y.raw;
        return result;
    }

    public static XVector2 operator *(XVector2 lhs, XVector2 rhs)
    {
        XVector2 result;
        result.x.raw = (lhs.x.raw * rhs.x.raw) >> FRACTION_BITS;
        result.y.raw = (lhs.y.raw * rhs.y.raw) >> FRACTION_BITS;
        return result;
    }

    public static XVector2 operator *(XVector2 lhs, XNumber rhs)
    {
        XVector2 result;
        result.x.raw = (lhs.x.raw * rhs.raw) >> FRACTION_BITS;
        result.y.raw = (lhs.y.raw * rhs.raw) >> FRACTION_BITS;
        return result;
    }

    public static XVector2 operator *(XNumber lhs, XVector2 rhs)
    {
        XVector2 result;
        result.x.raw = (lhs.raw * rhs.x.raw) >> FRACTION_BITS;
        result.y.raw = (lhs.raw * rhs.y.raw) >> FRACTION_BITS;
        return result;
    }

    //public static XVector2 operator /(XVector2 lhs, XVector2 rhs)
    //{
    //    XVector2 result;
    //    result.x = lhs.x / rhs.x;
    //    result.y = lhs.y / rhs.y;
    //    return result;
    //}

    public static XVector2 operator /(XVector2 lhs, XNumber rhs)
    {
        var factor = 1;
        if (rhs.raw < 0)
            factor = -1;

        if ((rhs.raw + factor) >> 1 == 0)
        {
            Debug.LogError("除0了");
            return XVector2.zero;
        }

        XVector2 result;
        result.x.raw = lhs.x.raw == 0 ? 0 : (int)((((long)lhs.x.raw << (FRACTION_BITS + 1)) / (long)rhs.raw + factor) >> 1);
        result.y.raw = lhs.y.raw == 0 ? 0 : (int)((((long)lhs.y.raw << (FRACTION_BITS + 1)) / (long)rhs.raw + factor) >> 1);
        return result;
    }

    public static bool operator ==(XVector2 lhs, XVector2 rhs)
    {
        return lhs.x.raw == rhs.x.raw && lhs.y.raw == rhs.y.raw;
    }

    public static bool operator !=(XVector2 lhs, XVector2 rhs)
    {
        return lhs.x.raw != rhs.x.raw || lhs.y.raw != rhs.y.raw;
    }

    /*
    public static explicit operator UnityEngine.XVector2(XVector2 lhs)
    {
        return new UnityEngine.XVector2((float)lhs.x, (float)lhs.y);
    }
     */

    //public void Normalize()
    //{
    //    this = normalized;
    //}

    //private static long _Dot(XVector2 lhs, XVector2 rhs)
    //{
    //    long xTmp = (long)lhs.x.raw * rhs.x.raw;
    //    long yTmp = (long)lhs.y.raw * rhs.y.raw;
    //    return xTmp + yTmp;
    //}

    public static XNumber Dot(XVector2 lhs, XVector2 rhs)
    {
        XNumber result;
        result.raw = (int)(((long)lhs.x.raw * rhs.x.raw + (long)lhs.y.raw * rhs.y.raw) >> XIntMath.FACTOR);
        return result;
    }

    public static XNumber Distance(XVector2 lhs, XVector2 rhs)
    {
        XNumber result;
        long dot = (long)(lhs.x.raw - rhs.x.raw) * (lhs.x.raw - rhs.x.raw) + (long)(lhs.y.raw - rhs.y.raw) * (lhs.y.raw - rhs.y.raw);
        result.raw = XIntMath.Sqrt_Long(dot);

        return result;
    }

    public static XNumber DistanceSquare(XVector2 lhs, XVector2 rhs)
    {
        lhs.x.raw -= rhs.x.raw;
        lhs.y.raw -= rhs.y.raw;
        return lhs.sqrMagnitude;
    }

    /// <summary>
    /// 计算平面投影
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="onNormal"></param>
    /// <returns></returns>
    public static XVector2 Project(XVector2 vector, XVector2 onNormal)
    {
        var ret = XVector3.Project(vector.x0z, onNormal.x0z);
        return ret.xz;
    }

    /// <summary>
    /// 两个向量的夹角， 无顺时针逆时针关系，范围夹角绝对值
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static XNumber Angle(XVector2 from, XVector2 to)
    {
        //return XVector3.Angle(from.x0z, to.x0z);

        if (from == zero || to == zero || from == to)
        {
            //Debug.LogError("用了零向量计算角度");
            return XNumber.zero;
        }

        var ret = XIntMath.Rad2Deg(XIntMath.Acos(XIntMath.Clamp(Dot(from.normalized, to.normalized), -XNumber.one, XNumber.one)));
#if XNUMBER_CHECK
        XIntMath.Check_Number(ret, Vector3.Angle(lhs, rhs));
#endif
        return ret;
    }

    /// <summary>
    /// 两个向量的夹角, 逆时针[0,180] 顺时(0,-180);
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static XNumber Angle180(XVector2 from, XVector2 to)
    {
        XNumber angle = Angle(from, to);
        if (XVector3.Cross(from.x0z, to.x0z).y > 0)
        {
            // left
            angle = -angle;
        }

        return angle;
    }

    /// <summary>
    /// 夹角绝对值[0, 180]
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static XNumber AngleAbs(XVector2 from, XVector2 to)
    {
        var angle = Angle180(from, to);
        return XIntMath.Abs(angle);
    }

    /// <summary>
    /// 两个向量的夹角, 逆时针[0,360)
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static XNumber Angle360(XVector2 from, XVector2 to)
    {
        XNumber angle = Angle(from, to);
        if (XVector3.Cross(from.x0z, to.x0z).y > 0)
        {
            angle = -angle;
        }

        if (angle < 0)
            angle += 360;

        return angle;
    }

    public static XNumber AngleRad(XVector2 lhs, XVector2 rhs)
    {
        long a = ((long)lhs.x.raw * rhs.x.raw + (long)lhs.y.raw * rhs.y.raw);
        long b = (((long)lhs.x.raw * lhs.x.raw + (long)lhs.y.raw * lhs.y.raw) >> XIntMath.FACTOR);
        long c = (((long)rhs.x.raw * rhs.x.raw + (long)rhs.y.raw * rhs.y.raw) >> XIntMath.FACTOR);
        int e = XIntMath.Sqrt_Long(b * c);
        if (e == 0)
        {
            return XIntMath.HALF_PI;
        }
        XNumber cosin;
        cosin.raw = (int)(a / e);
        cosin = XIntMath.Clamp(cosin, -XNumber.one, XNumber.one);
        return XIntMath.Acos(cosin);
    }

    //public static XVector2 Lerp(XVector2 from, XVector2 to, XNumber t)
    //{
    //    t = XIntMath.Clamp(t, XNumber.zero, XNumber.one);
    //    XVector2 vec = from * (XNumber.one - t) + to * t;
    //    return vec;
    //}

    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }

    public string ToString(string fmt)
    {
        return string.Format("({0}, {1})", x.ToString(fmt), y.ToString(fmt));
    }

    public static XVector2 Parse(string text)
    {
        if (text[0] == '(' || text[text.Length - 1] == ')')
            text = text.Substring(1, text.Length - 2);
        string[] tokens = text.Split(',');
        if (tokens.Length != 2)
            tokens = text.Split(' ');
        if (tokens.Length != 2)
            throw new System.FormatException("The input text must contains 2 elements.");
        XVector2 result;
        result.x = XNumber.Parse(tokens[0]);
        result.y = XNumber.Parse(tokens[1]);
        return result;
    }

    public bool Equals(XVector2 other)
    {
        return this.x.raw == other.x.raw && y.raw == other.y.raw;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }

    public static implicit operator UnityEngine.Vector2(XVector2 lhs)
    {
        UnityEngine.Vector2 result;
        result.x = (float)lhs.x;
        result.y = (float)lhs.y;
        return result;
    }

    public static implicit operator XVector2(UnityEngine.Vector2 lhs)
    {
        XVector2 result;
        result.x = lhs.x;
        result.y = lhs.y;
        return result;
    }

    /// <summary>
    /// 逆时针旋转这个向量angle度;
    /// </summary>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    public XVector2 RotateSelf(XNumber angle)
    {
        if (this != zero)
        {
            var radius = XIntMath.PI * angle / XNumber.create(180, 0);
            var sina = XIntMath.Sin(radius);
            var cosa = XIntMath.Cos(radius);
            var x1 = (x.raw * cosa.raw - y.raw * sina.raw) >> FRACTION_BITS;
            var y1 = (x.raw * sina.raw + y.raw * cosa.raw) >> FRACTION_BITS;
            x.raw = x1;
            y.raw = y1;

            return this;
        }

        return this;
    }

    /// <summary>
    /// 逆时针旋转这个向量angle度;
    /// </summary>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    public XVector2 Rotate(XNumber angle)
    {
        if (this != zero)
        {
            var radius = XIntMath.PI * angle / XNumber.create(180, 0);
            var sina = XIntMath.Sin(radius);
            var cosa = XIntMath.Cos(radius);
            var x1 = (x.raw * cosa.raw - y.raw * sina.raw) >> FRACTION_BITS;
            var y1 = (x.raw * sina.raw + y.raw * cosa.raw) >> FRACTION_BITS;
            XVector2 ret;
            ret.x.raw = x1;
            ret.y.raw = y1;

            return ret;
        }

        return zero;
    }

    public XVector2 MirrorY()
    {
        return new XVector2(-x, y);
    }

    public XVector2 cross
    {
        get
        {
            return new XVector2(y, -x);
        }
    }
}
