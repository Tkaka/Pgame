/*
 * File Name:               XXVector3.cs
 *
 * Description:             整型基础数学库
 * Author:                  zhangwei
 * Create Date:             2016/12/30
 */

using UnityEngine;

[System.Serializable]
public struct XVector3// : IEquatable<XVector3>
{
    public XNumber x;
    public XNumber y;
    public XNumber z;

    public static XVector3 zero = new XVector3(XNumber.zero, XNumber.zero, XNumber.zero);
    public static XVector3 one = new XVector3(XNumber.one, XNumber.one, XNumber.one);
    public static XVector3 up = new XVector3(XNumber.zero, XNumber.one, XNumber.zero);
    public static XVector3 down = new XVector3(XNumber.zero, -XNumber.one, XNumber.zero);
    public static XVector3 left = new XVector3(-XNumber.one, XNumber.zero, XNumber.zero);
    public static XVector3 right = new XVector3(XNumber.one, XNumber.zero, XNumber.zero);
    public static XVector3 forward = new XVector3(XNumber.zero, XNumber.zero, XNumber.one);
    public static XVector3 back = new XVector3(XNumber.zero, XNumber.zero, -XNumber.one);
    public static XVector3 gravity = new XVector3(XNumber.zero, -XNumber.create(9, 800), XNumber.zero);

    private const int FRACTION_BITS = XNumber.FRACTION_BITS;                                      // 小数位位数 10
    private const int FRACTION_RANGE = XNumber.FRACTION_RANGE;                                    // 1024 == 1000000000

    public XNumber magnitude
    {
        get
        {
            XNumber r;
            long dot = (long)x.raw * x.raw + (long)y.raw * y.raw + (long)z.raw * z.raw;
            r.raw = XIntMath.Sqrt_Long(dot);
#if XNUMBER_CHECK
            XIntMath.Check_Number(r, ((Vector3)this).magnitude);
#endif
            return r;
        }
    }

    public XNumber sqrMagnitude
    {
        get
        {
            // return ((Vector3)this).sqrMagnitude;
            XNumber ret;
            ret.raw = (int)(((long)x.raw * x.raw + (long)y.raw * y.raw + (long)z.raw * z.raw) >> XIntMath.FACTOR);
#if XNUMBER_CHECK
            XIntMath.Check_Number(ret, ((Vector3)this).sqrMagnitude);
#endif

            return ret;
        }
    }

    public XVector3 normalized
    {
        get
        {
            int mag = magnitude.raw;
            if (mag == 0)
                return this;

            XVector3 ret;
            ret.x.raw = (int)((((long)x.raw << (FRACTION_BITS + 1)) / (long)mag + 1) >> 1);
            ret.y.raw = (int)((((long)y.raw << (FRACTION_BITS + 1)) / (long)mag + 1) >> 1);
            ret.z.raw = (int)((((long)z.raw << (FRACTION_BITS + 1)) / (long)mag + 1) >> 1);

#if XNUMBER_CHECK
            XIntMath.Check_Vector3(ret, ((Vector3)this).normalized);
#endif

            return ret;
        }
    }

    public XVector3 XZMirror
    {
        get
        {
            return new XVector3(-x, y, -z);
        }
    }

    public XVector3(XNumber x, XNumber y, XNumber z)
    {
        //ProfileTool.Begin("XVector3.ctor(x,y,z)");
        this.x.raw = x.raw;
        this.y.raw = y.raw;
        this.z.raw = z.raw;
        //ProfileTool.End();
    }

    public static XVector3 operator +(XVector3 lhs, XVector3 rhs)
    {
        XVector3 result;
        result.x.raw = lhs.x.raw + rhs.x.raw;
        result.y.raw = lhs.y.raw + rhs.y.raw;
        result.z.raw = lhs.z.raw + rhs.z.raw;
        return result;
    }

    public static XVector3 operator -(XVector3 lhs, XVector3 rhs)
    {
        XVector3 result;
        result.x.raw = lhs.x.raw - rhs.x.raw;
        result.y.raw = lhs.y.raw - rhs.y.raw;
        result.z.raw = lhs.z.raw - rhs.z.raw;
        return result;
    }

    public static XVector3 operator -(XVector3 lhs)
    {
        XVector3 result;
        result.x.raw = -lhs.x.raw;
        result.y.raw = -lhs.y.raw;
        result.z.raw = -lhs.z.raw;
        return result;
    }

