/*
 * File Name:               XXMatrix4x4.cs
 * 
 * Description:             普通类
 * Author:                  lisiyu <576603306@qq.com>
 * Create Date:             2017/01/04
 */

using System;
using UnityEngine;

public struct XMatrix4x4
{
    public XNumber m00;
    public XNumber m10;
    public XNumber m20;
    public XNumber m30;
    public XNumber m01;
    public XNumber m11;
    public XNumber m21;
    public XNumber m31;
    public XNumber m02;
    public XNumber m12;
    public XNumber m22;
    public XNumber m32;
    public XNumber m03;
    public XNumber m13;
    public XNumber m23;
    public XNumber m33;

    public static implicit operator UnityEngine.Matrix4x4(XMatrix4x4 lhs)
    {
        UnityEngine.Matrix4x4 result;
        result.m00 = lhs.m00;
        result.m10 = lhs.m10;
        result.m20 = lhs.m20;
        result.m30 = lhs.m30;
        result.m01 = lhs.m01;
        result.m11 = lhs.m11;
        result.m21 = lhs.m21;
        result.m31 = lhs.m31;
        result.m02 = lhs.m02;
        result.m12 = lhs.m12;
        result.m22 = lhs.m22;
        result.m32 = lhs.m32;
        result.m03 = lhs.m03;
        result.m13 = lhs.m13;
        result.m23 = lhs.m23;
        result.m33 = lhs.m33;

        return result;
    }

    public static implicit operator XMatrix4x4(Matrix4x4 lhs)
    {
        XMatrix4x4 result;
        result.m00 = lhs.m00;
        result.m10 = lhs.m10;
        result.m20 = lhs.m20;
        result.m30 = lhs.m30;
        result.m01 = lhs.m01;
        result.m11 = lhs.m11;
        result.m21 = lhs.m21;
        result.m31 = lhs.m31;
        result.m02 = lhs.m02;
        result.m12 = lhs.m12;
        result.m22 = lhs.m22;
        result.m32 = lhs.m32;
        result.m03 = lhs.m03;
        result.m13 = lhs.m13;
        result.m23 = lhs.m23;
        result.m33 = lhs.m33;

        return result;
    }

    public XNumber this[int row, int column]
    {
        get
        {
            return this[row + (column * 4)];
        }
        set
        {
            this[row + (column * 4)] = value;
        }
    }
    public XNumber this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.m00;

                case 1:
                    return this.m10;

                case 2:
                    return this.m20;

                case 3:
                    return this.m30;

                case 4:
                    return this.m01;

                case 5:
                    return this.m11;

                case 6:
                    return this.m21;

                case 7:
                    return this.m31;

                case 8:
                    return this.m02;

                case 9:
                    return this.m12;

                case 10:
                    return this.m22;

                case 11:
                    return this.m32;

                case 12:
                    return this.m03;

                case 13:
                    return this.m13;

                case 14:
                    return this.m23;

