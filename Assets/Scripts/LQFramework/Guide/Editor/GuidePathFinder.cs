using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GuidePathFinder : EditorWindow
{
    [MenuItem("Tools/Game/新手引导路径查找")]
    static void showWindow()
    {
        GuidePathFinder window = GetWindow<GuidePathFinder>("新手引导路径查找");
        window.Show();
    }

    string ret = "";
    string findName = "closeBtn";

    void OnGUI()
    {
        findName = EditorGUILayout.TextField("查找名字", findName);
        if(GUILayout.Button("查找"))
        {
            if (!Application.isPlaying)
            {
                ShowNotification(new GUIContent("运行时才有效"));
                return;
            }
            if(string.IsNullOrEmpty(findName))
            {
                ShowNotification(new GUIContent("查找内容为空"));
                return;
            }

            if (FairyGUI.GRoot.inst == null)
                return;

            var list = new List<FairyGUI.GObject>();
            find(findName, FairyGUI.GRoot.inst, list);

            ret = "";
            foreach (var obj in list)
            {
                var path = obj.name;
                var o = obj;
                while (o.parent != null)
                {
                    o = o.parent;
                    if (o == FairyGUI.GRoot.inst)
                        break;
                    path = o.name + "/" + path;
                }
                ret += path + "\n";
            }
            if (string.IsNullOrEmpty(ret))
                ret = "没有找到";
        }
        ret = EditorGUILayout.TextArea(ret);
    }

    private void find(string name, FairyGUI.GObject obj, List<FairyGUI.GObject> retList)
    {
        if (obj.name == name)
            retList.Add(obj);

        var com = obj.asCom;
        if(com != null)
        {
            foreach(var child in com.GetChildren())
            {
                find(name, child, retList);
            }
        }
    }
}