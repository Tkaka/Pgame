/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.11.28
*/

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LightMapGettingWindow : EditorWindow
{
    private bool async = false;
    private bool autoSet = true;
    private string tag = "default";
    void OnGUI()
    {
        autoSet = EditorGUILayout.Toggle("自动还原", autoSet);
        async = EditorGUILayout.Toggle("异步还原", async);
        tag = EditorGUILayout.TextField("tag", tag);
        if(GUILayout.Button("提取光照信息"))
        {
            Scene s = EditorSceneManager.GetActiveScene();
            PrefabSceneBuilder.PickSceneHolder(s, tag, autoSet, async);
            AssetDatabase.SaveAssets();
        }

        if(GUILayout.Button("还原光照信息"))
        {
            PrefabSceneHolder.ResetCurrentScene(tag);
            AssetDatabase.SaveAssets();
        }
    }
}

public class PrefabSceneBuilder
{
    [MenuItem("Tools/AB/场景光照提取", false, 1)]
    public static void ShowLightMapPickWindow()
    {
        var window = EditorWindow.GetWindow<LightMapGettingWindow>("场景光照提取", true);
        window.Show();
    }

    public static void PickSceneHolder(Scene s, string tag, bool autoSet, bool async)
    {
        GameObject[] roots = s.GetRootGameObjects();
        if(roots.Length == 0)
            return;

        GameObject go = null;
        for(int i = 0; i < roots.Length; ++i)
        {
            if(roots[i].name.ToLower() == s.name.ToLower())
            {
                go = roots[i];
                break;
            }
        }
        if(go == null)
        {
            Debug.LogError("场景中没有与场景对应的预制件" + s.name);
            return;
        }

        List<Texture2D> lmList = new List<Texture2D>();
        var lms = LightmapSettings.lightmaps;
        for(int i=0; i<lms.Length; ++i)
        {
            lmList.Add(lms[i].lightmapColor);
            lmList.Add(lms[i].lightmapDir);
            lmList.Add(lms[i].shadowMask);
        }

        List<PrefabSceneHolder.LightMapInfo> lmInfoList = new List<PrefabSceneHolder.LightMapInfo>();
        var renderArr = go.GetComponentsInChildren<Renderer>();
        foreach(var render in renderArr)
        {
            var info = new PrefabSceneHolder.LightMapInfo();
            if(render.lightmapIndex >= 0)
            {
                info.render = render;
                info.lightMapIdx = render.lightmapIndex;
                info.lightMapScaleOffset = render.lightmapScaleOffset;
                //info.realLightMapIdx = render.realtimeLightmapIndex;
                //info.realLightMapScaleOffset = render.realtimeLightmapScaleOffset;
                lmInfoList.Add(info);
            }
        }

        //tag相同的销毁
        PrefabSceneHolder[] holders = go.GetComponents<PrefabSceneHolder>();
        if(holders != null)
        {
            foreach(var holder in holders)
            {
                if(holder.holderTag == tag)
                    GameObject.DestroyImmediate(holder);
            }
        }

        //烘焙光照贴图
        PrefabSceneHolder sh = go.AddComponent<PrefabSceneHolder>();
        sh.holderTag = tag;
        sh.autoSet = autoSet;
        sh.asyncRevert = async;
        sh.lightMapRenderArr = lmInfoList.ToArray();
        sh.lmMode = LightmapSettings.lightmapsMode;

        sh.lightMapTexArr = lmList.ToArray();
        if(sh.lightMapTexArr.Length == 0)
            Debug.LogWarning("没有检测到光照贴图 >> " + s.name);

        //RenderSettings 雾
        sh.fog = RenderSettings.fog;
        sh.fogMode = RenderSettings.fogMode;
        sh.fogColor = RenderSettings.fogColor;
        sh.fogDensity = RenderSettings.fogDensity;
        sh.fogStartDistance = RenderSettings.fogStartDistance;
        sh.fogEndDistance = RenderSettings.fogEndDistance;

        //RenderSettings 环境光
        sh.ambientMode = RenderSettings.ambientMode;
        sh.ambientSkyColor = RenderSettings.ambientSkyColor;
        sh.ambientEquatorColor = RenderSettings.ambientEquatorColor;
        sh.ambientGroundColor = RenderSettings.ambientGroundColor;
        sh.ambientLight = RenderSettings.ambientLight;
        sh.ambientIntensity = RenderSettings.ambientIntensity;

        //分散节点,动态加载对象
        List<PrefabSceneHolder.DynamicSceneItem> itemList = new List<PrefabSceneHolder.DynamicSceneItem>();
        for(int i = 0; i < roots.Length; ++i)
        {
            if(roots[i].name.ToLower() != s.name.ToLower() && roots[i].name.ToLower().StartsWith(s.name.ToLower()))
            {
                GameObject obj = roots[i];
                PrefabSceneHolder.DynamicSceneItem item = new PrefabSceneHolder.DynamicSceneItem();
                GameObject.DestroyImmediate(obj.GetComponent<PrefabSceneItemHolder>());
                var itemHolder = roots[i].AddComponent<PrefabSceneItemHolder>();
                lmInfoList = new List<PrefabSceneHolder.LightMapInfo>();
                renderArr = obj.GetComponentsInChildren<Renderer>();
                foreach(var render in renderArr)
                {
                    var info = new PrefabSceneHolder.LightMapInfo();
                    if(render.lightmapIndex >= 0)
                    {
                        info.render = render;
                        info.lightMapIdx = render.lightmapIndex;
                        info.lightMapScaleOffset = render.lightmapScaleOffset;
                        lmInfoList.Add(info);
                    }
                }
                itemHolder.holderTag = tag;
                itemHolder.lightMapRenderArr = lmInfoList.ToArray();

                item.bounds = GetGameObjectViewRect(obj);
                item.resName = obj.name.ToLower();
                item.showing = false;
                item.loaded = false;
                item.obj = null;
                itemList.Add(item);
            }
        }
        if(itemList.Count > 0)
            sh.sceneItems = itemList.ToArray();

        Selection.activeObject = go;
        EditorSceneManager.MarkSceneDirty(s);
        EditorSceneManager.SaveScene(s);

        Debug.Log("提取光照信息完成 " + go.name);
    }

    private static Bounds GetGameObjectViewRect(GameObject obj)
    {
        Vector3 center = Vector3.zero;
        Renderer[] renders = obj.GetComponentsInChildren<Renderer>();
        foreach(Renderer child in renders)
            center += child.bounds.center;
        center /= renders.Length;

        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach(Renderer child in renders)
        {
            //粒子特效暂不计入区域范围
            if(!(child is ParticleSystemRenderer))
                bounds.Encapsulate(child.bounds);
        }
        bounds.center = bounds.center - obj.transform.position;

        Vector3 tmpVec = bounds.center;
        tmpVec.x /= obj.transform.lossyScale.x;
        tmpVec.y /= obj.transform.lossyScale.y;
        tmpVec.z /= obj.transform.lossyScale.z;
        bounds.center = tmpVec;

        tmpVec = bounds.size;
        tmpVec.x /= obj.transform.lossyScale.x;
        tmpVec.y /= obj.transform.lossyScale.y;
        tmpVec.z /= obj.transform.lossyScale.z;
        bounds.size = tmpVec;

        return bounds;
    }
}