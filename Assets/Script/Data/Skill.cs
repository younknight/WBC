using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum skillType { buff }
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
    [Space(10f)]
    [Header("skillValue")]
    public float buffTime = 4;
    public float buffHp = 0;
    public float buffAttack = 0;
    public float buffdDfence = 0;
    public float buffCriDamage = 0;
    public float buffCriRate = 0;
    [Header("Spawner")]
    public List<GameObject> summons;
    private void OnValidate()
    {
        skillName = name;
    }
}
