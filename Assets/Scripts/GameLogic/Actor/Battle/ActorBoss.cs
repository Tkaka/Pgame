using Data.Beans;

public class ActorBoss : Actor
{
    public ActorBoss(int temlateId, ActorType type, ActorCamp camp, long roleId) : base(temlateId, type, camp, roleId)
    {

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

    protected override void SetActorTypes()
    {
        t_monster_boosBean mBean = ConfigBean.GetBean<t_monster_boosBean, int>(mTemplateId);
        if (mBean != null)
        {
            Level = mBean.t_dengji;
            GrowType = (PetType)mBean.t_type;
            SoulType = (SoulType)mBean.t_soul_detail_type;
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(mBean.t_pet_id);
            if (petBean != null)
            {
                Sex = (Sex)petBean.t_raceiselement;
            }
        }
    }

    protected override void loadPrefab(ActorParam instanceData)
    {
        t_monster_boosBean mBean = ConfigBean.GetBean<t_monster_boosBean, int>(mTemplateId);
        //mShowObj = Res.Singleton.InstantiateModel(mBean.t_prefab, instanceData.Pos);
        ShowObj = resPacker.LoadGo(mBean.t_prefab, instanceData.Pos);
        if (mShowObj != null)
            mShowObj.transform.forward = instanceData.Dir;
    }

    protected override void registerSkills()
    {
        int skillId = 0;
        t_monster_boosBean mBean = ConfigBean.GetBean<t_monster_boosBean, int>(mTemplateId);
        if (mBean != null)
        {
            Skill skill = null;
            //注册普攻
            skillId = UIUtils.GetNormalAttackSkillID(mTemplateId);
            SkillConfig config = mBehavior.GetSkillConfig(SkillType.NormalAttack);
            if (config != null)
            {
                config.skillId = skillId;
                NormalSkillId = skillId;
                skill = TemplateFactory.CreateSkill(config, this);
                getSkillManager().registerSkill(skill, false);
            }

            //注册小技能
            //skillId = UIUtils.GetSmallSkillID(mTemplateId);
            skillId = mBean.t_xiao_jineng;
            config = mBehavior.GetSkillConfig(SkillType.SmallSkill);
            if (config != null)
            {
                config.skillId = skillId;
                SmallSkillId = skillId;
                getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
            }

            //注册绝杀技
            //skillId = UIUtils.GetMasterSkillID(mTemplateId);
            skillId = mBean.t_da_jineng;
            config = mBehavior.GetSkillConfig(SkillType.MasterSkill);
            if (config != null)
            {
                config.skillId = skillId;
                MasterSkillId = skillId;
                getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
            }

            //成长技跳过
            //核心被动技能
            skillId = mBean.t_beidong;
            CoreSkillId = skillId;
            CoreSkillLevel = 1;
            TriggerManager.Singleton.InitPassiveEffect(this, skillId, 1);
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

    public override bool IsActorRace(ActorRace raceType)
    {
        t_monster_boosBean mBean = ConfigBean.GetBean<t_monster_boosBean, int>(mTemplateId);
        if (mBean != null && !string.IsNullOrEmpty(mBean.t_race))
        {
            string soulStr = (int)raceType + "";
            return mBean.t_race.Contains(soulStr);
        }
        return false;
    }

}

