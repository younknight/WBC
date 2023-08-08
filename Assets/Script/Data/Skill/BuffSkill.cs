using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffSkill : ScriptableObject, ISkill
{
    [Space(10f)]
    [Header("skillInfo")]
    public string skillName;
    [Multiline(5)]
    public string skillExplain;
    [Space(10f)]
    [Header("skillType")]
    public skillType skillType;
    [Space(10f)]
    [Header("skillValue")]
    public float buffTime = 4;
    public float buffAttack = 0;
    public float buffAttackSpeed = 0;
    public float buffAttackTarget = 0;
    public float buffdDfence = 0;
    public float buffCriDamage = 0;
    public float buffCriRate = 0;

    public string GetExplain() { return skillExplain; }

    public string GetName() { return skillName; }
    public skillType GetSkillType() { return skillType; }

    private void OnValidate() { skillName = name; }
    
}
