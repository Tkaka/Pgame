using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using System;
using Message.Guild;

public class GameGuildState : GameBaseState
{
    private List<Vector3> m_posList = new List<Vector3>();     //路径点列表
    private List<ActorMC> m_actorList = new List<ActorMC>();
    private float m_lastUpdateTime = 0;
    private System.Random m_random;
   

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        Logger.log("enter Guild state");
        WinMgr.Singleton.Open<GuildScenceWnd>();
        m_random = new System.Random();
        _InitWayPointsList();
        _CreateActors();
        _LoadBuilds();
        CPlayerInput.GetInstance().Enable = true;
        GED.ED.addListener(EventID.GameObjectClick, _OnModelClick);
        GED.ED.dispatchEvent(EventID.GuildEnter);
    }


    public override void onUpdate()
    {
        if (UnityEngine.Time.realtimeSinceStartup - m_lastUpdateTime >= 5)
        {
            _StartRandomMove();
            m_lastUpdateTime = UnityEngine.Time.realtimeSinceStartup;

        }
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        CPlayerInput.GetInstance().Enable = false;
        for (int i = 0; i < m_actorList.Count; i++)
        {
            m_actorList[i].destoryMe();
        }
        m_actorList.Clear();

        GED.ED.removeListener(EventID.GameObjectClick, _OnModelClick);
        ActorManagerMC.Singleton.RemoveAll();
        ActorManagerMC.Singleton.Clear();
    }


    //加载建筑
    private void _LoadBuilds()
    {
        ActorParam actorParam = ActorParam.create(m_posList[0], Vector3.zero);
        ActorMC ac = ActorManagerMC.Create(101, ActorType.Pet, ActorCamp.CampFriend, actorParam);
        ac.setObjName("GuildHall");

        actorParam = ActorParam.create(m_posList[1], Vector3.zero);
        ac = ActorManagerMC.Create(101, ActorType.Pet, ActorCamp.CampFriend, actorParam);
        ac.setObjName("GuildShop");
    }

    private void _CreateActors()
    {
        m_actorList.Clear();

        int showNum = m_random.Next(1, 5);
        List<Member> members = GuildService.Singleton.GetMembers();       
        if (showNum >= members.Count)
        {
            for (int i = 0; i < members.Count; i++)
            {
                _CreateActor(members[i]);
            }
        }
        else
        {
            List<Member> copyList = new List<Member>();
            for (int i = 0; i < members.Count; i++)
            {
                copyList.Add(members[i]);
            }

            while (m_actorList.Count < showNum || copyList.Count == 0)
            {
                int index = m_random.Next(copyList.Count);
                _CreateActor(copyList[index]);
                copyList.Remove(copyList[index]);
            }

            copyList.Clear();
        }

    }

    private void _CreateActor(Member memberInfo)
    {
        ActorParam param = null;
        int index = m_random.Next(0, m_posList.Count);
        param = ActorParam.create(m_posList[index], Vector3.zero);
        ActorMC actor = ActorManagerMC.Create(100, ActorType.Player, ActorCamp.CampNeutral, param);
        string strDes = string.Format("{0}({1}级)", memberInfo.name, memberInfo.level);
        actor.createHangPointInfo(strDes);
        m_actorList.Add(actor);
        
    }


    private void _StartRandomMove()
    {
        if (m_posList.Count == 0 || m_actorList.Count == 0)
            return;
        for (int i = 0; i < m_actorList.Count; i++)
        {
            int index = m_random.Next(0,m_posList.Count);
            if (m_actorList[i].TransformExt.position.Equals(m_posList[index]))
                continue;
            m_actorList[i].MoveToTarget(m_posList[index]);

        }
    }

    private void _InitWayPointsList()
    {
        //Vector3 pos = WayPointMgr.Singleton.playerSpawner.position + new Vector3(3, 0, 6);
        //m_posList.Add(pos);
        //pos = WayPointMgr.Singleton.playerSpawner.position + new Vector3(-6, 0, 3);
        //m_posList.Add(pos);
        //pos = WayPointMgr.Singleton.playerSpawner.position + new Vector3(5, 0, 9);
        //m_posList.Add(pos);
        //pos = WayPointMgr.Singleton.playerSpawner.position + new Vector3(9, 0, 5);
        //m_posList.Add(pos);
        //pos = WayPointMgr.Singleton.playerSpawner.position + new Vector3(1, 0, -8);
        //m_posList.Add(pos);
    }

    private void _OnModelClick(GameEvent evt)
    {
        string modelName = evt.Data as string;
        if (modelName == null)
            return;

        switch (modelName)
        {
            case "GuildHall":
                WinMgr.Singleton.Open<GuildHallWnd>();
                break;
            case "GuildShop":
                WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null, true, 4), UILayer.Popup);
                break;
        }
    }

    public override string getStateKey()
    {
        return GameState.Guild;
    }
}