/*
 * File Name:               XNumber.cs
 *
 * Description:             整型数字 第1位为符号位 中间21位为整数位(2097152) 后10位为小数位(1024)
 * Author:                  zhangwei
 * Create Date:             2016/12/30
 */

using System;
using System.Runtime.Serialization;
using UnityEngine;

//精确到小数点后面3位
[System.Serializable]
public struct XNumber : IComparable<XNumber>, IEquatable<XNumber>, ISerializable
{
    public const int FRACTION_BITS = 10;                                      // 小数位位数 10
    public const int INTEGER_BITS = sizeof(int) * 8 - FRACTION_BITS;          // 整数位位数 22

    public const int FRACTION_MASK = (int)(uint.MaxValue >> INTEGER_BITS);    // 1023  == 0111111111
    public const int INTEGER_MASK = (int)(-1 & ~FRACTION_MASK);               // -1024
    public const int FRACTION_RANGE = FRACTION_MASK + 1;                      // 1024 == 1000000000

    public static readonly XNumber MaxValue = create_row(int.MaxValue);
    public static readonly XNumber MinValue = create_row(int.MinValue);
    public static readonly XNumber zero = create_row(0);
    public static readonly XNumber one = 1;
    public static readonly XNumber minus_one = -one;
    public static readonly XNumber epsilon = create_row(1);
    public static readonly XNumber two = 2;
    public static readonly XNumber three = 3;
    public static readonly XNumber four = 4;
    public static readonly XNumber five = 5;
    public static readonly XNumber six = 6;
    public static readonly XNumber seven = 7;
    public static readonly XNumber eight = 8;
    public static readonly XNumber nine = 9;
    public static readonly XNumber ten = 10;
    public static readonly XNumber half = create(0, 500);
    public static readonly XNumber deviation = create(0, 001);
    public static readonly XNumber NegativeInfinity = zero;
    public static readonly XNumber hundred = 100;

    public int raw;

    public int ceiling
    {
        get
        {
            XNumber r;
            r.raw = (raw + FRACTION_MASK) & INTEGER_MASK;

            return (int)r;
        }
    }

    public int floor
    {
        get
        {
            XNumber r;
            r.raw = raw & INTEGER_MASK;

            return (int)r;
        }
    }

    public int round
    {
        get
        {
            XNumber r;
            r.raw = (raw + half.raw) & INTEGER_MASK;
            return (int)r;
        }
    }

    public static XNumber create(int i, int f)
    {
#if XNUMBER_CHECK
        if (i > 2000000 || i < -2000000 || f > 999 || f < -999)
            Debug.LogError("Xnumber 创建失败！ " + i + "." + f);
#endif

        var sign = (i ^ f) >= 0 ? 1 : -1;

        if (i < 0) i = -i;
        if (f < 0) f = -f;

        i = i << FRACTION_BITS;
        f = (f << FRACTION_BITS) / 1000;

        XNumber ret;
        ret.raw = sign * (i + f);
        return ret;
    }

    public static XNumber create_row(int i)
    {
        XNumber ret;
        ret.raw = i;
        return ret;
    }

    public XNumber(int i, int f)
    {
#if XNUMBER_CHECK
        if (i > 2000000 || i < -2000000 || f > 999 || f < -999)
            Debug.LogError("Xnumber 创建失败！ " + i + "." + f);
#endif
        var sign = (i ^ f) >= 0 ? 1 : -1;

        if (i < 0) i = -i;
        if (f < 0) f = -f;

        i = i << FRACTION_BITS;
        f = (f << FRACTION_BITS) / 1000;
        raw = sign * (i + f);
    }

    // 一元操作符 -
    public static XNumber operator -(XNumber x)
    {
        XNumber r;
        r.raw = -x.raw;
        return r;
    }

    // 二元操作符 +
    public static XNumber operator +(XNumber lhs, XNumber rhs)
    {
        XNumber r;
        r.raw = lhs.raw + rhs.raw;
        return r;
    }

    // 二元操作符 -
    public static XNumber operator -(XNumber lhs, XNumber rhs)
    {
        XNumber r;
        r.raw = lhs.raw - rhs.raw;
        return r;
    }

    // 二元操作符 *
    public static XNumber operator *(XNumber lhs, XNumber rhs)
    {
#if XNUMBER_CHECK
        var tmp = (int)lhs * (int)rhs;
        if (tmp > 2097152 || tmp < -2097152)
            Debug.LogError("Number数据超上限了 " + lhs + " * " + rhs);
#endif

        XNumber r;
        r.raw = (int)(((long)lhs.raw * rhs.raw + (FRACTION_RANGE >> 1)) >> FRACTION_BITS);
        return r;
    }

    // 二元操作符 /
    public static XNumber operator /(XNumber lhs, XNumber rhs)
    {
        if (lhs.raw == 0)
        {
            return 0;
        }
        var factor = 1;
        if (rhs.raw < 0)
            factor = -1;

        if ((rhs.raw + factor) >> 1 == 0)
        {
            //Debug.LogError("除0了");
            return 0;
        }

        XNumber r;
        r.raw = (int)((((long)lhs.raw << (FRACTION_BITS + 1)) / (long)rhs.raw + factor) >> 1);
        return r;
    }

    // 二元操作符 %
    public static XNumber operator %(XNumber lhs, XNumber rhs)
    {
        XNumber r;
        r.raw = lhs.raw % rhs.raw;
        return r;
    }

    // int类型转换
    public static explicit operator int(XNumber number)
    {
        if (number.raw > 0)
            return number.raw >> FRACTION_BITS;
        else
            return (number.raw + FRACTION_MASK) >> FRACTION_BITS;
    }

