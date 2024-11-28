using UnityEngine;
using System.IO;
using SimpleJSON;

public class PlayerLocalDataKey
{
    public const string NewNormalChapterID = "NewNormalChapterID";               // 普通关卡的最新章节ID
    public const string NewEliteChapterID = "NewEliteChapterID";                 // 精英关卡的最新章节ID
    public const string OpenAutoRecommend = "OpenAutoRecommend";                 // 神器培养是否开启自动推荐
    public const string CloseTrainJumpTip = "ShowTrainJumpTip";                   // 是否显示终极试炼中的一键爬塔提示界面
}

public class PlayerLocalData
{
	const string Path = "/PrefsData/";
	const string Name = "LocalPrefs.json";

	public const string key_Login_Name 			= "key1";
	public const string key_Last_Server_GroupID = "key2";
	public const string key_Last_Server_ID		= "key3";
	public const string key_Last_RoleId			= "key4";
    public const string key_chat_team = "chat_team_{0}_{1}";
    public const string key_chat_guild = "chat_guild_{0}_{1}";

    public static bool AutoSave = true;
    private static JSONClass json;
	private static FileInfo fileInfo;

	///  初始化文件.
	public static void Initialize()
	{
		if( json != null ) return;

		string dataPath = "";
		string cachePath = Application.temporaryCachePath + Path + Name;
		string persistentPath = Application.persistentDataPath + Path + Name;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
		dataPath = Application.dataPath;
#else
        dataPath = Application.persistentDataPath;
#endif

        fileInfo = new FileInfo( dataPath + Path + Name );

		if( fileInfo.Exists )
        {
            StreamReader reader = fileInfo.OpenText();
			try
            {
            	json = SimpleJSON.JSON.Parse( Deconde( reader.ReadToEnd() ) ) as JSONClass;
				if( json == null )
					json = new JSONClass();
			}
            catch
            {
				json = new JSONClass();
			}
			reader.Close(); 
			reader.Dispose();
		}
		else
        {
			DirectoryInfo dir = new DirectoryInfo( dataPath + Path );
			if(!dir.Exists )
				dir.Create();
            json = new JSONClass();
		}
	}

	/// 是否包含元素.
	public static bool HasData( string key )
	{
		if( json == null ) return false;
        return json.ContainsKey(key);
	}

	/// 删除元素.
	public static void DeleteKey( string key )
	{
		if( json == null ) return;
        if (json.ContainsKey(key))
        {
			json.Remove( key );
		}
		if( AutoSave )
			Save();
	}

	/// 清除所有数据.
	public static void Clear()
	{
        json = null;
        json = new JSONClass();
		Save();
	}

	/// 载入参数. int, float, string
	public static void SetData( string key, object value )
	{
		if( json == null ) return;

        if (value is int)
            json[key].AsInt = (int)value;
        else if (value is float)
            json[key].AsFloat = (float)value;
        else if (value is string)
            json[key] = (string)value;
        else
            json[key] = JsonUtility.ToJson(value);

		if( AutoSave )
			Save();
	}

	/// 获取数据. int, float, string   // float 精度为0.1f
	public static object GetData( string key, object defaultValue )
	{
		if( json == null ) return defaultValue;

        if (json.ContainsKey(key))
        {
            defaultValue = json[key].Value;
        }
        else
        {
            return defaultValue;
        }
		return defaultValue;
	}

	/// 保存数据.
	public static void Save()
	{
		try
		{
			StreamWriter stream = new StreamWriter( fileInfo.FullName , false );
			stream.Write( EnDecode( json.ToString() ) );
			stream.Flush();
			stream.Close();
			stream.Dispose();
		}
        catch(System.Exception e)
		{
            Debug.Log(e.ToString());
		}
	}





    /************暂时不做加密************/
	private static string EnCode( string value )
	{
        return value;
		//return EnDecode( value );
	}

	private static string Deconde( string value )
	{
        return value;
		value = EnDecode( value );
		Logger.wrn( "本地设置：" + value );
		return value;
	}

	public static string EnDecode( string value )
	{
        return value;
		if( string.IsNullOrEmpty( value ) )
			return value;
		string key = "1398";
		int keyLength = key.Length;
		int valueLength = value.Length;
		char[] result = new char[valueLength];
		for (int i = 0; i < valueLength; i++)
			result[i] = (char)(value[i] ^ key[i % keyLength]);
		string ret = new string( result );
		return ret;
	}

}

