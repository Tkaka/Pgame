using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Data.Beans;
using System.Text;
using Message.Chat;
using UnityEngine;

public class ChatTool
{
    private static ChatTool m_instance;
    private Dictionary<ChatService.EChannelType, int> m_channelShowDic = new Dictionary<ChatService.EChannelType, int>();
    private Dictionary<string, string> m_emojiDic = new Dictionary<string, string>();
    private Dictionary<ChatService.EChannelType, bool> m_sendCoolDic = new Dictionary<ChatService.EChannelType, bool>();   //发送消息冷却时间
    private Regex m_regWord = null;
    private Regex m_regEmoji = null;
    private string m_strSensitives;
    private string m_strEmojiName;
 
    private int m_singleChannelMaxNum = 50;                                     //单频道最大大的消息数量


    public static ChatTool GetInstance()
    {
        if (m_instance == null)
            m_instance = new ChatTool();

        return m_instance;
    }

    public ChatTool()
    {
        _Init();
        _InitDataBaseBlockWords();
        _InitEmojiInfo();
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(502008);
        if (bean != null)
        {
            m_singleChannelMaxNum = bean.t_int_param;
        }
    }

    private void _Init()
    {
         foreach (int channel in Enum.GetValues(typeof(ChatService.EChannelType)))
         {
            int value = 0;
            string strValue;
            if ((ChatService.EChannelType)channel == ChatService.EChannelType.World || (ChatService.EChannelType)channel == ChatService.EChannelType.System)
            {
                //世界和系统默认为显示
                strValue = (string)PlayerLocalData.GetData(string.Format("chat_channel_{0}", channel), "1");
            }
            else
            {
                strValue = (string)PlayerLocalData.GetData(string.Format("chat_channel_{0}", channel), "0");
            }

            int.TryParse(strValue, out value);

            if (!m_channelShowDic.ContainsKey((ChatService.EChannelType)channel))
            {
                m_channelShowDic.Add((ChatService.EChannelType)channel, value);
            }
            else
            {
                m_channelShowDic[(ChatService.EChannelType)channel] = value;
            }
         }
 
    }

    public bool GetChannelIsShow(ChatService.EChannelType channel)
    {
        if (m_channelShowDic.ContainsKey(channel))
        {
            return m_channelShowDic[channel] == 1;
        }

        return false;
    }

    //获得显示频道数量
    public int GetShowChannelNum()
    {
        int num = 0;
        foreach (var info in m_channelShowDic)
        {
            if (info.Value == 1)
            {
                num++;
            }
        }

        return num;
    }

    //1为显示 0 为不显示
    public void SetChannelIsShow(ChatService.EChannelType channel, int isShow)
    {
        if (m_channelShowDic.ContainsKey(channel))
        {
            if (m_channelShowDic[channel] != isShow)
            {
                m_channelShowDic[channel] = isShow;

                PlayerLocalData.SetData(string.Format("chat_channel_{0}", (int)channel), isShow);
            }
        }
    }


    public string GetChannel(ChatService.EChannelType channelType)
    {
        string str = "";
        switch (channelType)
        {
            case ChatService.EChannelType.Guild:
                str = "帮会";
                break;
            case ChatService.EChannelType.System:
                str = "系统";
                break;
            case ChatService.EChannelType.Team:
                str = "队伍";
                break;
            case ChatService.EChannelType.World:
                str = "世界";
                break;
            case ChatService.EChannelType.ZuiDui:
                str = "组队";
                break;
            default:
                break;
        }
        return str;
    }


