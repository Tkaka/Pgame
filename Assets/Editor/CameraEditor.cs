//using System.Reflection;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(Camera))]
//public class MyCameraEditor : Editor
//{

//    private Editor editor;
//    private Camera camera;
//    void OnEnable()
//    {
//        camera = target as Camera;
//        editor = CreateEditor(target, Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.CameraEditor", true));
//    }

//    public override void OnInspectorGUI()
//    {
//        if (editor != null)
//            editor.OnInspectorGUI();

//        if (GUI.changed)
//        {
//            Transform trans = camera.transform.Find("SceneCamera");
//            if (trans != null)
//            {
//                Camera cam = trans.GetComponent<Camera>();
//                if (cam != null)
//                    cam.fieldOfView = camera.fieldOfView;
//            }
//        }

//    }

//}
