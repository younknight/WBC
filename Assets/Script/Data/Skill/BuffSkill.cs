using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffSkill : Skill
{
    [Space(10f)]
    [Header("skillValue")]
    public float buffTime = 4;
    public float buffAttack = 0;
    public float buffAttackSpeed = 0;
    public float buffAttackTarget = 0;
    public float buffdDfence = 0;
    public float buffCriDamage = 0;
    public float buffCriRate = 0;

    private void OnValidate() {   skillType = skillType.buff; }

}