    public static XVector3 operator *(XVector3 lhs, XNumber rhs)
    {
        XVector3 result;
        result.x.raw = (int)(((long)lhs.x.raw * rhs.raw + (FRACTION_RANGE >> 1)) >> FRACTION_BITS);
        result.y.raw = (int)(((long)lhs.y.raw * rhs.raw + (FRACTION_RANGE >> 1)) >> FRACTION_BITS);
        result.z.raw = (int)(((long)lhs.z.raw * rhs.raw + (FRACTION_RANGE >> 1)) >> FRACTION_BITS);

        return result;
    }

    public static XVector3 operator *(XNumber lhs, XVector3 rhs)
    {
        XVector3 result;
        result.x.raw = (int)(((long)lhs.raw * rhs.x.raw + (FRACTION_RANGE >> 1)) >> FRACTION_BITS);
        result.y.raw = (int)(((long)lhs.raw * rhs.y.raw + (FRACTION_RANGE >> 1)) >> FRACTION_BITS);
        result.z.raw = (int)(((long)lhs.raw * rhs.z.raw + (FRACTION_RANGE >> 1)) >> FRACTION_BITS);

        return result;
    }

    public static XVector3 operator /(XVector3 lhs, XNumber rhs)
    {
        var factor = 1;
        if (rhs.raw < 0)
            factor = -1;

        if ((rhs.raw + factor) >> 1 == 0)
        {
            Debug.LogError("除0了");
            return XVector3.zero;
        }

        XVector3 result;
        result.x.raw = lhs.x.raw == 0 ? 0 : (int)((((long)lhs.x.raw << (FRACTION_BITS + 1)) / (long)rhs.raw + factor) >> 1);
        result.y.raw = lhs.y.raw == 0 ? 0 : (int)((((long)lhs.y.raw << (FRACTION_BITS + 1)) / (long)rhs.raw + factor) >> 1);
        result.z.raw = lhs.z.raw == 0 ? 0 : (int)((((long)lhs.z.raw << (FRACTION_BITS + 1)) / (long)rhs.raw + factor) >> 1);

        return result;
    }

    public static bool operator ==(XVector3 lhs, XVector3 rhs)
    {
        return lhs.x.raw == rhs.x.raw && lhs.y.raw == rhs.y.raw && lhs.z.raw == rhs.z.raw;
    }

    public static bool operator !=(XVector3 lhs, XVector3 rhs)
    {
        return lhs.x.raw != rhs.x.raw || lhs.y.raw != rhs.y.raw || lhs.z.raw != rhs.z.raw;
    }

    public static implicit operator Vector3(XVector3 lhs)
    {
        Vector3 result;
        result.x = lhs.x;
        result.y = lhs.y;
        result.z = lhs.z;
        return result;
    }

    public static explicit operator XVector3(Vector3 lhs)
    {
        return XIntMathTools.Convert(lhs);
    }

    public static explicit operator XVector3(Vector2 lhs)
    {
        // tmp
        return new XVector3(lhs.x, 0, lhs.y);
    }

    //public void Normalize()
    //{
    //    this = normalized;
    //}

    //private static long _Dot(XVector3 lhs, XVector3 rhs)
    //{
    //    long xTmp = (long)lhs.x.raw * rhs.x.raw;
    //    long yTmp = (long)lhs.y.raw * rhs.y.raw;
    //    long zTmp = (long)lhs.z.raw * rhs.z.raw;

    //    return xTmp + yTmp + zTmp;
    //}

    public static XNumber Dot(XVector3 lhs, XVector3 rhs)
    {
        XNumber result;
        result.raw = (int)(((long)lhs.x.raw * rhs.x.raw + (long)lhs.y.raw * rhs.y.raw + (long)lhs.z.raw * rhs.z.raw) >> XIntMath.FACTOR);

#if XNUMBER_CHECK
        XIntMath.Check_Number(result, Vector3.Dot(lhs, rhs));
#endif
        return result;
    }

