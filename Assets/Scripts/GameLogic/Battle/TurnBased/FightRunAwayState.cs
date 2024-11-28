
using Data.Beans;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UI_Battle;
using FairyGUI;

/// <summary>
/// 战斗逃跑状态
/// </summary>
public class FightRunAwayState : FightBaseState
{
    private int m_needMoveNum = 0;
    private List<Actor> m_sameActorList;      //相同宠物列表
    private List<GObject> m_sameDesList = new List<GObject>();
    private List<GObject> m_runAwayObjList = new List<GObject>();
    private long coroId = 0;


    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        GED.ED.addListener(EventID.ActorMoveCmp, OnActorMoveCmp);

        m_sameActorList = SpawnerHelper.Singleton.GetSamePetInfo();

        coroId = CoroutineManager.Singleton.startCoroutine(_PrpareShow());
        
 

    }

    private IEnumerator _PrpareShow()
    {
        _ShowSameDes();
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < m_sameDesList.Count; i++)
        {
            m_sameDesList[i].visible = false;
        }

        _ShowRunAaway();
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < m_runAwayObjList.Count; i++)
        {
            m_runAwayObjList[i].visible = false;
        }

        _StartMove();

        yield return 0;
    }

    //修正位置到目标头顶
    private void _fixPos(Actor actor, GObject obj)
    {

        Vector3 ownerPos = actor.monoBehavior.headBarPos;
        ownerPos.y += 1;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(ownerPos);
        screenPos.y = Screen.height - screenPos.y; //convert to Stage coordinates system

        Vector3 pt = GRoot.inst.GlobalToLocal(screenPos);
        obj.SetXY(Mathf.RoundToInt(pt.x - obj.actualWidth * 0.5f), Mathf.RoundToInt(pt.y - obj.actualHeight * 0.5f));
    }

    //显示逃跑对话
    private void _ShowRunAaway()
    {
        for (int i = 0; i < m_sameActorList.Count; i++)
        {
            Actor actor = m_sameActorList[i];
            UI_RunAwayDes objDes = UI_RunAwayDes.CreateInstance();
            WinMgr.Singleton.PopupLayer.AddChild(objDes);
            objDes.m_txtDes.text = "遇到真身了！快跑!!!!!!!!";

            _fixPos(actor, objDes);
            //Vector3 pos = WinMgr.Singleton.WorldToScreen(actor.TransformExt.position);
            //objDes.SetXY(pos.x, pos.y);
            objDes.visible = true;
 
            m_runAwayObjList.Add(objDes);
        }
    }


    //显示相同的描述
    private void _ShowSameDes()
    {
        for (int i = 0; i < m_sameActorList.Count; i++)
        {
            //找到己方阵营相同的宠物
            int petId = 0;
            Actor enemyActor = m_sameActorList[i];
            t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(enemyActor.getTemplateId());
            if (bossBean != null)
                petId = bossBean.t_pet_id;
       
            foreach (var info in FightManager.Singleton.Grid.PlayerCamp)
            {
                if (info.templateId == petId)
                {
                    UI_SameDes objDes = UI_SameDes.CreateInstance();
                    WinMgr.Singleton.PopupLayer.AddChild(objDes);
                    Actor petActor = ActorManager.Singleton.Get(info.actorId);
                    //Vector3 pos = WinMgr.Singleton.WorldToScreen(petActor.TransformExt.position);
                    //objDes.SetXY(pos.x, pos.y);
                    _fixPos(petActor, objDes);
                    objDes.visible = true;
                   
                    m_sameDesList.Add(objDes);
                }
            }

            UI_SameDes obj = UI_SameDes.CreateInstance();
            WinMgr.Singleton.PopupLayer.AddChild(obj);
            //Vector3 pos2 = WinMgr.Singleton.WorldToScreen(enemyActor.TransformExt.position);
            //obj.SetXY(pos2.x, pos2.y);
            _fixPos(enemyActor, obj);
            obj.visible = true;
     
            m_sameDesList.Add(obj);
        }

        
    }

    private void _StartMove()
    {
     
        m_needMoveNum = m_sameActorList.Count;
        for (int i = 0; i < m_sameActorList.Count; i++)
        {

            Actor actor = m_sameActorList[i];
            FightManager.Singleton.Remove(actor.getCamp(), actor.getActorId(), actor.GridId);
            PathParam pathParam;

            pathParam = SpawnerHelper.Singleton.GetPlayerPathParam(SpawnerHelper.Singleton.RealWave);
            Vector3? lastPos = pathParam.GetLastPos();
            if (lastPos.HasValue)
            {
                float dis = GTools.distanceIgnoreY(lastPos.Value, actor.TransformExt.position);
                pathParam.dur = dis / actor.Velocity;
            }
            actor.changeState(ActorState.move, pathParam);
             
        }
    }

    private void OnActorMoveCmp(GameEvent obj)
    {
        Actor actor = obj.Data as Actor;
        if (actor != null)
        {
            m_needMoveNum--;
            actor.ToggleVisible(false);
            ActorManager.Singleton.Remove(actor.getActorId());
        }

        if(m_needMoveNum <= 0)
            fm.ChangeState(FightState.PrepareNextTurnState, ChangeReason.RunAwayCmp);
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        CoroutineManager.Singleton.stopCoroutine(coroId);

        GED.ED.removeListener(EventID.ActorMoveCmp, OnActorMoveCmp);
        for (int i = 0; i < m_sameDesList.Count; i++)
        {
            m_sameDesList[i].Dispose();
        }

        for (int i = 0; i < m_runAwayObjList.Count; i++)
        {
            m_runAwayObjList[i].Dispose();
        }

        m_runAwayObjList.Clear();
        m_sameDesList.Clear();
    }

    public override string getStateKey()
    {
        return FightState.RunAway;// FightState.TurnStart;
    }

}
