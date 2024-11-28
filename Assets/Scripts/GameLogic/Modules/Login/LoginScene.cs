using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : SingletonTemplate<LoginScene>
{
    private string curScene;
    private GameObject curSceneObj;

    private ResPack resPack = new ResPack("LoginScene");
    public void Load(string sceneName)
    {
        if (curScene == sceneName)
            return;

        curScene = sceneName;
        GameObject.DestroyImmediate(curSceneObj);
        resPack.Request(curScene, curScene, null, (a, b, t)=>
        {
            curSceneObj = resPack.GetObject(a, b, t) as GameObject;
        });
    }
	
    public void Unload()
    {
        curScene = null;
        GameObject.DestroyImmediate(curSceneObj);
        resPack.ReleaseAllRes();
    }
}