using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Movement2D movement2D;
    Transform target;
    [SerializeField] float damage;//
    [SerializeField] unitType isPlayer;//
    public void Setup(Transform target, float damage, unitType isPlayer)
    {
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;
        this.target = target;
        this.isPlayer = isPlayer;
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("충돌");
        if (isPlayer == unitType.player)
        {
            if (!collision.CompareTag("Enemy"))
            {
                //Debug.Log("적이 아니다");
                return;
            }
        }
        if(isPlayer == unitType.enemy)
        {
            if (!collision.CompareTag("Player"))
            {

               // Debug.Log("플레이어가 아니다");
                return;
            }
        }
        if (collision.transform != target) return;
       // Debug.Log(damage);
        Destroy(gameObject);
        if (isPlayer == unitType.player || isPlayer == unitType.summon) collision.GetComponent<Unit>().Damaged(damage);
        if (isPlayer == unitType.enemy) collision.GetComponent<Player>().Unit.Damaged(damage);
    }
}