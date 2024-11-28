/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.09.22
*/

using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ABBuilder
{
    public static BuildAssetBundleOptions buildOptions = BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression;

    private static void initPath()
    {
        if(!Directory.Exists(outPath))
            Directory.CreateDirectory(outPath);
    }

    [MenuItem("Tools/AB/修改打包ab输出路径", false, 200)]
    public static void SetABOutPath()
    {
        string defaultPath = Application.dataPath + "/../ABOut";
        string path = EditorPrefs.GetString(AB_Path, defaultPath);
        if(path == defaultPath)
            path = Path.GetFullPath(path);
        string selectPath = EditorUtility.OpenFolderPanel("选择打包输出路径", path, "");
        if(!string.IsNullOrEmpty(selectPath))
            path = selectPath;
        EditorPrefs.SetString(AB_Path, path);
        Debug.LogWarning("ab路径修改为->" + path);
    }
    private const string AB_Path = "Editor_AB_Out_Path";
    private static string outPath
    {
        get
        {
#if UNITY_ANDROID
            string defaultDic = Application.dataPath + "/../ABOut/Android";
#elif UNITY_IPHONE
            string defaultDic = Application.dataPath + "/../ABOut/IOS";
#else
            string defaultDic = Application.dataPath + "/../ABOut/Other";
#endif
            return EditorPrefs.GetString(AB_Path, defaultDic);
        }
    }

    private static List<string> shaderList = new List<string>();
    /// 获取shaderList
    private static void getShaderList()
    {
        shaderList.Clear();
        SerializedObject graphicsSettings = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/GraphicsSettings.asset")[0]);
        SerializedProperty it = graphicsSettings.GetIterator();
        while(it.NextVisible(true))
        {
            if(it.name == "m_AlwaysIncludedShaders")
            {
                for(int i=0, len = it.arraySize; i<len; ++i)
                {
                    var data = it.GetArrayElementAtIndex(i);
                    if(data.objectReferenceValue != null)
                        shaderList.Add(data.objectReferenceValue.name);
                }
                break;
            }
        }
    }

    [MenuItem("Tools/AB/选中资源打包AB #&C", false, 201)]
    public static void BuildAB()
    {
        EditorUtility.DisplayProgressBar("提示", "准备打包", 0);
        Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        if(objs == null || objs.Length == 0)
        {
            EditorUtility.DisplayDialog("提示", "当前选择无效,无法打包\n请选择Assets目录下的资源或者预制件", "确定");
            return;
        }

        initPath();

        EditorUtility.DisplayProgressBar("准备", "开始打包", 0);
        getShaderList();
        BuildOneAB(objs);

        EditorUtility.DisplayProgressBar("结束", "打包完成", 1);
        EditorUtility.ClearProgressBar();
        System.Diagnostics.Process.Start(outPath);
        Debug.Log("打包完成, 路径-> " + outPath);
    }

    /// <summary>
    /// 打包打个文件，包含依赖
    /// </summary>
    public static void BuildOneAB(Object[] objs)
    {
        //检查是否包含Unity自带资源
        if(checkHasDefaultRes(objs))
            return;

        List<Object> inBuildList = new List<Object>();
        string alwaysPath = "Assets/ArtResources/ArtBase/Effect";
        string pathRoot = "Assets/ArtResources/OutPut/";
        List<AssetBundleBuild> buildList = new List<AssetBundleBuild>();
        foreach(var obj in objs)
        {
            HookCreater.CreateObjHook(obj as GameObject);

            string path = AssetDatabase.GetAssetPath(obj);
            if(path.StartsWith(pathRoot + "UI"))
            {
                if(path.StartsWith(pathRoot + "UI/UIWindow"))
                {
                    //UI分为界面和资源     文件夹不管
                    if (Directory.Exists(Application.dataPath + path.Substring(6)))
                        continue;

                    string prePath = path.Substring(0, path.Length - Path.GetFileName(path).Length);
                    string[] arr = obj.name.Split('@');
                    string ui = arr[0];
                    if (ui == obj.name)
                    {
                        //界面    
                        AssetBundleBuild abBuild = new AssetBundleBuild();
                        abBuild.assetBundleName = obj.name.ToLower() + PathUtil.abSuffix;
                        abBuild.assetNames = new string[] { path };
                        buildList.Add(abBuild);
                    }
                    else
                    {
                        //资源
                        List<string> list = new List<string>();
                        if (File.Exists(Application.dataPath + prePath.Substring(6) + ui + "@sprites.bytes"))
                            list.Add(prePath + ui + "@sprites.bytes");

                        for (int i = 0; i < 100; ++i)
                        {
                            if (File.Exists(Application.dataPath + prePath.Substring(6) + ui + "@atlas" + i + ".png"))
                                list.Add(prePath + ui + "@atlas" + i + ".png");
                            else
                                break;
                            if (File.Exists(Application.dataPath + prePath.Substring(6) + ui + "@atlas" + i + "!a.png"))
                                list.Add(prePath + ui + "@atlas" + i + "!a.png");
                        }
                        AssetBundleBuild abBuild = new AssetBundleBuild();
                        abBuild.assetBundleName = ui + "_res" + PathUtil.abSuffix;
                        abBuild.assetNames = list.ToArray();
                        buildList.Add(abBuild);
                    }
                }
                else if(path.StartsWith(pathRoot + "UI/UISingle"))
                {
                    //排除文件夹
                    if (Directory.Exists(Application.dataPath + path.Substring(6)))
                        continue;

                    //大的UI散图
                    string suffix = Path.GetExtension(path).ToLower();
                    if(suffix == ".png" || suffix == ".jpg" || suffix == ".jpeg" || suffix == ".tga")
                    {
                        AssetBundleBuild abBuild = new AssetBundleBuild();
                        abBuild.assetBundleName = obj.name.ToLower() + PathUtil.abSuffix;
                        abBuild.assetNames = new string[] { path };
                        buildList.Add(abBuild);
                    }
                }
                else
                {
                    //ui纹理 不是文件夹不管
                    if (!Directory.Exists(Application.dataPath + path.Substring(6)))
                        continue;

                    List<string> list = new List<string>();
                    DirectoryInfo info = new DirectoryInfo(Application.dataPath + path.Substring(6));
                    var files = info.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                    foreach(var file in files)
                    {
                        if (!file.Name.EndsWith(".meta"))
                        {
                            string filePath = file.FullName.Substring(Application.dataPath.Length - 6);
                            list.Add(filePath);
                        }
                    }
                    AssetBundleBuild abBuild = new AssetBundleBuild();
                    abBuild.assetBundleName = obj.name.ToLower() + PathUtil.abSuffix;
                    abBuild.assetNames = list.ToArray();
                    buildList.Add(abBuild);
                }
            }
            else if(path.StartsWith(pathRoot + "Scene"))
            {
                //场景全部拆分
                //不是预制件不管
                if (false == path.ToLower().EndsWith(".prefab"))
                    continue;

                string[] deps = AssetDatabase.GetDependencies(path, true);
                foreach (var dep in deps)
                {
                    if (!Buildable(AssetDatabase.LoadAssetAtPath<Object>(dep)))
                        continue;
                    
                    //剔除材质球
                    Object depObj = AssetDatabase.LoadAssetAtPath<Object>(dep);
                    if (depObj == null || depObj is Material || depObj is TextAsset)
                        continue;

                    if (depObj != obj && dep.ToLower().EndsWith(".prefab"))
                    {
                        EditorUtility.DisplayDialog("提示", "不允许GameObject被依赖的情况" + obj.name + ">" + depObj.name, "确定");
                        return;
                    }

                    if (!inBuildList.Contains(depObj))
                    {
                        inBuildList.Add(depObj);
                        string depName = Path.GetFileNameWithoutExtension(dep);
                        AssetBundleBuild abb = new AssetBundleBuild();
                        abb.assetBundleName = depName.ToLower() + PathUtil.abSuffix;
                        abb.assetNames = new string[] { dep };
                        buildList.Add(abb);
                    }
                }
            }
            else if (path.StartsWith(pathRoot + "Model"))
            {
                //模型分为三部分：预制件，贴图，其他

                //不是预制件不管
                if (false == path.ToLower().EndsWith(".prefab"))
                    continue;

                string[] deps = AssetDatabase.GetDependencies(path, true);
                List<string> otherList = new List<string>();
                foreach (var dep in deps)
                {
                    if (!Buildable(AssetDatabase.LoadAssetAtPath<Object>(dep)))
                        continue;
                    
                    string depName = Path.GetFileNameWithoutExtension(dep);
                    Object depObj = AssetDatabase.LoadAssetAtPath<Object>(dep);
                    if (depObj == null)
                        continue;

                    if (depObj != obj && dep.ToLower().EndsWith(".prefab"))
                    {
                        if(obj.name != "battle_character_man_tl" )
                        {
                            //timeline的特殊模型
                            EditorUtility.DisplayDialog("提示", "不允许GameObject被依赖的情况" + obj.name + ">" + depObj.name, "确定");
                            return;
                        }
                    }

                    if (depObj is Texture || (depObj is GameObject && !dep.ToLower().EndsWith(".fbx")) || dep.StartsWith(alwaysPath))
                    {
                        if (!inBuildList.Contains(depObj))
                        {
                            inBuildList.Add(depObj);
                            AssetBundleBuild abBuild = new AssetBundleBuild();
                            abBuild.assetBundleName = depName.ToLower() + PathUtil.abSuffix;
                            abBuild.assetNames = new string[] { dep };
                            buildList.Add(abBuild);
                        }
                    }else if(depObj is TextAsset)
                    {
                        //逻辑拆分打到Prefab一起
                    }
                    else
                    {
                        otherList.Add(dep);
                    }
                }

                //其他类型统一放到ab
                if(otherList.Count > 0)
                {
                    AssetBundleBuild abb = new AssetBundleBuild();
                    string otherAbName = obj.name.ToLower().Substring(obj.name.IndexOf("_") + 1) + "_other_res";
                    abb.assetBundleName = otherAbName + PathUtil.abSuffix;
                    abb.assetNames = otherList.ToArray();
                    
                    bool added = false;
                    foreach (var build in buildList)
                    {
                        if (build.assetBundleName == abb.assetBundleName)
                        {
                            added = true;
                            break;
                        }
                    }
                    if (!added)
                        buildList.Add(abb);
                }
            }
            else if (path.StartsWith(pathRoot + "Effect"))
            {
                //特效只有材质球不拆分
                string[] deps = AssetDatabase.GetDependencies(path, true);
                foreach (var dep in deps)
                {
                    if (!Buildable(AssetDatabase.LoadAssetAtPath<Object>(dep)))
                        continue;

                    //剔除材质球和动画
                    Object depObj = AssetDatabase.LoadAssetAtPath<Object>(dep);
                    if (depObj == null || depObj is AnimationClip || depObj is TextAsset)
                        continue;

                    if (depObj != obj && dep.ToLower().EndsWith(".prefab"))
                    {
                        EditorUtility.DisplayDialog("提示", "不允许GameObject被依赖的情况" + obj.name + ">" + depObj.name, "确定");
                        return;
                    }

                    if (!inBuildList.Contains(depObj))
                    {
                        inBuildList.Add(depObj);
                        string depName = Path.GetFileNameWithoutExtension(dep);
                        AssetBundleBuild abb = new AssetBundleBuild();
                        abb.assetBundleName = depName.ToLower() + PathUtil.abSuffix;
                        abb.assetNames = new string[] { dep };
                        buildList.Add(abb);
                    }
                }
            }
            else if(path.StartsWith(pathRoot + "Other"))
            {
                //其他按默认规则走，无特殊命名则全部拆分
                if (Directory.Exists(Application.dataPath + path.Substring(6)) && path.EndsWith("_wjj"))
                {
                    //文件夹打包
                    DirectoryInfo dir = new DirectoryInfo(Application.dataPath + path.Substring(6));
                    List<string> fileInFolder = new List<string>();
                    foreach (var file in dir.GetFiles("*.*", SearchOption.TopDirectoryOnly))
                    {
                        string assetPath = file.FullName.Substring(Application.dataPath.Length - 6);
                        if (Buildable(AssetDatabase.LoadAssetAtPath<Object>(assetPath)))
                        {
                            fileInFolder.Add(assetPath);
                        }
                    }

                    AssetBundleBuild abb = new AssetBundleBuild();
                    abb.assetBundleName = obj.name.ToLower() + PathUtil.abSuffix;
                    abb.assetNames = fileInFolder.ToArray();
                    buildList.Add(abb);
                    continue;
                }
                if (!Buildable(obj))
                    continue;

                string[] deps = AssetDatabase.GetDependencies(path, true);
                List<string> depList = new List<string>();
                foreach (var dep in deps)
                {
                    if (!Buildable(AssetDatabase.LoadAssetAtPath<Object>(dep)))
                        continue;

                    var depObj = AssetDatabase.LoadAssetAtPath<Object>(dep);
                    if (depObj != obj && dep.ToLower().EndsWith(".prefab"))
                    {
                        EditorUtility.DisplayDialog("提示", "不允许GameObject被依赖的情况" + obj.name + ">" + depObj.name, "确定");
                        return;
                    }

                    string folder = Path.GetDirectoryName(path);
                    if (folder.EndsWith("_wjj"))
                        continue;

                    string depName = Path.GetFileNameWithoutExtension(dep);
                    if (depName.EndsWith("_bcf") || dep == path)
                    {
                        //不拆分的资源
                        depList.Add(dep);
                    }else if(depName.EndsWith("_bdb"))
                    {
                        //不打包的资源
                        continue;
                    }
                    else
                    {
                        AssetBundleBuild abb = new AssetBundleBuild();
                        abb.assetBundleName = depName.ToLower() + PathUtil.abSuffix;
                        abb.assetNames = new string[] { dep };
                        buildList.Add(abb);
                    }
                }

                //批量打包
                AssetBundleBuild abBuild = new AssetBundleBuild();
                abBuild.assetBundleName = obj.name.ToLower() + PathUtil.abSuffix;
                abBuild.assetNames = depList.ToArray();
                buildList.Add(abBuild);
            }
        }

        foreach(var build in buildList)
        {
            foreach(var build2 in buildList)
            {
                foreach(var name in build2.assetNames)
                {
                    if (build.assetBundleName == build2.assetBundleName)
                        continue;

                    foreach(var name2 in build.assetNames)
                    {
                        if(name == name2)
                        {
                            Debuger.Err("----一个资源去了多个ab", name, build.assetBundleName, build2.assetBundleName);
                        }
                    }
                }
            }
        }
        BuildPipeline.BuildAssetBundles(outPath, buildList.ToArray(), buildOptions, EditorUserBuildSettings.activeBuildTarget);
        ChangeDependenceConf();
    }

    private static bool checkException(Dictionary<Object, string> buildMap, Object obj, string abName)
    {
        return false;
    }

    /// <summary>
    /// 是否包含Unity自带资源(包含引用)
    /// </summary>
    private static bool checkHasDefaultRes(Object[] objs)
    {
        if(objs == null || objs.Length == 0) return true;
        foreach(var obj in objs)
        {
            var depObjs = EditorUtility.CollectDependencies(new Object[] { obj });
            foreach(var dep in depObjs)
            {
                if(dep is Shader)
                {
                    //shader
                    if(!shaderList.Contains(dep.name))
                    {
                        EditorUtility.DisplayDialog("提示", obj.name + " 引用shader: " + dep.name + "\n请先将shader加入Edit->ProjectSettings->Graphics->AlwaysIncludedShaders然后重新打包", "确定");
                        return true;
                    }
                    if(dep.name == "Standard")
                    {
                        EditorUtility.DisplayDialog("提示", obj.name + " 引用了Standard.shader，请用其他shader替换standard", "确定");
                        return true;
                    }
                } else if(AssetDatabase.GetAssetPath(dep).ToLower().Contains("unity_builtin_extra"))
                {
                    if(dep is Material)
                    {
                        //材质球暂时允许
                        Debuger.Wrn("资源引用Unity自带资源，材质球 > ", dep.name);
                    } else
                    {
                        Debuger.Err("资源引用Unity自带资源，请改后重新打包");
                        EditorUtility.DisplayDialog("提示", string.Format("资源引用Unity自带资源，请改后重新打包\n{0} 包含 {1}", obj.name, dep.name), "确定");
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 改变依赖文件格式
    /// </summary>
    private static void ChangeDependenceConf()
    {
        var abPath = outPath + "/" + Path.GetFileName(outPath).ToLower();
        AssetBundle ab = AssetBundle.LoadFromFile(abPath);
        if(ab == null) return;
        var abm = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        if(abm == null) return;
        var arr = abm.GetAllAssetBundles();
        foreach(var name in arr)
        {
            var deps = abm.GetDirectDependencies(name);
            for(int i = 0; i < deps.Length; ++i)
                deps[i] = Path.GetFileNameWithoutExtension(deps[i]);
            if(deps.Length > 0)
                File.WriteAllLines(outPath + "/" + Path.GetFileNameWithoutExtension(name) + ".d", deps);
        }
        ab.Unload(true);

        File.Delete(abPath);
        File.Delete(abPath + ".manifest");
    }

    /// <summary>
    /// 是否可以被打包
    /// </summary>
    public static bool Buildable(Object obj, bool log = true)
    {
        string path = AssetDatabase.GetAssetPath(obj);
        if(path.ToLower().EndsWith(".asset"))
        {
            Object depObj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            if(depObj.GetType().FullName.Contains("UnityEditor"))
            {
                if(log)
                    Debuger.Log("Editor Only资源不能打包，跳过 > ", path);
                return false;
            }
        }
        return Buildable(path);
    }

    /// <summary>
    /// 是否可以被打包
    /// </summary>
    public static bool Buildable(string path)
    {
        string name = Path.GetFileName(path).ToLower();
        if (name.EndsWith(".png")          //纹理
            || name.EndsWith(".jpg")          //纹理
            || name.EndsWith(".jpeg")          //纹理
            || name.EndsWith(".tga")          //纹理
            || name.EndsWith(".psd")          //纹理

            || name.EndsWith(".mat")          //材质

            || name.EndsWith(".ogg")          //声音
            || name.EndsWith(".mp3")          //声音
            || name.EndsWith(".wav")          //声音

            || name.EndsWith(".mp4")          //视频
            || name.EndsWith(".flv")          //视频

            || name.EndsWith(".prefab")       //预制件
            || name.EndsWith(".anim")         //动画
            || name.EndsWith(".controller")   //动画控制器
            || name.EndsWith(".mesh")         //mesh文件
            || name.EndsWith(".bytes")        //二进制文件
            || name.EndsWith(".asset")        //其它文件

            || name.EndsWith(".ttf")        //字体文件
            || name.EndsWith(".fontsettings") //图片字体文件

            || name.EndsWith(".fbx")            //fbx
            //|| name.EndsWith(".unity")      //场景
            //|| name.EndsWith(".exr")        //场景光照贴图
            )
        {
            return true;
        }
        return false;
    }
}
