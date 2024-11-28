/*
 * File Name:               XXVector4.cs
 * 
 * Description:             普通类
 * Author:                  lisiyu <576603306@qq.com>
 * Create Date:             2017/01/04
 */

using System;

public struct XVector4
{
    // public const XNumber kEpsilon = 1E-05f;
    public XNumber x;
    public XNumber y;
    public XNumber z;
    public XNumber w;
    public XVector4(XNumber x, XNumber y, XNumber z, XNumber w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public XVector4(XNumber x, XNumber y, XNumber z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = 0;
    }

    public XVector4(XNumber x, XNumber y)
    {
        this.x = x;
        this.y = y;
        this.z = 0;
        this.w = 0;
    }

    public XNumber this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.x;

                case 1:
                    return this.y;

                case 2:
                    return this.z;

                case 3:
                    return this.w;
            }
            throw new IndexOutOfRangeException("Invalid XVector4 index!");
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.x = value;
                    break;

                case 1:
                    this.y = value;
                    break;

                case 2:
                    this.z = value;
                    break;

                case 3:
                    this.w = value;
                    break;

                default:
                    throw new IndexOutOfRangeException("Invalid XVector4 index!");
            }
        }
    }

    public XVector2 xy
    {
        get
        {
            XVector2 ret;
            ret.x = x;
            ret.y = y;

            return ret;
        }
    }

    public XVector2 zw
    {
        get
        {
            XVector2 ret;
            ret.x = z;
            ret.y = w;

            return ret;
        }
    }

    public void Set(XNumber new_x, XNumber new_y, XNumber new_z, XNumber new_w)
    {
        this.x = new_x;
        this.y = new_y;
        this.z = new_z;
        this.w = new_w;
    }

    public static XVector4 Lerp(XVector4 a, XVector4 b, XNumber t)
    {
        t = XIntMath.Clamp01(t);
        return new XVector4(a.x + ((b.x - a.x) * t), a.y + ((b.y - a.y) * t), a.z + ((b.z - a.z) * t), a.w + ((b.w - a.w) * t));
    }

    public static XVector4 LerpUnclamped(XVector4 a, XVector4 b, XNumber t)
    {
        return new XVector4(a.x + ((b.x - a.x) * t), a.y + ((b.y - a.y) * t), a.z + ((b.z - a.z) * t), a.w + ((b.w - a.w) * t));
    }

    public static XVector4 MoveTowards(XVector4 current, XVector4 target, XNumber maxDistanceDelta)
    {
        XVector4 vector = target - current;
        XNumber magnitude = vector.magnitude;
        if ((magnitude > maxDistanceDelta) && (magnitude != 0))
        {
            return (current + ((XVector4)((vector / magnitude) * maxDistanceDelta)));
        }
        return target;
    }

    public static XVector4 Scale(XVector4 a, XVector4 b)
    {
        return new XVector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    }

    public void Scale(XVector4 scale)
    {
        this.x *= scale.x;
        this.y *= scale.y;
        this.z *= scale.z;
        this.w *= scale.w;
    }

    public override int GetHashCode()
    {
        return (((this.x.GetHashCode() ^ (this.y.GetHashCode() << 2)) ^ (this.z.GetHashCode() >> 2)) ^ (this.w.GetHashCode() >> 1));
    }

    public override bool Equals(object other)
    {
        if (!(other is XVector4))
        {
            return false;
        }
        XVector4 vector = (XVector4)other;
        return (((this.x.Equals(vector.x) && this.y.Equals(vector.y)) && this.z.Equals(vector.z)) && this.w.Equals(vector.w));
    }

    public static XVector4 Normalize(XVector4 a)
    {
        XNumber num = Magnitude(a);
        if (num > XNumber.epsilon)
        {
            return (XVector4)(a / num);
        }
        return zero;
    }

    public void Normalize()
    {
        XNumber num = Magnitude(this);
        if (num > XNumber.epsilon)
        {
            this = (XVector4)(this / num);
        }
        else
        {
            this = zero;
        }
    }

    public XVector4 normalized
    {
        get
        {
            return Normalize(this);
        }
    }
    public override string ToString()
    {
        object[] args = new object[] { this.x, this.y, this.z, this.w };
        return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", args);
    }

    public string ToString(string format)
    {
        object[] args = new object[] { this.x.ToString(format), this.y.ToString(format), this.z.ToString(format), this.w.ToString(format) };
        return string.Format("({0}, {1}, {2}, {3})", args);
    }

    public static XVector4 Parse(string text)
    {
        if (text[0] == '(' || text[text.Length - 1] == ')')
            text = text.Substring(1, text.Length - 2);
        string[] tokens = text.Split(',');
        if (tokens.Length != 4)
            tokens = text.Split(' ');
        if (tokens.Length != 4)
            throw new System.FormatException("The input text must contains 4 elements.");
        XNumber x = XNumber.Parse(tokens[0]);
        XNumber y = XNumber.Parse(tokens[1]);
        XNumber z = XNumber.Parse(tokens[2]);
        XNumber w = XNumber.Parse(tokens[3]);
        return new XVector4(x, y, z, w);
    }

    public static XNumber Dot(XVector4 a, XVector4 b)
    {
        return ((((a.x * b.x) + (a.y * b.y)) + (a.z * b.z)) + (a.w * b.w));
    }

    public static XVector4 Project(XVector4 a, XVector4 b)
    {
        return (XVector4)((b * Dot(a, b)) / Dot(b, b));
    }

    public static XNumber Distance(XVector4 a, XVector4 b)
    {
        return Magnitude(a - b);
    }

    public static XNumber Magnitude(XVector4 a)
    {
        return XIntMath.Sqrt(Dot(a, a));
    }

    public XNumber magnitude
    {
        get
        {
            return XIntMath.Sqrt(Dot(this, this));
        }
    }
    public static XNumber SqrMagnitude(XVector4 a)
    {
        return Dot(a, a);
    }

    public XNumber SqrMagnitude()
    {
        return Dot(this, this);
    }

    public XNumber sqrMagnitude
    {
        get
        {
            return Dot(this, this);
        }
    }
    public static XVector4 Min(XVector4 lhs, XVector4 rhs)
    {
        return new XVector4(XIntMath.Min(lhs.x, rhs.x), XIntMath.Min(lhs.y, rhs.y), XIntMath.Min(lhs.z, rhs.z), XIntMath.Min(lhs.w, rhs.w));
    }

    public static XVector4 Max(XVector4 lhs, XVector4 rhs)
    {
        return new XVector4(XIntMath.Max(lhs.x, rhs.x), XIntMath.Max(lhs.y, rhs.y), XIntMath.Max(lhs.z, rhs.z), XIntMath.Max(lhs.w, rhs.w));
    }

    public static XVector4 zero
    {
        get
        {
            return new XVector4(0, 0, 0, 0);
        }
    }
    public static XVector4 one
    {
        get
        {
            return new XVector4(1, 1, 1, 1);
        }
    }

    public static XVector4 operator +(XVector4 a, XVector4 b)
    {
        return new XVector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    public static XVector4 operator -(XVector4 a, XVector4 b)
    {
        return new XVector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    public static XVector4 operator -(XVector4 a)
    {
        return new XVector4(-a.x, -a.y, -a.z, -a.w);
    }

    public static XVector4 operator *(XVector4 a, XNumber d)
    {
        return new XVector4(a.x * d, a.y * d, a.z * d, a.w * d);
    }

    public static XVector4 operator *(XNumber d, XVector4 a)
    {
        return new XVector4(a.x * d, a.y * d, a.z * d, a.w * d);
    }

    public static XVector4 operator /(XVector4 a, XNumber d)
    {
        return new XVector4(a.x / d, a.y / d, a.z / d, a.w / d);
    }

    public static bool operator ==(XVector4 lhs, XVector4 rhs)
    {
        return (SqrMagnitude(lhs - rhs) == 0);
    }

    public static bool operator !=(XVector4 lhs, XVector4 rhs)
    {
        return (SqrMagnitude(lhs - rhs) != 0);
    }

    public static implicit operator XVector4(XVector3 v)
    {
        return new XVector4(v.x, v.y, v.z, 0);
    }

    public static implicit operator XVector3(XVector4 v)
    {
        return new XVector3(v.x, v.y, v.z);
    }

    public static implicit operator XVector4(XVector2 v)
    {
        return new XVector4(v.x, v.y, 0, 0);
    }

    public static implicit operator XVector2(XVector4 v)
    {
        return new XVector2(v.x, v.y);
    }
}