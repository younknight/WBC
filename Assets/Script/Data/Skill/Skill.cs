
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum skillType { buff, summon, NonTargetAttack }

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [Space(10f)]
    [Header("skillInfo")]
    public string skillName;
    [Multiline(5)]
    public string skillExplain;
    [Space(10f)]
    [Header("skillType")]
    public skillType skillType;
    public string GetExplain() { return skillExplain; }

    public string GetName() { return skillName; }
    public skillType GetSkillType() { return skillType; }

    private void OnValidate() { skillName = name; }
}
