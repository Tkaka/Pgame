/*
 * File Name:               XQuaternion.cs
 *
 * Description:             整型基础数学库
 * Author:                  zhangwei
 * Create Date:             2016/12/30
 */

//using UE = UnityEngine;

using UnityEngine;

public struct XQuaternion
{
    public XNumber x;
    public XNumber y;
    public XNumber z;
    public XNumber w;

    private const int FRACTION_BITS = XNumber.FRACTION_BITS;                                      // 小数位位数 10
    private const int FRACTION_RANGE = XNumber.FRACTION_RANGE;                                    // 1024 == 1000000000

    public static implicit operator Quaternion(XQuaternion lhs)
    {
        return new Quaternion((float)lhs.x, (float)lhs.y, (float)lhs.z, (float)lhs.w);
    }

    public static explicit operator XQuaternion(Quaternion lhs)
    {
        return XIntMathTools.Convert(lhs);
    }

    public static XQuaternion identity = new XQuaternion(XNumber.zero, XNumber.zero, XNumber.zero, XNumber.one);

//    public XVector3 eulerAngles
//    {
//        get
//        {
//            var ret = normalized.ToMatrix3x3().ToEuler().Rad2Deg;

//            ret = Internal_MakePositive(ret);

//#if XNUMBER_CHECK
//            // XIntMath.Check_Vector3(ret, mTemp.eulerAngles);
//#endif
//            return ret;
//        }
//    }

    public long sqrMagnitude
    {
        get
        {
            return (long)x.raw * x.raw + (long)y.raw * y.raw + (long)z.raw * z.raw + (long)w.raw * w.raw;
        }
    }

    public XNumber magnitude
    {
        get
        {
            XNumber r;
            r.raw = XIntMath.Sqrt_Long(sqrMagnitude);
            return r;
        }
    }

    public XQuaternion normalized
    {
        get
        {
            long sqrMag = sqrMagnitude;
            if (sqrMag <= 0)
                return identity;

            XNumber nx;
            nx.raw = XIntMath.Sqrt_Long((long)x.raw * x.raw * XIntMath.SQR_FACTOR / sqrMag) * (x.raw > 0 ? 1 : -1);
            XNumber ny;
            ny.raw = XIntMath.Sqrt_Long((long)y.raw * y.raw * XIntMath.SQR_FACTOR / sqrMag) * (y.raw > 0 ? 1 : -1);
            XNumber nz;
            nz.raw = XIntMath.Sqrt_Long((long)z.raw * z.raw * XIntMath.SQR_FACTOR / sqrMag) * (z.raw > 0 ? 1 : -1);
            XNumber nw;
            nw.raw = XIntMath.Sqrt_Long((long)w.raw * w.raw * XIntMath.SQR_FACTOR / sqrMag) * (w.raw > 0 ? 1 : -1);

            return new XQuaternion(nx, ny, nz, nw);
        }
    }

    public XQuaternion(XNumber x, XNumber y, XNumber z, XNumber w)
    {
        this.x.raw = x.raw;
        this.y.raw = y.raw;
        this.z.raw = z.raw;
        this.w.raw = w.raw;
    }

    //public static XQuaternion operator /(XQuaternion lhs, XNumber rhs)
    //{
    //    return new XQuaternion(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
    //}

    //public static XQuaternion operator *(XQuaternion lhs, XNumber rhs)
    //{
    //    return new XQuaternion(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
    //}

