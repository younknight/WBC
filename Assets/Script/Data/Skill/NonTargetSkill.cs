using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NonTargetSkill : ScriptableObject, ISkill
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
    public float radius = 1f;
    public float damage = 1;

    public string GetExplain() { return skillExplain; }

    public string GetName() { return skillName; }
    public skillType GetSkillType() { return skillType; }

    private void OnValidate() { skillName = name; }
}
