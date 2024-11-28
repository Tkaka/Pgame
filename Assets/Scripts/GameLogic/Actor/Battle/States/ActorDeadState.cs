

public class ActorDeadState : ActorBaseState
{

    public override void onEnter(object obj = null)
    {
        bool fromBuff = false;
        if (obj != null)
            fromBuff = (bool)obj;
        mActor = mOwner as Actor;
        if (mActor != null)
        {
            //如果不是来自buff死亡，则掉魂珠
            if(!fromBuff)
                DropSoul();
            if (!mActor.WillReborn)
            {
                //掉落金币
                if (mActor.isCampOf(ActorCamp.CampEnemy) && !FightManager.Singleton.IsReplay)
                {
                    BattleService.Singleton.AddGold(SpawnerHelper.Singleton.CurWave);
                    new DropGoldCmd().Excute(mActor.TransformExt.position);
                    DropItemMgr.Singleton.StarMove();
                }
                mActor.GetActionManager().PlayCommonAnimation(getStateKey(), OnActionFinish);
            }
            else
            {
                if (mActor.isCampOf(ActorCamp.CampFriend))
                    FightManager.Singleton.FriendLevelList.Add(mActor.getActorId());
                else if (mActor.isCampOf(ActorCamp.CampEnemy))
                    FightManager.Singleton.EnemyLevelList.Add(mActor.getActorId());
                FightManager.Singleton.Remove(mActor.getCamp(), mActor.getActorId(), mActor.GridId);
                mActor.GetActionManager().PlayCommonAnimation(getStateKey());
            }
        }
        else
        {
            Logger.err("ActorDeadState Owner Type Error");
        }
    }

    private void DropSoul()
    {
        if (mActor.isCampOf(ActorCamp.CampEnemy))
        {
            Actor attacker = ActorManager.Singleton.Get(mActor.attackerId);
            if (attacker != null)
            {
                LNumber val = attacker.getProperty(PropertyType.KillGetMp);
                if (val > 0)
                {
                    AttackUtils.DropSoul(attacker.getActorId(), mActor, SoulBallType.Blue, val);
                }
            }
        }
    }

    private void OnActionFinish(int key)
    {
        FightManager.R.LoadGo("eff_dead", mActor.TransformExt.position);
        FightManager.Singleton.Remove(mActor.getCamp(), mActor.getActorId(), mActor.GridId);
        ActorManager.Singleton.Remove(mActor.getActorId());
    }

    public override string getStateKey()
    {
        return ActorState.dead;
    }

}