    //初始化表情信息
    private void _InitEmojiInfo()
    {

        List<t_emojiBean> emojiBeans = ConfigBean.GetBeanList<t_emojiBean>();
        StringBuilder patt = new StringBuilder();
        for (int i = 0; i < emojiBeans.Count; i++)
        {
            t_emojiBean bean = emojiBeans[i];
            if (!m_emojiDic.ContainsKey(bean.t_emoji_name))
            {
                if (bean.t_emoji_name.Length == 0)
                    continue;
                m_emojiDic.Add(bean.t_emoji_name, bean.t_emoji_icon);
                patt.AppendFormat("|{0}", bean.t_emoji_name);

            }
        }

        if (patt.Length > 0)
        {
            patt.Remove(0, 1);
        }
        m_strEmojiName = patt.ToString();
        if(m_regEmoji == null)
            m_regEmoji = new Regex(m_strEmojiName, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);
    }

 
    //初始化敏感词信息
    private void _InitDataBaseBlockWords()
    {
        List<t_sensitive_wordBean> list = ConfigBean.GetBeanList<t_sensitive_wordBean>();
        StringBuilder patt = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].t_name.Length == 0)
                continue;
            patt.AppendFormat("|{0}", list[i].t_name);
    
        }

        if (patt.Length > 0)
        {
            patt.Remove(0, 1);
        }

        m_strSensitives = patt.ToString();
        if(m_regWord == null)
             m_regWord = new Regex(m_strSensitives, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);
    }


    public bool HasBlockWords(string raw)
    {
        return m_regWord.Match(raw).Success;
    }

    public string ConvertToFilterWords(string raw)
    {
        string ret = raw;
        if (HasBlockWords(raw))
        {
            ret = m_regWord.Replace(raw, "***");
        }
        return ret;
    }

    //表情转换
    public string ConvertToFilterEmoji(string str)
    {
        int index = -1;
        int nextIndex = -1;
        int backIndex = -1;

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            index = str.IndexOf("[", i);
            if (index == -1)
                break;

            int end = i + 1;
            end = end > str.Length ? str.Length - 1 : end;
            nextIndex = str.IndexOf("[", end);
            backIndex = str.IndexOf("]", i);

            if (nextIndex != -1 && nextIndex < backIndex)
            {
                index = nextIndex;
            }

            //string fir = str.Substring(index, index + 1);

            builder.Append(str.Substring(i, index - i));
            string temp = str.Substring(index, backIndex - index + 1);
            if (m_emojiDic.ContainsKey(temp))
            {
                builder.Append(CreateChatImg(m_emojiDic[temp]));
            }
            else
            {
                builder.Append(temp);
            }
 

            i = backIndex;
        }

        if (backIndex + 1 < str.Length)
            builder.Append(str.Substring(backIndex + 1, str.Length - backIndex - 1));
        //string ret = str;
        //MatchCollection result = m_regEmoji.Matches(str);
        ////if (result.Success)
        //{
        //    foreach (var info in m_emojiDic)
        //    {
        //        ret = ret.Replace(info.Key, CreateChatImg(info.Value));
        //    }
        //}
        //return ret;
        return builder.ToString();
    }

    public string CreateChatImg(string path, float wight = 30f, float heigh = 30f)
    {
        return string.Format("<img src='{0}' width='{1}' height='{2}'/>", path, wight, heigh);
    }

    //保存帮会聊天或队伍消息到本地
    public void SaveChatInfoToLocal(ChatService.EChannelType channel, List<ChatInfo> chats)
    {
        if (chats == null || chats.Count == 0)
            return;

        string key = "";
        if (channel == ChatService.EChannelType.Team)
            key = PlayerLocalData.key_chat_team;
        else if (channel == ChatService.EChannelType.Guild)
            key = PlayerLocalData.key_chat_guild;
        else
            return;


        for (int i = 0; i < chats.Count; i++)
        {
            if (i >= m_singleChannelMaxNum)
                break;

            PlayerLocalData.SetData(string.Format(key,i, RoleService.Singleton.GetRoleInfo().roleId), chats[i]);
        }

    
    }

    public List<ChatInfo> GetChatInfoInLocal(ChatService.EChannelType channel)
    {
        string key = "";
        if (channel == ChatService.EChannelType.Team)
            key = PlayerLocalData.key_chat_team;
        else if (channel == ChatService.EChannelType.Guild)
            key = PlayerLocalData.key_chat_guild;
        else
            return null;

        List<ChatInfo> chatInfos = new List<ChatInfo>();
        for (int i = 0; i < m_singleChannelMaxNum; i++)
        {
            string strJson = PlayerLocalData.GetData(string.Format(key, i, RoleService.Singleton.GetRoleInfo().roleId), null) as string;
            if (strJson == null)
                break;

            ChatInfo chatInfo = JsonUtility.FromJson<ChatInfo>(strJson);
            chatInfos.Add(chatInfo);

        }


        return chatInfos;
    }

    //设置当前聊天消息的冷却
    public void SetChannelChatCool(ChatService.EChannelType channel)
    {
        int globaId = 0;
        switch (channel)
        {
            case ChatService.EChannelType.Guild:
                globaId = 502001;
                break;
            case ChatService.EChannelType.World:
                globaId = 502003;
                break;
            case ChatService.EChannelType.Team:
                globaId = 502002;
                break;
        }

        int coolTime = 0;
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(globaId);
        if (bean != null)
        {
            coolTime = bean.t_int_param;
        }

        if (m_sendCoolDic.ContainsKey(channel))
        {
            m_sendCoolDic[channel] = true;
        }
        else
        {
            m_sendCoolDic.Add(channel, true);
        }

        long coroutineId = CoroutineManager.Singleton.delayedCall(coolTime, () =>
        {
            if (m_sendCoolDic.ContainsKey(channel))
            {
                m_sendCoolDic[channel] = false;
            }
        });

    }

    //当前频道消息是否冷却中
    public bool GetCurChannelIsCooling(ChatService.EChannelType channel)
    {
        if (m_sendCoolDic.ContainsKey(channel))
        {
            return m_sendCoolDic[channel];
        }

        return false;
    }

}