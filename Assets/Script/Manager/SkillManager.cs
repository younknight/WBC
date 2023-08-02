using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] Player player;

    private void Awake()
    {
        if (EquipmentManager.instance != null) EquipmentManager.instance.SetEquipManager();
    }


    public void UseSkill(Weapon weapon)
    {
        Skill skill = weapon.skills;
        switch (skill.skillType)
        {
            case skillType.buff:
                StartCoroutine(BuffPlayer(skill));
                break;
        }
    }
    IEnumerator BuffPlayer(Skill skill)
    {
        float attack = skill.buffAttack;
        float defence = skill.buffdDfence;
        float criDamage = skill.buffCriDamage;
        float criRate = skill.buffCriRate;
        player.Unit.BuffStatus(true, statusType.attack, attack);
        player.Unit.BuffStatus(true, statusType.defence, defence);
        player.Unit.BuffStatus(true, statusType.criDamage, criDamage);
        player.Unit.BuffStatus(true, statusType.criRate, criRate);
        yield return new WaitForSeconds(skill.buffTime);
        player.Unit.BuffStatus(false, statusType.attack, attack);
        player.Unit.BuffStatus(false, statusType.defence, defence);
        player.Unit.BuffStatus(false, statusType.criDamage, criDamage);
        player.Unit.BuffStatus(false, statusType.criRate, criRate);
    }
}
