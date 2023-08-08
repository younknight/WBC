using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SummonSkill : ScriptableObject, ISkill
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
    public float spawnTime = 4;
    public List<GameObject> summons;







    public string GetExplain() { return skillExplain; }

    public string GetName() { return skillName; }
    public skillType GetSkillType() { return skillType; }

    private void OnValidate() { skillName = name; }
}
