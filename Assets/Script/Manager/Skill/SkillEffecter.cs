using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffecter : MonoBehaviour
{
    NonTargetSkill skill;
    Vector3 attackPoint;
    Collider2D[] colliders;

    public void Setup(NonTargetSkill skill, Vector3 attackPoint)
    {
        this.skill = skill;
        this.attackPoint = attackPoint;
        gameObject.transform.localScale = new Vector3(skill.radiusX, skill.radiusY, 1);
    }
    void TryOverlap()
    {
        if (skill.areaType == AreaType.circle) colliders = Physics2D.OverlapCircleAll(attackPoint, skill.radiusX / 2);
        if (skill.areaType == AreaType.square) colliders = Physics2D.OverlapAreaAll(new Vector2(attackPoint.x +  skill.radiusX /2, attackPoint.y + skill.radiusY / 2), new Vector2(attackPoint.x - skill.radiusX / 2, attackPoint.y - skill.radiusY / 2));
    }
    public void Attack()
    {
        TryOverlap();
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != null)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    collider.GetComponent<Unit>().Damaged(skill.damage);
                }
            }
        }
    }
    public void FinalAttack()
    {
        Attack();
        Destroy(gameObject);
    }
}
