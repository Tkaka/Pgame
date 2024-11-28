using UnityEngine;

public class ActorPlayerMC : ActorMC
{
    public ActorPlayerMC(int temlateId, ActorType type, ActorCamp camp, long roleId) : base(temlateId, type, camp, roleId)
    {

    }

    public void SetRotByJoystick(float degree)
    {
        Vector3 nowRot = TransformExt.rotation.eulerAngles;
        TransformExt.rotateY(degree - nowRot.y);
        Vector3 dir = Camera.main.transform.TransformDirection(TransformExt.forward);
        dir.y = 0;
        setDirection(dir);
    }

}
