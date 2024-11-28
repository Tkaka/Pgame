/*
 * File Name:               XIntMath.cs
 *
 * Description:             整型基础数学库
 * Author:                  zhangwei
 * Create Date:             2016/12/30
 */

using UnityEngine;

public static class XIntMath
{
    public const int DOUBLE_FACTOR = 20; //位移
    public const int Muti_DOUBLE_FACTOR = 1048576; //1024*1024
    public const int FACTOR = 10;
    public const int Muti_FACTOR = 1024;

    //public const int SQR_FACTOR = 1024 * 1024;
    public const int SQR_FACTOR = 1048576; //1024*1024

    //public static readonly Number PI = new Number(3, 142);     //3.14159265358979323846264338327950288f
    public static readonly XNumber PI = XNumber.create_row(3217); //3.14159265358979323846264338327950288f

    //public static readonly Number TWO_PI = new Number(6, 283); //6.28318530717958647692528676655900576f
    public static readonly XNumber TWO_PI = XNumber.create_row(6434); //6.28318530717958647692528676655900576f

    //public static readonly Number ROOT_PI = new Number(1, 772);//1.772453850905516027f
    public static readonly XNumber ROOT_PI = XNumber.create_row(1815); //1.772453850905516027f

    //public static readonly Number HALF_PI = new Number(1, 571);//1.57079632679489661923132169163975144f
    public static readonly XNumber HALF_PI = XNumber.create_row(1608);//1.57079632679489661923132169163975144f

    public static readonly XNumber QTR_CIRCLE = 90;
    public static readonly XNumber HALF_CIRCLE = 180;
    public static readonly XNumber CIRCLE = 360;
    public static readonly XNumber MIN_LENGTH = -22;
    public static readonly XNumber MAX_LENGTH = 22;
    public static readonly XNumber MIN_ANGLE = -TWO_PI * 128;
    public static readonly XNumber MAX_ANGLE = TWO_PI * 128;

    public static XNumber Deg2Rad(XNumber degrees)
    {
        XNumber r;
        r.raw = (int)(((long)degrees.raw * 73205) >> 22);
        // r.raw = (int)((long)degrees.raw * PI.raw / 180 / 1024);

#if XNUMBER_CHECK
        Check_Number(r, Mathf.Deg2Rad * degrees);
#endif
        return r;
    }

    public static XNumber Rad2Deg(XNumber radians)
    {
        if (radians < XNumber.zero)
        {
            radians += TWO_PI;
            // Debug.LogError(" XNumber Rad2Deg(XNumber radians) error");
        }
        XNumber r;
        r.raw = (int)(((long)radians.raw * 60078979) >> 20);
        // r.raw = (int)((long)radians.raw * 180 * 1024 / PI.raw);

#if XNUMBER_CHECK
        Check_Number(r, Mathf.Rad2Deg * radians);
#endif
        return r;
    }

    public static XNumber Min(XNumber x, XNumber y)
    {
        return y < x ? y : x;
    }

    public static long Min(long x, long y)
    {
        return y < x ? y : x;
    }

    public static int Min(int a, int b)
    {
        return a <= b ? a : b;
    }

    public static XNumber Max(XNumber x, XNumber y)
    {
        return y > x ? y : x;
    }

    public static int Max(int x, int y)
    {
        return y > x ? y : x;
    }

    public static long Max(long x, long y)
    {
        return y > x ? y : x;
    }

    internal static int Clamp(int x, int min, int max)
    {
        var ret = x;
        if (x < min)
            ret = min;
        else if (x > max)
            ret = max;

        return ret;
    }

    public static long Clamp(long x, long min, long max)
    {
        var ret = x;
        if (x < min)
            ret = min;
        else if (x > max)
            ret = max;

        return ret;
    }

    public static XNumber Clamp(XNumber x, XNumber min, XNumber max)
    {
        if (min > max)
        {
            var temp = min;
            min = max;
            max = temp;
        }

        if (x.raw < min.raw)
            return min;
        else if (x.raw > max.raw)
            return max;
        else
            return x;
    }

    public static XNumber Clamp01(XNumber x)
    {
        return Clamp(x, XNumber.zero, XNumber.one);
    }


    public static XVector2 Lerp(XVector2 from, XVector2 to, XNumber t)
    {
        t = Clamp(t, XNumber.zero, XNumber.one);
        return from * (XNumber.one - t) + to * t;
    }

