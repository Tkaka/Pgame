
using Data.Beans;
using System;


public class TemplateFactory : SingletonTemplate<TemplateFactory>
 {

    public static Skill CreateSkill(SkillConfig config, Actor owner)
    {
        t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(config.skillId);
        if (skillBean == null)
        {
            Logger.err("找不到技能：" + config.skillId);
            return null;
        }
        string skillType = Enum.GetName(typeof(SkillTemplate), config.skillTemplate);
        Type classType = Type.GetType(skillType);
        Skill skill = Activator.CreateInstance(classType) as Skill;
        if (skill != null)
            skill.Init(config, owner);
        return skill;
    }

    public static BaseEffect CreateEffect(int templateId)
    {
        string effectType = Enum.GetName(typeof(SkillEffectType), templateId);
        Type classType = Type.GetType(effectType);
        BaseEffect effect = Activator.CreateInstance(classType) as BaseEffect;
        if (effect == null)
        {
            Logger.err("TemplateFactory:CreateEffect:创建效果失败");
        }
        return effect;
    }


}

