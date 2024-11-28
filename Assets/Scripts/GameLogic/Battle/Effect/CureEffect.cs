
/// <summary>
/// 治愈效果
/// </summary>
public class CureEffect 
{

    private SkillConfig skillConfig;

    public void Apply(Skill skill, Actor defender, long hurt)
    {
        defender.ChangeProperty(PropertyType.Hp, hurt);
        Logger.log("加血成功：" + hurt);
    }

    public void Show()
    {

    }


}


