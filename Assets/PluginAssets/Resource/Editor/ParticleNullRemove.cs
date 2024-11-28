using UnityEditor;
using UnityEngine;

public class ParticleNullRemove
{
    [MenuItem("Tools/Game/移除特效中空节点的材质球", false, 1)]
    public static void RemoveNull()
    {
        Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        if (objs == null || objs.Length == 0)
        {
            Debuger.Log("没有选中任何对象");
            return;
        }

        EditorUtility.DisplayProgressBar("开始", "请稍等", 0f);
        foreach (var obj in objs)
        {
            GameObject go = obj as GameObject;
            if(go != null)
            {
                var arr = go.GetComponentsInChildren<ParticleSystemRenderer>();
                foreach(var par in arr)
                {
                    if (par.enabled == false)
                        par.material = null;
                }
                EditorUtility.DisplayProgressBar("开始", "请稍等", 0.5f);
            }
            EditorUtility.SetDirty(obj);
        }
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayProgressBar("完成", "完成", 1f);
        Debug.Log("特效材质球移除完成");
    }
}
