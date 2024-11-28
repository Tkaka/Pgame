using UnityEngine;

public class GPlayerPrefs
{

    public static bool AutoSave = true;

    public const string BattleSpeedKey = "BattleSpeedKey";

    //1 = auto 0 =  手动
    public const string BattleAutoKey = "BattleAutoKey";

    public static void SetString(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
        if(AutoSave)
            PlayerPrefs.Save();
    }

    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public static void SetInt(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
        if (AutoSave)
            PlayerPrefs.Save();
    }

    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public static void SetFloat(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
        if (AutoSave)
            PlayerPrefs.Save();
    }

    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    public static bool DeleteKey(string key)
    {
        if (HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            return true;
        }
        return false;
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }

}
