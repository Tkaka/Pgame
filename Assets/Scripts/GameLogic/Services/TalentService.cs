using Message.Talent;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;

public class TalentService : SingletonService<TalentService>
{
    private Dictionary<int, TalentPage> m_talentPageDic = new Dictionary<int, TalentPage>();
    private Dictionary<int, TalentInfo> m_talentDic = new Dictionary<int, TalentInfo>();

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResChange.MsgId, _ResChange);
        GED.NED.addListener(ResTalentInfo.MsgId, _ResTalentInfo);
        GED.NED.addListener(ResUnlockTalent.MsgId, _ResUnlockTalent);
        GED.NED.addListener(ResUnlockTalentPage.MsgId, _ResUnlockTalentPage);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResChange.MsgId, _ResChange);
        GED.NED.removeListener(ResTalentInfo.MsgId, _ResTalentInfo);
        GED.NED.removeListener(ResUnlockTalent.MsgId, _ResUnlockTalent);
        GED.NED.removeListener(ResUnlockTalentPage.MsgId, _ResUnlockTalentPage);
    }


    //获得天赋等级
    public int GetTalentLevel(int talentId)
    {
        if (m_talentDic.ContainsKey(talentId))
        {
            return m_talentDic[talentId].level;
        }

        return -1;
    }

    //获得天赋页信息
    public TalentPage GetTalentPageInfo(int page)
    {
        if (m_talentPageDic.ContainsKey(page))
            return m_talentPageDic[page];
        return null;
    }
    //---------------------------------------------------------消息

    //天赋等级改变
    private void _ResChange(GameEvent evt)
    {
        ResChange msg = GetCurMsg<ResChange>(evt.EventId);
        if (m_talentPageDic.ContainsKey(msg.id))
        {
            m_talentPageDic[msg.id].num = msg.num;
            //m_talentPageDic[msg.id].open = msg.open;
        }
        else
        {
            Debug.LogError("升级的天赋点没有对应的天赋页");
        }

        if (m_talentDic.ContainsKey(msg.talent.id))
        {
            m_talentDic[msg.talent.id] = msg.talent;
        }
        else
        {
            m_talentDic.Add(msg.talent.id, msg.talent);
        }

        TwoParam<int, int> param = new TwoParam<int, int>();
        param.value1 = msg.id;
        param.value2 = msg.talent.id;
        GED.ED.dispatchEvent(EventID.TalentLevelUp, param);
    }

    //重置和初始化
    private void _ResTalentInfo(GameEvent evt)
    {
        ResTalentInfo msg = GetCurMsg<ResTalentInfo>(evt.EventId);
        m_talentDic.Clear();
        m_talentPageDic.Clear();
        for (int i = 0; i < msg.talents.Count; i++)
        {
            TalentPage page = msg.talents[i];
            if (m_talentPageDic.ContainsKey(page.id))
            {
                m_talentPageDic[page.id] = page;
            }
            else
            {
                m_talentPageDic.Add(page.id, page);
            }

            for (int j = 0; j < page.talents.Count; j++)
            {
                TalentInfo info = page.talents[j];
                if (m_talentDic.ContainsKey(info.id))
                {
                    m_talentDic[info.id] = info;
                }
                else
                {
                    m_talentDic.Add(info.id, info);
                }
            }
        }

        GED.ED.dispatchEvent(EventID.TalentReset);
    }

    //解锁天赋
    private void _ResUnlockTalent(GameEvent evt)
    {
        ResUnlockTalent msg = GetCurMsg<ResUnlockTalent>(evt.EventId);
        if (m_talentDic.ContainsKey(msg.info.id))
        {
            m_talentDic[msg.info.id] = msg.info;
        }
        else
        {
            m_talentDic.Add(msg.info.id, msg.info);
        }

        TwoParam<int, int> param = new TwoParam<int, int>();
        param.value1 = msg.info.id / 100;
        param.value2 = msg.info.id;
        GED.ED.dispatchEvent(EventID.TalentLevelUp, param);
    }

    //解锁天赋页
    private void _ResUnlockTalentPage(GameEvent evt)
    {
        ResUnlockTalentPage msg = GetCurMsg<ResUnlockTalentPage>(evt.EventId);
        TalentPage page = msg.page;
        if (m_talentPageDic.ContainsKey(page.id))
        {
            m_talentPageDic[page.id] = page;
        }
        else
        {
            m_talentPageDic.Add(page.id, page);
        }

        for (int j = 0; j < page.talents.Count; j++)
        {
            TalentInfo info = page.talents[j];
            if (m_talentDic.ContainsKey(info.id))
            {
                m_talentDic[info.id] = info;
            }
            else
            {
                m_talentDic.Add(info.id, info);
            }
        }

        GED.ED.dispatchEvent(EventID.TalentPageUnlock);
    }
    //----------------------------------------------------------请求
    public void ReqLevel(int id)
    {
        ReqLevel msg = GetEmptyMsg<ReqLevel>();
        msg.id = id;
        SendMsg<ReqLevel>(ref msg);
    }

    public void ReqReset()
    {
        ReqReset msg = GetEmptyMsg<ReqReset>();
        SendMsg<ReqReset>(ref msg);    
    }

}

