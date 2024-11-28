

using UnityEngine;
using FairyGUI;

public class InputManager : SingletonTemplate<InputManager>
{


    HurtFloatingState fstate = new HurtFloatingState();
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
        }
    }


    public void ToogleSelectActor(bool flag)
    {
        if (flag)
            Stage.inst.onTouchEnd.Add(OnTouchEnd);
        else
            Stage.inst.onTouchEnd.Remove(OnTouchEnd);
    }

    private void OnTouchEnd()
    {
        if (!IsTouchOnUI())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Stage.inst.touchPosition.x, Screen.height - Stage.inst.touchPosition.y));
            if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer("Actor")))
            {
                if (hit.transform != null)
                {
                    ActorBehavior ab = hit.transform.GetComponent<ActorBehavior>();
                    if (ab != null)
                    {
                        GED.ED.dispatchEvent(EventID.OnSelectActor, ab.actorId);
                    }
                }
            }
        }
    }

    public bool IsTouchOnUI()
    {
        if (GRoot.inst.touchTarget != null && GRoot.inst.touchTarget.name != "comboToucher")
        {
            return true;
        }
        return false;
    }

}