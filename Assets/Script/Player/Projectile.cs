using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Movement2D movement2D;
    Transform target;
    float damage;
    [SerializeField] bool isPlayer;//
    public void Setup(Transform target, float damage, bool isPlayer)
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
        Debug.Log("asd");
        if (isPlayer)
        {
            if (!collision.CompareTag("Enemy")) return;
        }
        else
        {
            if (!collision.CompareTag("Player")) return;
        }
        if (collision.transform != target) return;
        Destroy(gameObject);
        collision.GetComponent<Unit>().Damaged(damage);
    }
}