    public static XVector3 operator *(XQuaternion rotation, XVector3 point)
    {
        XVector3 vector;

        int num = rotation.x.raw * 2;
        int num2 = rotation.y.raw * 2;
        int num3 = rotation.z.raw * 2;
        int num4 = (rotation.x.raw * num) >> FRACTION_BITS;
        int num5 = (rotation.y.raw * num2) >> FRACTION_BITS;
        int num6 = (rotation.z.raw * num3) >> FRACTION_BITS;
        int num7 = (rotation.x.raw * num2) >> FRACTION_BITS;
        int num8 = (rotation.x.raw * num3) >> FRACTION_BITS;
        int num9 = (rotation.y.raw * num3) >> FRACTION_BITS;
        int num10 = (rotation.w.raw * num) >> FRACTION_BITS;
        int num11 = (rotation.w.raw * num2) >> FRACTION_BITS;
        int num12 = (rotation.w.raw * num3) >> FRACTION_BITS;
        vector.x.raw = (((FRACTION_RANGE - (num5 + num6)) * point.x.raw) >> FRACTION_BITS) + (((num7 - num12) * point.y.raw) >> FRACTION_BITS) + (((num8 + num11) * point.z.raw) >> FRACTION_BITS);
        vector.y.raw = (((num7 + num12) * point.x.raw) >> FRACTION_BITS) + (((FRACTION_RANGE - (num4 + num6)) * point.y.raw) >> FRACTION_BITS) + (((num9 - num10) * point.z.raw) >> FRACTION_BITS);
        vector.z.raw = (((num8 - num11) * point.x.raw) >> FRACTION_BITS) + (((num9 + num10) * point.y.raw) >> FRACTION_BITS) + (((FRACTION_RANGE - (num4 + num5)) * point.z.raw) >> FRACTION_BITS);

#if XNUMBER_CHECK
        XIntMath.Check_Vector3(vector, (Quaternion)rotation * (Vector3)point);
#endif

        return vector;
    }

    public static XQuaternion operator *(XQuaternion lhs, XQuaternion rhs)
    {
        long w = ((long)lhs.w.raw * rhs.w.raw - (long)lhs.x.raw * rhs.x.raw - (long)lhs.y.raw * rhs.y.raw - (long)lhs.z.raw * rhs.z.raw) >> XIntMath.FACTOR;
        long x = ((long)lhs.w.raw * rhs.x.raw + (long)lhs.x.raw * rhs.w.raw + (long)lhs.y.raw * rhs.z.raw - (long)lhs.z.raw * rhs.y.raw) >> XIntMath.FACTOR;
        long y = ((long)lhs.w.raw * rhs.y.raw + (long)lhs.y.raw * rhs.w.raw + (long)lhs.z.raw * rhs.x.raw - (long)lhs.x.raw * rhs.z.raw) >> XIntMath.FACTOR;
        long z = ((long)lhs.w.raw * rhs.z.raw + (long)lhs.z.raw * rhs.w.raw + (long)lhs.x.raw * rhs.y.raw - (long)lhs.y.raw * rhs.x.raw) >> XIntMath.FACTOR;
        //return new Quaternion(new Number((int)x), new Number((int)y), new Number((int)z), new Number((int)w));

        XQuaternion ret;
        ret.x.raw = (int)x;
        ret.y.raw = (int)y;
        ret.z.raw = (int)z;
        ret.w.raw = (int)w;

#if XNUMBER_CHECK
        XIntMath.Check_Quaternion(ret, (Quaternion)lhs * (Quaternion)rhs);
#endif

        return ret;
    }

    public static XQuaternion Euler(XNumber x, XNumber y, XNumber z)
    {
        int eulerX = (int)(((long)x.raw * 73205) >> 22) >> 1;
        int eulerY = (int)(((long)y.raw * 73205) >> 22) >> 1;
        int eulerZ = (int)(((long)z.raw * 73205) >> 22) >> 1;

        int cosX = XIntMath.Cos(eulerX);
        int sinX = XIntMath.Sin(eulerX);

        int cosY = XIntMath.Cos(eulerY);
        int sinY = XIntMath.Sin(eulerY);

        int cosZ = XIntMath.Cos(eulerZ);
        int sinZ = XIntMath.Sin(eulerZ);

        XQuaternion ret;
        ret.x.raw = (int)(((long)sinX * cosY * cosZ + (long)cosX * sinY * sinZ) >> XIntMath.DOUBLE_FACTOR);
        ret.y.raw = (int)(((long)cosX * sinY * cosZ - (long)sinX * cosY * sinZ) >> XIntMath.DOUBLE_FACTOR);
        ret.z.raw = (int)(((long)cosX * cosY * sinZ - (long)sinX * sinY * cosZ) >> XIntMath.DOUBLE_FACTOR);
        ret.w.raw = (int)(((long)cosX * cosY * cosZ + (long)sinX * sinY * sinZ) >> XIntMath.DOUBLE_FACTOR);

#if XNUMBER_CHECK
        XIntMath.Check_Quaternion(ret, Quaternion.Euler(x, y, z));
#endif
        return ret.normalized;
    }

