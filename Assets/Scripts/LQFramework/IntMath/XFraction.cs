/*
 * File Name:               XFraction.cs
 * 
 * Description:             普通类
 * Author:                  lisiyu <576603306@qq.com>

 * Create Date:             2017/06/13
 */

using System;

public struct XFraction 
{
    public int Numerator;                   // 分子
    public int Denominator;                 // 分母

    public XFraction(int num, int den)
    {
        Numerator = num;
        Denominator = den;
    }

#region 类型转换

    #region int
    public static explicit operator int(XFraction fraction)
    {
        return fraction.Numerator / fraction.Denominator;
    }

    public static explicit operator XFraction(int number)
    {
        return new XFraction(number, 1);
    }
    #endregion int

    #region float
    public static explicit operator float(XFraction fraction)
    {
        return  1f * fraction.Numerator / fraction.Denominator;
    }

    public static explicit operator XFraction(float number)
    {
        return new XFraction((int)(number * 1000), 1000);
    }
    #endregion float

    #endregion 类型转换

#region 运算符
    public static XFraction operator +(XFraction left, XFraction right)
    {
        return new XFraction() {
            Numerator = left.Numerator * right.Denominator + right.Numerator * left.Denominator,
            Denominator = left.Denominator * right.Denominator,
        };   
    }

    public static XFraction operator -(XFraction left, XFraction right)
    {
        return new XFraction()
        {
            Numerator = left.Numerator * right.Denominator - right.Numerator * left.Denominator,
            Denominator = left.Denominator * right.Denominator,
        };
    }

    public static XFraction operator *(XFraction left, XFraction right)
    {
        return new XFraction()
        {
            Numerator = left.Numerator * right.Numerator,
            Denominator = left.Denominator * right.Denominator,
        };
    }

    public static XFraction operator /(XFraction left, XFraction right)
    {
        return new XFraction()
        {
            Numerator = left.Numerator * right.Denominator,
            Denominator = left.Denominator * right.Numerator,
        };
    }

    #endregion 运算符

    private static int getGCD(int num1, int num2)
    {
        int numOne = Math.Abs(num1);
        int numTwo = Math.Abs(num2);

        while (numTwo != 0)
        {
            int temp = numOne;
            numOne = numTwo;
            numTwo = temp % numTwo;
        }

        return numOne;
    }
}
