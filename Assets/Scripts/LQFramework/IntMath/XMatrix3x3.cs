///*
// * File Name:               XMatrix3x3.cs
// * 
// * Description:             普通类
// * Author:                  lisiyu <576603306@qq.com>
// * Create Date:             2017/01/25
// */

///// <summary>
///// 下标规则;
///// 1   4   7
///// 2   5   8
///// 3   6   9
///// </summary>
//public struct XMatrix3x3 
//{
//    //public XNumber[] m_Data;
//    public XNumber a11, a12, a13;
//    public XNumber a21, a22, a23;
//    public XNumber a31, a32, a33;

////     private XMatrix3x3()
////     {
////         a11 = XNumber.zero; a12 = XNumber.zero; a13 = XNumber.zero;
////         a21 = XNumber.zero; a22 = XNumber.zero; a23 = XNumber.zero;
////         a31 = XNumber.zero; a32 = XNumber.zero; a33 = XNumber.zero;
////     }

//    XNumber Get(int row, int column)
//    {
//        if (row == 0)
//        {
//            if (column == 0)
//                return a11;
//            else if (column == 1)
//                return a12;
//            else
//                return a13;
//        }
//        else if (row == 1)
//        {
//            if (column == 0)
//                return a21;
//            else if (column == 1)
//                return a22;
//            else
//                return a23;
//        }
//        else
//        {
//            if (column == 0)
//                return a31;
//            else if (column == 1)
//                return a32;
//            else
//                return a33;
//        }
//    }
    
//    public XVector3 ToEuler()
//    {
//        XVector3 ret;
//        // from http://www.geometrictools.com/Documentation/EulerAngles.pdf
//        // YXZ order
//        XNumber temp = XNumber.create_row(1023);
//        XNumber value1_2 = Get(1, 2);
//	    if (value1_2 < temp) // some fudge for imprecision
//	    {
//		    if (value1_2 > -temp) // some fudge for imprecision
//		    {
//                ret.x = XIntMath.Asin(-value1_2);
//                ret.y = XIntMath.Atan2(Get(0,2), Get(2,2));
//                ret.z = XIntMath.Atan2(Get(1,0), Get(1,1));
//            }
//            else
//            {
//                // WARNING.  Not unique.  YA - ZA = atan2(r01,r00)
//                ret.x = XIntMath.PI * XNumber.half;
//                ret.y = XIntMath.Atan2(Get (0,1), Get(0,0));
//                ret.z = 0;
//            }
//        }
//        else
//        {
//            // WARNING.  Not unique.  YA + ZA = atan2(-r01,r00)
//            ret.x = -XIntMath.PI * XNumber.half;
//            ret.y = XIntMath.Atan2(-Get(0,1),Get(0,0));
//            ret.z = 0;
//        }

//        return ret.MakePositive();
//    }

//    // Right handed
//    public static bool LookRotationToMatrix(XVector3 viewVec, XVector3 upVec, ref XMatrix3x3 m)
//    {
//        XVector3 z = viewVec;

//        XNumber mag = z.magnitude;
//        if (mag < XNumber.epsilon)
//        {
//            m.SetIdentity();
//            return false;
//        }

//        z /= mag;

//        XVector3 x = XVector3.Cross(upVec, z);
//        mag = x.magnitude;
//        if(mag < XNumber.epsilon)
//        {
//            m.SetIdentity();
//            return false;
//        }

//        x /= mag;

//        XVector3 y = XVector3.Cross(z, x);
//        if (y.magnitude - XNumber.one > XNumber.epsilon)
//            return false;

//        m.SetOrthoNormalBasis(x, y, z);
//        return true;
//    }

//    private void SetIdentity()
//    {
//        a11 = 1; a12 = 0; a13 = 0;
//        a21 = 0; a22 = 1; a23 = 0;
//        a31 = 0; a32 = 0; a33 = 1;
//    }

//    private void SetOrthoNormalBasis(XVector3 inX, XVector3 inY, XVector3 inZ)
//    {
//        a11 = inX.x; a12 = inY.x; a13 = inZ.x;
//        a21 = inX.y; a22 = inY.y; a23 = inZ.y;
//        a31 = inX.z; a32 = inY.z; a33 = inZ.z;
//    }