    public static XQuaternion Euler(XVector3 euler)
    {
        var ret = Euler(euler.x, euler.y, euler.z);

#if XNUMBER_CHECK
        XIntMath.Check_Quaternion(ret, Quaternion.Euler(euler));
#endif

        return ret;
    }

    public static XQuaternion AngleAxis(XNumber angle, XVector3 axis)
    {
        XQuaternion quaternion;
        int rad = (int)(((long)angle.raw * 73205) >> 22);
        int mag = axis.magnitude.raw;
        if (mag > XNumber.epsilon)
        {
            int halfRad = rad * XNumber.half.raw >> FRACTION_BITS;
            var sin = XIntMath.Sin(halfRad);

            int s = (int)((((long)sin << (FRACTION_BITS + 1)) / (long)mag + 1) >> 1);
            quaternion.x.raw = axis.x.raw * s >> FRACTION_BITS;
            quaternion.y.raw = axis.y.raw * s >> FRACTION_BITS;
            quaternion.z.raw = axis.z.raw * s >> FRACTION_BITS;
            quaternion.w.raw = XIntMath.Cos(halfRad);
        }
        else
        {
            return identity;
        }


#if XNUMBER_CHECK
        XIntMath.Check_Quaternion(quaternion.normalized, Quaternion.AngleAxis(angle, axis));
#endif

        return quaternion;
    }

//    public static XQuaternion LookRotation(XVector3 forward)
//    {
//        var ret = LookRotation(forward, XVector3.up);

////#if XNUMBER_CHECK
////        var unity = Quaternion.LookRotation(forward);
////        XIntMath.Check_Quaternion(ret, unity);
////#endif
//        return ret;
//    }

    //public static XQuaternion LookRotation(XVector3 forward, XVector3 up)
    //{
    //    XQuaternion ret = identity;
    //    if(!LookRotationToQuaternion(forward, up, ref ret))
    //    {
    //        XNumber mag = forward.sqrMagnitude;
    //        if(mag > XNumber.epsilon)
    //        {
    //            var m = new XMatrix3x3();
    //            m.SetFromToRotation(XVector3.forward, forward.normalized);
    //            ret = m.MatrixToQuaternion();
    //        }
    //        else
    //        {
    //            Debug.LogError("Look rotation viewing vector is zero");
    //        }
    //    }

    //    return ret;

    //}

    //private static bool LookRotationToQuaternion(XVector3 viewVec, XVector3 upVec, ref XQuaternion res)
    //{
    //    var m = new XMatrix3x3();
    //    if (!XMatrix3x3.LookRotationToMatrix(viewVec, upVec, ref m))
    //        return false;

    //    res = m.MatrixToQuaternion();
    //    return true;
    //}

    public override string ToString()
    {
        return string.Format("({0}, {1}, {2}, {3})", x, y, z, w);
    }

    public static XQuaternion Inverse(XQuaternion src)
    {
        var ret = src;
        ret.x = -src.x;
        ret.y = -src.y;
        ret.z = -src.z;
        ret.w = src.w;

        ret = ret.normalized;

#if XNUMBER_CHECK
        XIntMath.Check_Quaternion(ret, Quaternion.Inverse(src));
#endif

        return ret;
    }