    public static XNumber Lerp(XNumber from, XNumber to, XNumber t)
    {
        t = Clamp(t, XNumber.zero, XNumber.one);
        XNumber n = from * (XNumber.one - t) + to * t;

#if XNUMBER_CHECK
        Check_Number(n, Mathf.Lerp(from, to, t));
#endif
        return n;
    }

    public static XNumber InverseLerp(XNumber a, XNumber b, XNumber value)
    {
        if (a != b)
        {
            return Clamp01((value - a) / (b - a));
        }
        return XNumber.zero;
    }

    public static XNumber Abs(XNumber x)
    {
        return x.raw < 0 ? -x : x;
    }

    public static int Abs(int x)
    {
        return x < 0 ? -x : x;
    }

    public static XNumber Cos(XNumber radians)
    {
        XNumber result = fastCos(radians);

#if XNUMBER_CHECK
        Check_Number(result, Mathf.Cos(radians));
#endif
        return result;
    }

    public static int Cos(int radians)
    {
        return fastCos(radians);
    }

    public static XNumber Sin(XNumber radians)
    {
        XNumber result = fastSin(radians);

#if XNUMBER_CHECK
        Check_Number(result, Mathf.Sin(radians));
#endif
        return result;
    }

    public static int Sin(int radians)
    {
        return fastCos(radians - HALF_PI.raw);
    }

    public static XNumber Asin(XNumber x)
    {
        XNumber result = fastAsin(x);

#if XNUMBER_CHECK
        Check_Number(result, Mathf.Asin(x));
#endif
        return result;
    }

    public static XNumber Acos(XNumber x)
    {
        XNumber result = HALF_PI - fastAsin(x);

#if XNUMBER_CHECK
        Check_Number(result, Mathf.Acos(Mathf.Clamp01(x)));
#endif
        return result;
    }

    public static XNumber Atan(XNumber x)
    {
        XNumber absX = Abs(x);
        XNumber rad;
        if (absX <= XNumber.one)
        {
            rad.raw = XTriangleTable.atanTable[absX.raw];
        }
        else
        {
            absX = XNumber.one / absX;
            rad.raw = HALF_PI.raw - XTriangleTable.atanTable[absX.raw];
        }
        if (x < XNumber.zero)
        {
            rad.raw = -rad.raw;
        }

#if XNUMBER_CHECK
        Check_Number(rad, Mathf.Atan(x));
#endif
        return rad;
    }

    //    public static XNumber Atan2(XNumber y, XNumber x)
    //    {
    //        XNumber result;

    //        if (y == XNumber.zero)
    //        {
    //            result = x < 0 ? (Abs(x) <= XNumber.deviation ? 0 : PI) : 0;
    //        }
    //        else if (x == XNumber.zero)
    //        {
    //            result = HALF_PI * Sign(y);
    //        }
    //        else
    //        {
    //            XNumber absX = Abs(x);
    //            XNumber absY = Abs(y);
    //            XNumber sign = Sign(y) * Sign(x);
    //            if (absX > absY)
    //            {
    //                result = Atan(absY / absX) * sign;
    //            }
    //            else
    //            {
    //                result = (HALF_PI - Atan(absX / absY)) * sign;
    //            }
    //            if (y < XNumber.zero && x < XNumber.zero)
    //                result -= PI;
    //            else if (y >= XNumber.zero && x < XNumber.zero)
    //                result += PI;
    //        }
    //#if XNUMBER_CHECK
    //        Check_Number(result, Mathf.Atan2(y, x));
    //#endif
    //        return result;
    //    }

    internal static int fastCos(int radians)
    {
        int angle = Abs(radians % TWO_PI.raw);

        int ret;
        if (angle < PI.raw)
            ret = tempCos(angle); // XTriangleTable.cosTable[angle];
        else
            ret = tempCos(TWO_PI.raw - angle); // -XTriangleTable.cosTable[angle - PI.raw];

        return ret;
    }

    private static int tempCos(int angle)
    {
        if (angle == HALF_PI.raw) return 0;

        if (angle < HALF_PI.raw)
        {
            return XTriangleTable.cosTable[angle];
        }
        else
        {
            return -XTriangleTable.cosTable[PI.raw - angle];
        }
    }