//    public XQuaternion MatrixToQuaternion()
//    {
//        XQuaternion q = XQuaternion.identity;
//        XNumber fTrace = Get(0, 0) + Get(1, 1) + Get(2, 2);
//        XNumber fRoot;

//        if (fTrace > 0)
//        {
//            // |w| > 1/2, may as well choose w > 1/2
//            fRoot = XIntMath.Sqrt(fTrace + 1);  // 2w
//            q.w = XNumber.half * fRoot;
//            fRoot = XNumber.half / fRoot;  // 1/(4w)
//            q.x = (Get(2, 1) - Get(1, 2)) * fRoot;
//            q.y = (Get(0, 2) - Get(2, 0)) * fRoot;
//            q.z = (Get(1, 0) - Get(0, 1)) * fRoot;
//        }
//        else
//        {
//            // |w| <= 1/2
//            // int[] s_iNext = { 1, 2, 0 };
//            // int i = 0;
//            // if (Get(1, 1) > Get(0, 0))
//            //     i = 1;
//            // if (Get(2, 2) > Get(i, i))
//            //     i = 2;
//            // int j = s_iNext[i];
//            // int k = s_iNext[j];

//            var i = 0;
//            var j = 1;
//            var k = 2;

//            if (Get(1, 1) > Get(0, 0))
//            {
//                i = 1;
//                j = 2;
//                k = 0;
//            }

//            if (Get(2, 2) > Get(i, i))
//            {
//                i = 2;
//                j = 0;
//                k = 1;
//            }

//            fRoot = XIntMath.Sqrt(Get(i, i) - Get(j, j) - Get(k, k) + 1);

//            //XNumber[] apkQuat = { q.x, q.y, q.z };

//            var apkQuat_0 = q.x;
//            var apkQuat_1 = q.y;
//            var apkQuat_2 = q.z;

//            //apkQuat[i] = XNumber.half * fRoot;
//            if (i == 0)
//                apkQuat_0 = XNumber.half * fRoot;
//            else if (i == 1)
//                apkQuat_1 = XNumber.half * fRoot;
//            else
//                apkQuat_2 = XNumber.half * fRoot;

//            fRoot = XNumber.half / fRoot;
//            q.w = (Get(k, j) - Get(j, k)) * fRoot;

//            //apkQuat[j] = (Get(j, i) + Get(i, j)) * fRoot;
//            if (j == 0)
//                apkQuat_0 = (Get(j, i) + Get(i, j)) * fRoot;
//            else if (j == 1)
//                apkQuat_1 = (Get(j, i) + Get(i, j)) * fRoot;
//            else
//                apkQuat_2 = (Get(j, i) + Get(i, j)) * fRoot;

//            //apkQuat[k] = (Get(k, i) + Get(i, k)) * fRoot;
//            if (k == 0)
//                apkQuat_0 = (Get(k, i) + Get(i, k)) * fRoot;
//            else if (k == 1)
//                apkQuat_1 = (Get(k, i) + Get(i, k)) * fRoot;
//            else
//                apkQuat_2 = (Get(k, i) + Get(i, k)) * fRoot;

//            q.x = apkQuat_0; //apkQuat[0];
//            q.y = apkQuat_1; //apkQuat[1];
//            q.z = apkQuat_2; //apkQuat[2];
//        }
//        q = q.normalized;
//        return q;
//    }

//    public void SetFromToRotation(XVector3 from, XVector3 to)
//    {
//        XNumber[,] mtx = new XNumber[3, 3];
//        fromToRotation(from, to, mtx);
//        a11 = mtx[0, 0]; a12 = mtx[0, 1]; a13 = mtx[0, 2];
//        a21 = mtx[1, 0]; a22 = mtx[1, 1]; a23 = mtx[1, 2];
//        a31 = mtx[2, 0]; a32 = mtx[2, 1]; a33 = mtx[2, 2];
//    }

