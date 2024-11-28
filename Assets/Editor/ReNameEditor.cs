using UnityEngine;
using System.Collections;
using UnityEditor;

public class ReNameEditor : EditorWindow
{
    [MenuItem("GAME/RenameWindow")]
    static void renameSequence()
    {
        ReNameEditor window = EditorWindow.GetWindow<ReNameEditor>();
        window.Show();
    }

    Object[] objs;
    string content;
    void OnGUI()
    {
        objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        content = EditorGUILayout.TextField("内容：", content);
        if(GUILayout.Button("添加前缀"))
        {
            foreach (var obj in objs)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), content + obj.name);
            }
            AssetDatabase.Refresh();
        }

        if(GUILayout.Button("删除前缀"))
        {
            foreach (var obj in objs)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), obj.name.TrimStart(content.ToCharArray()));
            }
            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("添加后缀"))
        {
            foreach (var obj in objs)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), obj.name + content);
            }
            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("删除后缀"))
        {
            foreach (var obj in objs)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), obj.name.TrimEnd(content.ToCharArray()));
            }
            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("转全小写"))
        {
            foreach (var obj in objs)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), obj.name.ToLower());
            }
            AssetDatabase.Refresh();
        }

        EditorGUILayout.LabelField("当前选择的物体：");
        foreach (var obj in objs)
        {
            EditorGUILayout.ObjectField(obj, typeof(Object));
        }
    }

    void OnSelectionChange()
    {
        this.Repaint();
    }
}
