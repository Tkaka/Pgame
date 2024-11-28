
public class ShowHurtEffect : ShowBase
{

    public MainEffectRes mainEftRes;

    public SkillConfig skillConfig;

    public override void Show()
    {
        Actor defender = ActorManager.Singleton.Get(actorId);
        if (defender != null && mainEftRes != null)
        {
            Actor attacker = ActorManager.Singleton.Get(mainEftRes.attackId);
            if (attacker != null)
            {
                Skill skill = attacker.getSkillManager().getSkill(mainEftRes.skillId);
                if (skill != null)
                {
                    HurtEffect hurtEffect = new HurtEffect();
                    hurtEffect.Apply(mainEftRes, skillConfig, defender);
                }
            }
        }
    }

}
