using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AreaType{ circle, square }
[CreateAssetMenu]
public class NonTargetSkill : Skill
{
    [Space(10f)]
    [Header("skillValue")]
    public float radiusX = 1f;
    public float radiusY = 1f;
    public float damage = 1;
    public AreaType areaType;
    public GameObject skillEffect;
    private void OnValidate()
    {
        skillType = skillType.NonTargetAttack;
        if (areaType == AreaType.circle) radiusY = radiusX;
    }
}
