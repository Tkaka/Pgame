/*
 * file VersionCompare.cs
 *
 * author: Pengmian
 * date: 2014/11/10   
 
/// <summary>
/// 版本比较工具
/// </summary>
using System;
public class VersionCompare
{
    private static readonly int MainVersion = 0;             // 主版本号索引
    private static readonly int SubVersion = 1;              // 次版本号索引
    private static readonly int PatchVersion = 2;            // 补丁版本号索引
	private static readonly int OtherVersion = 3;			 // 附加索引

    /// <summary>
    /// app版本比较 ( ret > 0 = l大于r, ret == 0 = 版本相同， ret < 0 = l小于r）
    /// </summary>
    /// <param name="server"></param>
    /// <param name="local"></param>
    public static int appVersionCompare(string l, string r)
    {
        int ret = compare(getVersion(l, MainVersion), getVersion(r, MainVersion));
        if (ret == 0)
        {
            ret = compare(getVersion(l, SubVersion), getVersion(r, SubVersion));
            if (ret == 0)
            {
                ret = compare(getVersion(l, PatchVersion), getVersion(r, PatchVersion));
				if(ret == 0){
					ret = compare(getVersion(l,OtherVersion),getVersion(r,OtherVersion));
				}
            }
        }
        return ret;
    }

    private static int compare(int l, int r)
    {
        if (l > r)
        {
            return 1;
        }
        else if (l == r)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }

    /// <summary>
    /// 解析版本格式
    /// </summary>
    /// <param name="strVer"></param>
    /// <param name="AppVersion"></param>
    /// <returns></returns>
    private static int getVersion(string strVer, int AppVersion)
    {
        string [] ver = strVer.Split('.');
        if ( ver.Length<=2 || AppVersion < 0 || AppVersion >= 5)
        {
            throw new System.ArgumentException("error, 版本格式不兼容");//error, 版本格式不兼容
        }
		if (ver.Length <= AppVersion)
			return 0;
        return Convert.ToInt32(ver[AppVersion]);
    }

    // 增加版本号
    public static string increaseVersion(string currentVersion, int idx)
    {
        int[] version = new int[4];
        for (int a = 0; a < version.Length; ++a )
        {
            version[a] = getVersion(currentVersion, a);
        }
        
        if (idx < 0 && idx >= version.Length)
        {
            Logger.err("版本号越界： " + idx);
            return string.Empty;
        }
        
        version[idx]++;

        return string.Format("{0}.{1}.{2}.{3}", version[0], version[1], version[2],version[3]);
        

    }

}

*/