    public static XVector3 Cross(XVector3 lhs, XVector3 rhs)
    {
        long x = (long)lhs.y.raw * rhs.z.raw - (long)rhs.y.raw * lhs.z.raw;
        long y = (long)lhs.z.raw * rhs.x.raw - (long)rhs.z.raw * lhs.x.raw;
        long z = (long)lhs.x.raw * rhs.y.raw - (long)rhs.x.raw * lhs.y.raw;          //2 power of factor

        XNumber xx;
        XNumber yy;
        XNumber zz;

        xx.raw = (int)(x >> XIntMath.FACTOR);
        yy.raw = (int)(y >> XIntMath.FACTOR);
        zz.raw = (int)(z >> XIntMath.FACTOR);

        XVector3 result;
        result.x = xx;
        result.y = yy;
        result.z = zz;

        //ProfileTool.End();
#if XNUMBER_CHECK
        XIntMath.Check_Vector3(result, Vector3.Cross(lhs, rhs));
#endif
        return result;
    }

    //public static XVector3 CrossAndNormalize(XVector3 lhs, XVector3 rhs)
    //{
    //    XVector3 crossVec = Cross(lhs, rhs);
    //    return crossVec.normalized;
    //}


    public static XNumber Angle(XVector3 lhs, XVector3 rhs)
    {
        // todo 误差较大
        // return Vector3.Angle(lhs, rhs);
        if (lhs == XVector3.zero || rhs == XVector3.zero || lhs == rhs)
        {
            //Debug.LogError("用了零向量计算角度");
            return XNumber.zero;
        }

        var ret = XIntMath.Rad2Deg(XIntMath.Acos(XIntMath.Clamp(Dot(lhs.normalized, rhs.normalized), -XNumber.one, XNumber.one)));
#if XNUMBER_CHECK
        XIntMath.Check_Number(ret, Vector3.Angle(lhs, rhs));
#endif
        return ret;
    }

    public static XNumber Distance(XVector3 lhs, XVector3 rhs)
    {
        var x = (long)(lhs.x.raw - rhs.x.raw);
        var y = (long)(lhs.y.raw - rhs.y.raw);
        var z = (long)(lhs.z.raw - rhs.z.raw);
        XNumber ret;
        ret.raw = XIntMath.Sqrt_Long(x * x + y * y + z * z);

#if XNUMBER_CHECK
        XIntMath.Check_Number(ret, Vector3.Distance(lhs, rhs));
#endif

        return ret;
    }

    // 该方法调用 100w次 耗时 0.281s
    public static XVector3 Lerp(XVector3 from, XVector3 to, XNumber t)
    {
        //t = XIntMath.Clamp(t, XNumber.zero, XNumber.one);
        if (t.raw > XNumber.one.raw) t.raw = XNumber.one.raw;
        if (t.raw < 0) t.raw = 0;

        XVector3 ret;
        ret.x.raw = from.x.raw + ((to.x.raw - from.x.raw) * t.raw >> XNumber.FRACTION_BITS);
        ret.y.raw = from.y.raw + ((to.y.raw - from.y.raw) * t.raw >> XNumber.FRACTION_BITS);
        ret.z.raw = from.z.raw + ((to.z.raw - from.z.raw) * t.raw >> XNumber.FRACTION_BITS);
#if XNUMBER_CHECK
        XIntMath.Check_Vector3(vec, Vector3.Lerp(from, to, t));
#endif

        return ret;
    }

    // 该方法调用 100w次 耗时 0.023s
    // ps 仅用在调用非常频繁的地方比较好 因为有使用值域限制
    public static void xlerp(ref XVector3 from, ref XVector3 to, ref XNumber t, out XVector3 ret)
    {
        // todo 做一些容错检查
        ret.x.raw = from.x.raw + ((to.x.raw - from.x.raw) * t.raw >> XNumber.FRACTION_BITS);
        ret.y.raw = from.y.raw + ((to.y.raw - from.y.raw) * t.raw >> XNumber.FRACTION_BITS);
        ret.z.raw = from.z.raw + ((to.z.raw - from.z.raw) * t.raw >> XNumber.FRACTION_BITS);
    }

    //public static XVector3 InverseLerp(XVector3 a, XVector3 b, XNumber value)
    //{
    //    XVector3 ret;

    //    ret.x = XIntMath.InverseLerp(a.x, b.x, value);
    //    ret.y = XIntMath.InverseLerp(a.y, b.y, value);
    //    ret.z = XIntMath.InverseLerp(a.z, b.z, value);

