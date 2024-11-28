using Cinemachine;
using Cinemachine.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CinemachineVirtualCamera))]
internal class VCamEditor : CinemachineVirtualCameraEditor
{

    CinemachineVirtualCamera vcam;

    protected override void OnEnable()
    {
        base.OnEnable();
        vcam = target as CinemachineVirtualCamera;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed)
        {
            CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
            if (brain != null)
            {
                Transform trans = brain.transform.Find("SceneCamera");
                if (trans != null)
                {
                    Camera cam = trans.GetComponent<Camera>();
                    if (cam != null && vcam != null)
                        cam.fieldOfView = vcam.m_Lens.FieldOfView;
                }
            }
        }
    }

}