    public static implicit operator float(XNumber number)
    {
        return (float)(double)number;
    }

    public static implicit operator XNumber(float number)
    {
#if XNUMBER_CHECK
        var tmp = (int)number;
        if (tmp > 2097152 || tmp < -2097152)
            Debug.LogError("Number数据超上限了 " + number);
#endif
        return XIntMathTools.Convert(number);
    }

    // double类型转换
    public static explicit operator double(XNumber number)
    {
        return (number.raw >> FRACTION_BITS) + (number.raw & FRACTION_MASK) / (double)FRACTION_RANGE;
    }

    // Number类型转换
    public static implicit operator XNumber(int value)
    {
#if XNUMBER_CHECK
        var tmp = value;
        if (tmp > 2097152 || tmp < -2097152)
            Debug.LogError("Number数据超上限了 " + value);
#endif

        XNumber r;
        r.raw = value << FRACTION_BITS;
        return r;
    }

    // Number类型转换
    public static implicit operator XNumber(uint value)
    {
#if XNUMBER_CHECK
        var tmp = value;
        if (tmp > 2097152 || tmp < -2097152)
            Debug.LogError("Number数据超上限了 " + value);
#endif
        return (int)value;
    }

    public int CompareTo(XNumber other)
    {
        return CompareTo(other.raw);
    }

    private int CompareTo(int other)
    {
        return raw.CompareTo(other);
    }

    // 比较运算符 <
    public static bool operator <(XNumber lhs, XNumber rhs)
    {
        return lhs.raw < rhs.raw;
    }

    // 比较运算符 <=
    public static bool operator <=(XNumber lhs, XNumber rhs)
    {
        return lhs.raw <= rhs.raw;
    }

    // 比较运算符 >
    public static bool operator >(XNumber lhs, XNumber rhs)
    {
        return lhs.raw > rhs.raw;
    }

    // 比较运算符 >=
    public static bool operator >=(XNumber lhs, XNumber rhs)
    {
        return lhs.raw >= rhs.raw;
    }

    // 比较运算符 ==
    public static bool operator ==(XNumber lhs, XNumber rhs)
    {
        return lhs.raw == rhs.raw;
    }

    // 比较运算符 !=
    public static bool operator !=(XNumber lhs, XNumber rhs)
    {
        return lhs.raw != rhs.raw;
    }

    public bool Equals(XNumber other)
    {
        return raw == other.raw;
    }

    public override bool Equals(object obj)
    {
        return (obj is XNumber && ((XNumber)obj) == this);
    }

    public static bool Approximately(XNumber lhs, XNumber rhs)
    {
        return Approximately(lhs, rhs, deviation);
    }

    public static bool Approximately(XNumber lhs, XNumber rhs, XNumber dev)
    {
        return XIntMath.Abs(lhs.raw - rhs.raw) <= dev.raw;
    }

    public override int GetHashCode()
    {
        return raw.GetHashCode();
    }

    public XNumber Deg2Rad
    {
        get
        {
            return XIntMath.Deg2Rad(this);
        }
    }

    public XNumber Rad2Deg
    {
        get
        {
            return XIntMath.Rad2Deg(this);
        }
    }

    // ps 仅用在调用非常频繁的地方比较好 因为有使用值域限制
    public static void xlerp(ref XNumber from, ref XNumber to, ref XNumber t, out XNumber ret)
    {
        // todo 做一些容错检查
        ret.raw = from.raw + ((to.raw - from.raw) * t.raw >> FRACTION_BITS);
    }

    #region for text

    public override string ToString()
    {
        return ((float)this).ToString("f3");
    }

    public string ToString(string format)
    {
        return ((float)this).ToString(format);
    }

    private static readonly long[] transTable =
    {
        5000000000L,
        2500000000L,
        1250000000L,
        0625000000L,
        0312500000L,
        0156250000L,
        0078125000L,
        0039062500L,
        0019531250L,
        0009765625L,
    };

    public static bool TryParse(string text, out XNumber result)
    {
        result = XNumber.zero;
        text = text.Trim();
        XNumber sign = XNumber.one;
        if (text.StartsWith("-"))
        {
            sign = -XNumber.one;
            text = text.Substring(1);
        }
        string[] tokens = text.Split('.');
        if (tokens.Length == 1)
        {
            int i = 0;
            if (int.TryParse(tokens[0], out i))
            {
                result = (XNumber)i * sign;
                return true;
            }
        }
        else if (tokens.Length == 2)
        {
            int i_part = 0;
            if (!int.TryParse(tokens[0], out i_part))
                return false;

            string strFrac = tokens[1];
            strFrac = strFrac.PadRight(FRACTION_BITS, '0');

            long decFrac;
            if (long.TryParse(strFrac, out decFrac))
            {
                int frac = ParseFracPart(decFrac);
                result = create_row(i_part << FRACTION_BITS | frac) * sign;
                return true;
            }
            else
                return false;
        }
        return false;
    }

    private static int ParseFracPart(long dec)
    {
        int frac = 0;
        for (int i = 0; i < 10; ++i)
        {
            long t = transTable[i];
            long d = dec / t;
            long mod = dec % t;
            if (d > 0)
                frac = frac | (1 << (FRACTION_BITS - i - 1));
            dec = mod;
        }
        return frac;
    }

    public static XNumber Parse(string text)
    {
        XNumber result;
        bool b = TryParse(text, out result);
        if (b)
            return result;
        else
            throw new FormatException("Number.Parse: Illegal format" + text);
    }

    public XNumber(SerializationInfo info, StreamingContext context)
    {
        raw = (int)info.GetValue("raw", typeof(int));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("raw", raw, typeof(int));
    }

    #endregion for text
}