using UnityEngine;
using Cinemachine;

public class ClampFreeLookX : MonoBehaviour
{
    CinemachineFreeLook mFreeLook;
    [Range(0, 360)]
    public float deadZoneSize = 60;

    private float originVal;

    // Use this for initialization
    void Start()
    {
        mFreeLook = GetComponent<CinemachineFreeLook>();
        originVal = mFreeLook.m_XAxis.Value;
        if (originVal < 0)
            originVal = originVal + 360;
    }


    float max1, min1, max2, min2;
    void Update()
    {
        float halfDeadZone = deadZoneSize / 2; 
        float axis = mFreeLook.m_XAxis.Value;
        if (originVal + halfDeadZone > 360)
        {
            max1 = 360;
            min1 = originVal - halfDeadZone;
            max2 = originVal + halfDeadZone - 360;
            min2 = 0;
            if (Mathf.Abs(axis - min1) < Mathf.Abs(axis - min2))
                axis = Mathf.Clamp(axis, min1, max1);
            else
                axis = Mathf.Clamp(axis, min2, max2);
        }
        else if (originVal - halfDeadZone < 0)
        {
            max1 = originVal + halfDeadZone;
            min1 = 0;
            max2 = 360;
            min2 = originVal - halfDeadZone + 360;
            if (Mathf.Abs(axis - min1) < Mathf.Abs(axis - min2))
                axis = Mathf.Clamp(axis, min1, max1);
            else
                axis = Mathf.Clamp(axis, min2, max2);
        }
        else
        {
            max1 = originVal + halfDeadZone;
            min1 = originVal - halfDeadZone;
            axis = Mathf.Clamp(axis, min1, max1);
        }
        mFreeLook.m_XAxis.Value = axis;
    }

}