    public static XQuaternion Lerp(XQuaternion q1, XQuaternion q2, XNumber t)
    {
        if (t == XNumber.zero)
            return q1;

        if (q1 == q2)
            return q1;

        XQuaternion ret;
        if (Dot(q1, q2) < 0)
        {
            ret = new XQuaternion(
                q1.x + t * (-q2.x - q1.x),
                q1.y + t * (-q2.y - q1.y),
                q1.z + t * (-q2.z - q1.z),
                q1.w + t * (-q2.w - q1.w));
        }
        else
        {
            ret = new XQuaternion(
                q1.x + t * (q2.x - q1.x),
                q1.y + t * (q2.y - q1.y),
                q1.z + t * (q2.z - q1.z),
                q1.w + t * (q2.w - q1.w));
        }

        ret = ret.normalized;
#if XNUMBER_CHECK
        XIntMath.Check_Quaternion(ret, Quaternion.Lerp(q1, q2, t));
#endif

        return ret;
    }

    // 比较运算符 ==
    public static bool operator ==(XQuaternion lhs, XQuaternion rhs)
    {
        return lhs.x.raw == rhs.x.raw && lhs.y.raw == rhs.y.raw && lhs.z.raw == rhs.z.raw && lhs.w.raw == rhs.w.raw;
    }

    // 比较运算符 !=
    public static bool operator !=(XQuaternion lhs, XQuaternion rhs)
    {
        return lhs.x.raw != rhs.x.raw || lhs.y.raw != rhs.y.raw || lhs.z.raw != rhs.z.raw || lhs.w.raw != rhs.w.raw;
    }

    //private XMatrix3x3 ToMatrix3x3()
    //{
    //    // Precalculate coordinate products
    //    XNumber x = this.x * 2;
    //    XNumber y = this.y * 2;
    //    XNumber z = this.z * 2;
    //    XNumber xx = this.x * x;
    //    XNumber yy = this.y * y;
    //    XNumber zz = this.z * z;
    //    XNumber xy = this.x * y;
    //    XNumber xz = this.x * z;
    //    XNumber yz = this.y * z;
    //    XNumber wx = this.w * x;
    //    XNumber wy = this.w * y;
    //    XNumber wz = this.w * z;

    //    // Calculate 3x3 matrix from orthonormal basis
    //    var ret = new XMatrix3x3();
    //    ret.a11 = 1 - (yy + zz);
    //    ret.a21 = xy + wz;
    //    ret.a31 = xz - wy;

    //    ret.a12 = xy - wz;
    //    ret.a22 = 1 - (xx + zz);
    //    ret.a32 = yz + wx;

    //    ret.a13 = xz + wy;
    //    ret.a23 = yz - wx;
    //    ret.a33 = 1 - (xx + yy);
    //    return ret;
    //}

    //private static XVector3 Internal_MakePositive(XVector3 euler)
    //{
    //    XNumber num = -XNumber.create(0, 6); // -0.005729578f;
    //    XNumber num2 = 360f + num;
    //    if (euler.x < num)
    //    {
    //        euler.x += 360;
    //    }
    //    else if (euler.x > num2)
    //    {
    //        euler.x -= 360;
    //    }
    //    if (euler.y < num)
    //    {
    //        euler.y += 360;
    //    }
    //    else if (euler.y > num2)
    //    {
    //        euler.y -= 360;
    //    }
    //    if (euler.z < num)
    //    {
    //        euler.z += 360;
    //        return euler;
    //    }
    //    if (euler.z > num2)
    //    {
    //        euler.z -= 360;
    //    }
    //    return euler;
    //}

    private static XNumber Dot(XQuaternion q1, XQuaternion q2)
    {
        return (q1.x * q2.x + q1.y * q2.y + q1.z * q2.z + q1.w * q2.w);
    }
}