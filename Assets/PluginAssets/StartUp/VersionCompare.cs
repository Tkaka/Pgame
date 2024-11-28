/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
*/
public class VersionCompare
{
    /// <summary>
    /// v1大返回1，v2大返回-1，一样大返回0
    /// </summary>
    public static int CompareVersion(string v1, string v2)
    {
        if(string.IsNullOrEmpty(v2) && string.IsNullOrEmpty(v1))
            return 0;
        if(string.IsNullOrEmpty(v2))
            return 1;
        if(string.IsNullOrEmpty(v1))
            return -1;

        string[] arr1 = v1.Split('.');
        string[] arr2 = v2.Split('.');

        for(int i=0; i < arr1.Length && i<arr2.Length; ++i)
        {
            int va1 = 0;
            int va2 = 0;
            int.TryParse(arr1[i], out va1);
            int.TryParse(arr2[i], out va2);
            if(va1 > va2)
                return 1;
            else if(va1 < va2)
                return -1;
        }
        return 0;
    }

    public static int ConvertToIntVersion(string ver)
    {
        if(string.IsNullOrEmpty(ver))
            return 0;
        string[] arr = ver.Split('.');
        int ret = 0;
        for(int i=0; i < arr.Length; ++i )
        {
            int v = 0;
            if(int.TryParse(arr[i], out v))
                ret += v;
        }
        return ret;
    }
}