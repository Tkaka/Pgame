using Data.Beans;
using Message.Fight;
using Message.Pet;
using System.Collections.Generic;

public class ActorPet : Actor
{

    public IHeadBar headBar1;

    public ActorPet(int temlateId, ActorType type, ActorCamp camp, long roleId) : base(temlateId, type, camp, roleId)
    {

    }

    public override void registerAllState()
    {
        base.registerAllState();
        registerState(ActorState.attack, new ActorSkillState());
        registerState(ActorState.hurt, new ActorHurtState());
        registerState(ActorState.move, new ActorTweenMoveState());
    }

    protected override void loadPrefab(ActorParam instanceData)
    {
        int star = -1;
        PetInfo petInfo = PetService.Singleton.GetPetInfo(mTemplateId);
        if (petInfo != null)
            star = petInfo.basInfo.star;
        t_petBean mBean = ConfigBean.GetBean<t_petBean, int>(mTemplateId);
        mShowObj = resPacker.LoadGo(UIUtils.GetBattlePrefab(mBean, star), instanceData.Pos);
        if (mShowObj != null)
            mShowObj.transform.forward = instanceData.Dir;
    }

    public override void createHeadBar()
    {
        base.createHeadBar();
        headBar1 = MonsterHeadBar.CreateInstance();
        headBar1.Init(this);
        headBar1.ToggleVisible(false);
    }

    public override void update()
    {
        base.update();
        if (headBar1 != null)
            headBar1.Update();
    }

    protected override void registerSkills()
    {
        int skillId = 0;

        if (mBehavior == null)
            return;

        if (GameManager.Singleton.IsDebug)
        {
            //注册普攻
            skillId = UIUtils.GetNormalAttackSkillID(mTemplateId);
            SkillConfig config = mBehavior.GetSkillConfig(SkillType.NormalAttack);
            if (config != null)
            {
                NormalSkillId = skillId;
                config.skillId = skillId;
                config.skillLevel = 1;
                getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
            }

            //注册小技能
            skillId = UIUtils.GetSmallSkillID(mTemplateId);
            config = mBehavior.GetSkillConfig(SkillType.SmallSkill);
            if (config != null)
            {
                SmallSkillId = skillId;
                config.skillId = skillId;
                config.skillLevel = 1;
                getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
            }

            //注册绝杀技
            skillId = UIUtils.GetMasterSkillID(mTemplateId);
            config = mBehavior.GetSkillConfig(SkillType.MasterSkill);
            if (config != null)
            {
                MasterSkillId = skillId;
                config.skillId = skillId;
                config.skillLevel = 1;
                getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
            }

            //成长技跳过
            //核心被动技能
            skillId = UIUtils.GetCoreSkillID(mTemplateId);
            CoreSkillId = skillId;
            CoreSkillLevel = 1;
            TriggerManager.Singleton.InitPassiveEffect(this, skillId, 1);
        }
        else
        {
            //注册普攻
            skillId = UIUtils.GetNormalAttackSkillID(mTemplateId);
            SkillConfig config = mBehavior.GetSkillConfig(SkillType.NormalAttack);
            if (config != null)
            {
                NormalSkillId = skillId;
                config.skillId = skillId;
                getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
            }


            if (FightManager.Singleton.IsReplay)
            {
                _RegisterSkills(ReplayService.Singleton.GetSkillParams(this));
            }
            else
            {

                FightParam fightParam = FightService.Singleton.GetParam(mCamp, mTemplateId);
                if (fightParam != null && fightParam.skills != null && fightParam.skills.Count > 0)
                {
                    _RegisterSkills(fightParam.skills);
                }
            }
 
        }
    }


    private void _RegisterSkills(List<SkillParam> skillParams)
    {
        if (skillParams == null)
            return;

        for (int i = 0; i < skillParams.Count; i++)
        {
            SkillParam param = skillParams[i];
            t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(param.id);
            if (skillBean.t_type == (int)SkillType.SmallSkill)
            {
                SkillConfig config = mBehavior.GetSkillConfig(SkillType.SmallSkill);
                if (config != null)
                {
                    SmallSkillId = param.id;
                    config.skillId = param.id;
                    config.skillLevel = param.level;
                    getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
                }
            }
            else if (skillBean.t_type == (int)SkillType.MasterSkill)
            {
                SkillConfig config = mBehavior.GetSkillConfig(SkillType.MasterSkill);
                if (config != null)
                {
                    MasterSkillId = param.id;
                    config.skillId = param.id;
                    config.skillLevel = param.level;
                    getSkillManager().registerSkill(TemplateFactory.CreateSkill(config, this), false);
                }
            }
            else if (skillBean.t_type == (int)SkillType.CoreSkill)
            {
                CoreSkillId = param.id;
                CoreSkillLevel = param.level;
                TriggerManager.Singleton.InitPassiveEffect(this, param.id, param.level);
            }
        }
    }

    public override void destoryMe()
    {
        base.destoryMe();
        if (headBar != null)
        {
            headBar.OnDead();
            headBar = null;
        }
        if (headBar1 != null)
        {
            headBar1.Destroy();
            headBar1 = null;
        }
    }

    protected override void SetActorTypes()
    {
        t_petBean mBean = ConfigBean.GetBean<t_petBean, int>(mTemplateId);
        if (mBean != null)
        {
            Level = 1;
            SoulType = UIUtils.GetSoulType(mBean.t_soul_detail_type);
            GrowType = (PetType)mBean.t_type;
            Sex = (Sex)mBean.t_raceiselement;
        }
    }

    public override bool IsActorRace(ActorRace raceType)
    {
        t_petBean mBean = ConfigBean.GetBean<t_petBean, int>(mTemplateId);
        if (mBean != null && !string.IsNullOrEmpty(mBean.t_race))
        {
            string soulStr = (int)raceType + "";
            return mBean.t_race.Contains(soulStr);
        }
        return false;
    }

}