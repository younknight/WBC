using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TouchDetecter nonTargetArea;
    private void Awake()
    {
        if (EquipmentManager.instance != null) EquipmentManager.instance.SetEquipManager();
    }


    bool isNonTarget(skillType skillType)
    {
        switch (skillType)
        {
            case skillType.buff:
                return false;
            case skillType.summon:
                return false;
            case skillType.NonTargetAttack:
                return true;
        }
        return false;
    }
    public void UseSkill(Weapon weapon, CircleTimer timer)
    {
        Skill skill = weapon.skills;
        if (!isNonTarget(weapon.skills.GetSkillType()))
        {
            timer.TimerStart(weapon.skills.coolTime);
        }
        switch (skill.GetSkillType())
        {
            case skillType.buff:
                StartCoroutine(BuffPlayer((BuffSkill)skill));
                player.Attacker.ChangeState(WeaponState.SearchTarget);
                break;
            case skillType.summon:
                StartCoroutine(SpawnSummon((SummonSkill)skill));
                break;
            case skillType.NonTargetAttack:
                nonTargetArea.transform.parent.gameObject.SetActive(true);
                nonTargetArea.Setup((NonTargetSkill)skill, timer, weapon.skills.coolTime);
                break;
        }
    }
    IEnumerator SpawnSummon(SummonSkill skill)
    {
        List<GameObject> summons = new List<GameObject>();
        for(int i = 0; i < skill.summons.Count; i++)
        {
            GameObject newSummon = Instantiate(skill.summons[i]);
            player.TargetCursor.AddSummons(newSummon.transform);
            summons.Add(newSummon);
        }
        yield return new WaitForSeconds(skill.spawnTime);
        foreach (GameObject summon in summons)
        {
            player.TargetCursor.DeleteSummon(summon.transform);
            Destroy(summon);
        }
    }
    IEnumerator BuffPlayer(BuffSkill skill)
    {
        float attack = skill.buffAttack;
        float attackSpeed = skill.buffAttackSpeed;
        float attackTarget = skill.buffAttackTarget;
        float defence = skill.buffdDfence;
        float criDamage = skill.buffCriDamage;
        float criRate = skill.buffCriRate;

        float attackSpeedDifference = 0;
        if(player.Unit.GetStatus(statusType.attackSpeed) - attackSpeed < 0.1f )
        {
            attackSpeedDifference = player.Unit.GetStatus(statusType.attackSpeed) - attackSpeed - 0.1f;
        }


        player.Unit.BuffStatus(true, statusType.attack, attack);
        player.Unit.BuffStatus(true, statusType.attackSpeed, attackSpeed);
        player.Unit.BuffStatus(true, statusType.attackTarget, attackTarget);
        player.Unit.BuffStatus(true, statusType.defence, defence);
        player.Unit.BuffStatus(true, statusType.criDamage, criDamage);
        player.Unit.BuffStatus(true, statusType.criRate, criRate);
        yield return new WaitForSeconds(skill.buffTime);
        player.Unit.BuffStatus(false, statusType.attack, attack);
        player.Unit.BuffStatus(false, statusType.attackSpeed, attackSpeed + attackSpeedDifference);
        player.Unit.BuffStatus(false, statusType.attackTarget, attackTarget);
        player.Unit.BuffStatus(false, statusType.defence, defence);
        player.Unit.BuffStatus(false, statusType.criDamage, criDamage);
        player.Unit.BuffStatus(false, statusType.criRate, criRate);
    }
}
