using UnityEngine;

public class ActorFooter : BaseBehaviour
{

    public float speed = 3;

    public float uiZRot = -20;

    public void ResetForUI()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, uiZRot)); 
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.up, speed, Space.Self);
    }


}
