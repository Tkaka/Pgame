using UnityEngine;

public interface IMoveAble
{
    /// <summary>
    /// 往正前方移动
    /// </summary>
    void Move();

    /// <summary>
    /// 按所给方向移动
    /// </summary>
    /// <param name="dir"></param>
    void Move(Vector3 dir);

    void MoveTo(Vector3 targetPos);

    void SetVelocity(float v);

}
