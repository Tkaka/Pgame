
using UnityEngine;

public class TL_Event  : MonoBehaviour
{

    private void OnEnable()
    {
        GED.ED.dispatchEvent(EventID.TimelineEvt);
    }

}
