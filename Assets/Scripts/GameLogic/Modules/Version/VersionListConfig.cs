using System;

// 版本列表
public class VersionListConfig
{
    public class VersionEntry
    {
        public string Version { get; set; }
    }

    public VersionEntry[] VersionList;


    public static VersionListConfig load(string text)
    {
        try
        {
            VersionListConfig config = new VersionListConfig();
            SimpleJSON.JSONNode node = SimpleJSON.JSON.Parse(text);
            SimpleJSON.JSONArray arr = node["VersionList"].AsArray;
            VersionEntry[] entryArr = null;
            VersionEntry entry = null;
            if (arr != null && arr.Count > 0)
            {
                entryArr = new VersionEntry[arr.Count];
                for (int i = 0; i < arr.Count; i++)
                {
                    entry = new VersionEntry();
                    entry.Version = arr[i]["Version"].Value;
                    entryArr[i] = entry;
                }
            }
            else
            {
                entryArr = new VersionEntry[0];
            }
            config.VersionList = entryArr;
            return config;
            //return SimpleJson.SimpleJson.DeserializeObject<VersionListConfig>(text);
        }
        catch (Exception ex)
        {
            Logger.err(ex.ToString());
            return null;
        }
    }

    public static VersionListConfig load(byte[] content)
    {
        return load(System.Text.Encoding.UTF8.GetString(content));
    }
}

