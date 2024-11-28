/*
 * file TransformExtension.cs
 *
 * author: Pengmian
 * date:   2014/09/18
 */

using UnityEngine;
/// <summary>
/// Transform扩展方法
/// </summary>
public static class TransformExtension
{
    #region 仿射变换

    #region position
    /// <summary>
    /// 设置X坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void setX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// 设置Y坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void setY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    /// <summary>
    /// 设置Z坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void setZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    /// <summary>
    /// 设置X、Y坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void setXY(this Transform transform, float x, float y)
    {
        transform.position = new Vector3(x, y, transform.position.z);
    }

    /// <summary>
    /// 设置Y、Z坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void setYZ(this Transform transform, float y, float z)
    {
        transform.position = new Vector3(transform.position.x, y, z);
    }

    /// <summary>
    /// 设置X、Z坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void setXZ(this Transform transform, float x, float z)
    {
        transform.position = new Vector3(x, transform.position.y, z);
    }

    /// <summary>
    /// 沿着X轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void translateX(this Transform transform, float x)
    {
        transform.position += new Vector3(x, 0, 0);
    }

    /// <summary>
    /// 沿着Y轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void translateY(this Transform transform, float y)
    {
        transform.position += new Vector3(0, y, 0);
    }

    /// <summary>
    /// 沿着Z轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void translateZ(this Transform transform, float z)
    {
        transform.position += new Vector3(0, 0, z);
    }

