public enum skillType { buff, summon, NonTargetAttack }

public interface ISkill
{
    public string GetName();
    public string GetExplain();
    public skillType GetSkillType();

}