    private static XNumber fastCos(XNumber radians)
    {
        XNumber r;
        r.raw = fastCos(radians.raw);
        return r;
    }

    private static XNumber fastSin(XNumber radians)
    {
        XNumber r;
        r.raw = fastCos(radians.raw - HALF_PI.raw);
        return r;
    }

    internal static int fastAsin(int x)
    {
        int sign = x > 0 ? 1 : -1;
        x = Clamp(Abs(x), XNumber.zero.raw, XNumber.one.raw);
        int result = XTriangleTable.asinTable[x];
        result = sign * result;
        return result;
    }

    private static XNumber fastAsin(XNumber x)
    {
        XNumber r;
        r.raw = fastAsin(x.raw);
        return r;
    }

    private static int[] sqrtTable = {
            0,    16,  22,  27,  32,  35,  39,  42,  45,  48,  50,  53,  55,  57,
            59,   61,  64,  65,  67,  69,  71,  73,  75,  76,  78,  80,  81,  83,
            84,   86,  87,  89,  90,  91,  93,  94,  96,  97,  98,  99, 101, 102,
            103, 104, 106, 107, 108, 109, 110, 112, 113, 114, 115, 116, 117, 118,
            119, 120, 121, 122, 123, 124, 125, 126, 128, 128, 129, 130, 131, 132,
            133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 144, 145,
            146, 147, 148, 149, 150, 150, 151, 152, 153, 154, 155, 155, 156, 157,
            158, 159, 160, 160, 161, 162, 163, 163, 164, 165, 166, 167, 167, 168,
            169, 170, 170, 171, 172, 173, 173, 174, 175, 176, 176, 177, 178, 178,
            179, 180, 181, 181, 182, 183, 183, 184, 185, 185, 186, 187, 187, 188,
            189, 189, 190, 191, 192, 192, 193, 193, 194, 195, 195, 196, 197, 197,
            198, 199, 199, 200, 201, 201, 202, 203, 203, 204, 204, 205, 206, 206,
            207, 208, 208, 209, 209, 210, 211, 211, 212, 212, 213, 214, 214, 215,
            215, 216, 217, 217, 218, 218, 219, 219, 220, 221, 221, 222, 222, 223,
            224, 224, 225, 225, 226, 226, 227, 227, 228, 229, 229, 230, 230, 231,
            231, 232, 232, 233, 234, 234, 235, 235, 236, 236, 237, 237, 238, 238,
            239, 240, 240, 241, 241, 242, 242, 243, 243, 244, 244, 245, 245, 246,
            246, 247, 247, 248, 248, 249, 249, 250, 250, 251, 251, 252, 252, 253,
            253, 254, 254, 255
        };

    public static XNumber Sqrt(XNumber n)
    {
        if (n < 0)
        {
            Debug.LogError(" Argument of Sqrt shouldn't be negative. " + n);
            return 0;
        }

        if (n.raw == 0)
            return XNumber.zero;

        XNumber r;
        r.raw = Sqrt_Int(n.raw) << 5;

#if XNUMBER_CHECK
        Check_Number(r, Mathf.Sqrt(n));
#endif

        return r;
    }

    public static int Sqrt_Long(long x)
    {
        //// todo 
        //return (int)Mathf.Sqrt(x);

        if (x < 0)
        {
#if UNITY_EDITOR && !ENABLE_DEEP_PROFILER
            throw new System.ArgumentException("Argument of Sqrt shouldn't be negative." + x);
#endif
            return 0;
        }

        int ret;
        if ((x >> 31) == 0)
        {
            ret = Sqrt_Int((int)x);
        }
        else
        {
            long t;
            long n = 0;
            long b = 0x80000000;
            int s = 31;
            do
            {
                if (x >= (t = (((n << 1) + b) << s--)))
                {
                    n += b;
                    x -= t;
                }
            }
            while ((b >>= 1) != 0);
            ret = (int)n;
        }

#if XNUMBER_CHECK
        Check_Number(ret, Mathf.Sqrt(x));
#endif
        return ret;
    }