    /// <summary>
    /// 沿着X、Y轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void translateXY(this Transform transform, float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
    }

    /// <summary>
    /// 沿着X、Z轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void translateXZ(this Transform transform, float x, float z)
    {
        transform.position += new Vector3(x, 0, z);
    }

    /// <summary>
    /// 沿着Y、Z轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void translateYZ(this Transform transform, float y, float z)
    {
        transform.position += new Vector3(0, y, z);
    }

    /// <summary>
    /// 将X、Y、Z都设为0
    /// </summary>
    /// <param name="transform"></param>
    public static void resetPosition(this Transform transform)
    {
        transform.position = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// 设置X坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void setLocalX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    /// <summary>
    /// 设置Y坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void setLocalY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    /// <summary>
    /// 设置Z坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void setLocalZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }

    /// <summary>
    /// 设置X、Y坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void setLocalXY(this Transform transform, float x, float y)
    {
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
    }

    /// <summary>
    /// 设置Y、Z坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void setLocalYZ(this Transform transform, float y, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, z);
    }

    /// <summary>
    /// 设置X、Z坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void setLocalXZ(this Transform transform, float x, float z)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, z);
    }

    /// <summary>
    /// local
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="direction"></param>
    public static void translateLocal(this Transform transform, Vector3 direction)
    {
        transform.localPosition += direction;
    }

    /// <summary>
    /// 沿着X轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void translateLocalX(this Transform transform, float x)
    {
        transform.localPosition += new Vector3(x, 0, 0);
    }

    /// <summary>
    /// 沿着Y轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void translateLocalY(this Transform transform, float y)
    {
        transform.localPosition += new Vector3(0, y, 0);
    }

    /// <summary>
    /// 沿着Z轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void translateLocalZ(this Transform transform, float z)
    {
        transform.localPosition += new Vector3(0, 0, z);
    }

    /// <summary>
    /// 沿着X、Y轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void translateLocalXY(this Transform transform, float x, float y)
    {
        transform.localPosition += new Vector3(x, y, 0);
    }

    /// <summary>
    /// 沿着X、Z轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void translateLocalXZ(this Transform transform, float x, float z)
    {
        transform.localPosition += new Vector3(x, 0, z);
    }

    /// <summary>
    /// 沿着Y、Z轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void translateLocalYZ(this Transform transform, float y, float z)
    {
        transform.localPosition += new Vector3(0, y, z);
    }

    /// <summary>
    /// 将X、Y、Z都设为0
    /// </summary>
    /// <param name="transform"></param>
    public static void resetLocalPosition(this Transform transform)
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
    #endregion

    #region Scale

    /// <summary>
    /// 设置缩放X（local）
    /// </summary>
    /// <param name="tranform"></param>
    /// <param name="x"></param>
    public static void setScaleX(this Transform transform, float x)
    {
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }


    /// <summary>
    /// 设置缩放Y（local）
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void setScaleY(this Transform transform, float y)
    {
        transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);

    }

    /// <summary>
    /// 设置缩放Z（local）
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void setScaleZ(this Transform transform, float z)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
    }


    /// <summary>
    /// 设置缩放XY（local）
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void setScaleXY(this Transform transform, float x, float y)
    {
        transform.localScale = new Vector3(x, y, transform.localScale.z);
    }

    /// <summary>
    /// 设置缩放YZ（local）
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void setScaleYZ(this Transform transform, float y, float z)
    {
        transform.localScale = new Vector3(transform.localScale.x, y, z);

    }

    /// <summary>
    /// 设置缩放XZ（local）
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void setScaleXZ(this Transform transform, float x, float z)
    {
        transform.localScale = new Vector3(x, transform.localScale.y, z);
    }

    /// <summary>
    /// 设置缩放XYZ（local）
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void setScaleXYZ(this Transform transform, float x, float y, float z)
    {
        transform.localScale = new Vector3(x, y, z);
    }


    /// <summary>
    /// 设置为原始大小（local）
    /// </summary>
    /// <param name="transform"></param>
    public static void resetScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 在X方向上缩放(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void scaleX(this Transform transform, float x)
    {
        transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// 在Y方向上缩放(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void scaleY(this Transform transform, float y)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z);
    }

    /// <summary>
    /// 在Z方向上缩放(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void scaleZ(this Transform transform, float z)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * z);
    }

    /// <summary>
    /// 在XY方向上缩放(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void scaleXY(this Transform transform, float x, float y)
    {
        transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y * y, transform.localScale.z);
    }

    /// <summary>
    /// 在YZ方向上缩放(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void scaleYZ(this Transform transform, float y, float z)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z * z);
    }

    /// <summary>
    /// 在XZ方向上缩放(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void scaleXZ(this Transform transform, float x, float z)
    {
        transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z * z);
    }

    /// <summary>
    /// 在XYZ方向上缩放(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void scaleXYZ(this Transform transform, float x, float y, float z)
    {
        transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y * y, transform.localScale.z * z);
    }

    #endregion

    #region Flip

    /// <summary>
    /// 在X方向上翻转
    /// </summary>
    /// <param name="transform"></param>
    public static void flipX(this Transform transform)
    {
        transform.setScaleX(-transform.localScale.x);
    }

    /// <summary>
    /// 在Y方向上翻转
    /// </summary>
    /// <param name="transform"></param>
    public static void flipY(this Transform transform)
    {
        transform.setScaleY(-transform.localScale.y);
    }

    /// <summary>
    /// 在Z方向上翻转
    /// </summary>
    /// <param name="transform"></param>
    public static void flipZ(this Transform transform)
    {
        transform.setScaleZ(-transform.localScale.z);
    }

    /// <summary>
    /// 在XY方向上翻转
    /// </summary>
    /// <param name="transform"></param>
    public static void flipXY(this Transform transform)
    {
        transform.setScaleXY(-transform.localScale.x, -transform.localScale.y);
    }

    /// <summary>
    /// 在YZ方向上翻转
    /// </summary>
    /// <param name="transform"></param>
    public static void flipYZ(this Transform transform)
    {
        transform.setScaleYZ(-transform.localScale.y, -transform.localScale.z);
    }

    /// <summary>
    /// 在XZ方向上翻转
    /// </summary>
    /// <param name="transform"></param>
    public static void flipXZ(this Transform transform)
    {
        transform.setScaleXZ(-transform.localScale.x, -transform.localScale.z);
    }

    /// <summary>
    /// 在XYZ方向上翻转
    /// </summary>
    /// <param name="transform"></param>
    public static void flipXYZ(this Transform transform)
    {
        transform.setScaleXYZ(-transform.localScale.x, -transform.localScale.y, -transform.localScale.z);
    }

    /// <summary>
    /// 重置所有的翻转
    /// </summary>
    /// <param name="transform"></param>
    public static void resetFilpXYZ(this Transform transform)
    {
        transform.setScaleXYZ(Mathf.Abs(transform.localScale.x),
            Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
    }

    #endregion

    #region Rotation

    /// <summary>
    /// 设置x方向旋转角度
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setRotationX(this Transform transform, float angle)
    {
        transform.eulerAngles = new Vector3(angle, 0, 0);
    }

    /// <summary>
    /// 设置Y方向旋转角度
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setRotationY(this Transform transform, float angle)
    {
        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    /// <summary>
    /// 设置Z方向旋转角度
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setRotationZ(this Transform transform, float angle)
    {
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    /// <summary>
    /// 设置x方向旋转角度(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setLocalRotationX(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
    }

    /// <summary>
    /// 设置Y方向旋转角度(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setLocalRotationY(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    /// <summary>
    /// 设置Z方向旋转角度(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setLocalRotationZ(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>
    /// 沿着X轴旋转
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void rotateX(this Transform transform, float angle)
    {
        transform.Rotate(new Vector3(angle, 0, 0));
    }

    /// <summary>
    /// 沿着Y轴旋转
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void rotateY(this Transform transform, float angle)
    {
        transform.Rotate(new Vector3(0, angle, 0));
    }

    /// <summary>
    /// 沿着Z轴旋转
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void rotateZ(this Transform transform, float angle)
    {
        transform.Rotate(new Vector3(0, 0, angle));
    }
    #endregion

    public static Transform[] getChilds(this Transform transform)
    {
        return transform.GetComponentsInChildren<Transform>();
    }

    public static void resetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void resetGlobal(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
    #endregion
}