//    /*
//     * A function for creating a rotation matrix that rotates a vector called
//     * "from" into another vector called "to".
//     * Input : from[3], to[3] which both must be *normalized* non-zero vectors
//     * Output: mtx[3][3] -- a 3x3 matrix in colum-major form
//     * Author: Tomas M�ller, 1999
//     */
//    private void fromToRotation(XVector3 from, XVector3 to, XNumber[,] mtx)
//    {
//        XVector3 v;
//        XNumber e, h;
//        v = XVector3.Cross(from, to);
//        e = XVector3.Dot(from, to);
//        if (e > 1 - XNumber.epsilon)     /* "from" almost or equal to "to"-vector? */
//        {
//            /* return identity */
//            mtx[0, 0] = 1; mtx[0, 1] = 0; mtx[0, 2] = 0;
//            mtx[1, 0] = 0; mtx[1, 1] = 1; mtx[1, 2] = 0;
//            mtx[2, 0] = 0; mtx[2, 1] = 0; mtx[2, 2] = 1;
//        }
//        else if (e < -1 + XNumber.epsilon) /* "from" almost or equal to negated "to"? */
//        {
//            XVector3 up, left;
//            XNumber invlen;
//            XNumber fxx, fyy, fzz, fxy, fxz, fyz;
//            XNumber uxx, uyy, uzz, uxy, uxz, uyz;
//            XNumber lxx, lyy, lzz, lxy, lxz, lyz;
//            /* left=CROSS(from, (1,0,0)) */
//            left.x = 0; left.y = from.z; left.z = -from.y;
//            if (XVector3.Dot(left, left) < XNumber.epsilon) /* was left=CROSS(from,(1,0,0)) a good choice? */
//            {
//                /* here we now that left = CROSS(from, (1,0,0)) will be a good choice */
//                left.x = -from.z; left.y = 0; left.z = from.x;
//            }
//            /* normalize "left" */
//            invlen = 1 / XIntMath.Sqrt(XVector3.Dot(left, left));
//            left.x *= invlen;
//            left.y *= invlen;
//            left.z *= invlen;
//            up = XVector3.Cross(left, from);
//            /* now we have a coordinate system, i.e., a basis;    */
//            /* M=(from, up, left), and we want to rotate to:      */
//            /* N=(-from, up, -left). This is done with the matrix:*/
//            /* N*M^T where M^T is the transpose of M              */
//            fxx = -from.x * from.x; fyy = -from.y * from.y; fzz = -from.z * from.z;
//            fxy = -from.x * from.y; fxz = -from.x * from.z; fyz = -from.y * from.z;

//            uxx = up.x * up.x; uyy = up.y * up.y; uzz = up.z * up.z;
//            uxy = up.x * up.y; uxz = up.x * up.z; uyz = up.y * up.z;

//            lxx = -left.x * left.x; lyy = -left.y * left.y; lzz = -left.z * left.z;
//            lxy = -left.x * left.y; lxz = -left.x * left.z; lyz = -left.y * left.z;
//            /* symmetric matrix */
//            mtx[0,0] = fxx + uxx + lxx; mtx[0,1] = fxy + uxy + lxy; mtx[0,2] = fxz + uxz + lxz;
//            mtx[1,0] = mtx[0,1]; mtx[1,1] = fyy + uyy + lyy; mtx[1,2] = fyz + uyz + lyz;
//            mtx[2,0] = mtx[0,2]; mtx[2,1] = mtx[1,2]; mtx[2,2] = fzz + uzz + lzz;
//        }
//        else  /* the most common case, unless "from"="to", or "from"=-"to" */
//        {
//            /* ...otherwise use this hand optimized version (9 mults less) */
//            XNumber hvx, hvz, hvxy, hvxz, hvyz;
//            h = (1 - e) / XVector3.Dot(v, v);
//            hvx = h * v.x;
//            hvz = h * v.z;
//            hvxy = hvx * v.y;
//            hvxz = hvx * v.z;
//            hvyz = hvz * v.y;
//            mtx[0,0] = e + hvx * v.x; mtx[0,1] = hvxy - v.z; mtx[0,2] = hvxz + v.y;
//            mtx[1,0] = hvxy + v.z; mtx[1,1] = e + h * v.y * v.y; mtx[1,2] = hvyz - v.x;
//            mtx[2,0] = hvxz - v.y; mtx[2,1] = hvyz + v.x; mtx[2,2] = e + hvz * v.z;
//        }
//    }
//}
