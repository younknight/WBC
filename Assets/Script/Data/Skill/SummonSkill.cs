using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SummonSkill :Skill
{
    [Space(10f)]
    [Header("skillValue")]
    public float spawnTime = 4;
    public List<GameObject> summons;
    private void OnValidate() { skillType = skillType.summon; }
}
