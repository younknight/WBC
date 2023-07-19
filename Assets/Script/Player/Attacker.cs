using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponState { SearchTarget = 0, AttackToTarget }

public class Attacker : MonoBehaviour
{
    [SerializeField] GameObject attackPrefap;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float attackRate = 1f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] WeaponState weaponState = WeaponState.SearchTarget;
    public Transform attackTarget = null;
    Unit unit;
    bool isPlayer;
    private void Start()
    {
        spawnPoint = transform;
        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            //Debug.Log("player");
        }
        else isPlayer = false;
        unit = GetComponent<Unit>();
        ChangeState(WeaponState.SearchTarget);
    }
    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }
    public IEnumerator SearchTarget()
    {
        while (true)
        {
            if (isPlayer)
            {
                float closestDisSqr = Mathf.Infinity;
                for (int i = 0; i < EnemySpawner.Instance.Enemies.Count; i++)
                {
                    for (int j = 0; j < EnemySpawner.Instance.Enemies[i].Count; j++)
                    {
                        float distance = Vector3.Distance(EnemySpawner.Instance.Enemies[i][j].transform.position, transform.position);
                       // Debug.Log(EnemySpawner.Instance.Enemies[i][j]);
                        if (distance <= attackRange && distance <= closestDisSqr)
                        {
                            closestDisSqr = distance;
                            attackTarget = EnemySpawner.Instance.Enemies[i][j].transform;
                        }
                    }
                }
            }
            else
            {
                if (!isPlayer && GameObject.FindWithTag("Player") != null)
                {
                    Transform player = GameObject.FindWithTag("Player").transform;
                    float distance = Vector3.Distance(player.position, transform.position);

                    if (distance <= attackRange)
                    {
                        attackTarget = player;
                    }
                }
            }

            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }
            yield return null;
        }
    }
    IEnumerator AttackToTarget()
    {
        while (true)
        {
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            SpawnAttackTile();
            yield return new WaitForSeconds(attackRate);
        }
    }
    void SpawnAttackTile()
    {
        GameObject clone = Instantiate(attackPrefap, spawnPoint.position, Quaternion.identity);
        float damage = unit.GetAttackDamage();
        clone.GetComponent<Projectile>().Setup(attackTarget, damage, isPlayer);
    }
}
