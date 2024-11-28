using FairyGUI;
using UnityEngine;

public class GameConfig
{

    public const float OffsetDuring = 0.5f;

    public const float Velocity = 10.0f;

    // 距离，速度，位置偏移等，相关参数的缩放系数
    public const float ScaleRate = 0.0001f;

    // 配置文件路径
    private static string mConfigPath = "Config/";

    // 服务器列表文件
    private static string ServerListFile = "ServerList_local";

    public static ServerList ServerList { private set; get; }

    public static bool Init()
    {
        PlayerLocalData.Initialize();

        //加载服务器列表
        //GetServerList();

        //加载配置表数据
        Data.Containers.GameDataManager.Instance.loadOneBean(Data.Containers.GameDataManager.Instance.t_languageContainer);

        //设置默认字体
        UIConfig.defaultFont = "微软雅黑";

        //Data.Beans.t_languageBean bean = ConfigBean.GetBean<Data.Beans.t_languageBean, int>(1002);
        //Logger.log(bean.t_content);

        //Data.Beans.t_professionBean bean1 = ConfigBean.GetBean<Data.Beans.t_professionBean, int>(100);
        //Logger.log(bean1.t_name);

        //Data.Beans.t_itemBean bean1 = ConfigBean.GetBean<Data.Beans.t_itemBean, int>(1000);
        //Logger.log(bean1.t_name);

        return true;
    }

    public static void GetServerList()
    {
        TextAsset ta = Resources.Load<TextAsset>(mConfigPath + ServerListFile);
        if (ta == null || string.IsNullOrEmpty(ta.text))//判断字符串是否为空
        {
            Logger.err("严重错误--在包内找不到ServerIndexConfig：" + ServerListFile);
            return;
        }
        /*ServerList serverList = ServerList.Load(ta.text);
        if (serverList.ServerLists == null || serverList.ServerLists.Length <= 0)
        {
            Logger.err("游戏加载服务器列表出错");
            return;
        }
        ServerList = serverList;*/
    }

    /// <summary>
    /// SD卡路径
    /// </summary>
    /// <returns></returns>
    public static string GetAppliactionPath()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        string ret = Application.dataPath;
        int idx = ret.LastIndexOf("/");
        if (idx < 0)
            return ret;
        else
            return ret.Substring(0, ret.Length - "Assets".Length - 1);
#else 
        return Application.persistentDataPath;
        /*if (Application.platform == RuntimePlatform.IPhonePlayer)
            return Application.temporaryCachePath;
        else
            return Application.persistentDataPath;*/
#endif
    }


}