                case 15:
                    return this.m33;
            }
            throw new IndexOutOfRangeException("Invalid matrix index!");
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.m00 = value;
                    break;

                case 1:
                    this.m10 = value;
                    break;

                case 2:
                    this.m20 = value;
                    break;

                case 3:
                    this.m30 = value;
                    break;

                case 4:
                    this.m01 = value;
                    break;

                case 5:
                    this.m11 = value;
                    break;

                case 6:
                    this.m21 = value;
                    break;

                case 7:
                    this.m31 = value;
                    break;

                case 8:
                    this.m02 = value;
                    break;

                case 9:
                    this.m12 = value;
                    break;

                case 10:
                    this.m22 = value;
                    break;

                case 11:
                    this.m32 = value;
                    break;

                case 12:
                    this.m03 = value;
                    break;

                case 13:
                    this.m13 = value;
                    break;

                case 14:
                    this.m23 = value;
                    break;

                case 15:
                    this.m33 = value;
                    break;

                default:
                    throw new IndexOutOfRangeException("Invalid matrix index!");
            }
        }
    }
    //public override int GetHashCode()
    //{
    //    return (((this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2)) ^ (this.GetColumn(2).GetHashCode() >> 2)) ^ (this.GetColumn(3).GetHashCode() >> 1));
    //}

    //public override bool Equals(object other)
    //{
    //    if (!(other is XMatrix4x4))
    //    {
    //        return false;
    //    }
    //    XMatrix4x4 matrixx = (XMatrix4x4)other;
    //    return (((this.GetColumn(0).Equals(matrixx.GetColumn(0)) && this.GetColumn(1).Equals(matrixx.GetColumn(1))) && this.GetColumn(2).Equals(matrixx.GetColumn(2))) && this.GetColumn(3).Equals(matrixx.GetColumn(3)));
    //}

    public UnityEngine.Matrix4x4 unity {
        get
        {
            return (UnityEngine.Matrix4x4)this;
        }
    }

    public XMatrix4x4 inverse
    {
        get
        {
            return XIntMathTools.Convert(unity.inverse);
            // return getInverse();
            //int[] ks = new int[4];
            //int[] js = new int[4];
            //XNumber fDet = 1;
            //int f = 1;

            //for (int k = 0; k < 4; k++)
            //{
            //    // 第一步，全选主元
            //    XNumber fMax = 0;
            //    for (int i = k; i < 4; i++)
            //    {
            //        for (int j = k; j < 4; j++)
            //        {
            //            XNumber f = XIntMath.Abs(this[i ,j]);
            //            if (f > fMax)
            //            {
            //                fMax = f;
            //                ks[k] = i;
            //                js[k] = j;
            //            }
            //        }
            //    }

            //    if (ks[k] != k)
            //    {
            //        f = -f;
            //        swap(this[k, 0], this[ks[k], 0]);
            //        swap(this[k, 1], this[ks[k], 1]);
            //        swap(this[k, 2], this[ks[k], 2]);
            //        swap(this[k, 3], this[ks[k], 3]);
            //    }
            //    if (js[k] != k)
            //    {
            //        f = -f;
            //        swap(this[0, k], this[0, js[k]]);
            //        swap(this[1, k], this[1, js[k]]);
            //        swap(this[2, k], this[2, js[k]]);
            //        swap(this[3, k], this[3, js[k]]);
            //    }

            //    // 计算行列值
            //    fDet *= this[k, k];

            //    // 计算逆矩阵

            //    // 第二步
            //    this[k, k] = 1 / this[k, k];
            //    // 第三步
            //    for (int j = 0; j < 4; j++)
            //    {
            //        if (j != k)
            //            this(k, j) *= this[k, k;
            //    }
            //    // 第四步
            //    for (int i = 0; i < 4; i++)
            //    {
            //        if (i != k)
            //        {
            //            for (j = 0; j < 4; j++)
            //            {
            //                if (j != k)
            //                    this(i, j) = this(i, j) - this(i, k) * this(k, j);
            //            }
            //        }
            //    }
            //    // 第五步
            //    for (i = 0; i < 4; i++)
            //    {
            //        if (i != k)
            //            this(i, k) *= -this(k, k);
            //    }
            //}

            //for (k = 3; k >= 0; k--)
            //{
            //    if (js[k] != k)
            //    {
            //        swap(this(k, 0), this(js[k], 0));
            //        swap(this(k, 1), this(js[k], 1));
            //        swap(this(k, 2), this(js[k], 2));
            //        swap(this(k, 3), this(js[k], 3));
            //    }
            //    if (is[k] != k)
            //    {
            //        swap(this(0, k), this(0, is[k]));
            //        swap(this(1, k), this(1, is[k]));
            //        swap(this(2, k), this(2, is[k]));
            //        swap(this(3, k), this(3, is[k]));
            //    }
            //}

            //return fDet * f;
        }
    }

    public XVector4 GetColumn(int i)
    {
        return new XVector4(this[0, i], this[1, i], this[2, i], this[3, i]);
    }

    public XVector4 GetRow(int i)
    {
        return new XVector4(this[i, 0], this[i, 1], this[i, 2], this[i, 3]);
    }

    public void SetColumn(int i, XVector4 v)
    {
        this[0, i] = v.x;
        this[1, i] = v.y;
        this[2, i] = v.z;
        this[3, i] = v.w;
    }

    public void SetRow(int i, XVector4 v)
    {
        this[i, 0] = v.x;
        this[i, 1] = v.y;
        this[i, 2] = v.z;
        this[i, 3] = v.w;
    }

    public XVector3 MultiplyPoint(XVector3 v)
    {
        return (XVector3)unity.MultiplyPoint(v);

        XVector3 vector;
        vector.x = (((this.m00 * v.x) + (this.m01 * v.y)) + (this.m02 * v.z)) + this.m03;
        vector.y = (((this.m10 * v.x) + (this.m11 * v.y)) + (this.m12 * v.z)) + this.m13;
        vector.z = (((this.m20 * v.x) + (this.m21 * v.y)) + (this.m22 * v.z)) + this.m23;
        XNumber num = (((this.m30 * v.x) + (this.m31 * v.y)) + (this.m32 * v.z)) + this.m33;
        num = 1 / num;
        vector.x *= num;
        vector.y *= num;
        vector.z *= num;
        return vector;
    }

    public XVector3 MultiplyPoint3x4(XVector3 v)
    {
        return XIntMathTools.Convert(unity.MultiplyPoint3x4((Vector3)v));

        XVector3 vector;
        vector.x = (((this.m00 * v.x) + (this.m01 * v.y)) + (this.m02 * v.z)) + this.m03;
        vector.y = (((this.m10 * v.x) + (this.m11 * v.y)) + (this.m12 * v.z)) + this.m13;
        vector.z = (((this.m20 * v.x) + (this.m21 * v.y)) + (this.m22 * v.z)) + this.m23;
        return vector;
    }

    public XVector3 MultiplyVector(XVector3 v)
    {
        return XIntMathTools.Convert(unity.MultiplyVector((Vector3)v));

        XVector3 vector;
        vector.x = ((this.m00 * v.x) + (this.m01 * v.y)) + (this.m02 * v.z);
        vector.y = ((this.m10 * v.x) + (this.m11 * v.y)) + (this.m12 * v.z);
        vector.z = ((this.m20 * v.x) + (this.m21 * v.y)) + (this.m22 * v.z);
        return vector;
    }

    public static XMatrix4x4 zero
    {
        get
        {
            return new XMatrix4x4
            {
                m00 = 0,
                m01 = 0,
                m02 = 0,
                m03 = 0,
                m10 = 0,
                m11 = 0,
                m12 = 0,
                m13 = 0,
                m20 = 0,
                m21 = 0,
                m22 = 0,
                m23 = 0,
                m30 = 0,
                m31 = 0,
                m32 = 0,
                m33 = 0
            };
        }
    }
    public static XMatrix4x4 identity
    {
        get
        {
            return new XMatrix4x4
            {
                m00 = 1,
                m01 = 0,
                m02 = 0,
                m03 = 0,
                m10 = 0,
                m11 = 1,
                m12 = 0,
                m13 = 0,
                m20 = 0,
                m21 = 0,
                m22 = 1,
                m23 = 0,
                m30 = 0,
                m31 = 0,
                m32 = 0,
                m33 = 1
            };
        }
    }

    public void SetTRS(XVector3 pos, XQuaternion q, XVector3 s)
    {
        this = TRS(pos, q, s);
        // this = TRS(pos, q, s);
    }

    public static XMatrix4x4 TRS(XVector3 pos, Quaternion q, XVector3 s)
    {
        return XIntMathTools.Convert(Matrix4x4.TRS(pos, Quaternion.Euler(q.eulerAngles), s));
        // return transform(pos) * rotate(q) * scale(s);
    }

    //public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
    //{
    //    return Matrix4x4.TRS(pos, Quaternion.Euler(q.eulerAngles), s);
    //    // return transform(pos) * rotate(q) * scale(s);
    //}


    //public override string ToString()
    //{
    //    object[] args = new object[] { this.m00, this.m01, this.m02, this.m03, this.m10, this.m11, this.m12, this.m13, this.m20, this.m21, this.m22, this.m23, this.m30, this.m31, this.m32, this.m33 };
    //    return string.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", args);
    //}

    //public string ToString(string format)
    //{
    //    object[] args = new object[] { this.m00.ToString(format), this.m01.ToString(format), this.m02.ToString(format), this.m03.ToString(format), this.m10.ToString(format), this.m11.ToString(format), this.m12.ToString(format), this.m13.ToString(format), this.m20.ToString(format), this.m21.ToString(format), this.m22.ToString(format), this.m23.ToString(format), this.m30.ToString(format), this.m31.ToString(format), this.m32.ToString(format), this.m33.ToString(format) };
    //    return string.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", args);
    //}

    // private static extern void INTERNAL_CALL_Perspective(XNumber fov, XNumber aspect, XNumber zNear, XNumber zFar, out XMatrix4x4 value);
    //public static XMatrix4x4 operator *(XMatrix4x4 lhs, XMatrix4x4 rhs)
    //{
    //    return new XMatrix4x4
    //    {
    //        m00 = (((lhs.m00 * rhs.m00) + (lhs.m01 * rhs.m10)) + (lhs.m02 * rhs.m20)) + (lhs.m03 * rhs.m30),
    //        m01 = (((lhs.m00 * rhs.m01) + (lhs.m01 * rhs.m11)) + (lhs.m02 * rhs.m21)) + (lhs.m03 * rhs.m31),
    //        m02 = (((lhs.m00 * rhs.m02) + (lhs.m01 * rhs.m12)) + (lhs.m02 * rhs.m22)) + (lhs.m03 * rhs.m32),
    //        m03 = (((lhs.m00 * rhs.m03) + (lhs.m01 * rhs.m13)) + (lhs.m02 * rhs.m23)) + (lhs.m03 * rhs.m33),
    //        m10 = (((lhs.m10 * rhs.m00) + (lhs.m11 * rhs.m10)) + (lhs.m12 * rhs.m20)) + (lhs.m13 * rhs.m30),
    //        m11 = (((lhs.m10 * rhs.m01) + (lhs.m11 * rhs.m11)) + (lhs.m12 * rhs.m21)) + (lhs.m13 * rhs.m31),
    //        m12 = (((lhs.m10 * rhs.m02) + (lhs.m11 * rhs.m12)) + (lhs.m12 * rhs.m22)) + (lhs.m13 * rhs.m32),
    //        m13 = (((lhs.m10 * rhs.m03) + (lhs.m11 * rhs.m13)) + (lhs.m12 * rhs.m23)) + (lhs.m13 * rhs.m33),
    //        m20 = (((lhs.m20 * rhs.m00) + (lhs.m21 * rhs.m10)) + (lhs.m22 * rhs.m20)) + (lhs.m23 * rhs.m30),
    //        m21 = (((lhs.m20 * rhs.m01) + (lhs.m21 * rhs.m11)) + (lhs.m22 * rhs.m21)) + (lhs.m23 * rhs.m31),
    //        m22 = (((lhs.m20 * rhs.m02) + (lhs.m21 * rhs.m12)) + (lhs.m22 * rhs.m22)) + (lhs.m23 * rhs.m32),
    //        m23 = (((lhs.m20 * rhs.m03) + (lhs.m21 * rhs.m13)) + (lhs.m22 * rhs.m23)) + (lhs.m23 * rhs.m33),
    //        m30 = (((lhs.m30 * rhs.m00) + (lhs.m31 * rhs.m10)) + (lhs.m32 * rhs.m20)) + (lhs.m33 * rhs.m30),
    //        m31 = (((lhs.m30 * rhs.m01) + (lhs.m31 * rhs.m11)) + (lhs.m32 * rhs.m21)) + (lhs.m33 * rhs.m31),
    //        m32 = (((lhs.m30 * rhs.m02) + (lhs.m31 * rhs.m12)) + (lhs.m32 * rhs.m22)) + (lhs.m33 * rhs.m32),
    //        m33 = (((lhs.m30 * rhs.m03) + (lhs.m31 * rhs.m13)) + (lhs.m32 * rhs.m23)) + (lhs.m33 * rhs.m33)
    //    };
    //}

    //public static XVector4 operator *(XMatrix4x4 lhs, XVector4 v)
    //{
    //    XVector4 vector;
    //    vector.x = (((lhs.m00 * v.x) + (lhs.m01 * v.y)) + (lhs.m02 * v.z)) + (lhs.m03 * v.w);
    //    vector.y = (((lhs.m10 * v.x) + (lhs.m11 * v.y)) + (lhs.m12 * v.z)) + (lhs.m13 * v.w);
    //    vector.z = (((lhs.m20 * v.x) + (lhs.m21 * v.y)) + (lhs.m22 * v.z)) + (lhs.m23 * v.w);
    //    vector.w = (((lhs.m30 * v.x) + (lhs.m31 * v.y)) + (lhs.m32 * v.z)) + (lhs.m33 * v.w);
    //    return vector;
    //}

    //public static bool operator ==(XMatrix4x4 lhs, XMatrix4x4 rhs)
    //{
    //    return ((((lhs.GetColumn(0) == rhs.GetColumn(0)) && (lhs.GetColumn(1) == rhs.GetColumn(1))) && (lhs.GetColumn(2) == rhs.GetColumn(2))) && (lhs.GetColumn(3) == rhs.GetColumn(3)));
    //}

    //public static bool operator !=(XMatrix4x4 lhs, XMatrix4x4 rhs)
    //{
    //    return !(lhs == rhs);
    //}



    public static XMatrix4x4 transform(XVector3 pos)
    {
        var ret = XMatrix4x4.identity;

        ret.m30 = pos.x;
        ret.m31 = pos.y;
        ret.m32 = pos.z;

        return ret;
    }

    public static XMatrix4x4 rotate(XQuaternion q)
    {
        var ret = XMatrix4x4.identity;

        ret.m00 = 1 - 2 * (q.y * q.y + q.z * q.z);
        ret.m01 = 2 * (q.z * q.y + q.z * q.w);
        ret.m02 = 2 * (q.x * q.z + q.y * q.w);
        ret.m10 = 2 * (q.x * q.y + q.z * q.w);
        ret.m11 = 1 - 2 * (q.x * q.x + q.z * q.z);
        ret.m12 = 2 * (q.y * q.z - q.x * q.w);
        ret.m20 = 2 * (q.x * q.z - q.y * q.w);
        ret.m21 = 2 * (q.y * q.z + q.x * q.w);
        ret.m22 = 1 - 2 * (q.x * q.x + q.y * q.y);

        return ret;
    }

    public static XMatrix4x4 scale(XVector3 s)
    {
        return new XMatrix4x4
        {
            m00 = s.x,
            m01 = 0,
            m02 = 0,
            m03 = 0,
            m10 = 0,
            m11 = s.y,
            m12 = 0,
            m13 = 0,
            m20 = 0,
            m21 = 0,
            m22 = s.z,
            m23 = 0,
            m30 = 0,
            m31 = 0,
            m32 = 0,
            m33 = 1
        };
    }


    public XMatrix4x4 getInverse()
    {
        var ret = identity;

        /*定义扩展矩阵*/
        XNumber[,] expand_matrix = new XNumber[4, 4 * 2];
        /*初始化扩展矩阵*/
        expand_matrix = getExpandMatrix();
        /*计算扩展矩阵*/
        calculateExpandMatrix(ref expand_matrix);
        /*用计算过的扩展矩阵取后面的N*N矩阵，为所求*/
        ret = getNewMatrix(expand_matrix);

        return ret;
    }

    /*初始化扩展矩阵*/
    private XNumber[,] getExpandMatrix()
    {
        var ret = new XNumber[4, 4 * 2];

        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4 * 2; j++)
            {
                if (j < 4)
                {//左边的N*N矩阵原样赋值  
                    ret[i, j] = this[i, j];
                }
                else
                {    //右边N*N赋值为单位矩阵  
                    if (j == ret.Length + i)//如果为右边矩阵的对角线就赋值为1  
                        ret[i, j] = 1;
                    else
                        ret[i, j] = 0;
                }
            }
        return ret;
    }

    /*调整扩展矩阵，若某一列全为0，则行列式的值等于0，不存在逆矩阵*/
    private bool adjustMatrix(double[][] expand_matrix)
    {

        for (int i = 0; i < expand_matrix.Length; i++)
        {
            if (expand_matrix[i][i] == 0)
            {//如果某行对角线数值为0  
                int j;
                /*搜索该列其他不为0的行，如果都为0，则返回false*/
                for (j = 0; j < expand_matrix.Length; j++)
                {

                    if (expand_matrix[j][i] != 0)
                    {//如果有不为0的行，交换这两行  
                        double[] temp = expand_matrix[i];
                        expand_matrix[i] = expand_matrix[j];
                        expand_matrix[j] = temp;
                        break;
                    }

                }
                if (j >= expand_matrix.Length)
                {//没有不为0的行  
                    // System.out.println("此矩阵没有逆矩阵");
                    return false;
                }
            }
        }
        return true;
    }

    /*计算扩展矩阵*/
    private void calculateExpandMatrix(ref XNumber[,] expand_matrix)
    {
        for (int i = 0; i < 4; i++)
        {
            XNumber first_element = expand_matrix[i, i];

            for (int j = 0; j < 8; j++)

                expand_matrix[i, j] /= first_element;//将该行所有元素除以首元素  

            /*把其他行再该列的数值都化为0*/
            for (int m = 0; m < 4; m++)
            {
                if (m == i)//遇到自己的行跳过  
                    continue;

                XNumber beishu = expand_matrix[m, i];
                for (int n = 0; n < 8; n++)
                {
                    expand_matrix[m, n] -= expand_matrix[i, n] * beishu;
                }
            }

        }

    }
    /*用计算过的扩展矩阵取后面的N*N矩阵，为所求*/
    private XMatrix4x4 getNewMatrix(XNumber[,] expand_matrix)
    {
        var ret = new XMatrix4x4();
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 8; j++)
            {
                if (j >= 4)
                    ret[i, j - 4] = expand_matrix[i, j];
            }
        return ret;
    }
}
