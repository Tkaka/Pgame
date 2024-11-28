/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 *  Coder：Zhou XiQuan
 *  Time ：2018.01.30
*/
using UnityEngine;

public class PrefabSceneItemHolder : MonoBehaviour
{
    public string holderTag = "default";
    public PrefabSceneHolder.LightMapInfo[] lightMapRenderArr;
    void Start()
    {
        if(PrefabSceneHolder.currentHolder == null || PrefabSceneHolder.currentHolder.holderTag != holderTag)
            return;

        PrefabSceneHolder.LightMapInfo info = null;
        for(int i = 0, len = lightMapRenderArr.Length; i < len; ++i)
        {
            info = lightMapRenderArr[i];
            if(info.render != null)
            {
                info.render.lightmapIndex = info.lightMapIdx;
                info.render.lightmapScaleOffset = info.lightMapScaleOffset;
            }
        }
    }
}