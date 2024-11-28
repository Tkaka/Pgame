/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.20
*/

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PlayerBuilder
{
    #region 是否为开发包
    private static bool developmentBuild = false;
    [MenuItem("Tools/Player/DevelopmentBuild", false, 1)]
    public static void diviceFolder()
    {
        developmentBuild = EditorPrefs.GetBool("PlayerBuilder_develop", false);
        developmentBuild = !developmentBuild;
        EditorPrefs.SetBool("PlayerBuilder_develop", developmentBuild);
        Menu.SetChecked("Tools/Player/DevelopmentBuild", developmentBuild);
    }

    [MenuItem("Tools/Player/DevelopmentBuild", true, 1)]
    public static bool diviceFolderOption()
    {
        developmentBuild = EditorPrefs.GetBool("PlayerBuilder_develop", false);
        Menu.SetChecked("Tools/Player/DevelopmentBuild", developmentBuild);
        return true;
    }
    #endregion


    [MenuItem("Tools/Player/出Android包 #&A", false, 1)]
    public static void BuildAndroid()
    {
        BuildOutPlayer(BuildTarget.Android);
    }

    [MenuItem("Tools/Player/出IOS包 #&I", false, 2)]
    public static void BuildIOS()
    {
        BuildOutPlayer(BuildTarget.iOS);
    }

    [MenuItem("Tools/Player/整理更新资源", false, 201)]
    public static void BuildUpdateLuaAndBin()
    {
        if(EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("提示", "请等待编译结束再执行操作", "确定");
            return;
        }

        //lua/bean/依赖  分别打到一个文件里面，不再挨个打ab
        string outputPath = PathUtil.GetForceABPath() + "../ToUpdate/";
        EditorUtility.DisplayProgressBar("打包", "打包更新lua", 0);
        buildDirToAB(Application.dataPath + "/Update/Lua", outputPath, "*.lua", "_fix_" + PathUtil.UpdateLuaSuffix, true);
        EditorUtility.DisplayProgressBar("打包", "打包更新配置表", 0.3f);
        buildDirToAB(Application.dataPath + "/Update/Bin", outputPath, "*.bytes", "_fix_" + PathUtil.UpdateBeanSuffix, true);
        EditorUtility.DisplayProgressBar("打包", "整理更新ab", 0.6f);
        MergeABDependence(Application.dataPath + "/Update/AB/", outputPath, "_fix_" + PathUtil.UpdateDepSuffix);
        EditorUtility.DisplayProgressBar("打包", "拷贝ab", 0.8f);
        var files = Directory.GetFiles(Application.dataPath + "/Update/AB/", "*" + PathUtil.abSuffix, SearchOption.AllDirectories);
        foreach(var file in files)
            File.Copy(file, outputPath + Path.GetFileName(file), true);
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
        System.Diagnostics.Process.Start(outputPath);
    }

    private static void BuildOutPlayer(BuildTarget target)
    {
        if(EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("提示", "请等待编译结束再执行操作", "确定");
            return;
        }
        if(EditorUserBuildSettings.activeBuildTarget != target)
        {
            EditorUtility.DisplayDialog("提示", string.Format("当前平台不是{0}, 需要先切换到对应平台才能出包", target), "确定");
            return;
        }

        if(target == BuildTarget.Android)
        {
            if (Directory.Exists(Application.dataPath + "/Resources/UITextures_IOS"))
                Directory.Delete(Application.dataPath + "/Resources/UITextures_IOS", true);
        }else if(target == BuildTarget.iOS)
        {
            if (Directory.Exists(Application.dataPath + "/Resources/UITextures"))
                Directory.Delete(Application.dataPath + "/Resources/UITextures", true);
        }

        //EditorUtility.DisplayProgressBar("打包", "打包lua", 0);
        //buildDirToAB(Application.dataPath + "/XLua/Lua", PathUtil.GetABBuildinPath(), "*.lua", PathUtil.LuaScriptsBundleName, true);
        EditorUtility.DisplayProgressBar("打包", "打包配置表", 0.3f);
        buildDirToAB(Application.dataPath + "/Bin", PathUtil.GetABBuildinPath(), "*.bytes", PathUtil.ConfigBundleName, true);
        AssetDatabase.Refresh();
        MergeABDependence(PathUtil.GetForceABPath(), PathUtil.GetABBuildinPath(), PathUtil.BuildinDepConfName);
        //拷贝ab
        EditorUtility.DisplayProgressBar("打包", "拷贝ab", 0.6f);
        var files = Directory.GetFiles(PathUtil.GetForceABPath(), "*" + PathUtil.abSuffix, SearchOption.AllDirectories);
        foreach(var file in files)
            File.Copy(file, PathUtil.GetABBuildinPath(Path.GetFileName(file)), true);

        //场景
        List<string> scenes = new List<string>();
        foreach(EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if(e != null && e.enabled)
                scenes.Add(e.path);
        }

        //输出路径
        string outputPath = Application.dataPath + "/../build_out/" + target.ToString().ToLower() + "/";
        DirectoryInfo di = new DirectoryInfo(outputPath);
        if(!di.Exists)
            di.Create();

        string timeStr = System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
        string fullPath = di.FullName + PlayerSettings.productName + "_" + timeStr;
        if(target == BuildTarget.Android)
            fullPath += "_unformal.apk";
        AssetDatabase.Refresh();

        //打包
        developmentBuild = EditorPrefs.GetBool("PlayerBuilder_develop", false);
        BuildOptions option = BuildOptions.CompressWithLz4;
        if(developmentBuild)
            option = BuildOptions.CompressWithLz4 | BuildOptions.Development;
        string ret = BuildPipeline.BuildPlayer(scenes.ToArray(), fullPath, target, option);

        //删除拷贝的文件
        if(Directory.Exists(Application.dataPath + "/StreamingAssets/ab"))
            Directory.Delete(Application.dataPath + "/StreamingAssets/ab", true);
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();

        System.Diagnostics.Process.Start(outputPath);
        Debuger.Log("打包完成...", ret);
    }

    /// <summary>
    /// 合并依赖
    /// </summary>
    private static void MergeABDependence(string inputPath, string outPath, string depName)
    {
        EditorUtility.DisplayProgressBar("打包", "合并资源依赖", 0);
        if(!Directory.Exists(outPath))
            Directory.CreateDirectory(outPath);

        string tempPath = Application.dataPath + "/" + Path.GetFileNameWithoutExtension(depName) + ".bytes";
        if(File.Exists(tempPath))
            File.Delete(tempPath);

        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        var files = Directory.GetFiles(inputPath, "*.d", SearchOption.AllDirectories);
        if(files.Length == 0)
            return;

        writer.Write((Int32)files.Length);
        int len = 0;
        foreach(var file in files)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            var arr = ResDepManager.Singleton.GetDependence(name);
            if(arr == null)
                continue;
            len++;
            writer.Write(name);
            writer.Write((Int16)arr.Length);
            foreach(var s in arr)
                writer.Write(s);
        }
        writer.Seek(0, SeekOrigin.Begin);
        writer.Write((Int32)files.Length);
        File.WriteAllBytes(tempPath, stream.ToArray());
        stream.Close();
        writer.Close();
        AssetDatabase.Refresh();

        AssetBundleBuild abb = new AssetBundleBuild();
        abb.assetBundleName = depName;
        abb.assetNames = new string[] { tempPath.Substring(tempPath.IndexOf("Assets/")) };
        BuildPipeline.BuildAssetBundles(outPath, new AssetBundleBuild[] { abb }, ABBuilder.buildOptions, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();

        File.Delete(tempPath);
        DirectoryInfo info = new DirectoryInfo(outPath);
        File.Delete(outPath + info.Name);
        File.Delete(outPath + info.Name + ".manifest");
        File.Delete(outPath + depName + ".manifest");
        EditorUtility.DisplayProgressBar("打包", "合并资源", 1);
    }

    //拷贝文件
    public static void buildDirToAB(string inPath, string outPath, string buildExtension, string outName = "", bool oneBuild = false)
    {
        DirectoryInfo outDir = new DirectoryInfo(outPath);
        if(!outDir.Exists)
            outDir.Create();

        List<string> buildList = new List<string>();
        string destDir = Application.dataPath + "/buildTmp/";
        Directory.CreateDirectory(destDir);
        string[] files = Directory.GetFiles(inPath, buildExtension, SearchOption.AllDirectories);
        for(int i = 0; i < files.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(files[i]);
            string dest = destDir + fileName + PathUtil.bytesSuffix;
            File.Copy(files[i], dest, true);
            buildList.Add(dest.Substring(dest.LastIndexOf("Assets/buildTmp/")));
        }
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

        //打成一个bundle
        AssetBundleBuild abd = new AssetBundleBuild();
        abd.assetBundleName = outName;
        abd.assetNames = buildList.ToArray();
        BuildPipeline.BuildAssetBundles(outPath, new AssetBundleBuild[] { abd }, ABBuilder.buildOptions, EditorUserBuildSettings.activeBuildTarget);

        AssetDatabase.Refresh();
        //删除临时目录
        Directory.Delete(Application.dataPath + "/buildTmp/", true);
        //删除多余的bundle文件
        foreach(var file in Directory.GetFiles(outPath))
        {
            if(!file.EndsWith(PathUtil.bytesSuffix)
                && !file.EndsWith(PathUtil.abSuffix)
                && !file.EndsWith(PathUtil.UpdateDepSuffix)
                && !file.EndsWith(PathUtil.UpdateBeanSuffix)
                && !file.EndsWith(PathUtil.UpdateLuaSuffix))
                File.Delete(file);
        }
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
    }
}