    //    return ret;
    //}

    public override string ToString()
    {
        return string.Format("({0}, {1}, {2})", x, y, z);
    }

    public string ToString(string fmt)
    {
        return string.Format("({0}, {1}, {2})", x.ToString(fmt), y.ToString(fmt), z.ToString(fmt));
    }

    public static XVector3 Parse(string text)
    {
        if (text[0] == '(' || text[text.Length - 1] == ')')
            text = text.Substring(1, text.Length - 2);
        string[] tokens = text.Split(',');
        if (tokens.Length != 3)
            tokens = text.Split(' ');
        if (tokens.Length != 3)
            throw new System.FormatException("The input text must contains 3 elements.");
        XNumber x = XNumber.Parse(tokens[0]);
        XNumber y = XNumber.Parse(tokens[1]);
        XNumber z = XNumber.Parse(tokens[2]);
        return new XVector3(x, y, z);
    }

    public bool Equals(XVector3 rhs)
    {
        return x.raw == rhs.x.raw && y.raw == rhs.y.raw && z.raw == rhs.z.raw;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
    }

    public static XVector3 Project(XVector3 vector, XVector3 onNormal)
    {
        // 前一个向量在后一个向量上的投影
        //return (XVector3)Vector3.Project(vector, onNormal);

        onNormal = onNormal.normalized;
        //var num = Dot(vector, onNormal);

        XNumber num;
        num.raw = (int)(((long)vector.x.raw * onNormal.x.raw + (long)vector.y.raw * onNormal.y.raw + (long)vector.z.raw * onNormal.z.raw) >> XIntMath.FACTOR);
        //return onNormal * num;

        //if (XIntMath.Abs(num) < XNumber.epsilon)
        //{
        //    return zero;
        //}

        onNormal.x.raw = (int)(((long)onNormal.x.raw * num.raw) >> FRACTION_BITS);
        onNormal.y.raw = (int)(((long)onNormal.y.raw * num.raw) >> FRACTION_BITS);
        onNormal.z.raw = (int)(((long)onNormal.z.raw * num.raw) >> FRACTION_BITS);
        return onNormal;
    }

    public static XVector3 ProjectOnPlane(XVector3 vector, XVector3 planeNormal)
    {
        return (vector - Project(vector, planeNormal));
    }

    public XVector2 xz
    {
        get
        {
            XVector2 result;
            result.x.raw = x.raw;
            result.y.raw = z.raw;
            return result;
        }
    }

    public XVector2 xy
    {
        get
        {
            XVector2 result;
            result.x.raw = x.raw;
            result.y.raw = y.raw;
            return result;
        }
    }

    public XVector2 zy
    {
        get
        {
            XVector2 result;
            result.x.raw = z.raw;
            result.y.raw = y.raw;
            return result;
        }
    }

    //public XVector3 Deg2Rad
    //{
    //    get
    //    {
    //        XVector3 ret;
    //        ret.x = x.Deg2Rad;
    //        ret.y = y.Deg2Rad;
    //        ret.z = z.Deg2Rad;
    //        return ret;
    //    }
    //}

    //public XVector3 Rad2Deg
    //{
    //    get
    //    {
    //        XVector3 ret;
    //        ret.x = x.Rad2Deg;
    //        ret.y = y.Rad2Deg;
    //        ret.z = z.Rad2Deg;
    //        return ret;
    //    }
    //}

    //public XVector3 MakePositive()
    //{
    //    XVector3 ret = this;

    //    XNumber negativeFlip = -XNumber.epsilon;
    //    XNumber positiveFlip = (XIntMath.PI * 2) - XNumber.epsilon;

    //    if (ret.x < negativeFlip)
    //        ret.x += 2 * XIntMath.PI;
    //    else if (ret.x > positiveFlip)
    //        ret.x -= 2 * XIntMath.PI;

    //    if (ret.y < negativeFlip)
    //        ret.y += 2 * XIntMath.PI;
    //    else if (ret.y > positiveFlip)
    //        ret.y -= 2 * XIntMath.PI;

    //    if (ret.z < negativeFlip)
    //        ret.z += 2 * XIntMath.PI;
    //    else if (ret.z > positiveFlip)
    //        ret.z -= 2 * XIntMath.PI;

    //    return ret;
    //}
}