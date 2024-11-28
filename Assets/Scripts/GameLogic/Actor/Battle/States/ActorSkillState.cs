using Data.Beans;

public class SkillStateParam{    public Skill Skill;    public object Param;    public static SkillStateParam create(Skill skill, object param = null)    {        SkillStateParam skillParam = new SkillStateParam();        skillParam.Skill = skill;        skillParam.Param = param;        return skillParam;
    }
}

/// <summary>
/// 攻击状态
/// </summary>
public class ActorSkillState : ActorBaseState
{
    protected Skill mSkill = null;

    public override void onEnter(object obj = null)
    {
        mActor = mOwner as Actor;
        SkillStateParam param = (SkillStateParam)obj;
        if (param != null)
        {
            mSkill = param.Skill;
        }

        if (mSkill == null)
        {
            Logger.err("攻击参数==null");
            mActor.changeState(ActorState.idle);
            return;
        }

        //大招不回复怒气 | 在出招时就立即回复怒气
        if (mSkill.IsMasterSkill())
            mActor.ViewPropertyMgr.ChangeProperty(PropertyType.Mp, -BattleParam.MaxMp);

        mSkill.onEnter(param.Param);
    }

    public override void onUpdate()
    {
        base.onUpdate();

        if (mSkill != null)
        {
            mSkill.onUpdate();
        }
    }

    public override void onLeave(string stateKey)
    {
        if (mSkill != null)
        {
            mSkill.stop();
        }
    }

    public override string getStateKey()
    {
        return ActorState.attack;
    }

}
