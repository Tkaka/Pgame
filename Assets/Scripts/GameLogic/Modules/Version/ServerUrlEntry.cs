using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ServerUrlEntry : IComparable
{
    // URL
    public string url { get; set; }

    // Random
    public int rand { get; set; }

    // 是否已使用
    public bool dirty { get; set; }

    // 按照权重排序
    public int CompareTo(object obj)
    {
        ServerUrlEntry other = obj as ServerUrlEntry;
        if (other == null)
            return -1;
        return rand.CompareTo(other.rand);
    }

    public static String getRandomOneUrl(ServerUrlEntry[] urls)
    {
        if (urls == null || urls.Length < 0)
        {
            Logger.err("空的ServerUrlEntry!");
            return null;
        }

        int total = 0;
        for (int a = 0; a < urls.Length; ++a)
        {
            total += urls[a].rand;
        }

        Array.Sort(urls);
        int random = new System.Random().Next(1, total);
//        Logger.dbg(random.ToString());

        int weight = 0;
        for (int b = 0; b < urls.Length; ++b)
        {
            weight += urls[b].rand;
            if (random <= weight && !urls[b].dirty)
            {
                urls[b].dirty = true;
                return urls[b].url;
            }
        }

        for (int c = 0; c < urls.Length; ++c)
        {
            if (!urls[c].dirty)
            {
                urls[c].dirty = true;
                return urls[c].url;
            }
        }
        return urls[0].url;
    }

    public static void resetAll(ServerUrlEntry[] urls)
    {
        for (int a = 0; a < urls.Length; ++a)
        {
            urls[a].dirty = false;
        }
    }


    public static bool tryAll(ServerUrlEntry[] urls)
    {
        for (int a = 0; a < urls.Length; ++a)
        {
            if (!urls[a].dirty)
                return false;
        }
        return true;
    }

    public static ServerUrlEntry[] parseServerUrlEntry(SimpleJSON.JSONNode node, string nodeName)
    {
        ServerUrlEntry[] rseEntry;
        SimpleJSON.JSONArray resServerNode = node[nodeName].AsArray;
        ServerUrlEntry tempResEntry = null;
        if (resServerNode != null && resServerNode.Count > 0)
        {
            rseEntry = new ServerUrlEntry[resServerNode.Count];
            for (int j = 0; j < resServerNode.Count; j++)
            {
                tempResEntry = new ServerUrlEntry();
                tempResEntry.url = resServerNode[j]["url"].Value;
                tempResEntry.rand = resServerNode[j]["random"].AsInt;
                rseEntry[j] = tempResEntry;
            }
        }
        else
        {
            rseEntry = new ServerUrlEntry[0];
        }
        return rseEntry;
    }
}

