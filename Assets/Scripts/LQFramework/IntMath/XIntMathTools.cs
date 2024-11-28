/*
 * File Name:               XIntMathTools.cs
 *
 * Description:             编辑器器工具 用来构造初始数据
 * Author:                  zhangwei
 * Create Date:             2016/12/30
 */

public static class XIntMathTools
{
    public static XNumber Convert(float f)
    {
        XNumber r;
        r.raw = (int)(f * XIntMath.Muti_FACTOR);
        return r;
    }

    public static XVector2 Convert(UnityEngine.Vector2 vec)
    {
        XNumber x = Convert(vec.x);
        XNumber y = Convert(vec.y);
        return new XVector2(x, y);
    }

    public static XVector3 Convert(UnityEngine.Vector3 vec)
    {
        XNumber x = Convert(vec.x);
        XNumber y = Convert(vec.y);
        XNumber z = Convert(vec.z);
        return new XVector3(x, y, z);
    }

    public static XQuaternion Convert(UnityEngine.Quaternion quat)
    {
        XNumber x = Convert(quat.x);
        XNumber y = Convert(quat.y);
        XNumber z = Convert(quat.z);
        XNumber w = Convert(quat.w);
        return new XQuaternion(x, y, z, w);
    }

    public static XMatrix4x4 Convert(UnityEngine.Matrix4x4 lhs)
    {

        XMatrix4x4 result;
        result.m00 = Convert(lhs.m00);
        result.m10 = Convert(lhs.m10);
        result.m20 = Convert(lhs.m20);
        result.m30 = Convert(lhs.m30);
        result.m01 = Convert(lhs.m01);
        result.m11 = Convert(lhs.m11);
        result.m21 = Convert(lhs.m21);
        result.m31 = Convert(lhs.m31);
        result.m02 = Convert(lhs.m02);
        result.m12 = Convert(lhs.m12);
        result.m22 = Convert(lhs.m22);
        result.m32 = Convert(lhs.m32);
        result.m03 = Convert(lhs.m03);
        result.m13 = Convert(lhs.m13);
        result.m23 = Convert(lhs.m23);
        result.m33 = Convert(lhs.m33);

        return result;
    }
}