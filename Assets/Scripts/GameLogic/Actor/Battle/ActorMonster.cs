using Data.Beans;
using System.Collections.Generic;
using UnityEngine;

public class ActorMonster : Actor
{

    public ActorMonster(int temlateId, ActorType type, ActorCamp camp, long roleId) : base(temlateId, type, camp, roleId)
    {

    }

    protected override void loadPrefab(ActorParam instanceData)
    {
        t_monsterBean mBean = ConfigBean.GetBean<t_monsterBean, int>(mTemplateId);
        //mShowObj = Res.Singleton.InstantiateModel(mBean.t_battle_prefab, instanceData.Pos);
        mShowObj = resPacker.LoadGo(mBean.t_battle_prefab, instanceData.Pos);
        if (mShowObj != null)
            mShowObj.transform.forward = instanceData.Dir;
    }

    private long runAniCoroId = 0;
    public virtual void ShowRunAni()
    {
        Vector3 oldPos = TransformExt.position;
        Vector3 targetPos = oldPos - TransformExt.forward * 3;
        TransformExt.position = targetPos;
        PathParam param = new PathParam();
        param.path = new List<Vector3>();
        param.path.Add(oldPos);
        param.dir = TransformExt.forward;
        changeState(ActorState.move, param);
        float dis = GTools.distanceIgnoreY(targetPos, oldPos);
        float dur = dis / mVelocity;
        runAniCoroId = CoroutineManager.Singleton.delayedCall(dur, () =>
        {
            changeState(ActorState.idle);
            //(headBar as MonsterHeadBar).ShowHeadBar();
        });
    }

    /// <summary>
    /// 创建血条
    /// </summary>
    public override void createHeadBar()
    {
        base.createHeadBar();
        headBar = MonsterHeadBar.CreateInstance();
        headBar.Init(this);
        headBar.ToggleVisible(false);
    }

    public override void registerAllState()
    {
        base.registerAllState();
        registerState(ActorState.attack, new ActorSkillState());
        registerState(ActorState.hurt, new ActorHurtState());
        registerState(ActorState.move, new ActorTweenMoveState());
    }

    protected override void registerSkills()
    {
        //注册普攻
        if (monoBehavior != null)
        {
            int skillId = UIUtils.GetNormalAttackSkillID(mTemplateId);
            SkillConfig config = mBehavior.GetSkillConfig(SkillType.NormalAttack);
            if (config != null)
            {
                config.skillId = skillId;
                NormalSkillId = skillId;
                getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
            }
        }
    }

    public override void destoryMe()
    {
        base.destoryMe();
        if (headBar != null)
        {
            headBar.Destroy();
            headBar = null;
        }
    }

    protected override void SetActorTypes()
    {
        t_monsterBean mBean = ConfigBean.GetBean<t_monsterBean, int>(mTemplateId);
        if (mBean != null)
        {
            Level = mBean.t_level;
            GrowType = (PetType)mBean.t_type;
            Sex = Sex.Middlesex;
            SoulType = SoulType.None;
        }
    }

    public override bool IsActorRace(ActorRace raceType)
    {
        return false;
    }

}