/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 *  Coder：Zhou XiQuan
 *  Time ：2016.04.18
*/

using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class PrefabSceneHolder : MonoBehaviour
{
    public string holderTag = "";
    public bool autoSet = false;
    public bool asyncRevert = false;
    public GameObject[] enableObjs;
    public GameObject[] disableObjs;

    [System.Serializable]
    public class LightMapInfo
    {
        public Renderer render;
        public int lightMapIdx;
        public Vector4 lightMapScaleOffset;

        //实时光支持
        //public int realLightMapIdx; 
        //public Vector4 realLightMapScaleOffset;
    }

    [System.Serializable]
    public class DynamicSceneItem
    {
        [System.NonSerialized]
        public bool loaded;
        [System.NonSerialized]
        public bool showing;
        [System.NonSerialized]
        public GameObject obj;

        public Bounds bounds;
        public string resName;
    }

    public DynamicSceneItem[] sceneItems;
    private Dictionary<string, DynamicSceneItem> dynamicMap = new Dictionary<string, DynamicSceneItem>();


    //烘焙光照贴图
    public LightmapsMode lmMode;
    //public string[] lightMapTexArr;
    public Texture2D[] lightMapTexArr;
    public LightMapInfo[] lightMapRenderArr;

    //RenderSettings 雾
    public bool fog;
    public FogMode fogMode;
    public Color fogColor;
    public float fogDensity;
    public float fogStartDistance;
    public float fogEndDistance;

    ////RenderSettings 环境光
    public AmbientMode ambientMode;
    public Color ambientSkyColor;
    public Color ambientEquatorColor;
    public Color ambientGroundColor;
    public Color ambientLight;
    public float ambientIntensity;

    public static PrefabSceneHolder currentHolder { get; private set; }
    private static Dictionary<string, PrefabSceneHolder> holderMap = new Dictionary<string, PrefabSceneHolder>();

    /// <summary>
    /// 还原当前场景光照
    /// </summary>
    /// <param name="tag">光照tag</param>
    public static void ResetCurrentScene(string tag = null)
    {
        if(temporariy)
        {
            hasReal = true;
            realTag = tag;
        } else
        {
            hasReal = false;
            setTag(tag);
        }
    }

    private static bool temporariy;
    private static string realTag;
    private static bool hasReal;
    /// <summary>
    /// 临时改变光照贴图
    /// </summary>
    /// <param name="changeOrBack">改变true，还原false</param>
    /// <param name="tag">如果是改变才有效，光照tag</param>
    public static void ChangeTagTemporarily(bool changeOrBack, string tag = "")
    {
        if(changeOrBack)
        {
            if(currentHolder != null && !hasReal)
            {
                hasReal = true;
                realTag = currentHolder.holderTag;
            }
            temporariy = true;
            setTag(tag);
        }else
        {
            if(hasReal)
                setTag(realTag);
            hasReal = false;
            temporariy = false;
        }
    }

    private static void setTag(string tag)
    {
        if (!Application.isPlaying)
        {
            holderMap.Clear();
            var holders = GameObject.FindObjectsOfType<PrefabSceneHolder>();
            foreach(var hold in holders)
                holderMap[hold.tag] = hold;
        }

        if (currentHolder != null)
        {
            if(string.IsNullOrEmpty(tag) || tag == currentHolder.holderTag)
                return;

            currentHolder.returnResRefrence();
            currentHolder = null;
        }

        var enu = holderMap.GetEnumerator();
        PrefabSceneHolder holder = null;
        while(enu.MoveNext())
        {
            holder = enu.Current.Value;
            //空tag或者tag相同则还原
            if(string.IsNullOrEmpty(tag) || tag == holder.holderTag)
            {
                currentHolder = holder;
                holder.resetScene();
                break;
            }
        }
        enu.Dispose();

        if(currentHolder == null && holderMap.Count > 0)
        {
            Debuger.Err("配置错误 --------- 当前场景找不到光照信息标签 > "+ tag + "强制设置为空");
            setTag("");
        }
    }

    public static void UpdateItems(Vector3 pos, float distance)
    {
        if(currentHolder != null)
            currentHolder.UpdateDynamicItems(pos, distance);
    }

    private void UpdateDynamicItems(Vector3 pos, float distance)
    {
        if(sceneItems == null) return;

        DynamicSceneItem item = null;
        var enu = dynamicMap.GetEnumerator();
        while(enu.MoveNext())
        {
            item = enu.Current.Value;
            if(item.showing)
            {
                //隐藏
                if(item.bounds.SqrDistance(pos) > distance)
                {
                    item.showing = false;
                    if(item.obj)
                        item.obj.SetActive(false);
                }
            } else
            {
                //显示
                if(item.bounds.SqrDistance(pos) < distance)
                {
                    item.showing = true;
                    if(!item.loaded)
                    {
                        //ResourceAskInfo ask = ClassCacheManager.New<ResourceAskInfo>();
                        //ask.resName = item.resName;
                        //ask.callback = _OnItemLoaded;
                        //Resource.Singleton.Request(ref ask);
                    } else if(item.obj != null)
                    {
                        item.obj.SetActive(true);
                    }
                }
            }
        }
        enu.Dispose();
    }

    private void _OnItemLoaded(string res, string dep, System.Type type)
    {
        if(currentHolder != this)
            return;

        if(!dynamicMap.ContainsKey(res))
            return;

        /*var obj = Resource.Singleton.GetLoadedObj(res, null, null) as GameObject;
        if(obj != null)
            obj.SetActive(dynamicMap[res].showing);*/
        dynamicMap[res].loaded = true;
        //dynamicMap[res].obj = obj;
    }

    void Awake()
    {
        holderMap[holderTag] = this;
        if(autoSet)
            ResetCurrentScene(holderTag);
    }

    private void resetScene()
    {
        if(enableObjs != null)
        {
            for(int i=0, len = enableObjs.Length; i < len; ++i )
            {
                if(enableObjs[i])
                    enableObjs[i].SetActive(true);
            }
        }

        if(disableObjs != null)
        {
            for(int i=0, len = disableObjs.Length; i < len; ++i)
            {
                if(disableObjs[i])
                    disableObjs[i].SetActive(false);
            }
        }

        if(dynamicMap.Count == 0 && sceneItems != null)
        {
            for(int i = 0, len = sceneItems.Length; i < len; ++i)
                dynamicMap[sceneItems[i].resName] = sceneItems[i];
        }

        //if(asyncRevert)
        //    askLightTextures();
        //else
            resetLightMapSetting();
        resetAmbientSource();
        resetFog();

        /*var ones = gameObject.GetComponentsInChildren<UnityEngine.AI.NavMeshSurface>();
        foreach (var n in ones)
            n.BuildNavMesh();*/
    }

    //还原环境光
    private void resetAmbientSource()
    {
        LightmapSettings.lightmapsMode = lmMode;
        RenderSettings.ambientMode = ambientMode;
        RenderSettings.ambientSkyColor = ambientSkyColor;
        RenderSettings.ambientEquatorColor = ambientEquatorColor;
        RenderSettings.ambientGroundColor = ambientGroundColor;
        RenderSettings.ambientLight = ambientLight;
        RenderSettings.ambientIntensity = ambientIntensity;
    }

    //还原雾
    private void resetFog()
    {
        RenderSettings.fog = fog;
        RenderSettings.fogMode = fogMode;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogStartDistance = fogStartDistance;
        RenderSettings.fogEndDistance = fogEndDistance;
    }

    //还原光照贴图
    private void resetLightMapSetting()
    {
        if(lightMapTexArr == null)
            return;

        LightmapData[] arr = new LightmapData[lightMapTexArr.Length / 3];
        for(int i=0, j=0; i < lightMapTexArr.Length; i = i + 3, ++j)
        {
            Texture2D tex1 = null, tex2 = null, tex3 = null;
            tex1 = lightMapTexArr[i];
            tex2 = lightMapTexArr[i + 1];
            tex3 = lightMapTexArr[i + 2];
            /*if(!string.IsNullOrEmpty(lightMapTexArr[i]))
                tex1 = Resource.Singleton.LoadObjSync(lightMapTexArr[i]) as Texture2D;
            if(!string.IsNullOrEmpty(lightMapTexArr[i + 1]))
                tex2 = Resource.Singleton.LoadObjSync(lightMapTexArr[i + 1]) as Texture2D;
            if(!string.IsNullOrEmpty(lightMapTexArr[i + 2]))
                tex3 = Resource.Singleton.LoadObjSync(lightMapTexArr[i + 2]) as Texture2D;*/
            arr[j] = new LightmapData();
            arr[j].lightmapColor = tex1;
            arr[j].lightmapDir = tex2;
            arr[j].shadowMask = tex3;
        }
        LightmapSettings.lightmaps = arr;

        //还原renderer 光照信息
        LightMapInfo info = null;
        for(int i=0, len=lightMapRenderArr.Length; i < len; ++i)
        {
            info = lightMapRenderArr[i];
            if(info.render != null)
            {
                info.render.lightmapIndex = info.lightMapIdx;
                info.render.lightmapScaleOffset = info.lightMapScaleOffset;
                //info.render.realtimeLightmapIndex = info.realLightMapIdx;
                //info.render.realtimeLightmapScaleOffset = info.realLightMapScaleOffset;
            }
        }
    }



    /*private int lightNum;
    private int lightLoaded;
    private void askLightTextures()
    {
        lightNum = 0;
        lightLoaded = 0;
        for(int i = 0; i < lightMapTexArr.Length; ++i)
        {
            if(!string.IsNullOrEmpty(lightMapTexArr[i]))
                lightNum++;
        }

        for(int i = 0; i < lightMapTexArr.Length; ++i)
        {
            if(!string.IsNullOrEmpty(lightMapTexArr[i]))
            {
                / *ResourceAskInfo ask = ClassCacheManager.New<ResourceAskInfo>();
                ask.resName = lightMapTexArr[i];
                ask.priority = ResPriority.Async;
                ask.callback = lightCB;
                Resource.Singleton.Request(ref ask);* /
            }
        }
    }

    private void lightCB(string res, string dep, System.Type type)
    {
        lightLoaded++;
        if(lightLoaded >= lightNum && currentHolder == this)
            resetLightMapSetting();
    }*/

    private void returnResRefrence()
    {
        //光照贴图
        /*if(lightMapTexArr == null)
            return;

        for(int i=0, len=lightMapTexArr.Length; i < len; ++i)
        {
            if(!string.IsNullOrEmpty(lightMapTexArr[i]))
            {
                / *Resource.Singleton.ReturnObj(lightMapTexArr[i]);
                Resource.Singleton.GCObj(lightMapTexArr[i]);* /
            }
        }

        //动态 item
        DynamicSceneItem item = null;
        var enu = dynamicMap.GetEnumerator();
        while(enu.MoveNext())
        {
            item = enu.Current.Value;
            if(item.loaded)
            {
                item.loaded = false;
                item.showing = false;
                / *Resource.Singleton.ReturnObj(item.resName);
                Resource.Singleton.GCObj(item.resName);* /
            }
            GameObject.DestroyImmediate(item.obj);
        }
        enu.Dispose();*/
    }

    void OnDestroy()
    {
        if(holderMap.ContainsKey(holderTag))
            holderMap.Remove(holderTag);
        if(currentHolder == this)
        {
            currentHolder.returnResRefrence();
            currentHolder = null;
        }
    }
}