using System.Collections.Generic;
using System.Linq;


public class SkillManager
{
    protected Map<string, Skill> mSkills;

    public Map<string, Skill> Skills
    {
        get
        {
            return mSkills;
        }
    }

    public SkillManager()
    {
        mSkills = new Map<string, Skill>();
    }

    public void registerSkill(Skill skill,bool prepareLoadEft = true)
    {
        if (skill != null)
            mSkills.add(skill.getTemplateId().ToString(), skill); 
        else
            Logger.err("注册了不存在的技能!");
    }

    public void removeSkill(int instanceId)
    {
        mSkills.remove(instanceId.ToString());
    }
    
    public Skill getSkill(int skillId)
    {
        return mSkills.get(skillId.ToString());
    }

    public Skill getRandomSkill()
    {
        var count = mSkills.Container.Keys.Count;
        var index = UnityEngine.Random.Range(0, count);

        return mSkills.Container.Values.ElementAt(index);
    }

    public void clear()
    {
        mSkills.clear();
    }

    internal List<Skill> getAllSkill()
    {
        return mSkills.Container.Values.ToList();
    } 
}