    public static int Sqrt_Int(int n)
    {
        int result = -1;
        if (n < 0)
        {
#if UNITY_EDITOR && !ENABLE_DEEP_PROFILER
            throw new System.ArgumentException("Argument of Sqrt shouldn't be negative." + n);
#endif
            return 0;
        }

        int xn;
        if (n >= 0x7FFEA810) result = 0xB504;
        if (n >= 0x10000)
        {
            if (n >= 0x1000000)
            {
                if (n >= 0x10000000)
                {
                    if (n >= 0x40000000)
                    {
                        xn = sqrtTable[n >> 24] << 8;
                    }
                    else
                    {
                        xn = sqrtTable[n >> 22] << 7;
                    }
                }
                else
                {
                    if (n >= 0x4000000)
                    {
                        xn = sqrtTable[n >> 20] << 6;
                    }
                    else
                    {
                        xn = sqrtTable[n >> 18] << 5;
                    }
                }

                xn = (xn + 1 + (n / xn)) >> 1;
                xn = (xn + 1 + (n / xn)) >> 1;
                result = ((xn * xn) > n) ? --xn : xn;
            }
            else
            {
                if (n >= 0x100000)
                {
                    if (n >= 0x400000)
                    {
                        xn = sqrtTable[n >> 16] << 4;
                    }
                    else
                    {
                        xn = sqrtTable[n >> 14] << 3;
                    }
                }
                else
                {
                    if (n >= 0x40000)
                    {
                        xn = sqrtTable[n >> 12] << 2;
                    }
                    else
                    {
                        xn = sqrtTable[n >> 10] << 1;
                    }
                }

                xn = (xn + 1 + (n / xn)) >> 1;
                //return ((xn * xn) > n) ? --xn : xn;
                result = ((xn * xn) > n) ? --xn : xn;
            }
        }
        else
        {
            if (n >= 0x100)
            {
                if (n >= 0x1000)
                {
                    if (n >= 0x4000)
                    {
                        xn = (sqrtTable[n >> 8]) + 1;
                    }
                    else
                    {
                        xn = (sqrtTable[n >> 6] >> 1) + 1;
                    }
                }
                else
                {
                    if (n >= 0x400)
                    {
                        xn = (sqrtTable[n >> 4] >> 2) + 1;
                    }
                    else
                    {
                        xn = (sqrtTable[n >> 2] >> 3) + 1;
                    }
                }
                result = ((xn * xn) > n) ? --xn : xn;
            }
            else
            {
                if (n >= 0)
                {
                    result = sqrtTable[n] >> 4;
                }
            }
        }

#if XNUMBER_CHECK
        Check_Number(result, Mathf.Sqrt(n));
#endif

        return result;
    }

    public static bool Approximately(XNumber a, XNumber b)
    {
        return XNumber.Approximately(a, b);
    }

    public static int FloorToInt(XNumber a)
    {
        return a.floor;
    }

    public static int CeilToInt(XNumber a)
    {
        return a.ceiling;
    }

    public static int RoundToInt(XNumber a)
    {
        return a.round;
    }

    public static XNumber Power(XNumber x, int power)
    {
        var ret = XNumber.one;
        int cout = Abs(power);
        for (int i = 0; i < cout; i++)
        {
            ret *= x;
        }

        return power < 0 ? 1 / ret : ret;
    }

#if XNUMBER_CHECK
    public static void Check_Number(XNumber lhs, XNumber rhs)
    {
        if ((lhs.raw < 0 && rhs.raw > 0)
            || (lhs.raw > 0 && rhs.raw < 0))
        {
            Debug.LogError("XNumber num 对比失败 " + lhs + " != " + rhs);
            return;
        }

        var d = Mathf.Abs(lhs.raw - rhs.raw);
        if (d <= Mathf.Abs(lhs.raw / 10f)
            && d <= Mathf.Abs(rhs.raw / 10f))
            return;

        if (XNumber.Approximately(lhs, rhs, XNumber.create_row(10)))
            return;

        Debug.LogError("XNumber num 对比失败 " + lhs + " != " + rhs);
    }

    public static void Check_Vector3(XVector3 lhs, Vector3 rhs)
    {
        Check_Number(lhs.x, rhs.x);
        Check_Number(lhs.y, rhs.y);
        Check_Number(lhs.z, rhs.z);
    }

    public static void Check_Quaternion(XQuaternion lhs, Quaternion rhs)
    {
        Check_Number(lhs.x, rhs.x);
        Check_Number(lhs.y, rhs.y);
        Check_Number(lhs.z, rhs.z);
        Check_Number(lhs.w, rhs.w);
    }
#endif
}