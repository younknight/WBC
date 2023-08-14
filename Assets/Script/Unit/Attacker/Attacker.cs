using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum WeaponState { SearchTarget = 0, AttackToTarget }
public enum unitType { player, summon, enemy }
public enum attackType { minDistance, random }

public class Attacker : MonoBehaviour
{
    float attackRange = 10f;
    public static bool CanAttack = true;
    [SerializeField] AnimationContol animator;
    [SerializeField] GameObject attackPrefap;
    [SerializeField] attackType attackType;
    [SerializeField] WeaponState weaponState = WeaponState.SearchTarget;
    [SerializeField] List<Transform> attackTarget = new List<Transform>();//
    Transform spawnPoint;
    Unit unit;
    unitType isPlayer;

    public Unit Unit { get => unit; set => unit = value; }
    private void Awake()
    {
        unit = GetComponent<Unit>();
        spawnPoint = transform;
        UnitSetup(unit);
    }
    public void UnitSetup(Unit unit)
    {
        this.unit = unit;
    }
    private void Start()
    {
        spawnPoint = transform;
        if (gameObject.CompareTag("Player"))  {  isPlayer = unitType.player; }
        if (gameObject.CompareTag("Enemy")) { isPlayer = unitType.enemy; }
        if (gameObject.CompareTag("Summon")) { isPlayer = unitType.summon; }
        if(isPlayer != unitType.player) ChangeState(WeaponState.SearchTarget);
    }
    public void ChangeState(WeaponState newState)
    {
        StopAllCoroutines();
        if (newState == WeaponState.SearchTarget)
        {
            SearchTarget();
        }
        if(newState == WeaponState.AttackToTarget) StartCoroutine(AttackToTarget());
        weaponState = newState;
    }
    public void SearchTarget()
    {
        attackTarget = new List<Transform>();
        if (isPlayer == unitType.player || isPlayer == unitType.summon)
        {
            if (EnemySpawner.Instance.GetEnemyList() != null)
            {
                float distance;
                List<Unit> enemies = EnemySpawner.Instance.GetEnemyList().ToList();
                if (attackType == attackType.minDistance)
                {
                    int count = enemies.Count;
                    for (int i = 0; i < unit.GetStatus(statusType.attackTarget) && i < count; i++)
                    {
                        float closestDisSqr = Mathf.Infinity;
                        int index = 0;
                        for (int j = 0; j < enemies.Count; j++)
                        {
                            distance = Vector3.Distance(enemies[j].transform.position, transform.position);
                            if (distance <= attackRange && distance <= closestDisSqr)
                            {
                                index = j;
                                closestDisSqr = distance;
                            }
                        }
                        attackTarget.Add(enemies[index].transform);
                        enemies.RemoveAt(index);
                    }
                }
                if (attackType == attackType.random)
                {
                    for (int i = 0; i < unit.GetStatus(statusType.attackTarget) && i < enemies.Count; i++)
                    {
                        int index = Random.Range(0, enemies.Count);
                        distance = Vector3.Distance(enemies[index].transform.position, transform.position);
                        if (distance <= attackRange)
                        {
                            attackTarget.Add(enemies[index].transform);
                            enemies.RemoveAt(index);
                        }
                    }
                }
            }
        }
        if (isPlayer == unitType.enemy)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                Transform player = GameObject.FindWithTag("Player").transform;
                float distance = Vector3.Distance(player.position, transform.position);
                if (distance <= attackRange)
                {
                    attackTarget.Add(player);
                }
            }
        }
        ChangeState(WeaponState.AttackToTarget);
    }
    IEnumerator AttackToTarget()
    {
        if(isPlayer == unitType.player) animator.SetAnimation("attack", true);
        while (true)
        {
            int count = attackTarget.Count;
            attackTarget.RemoveAll(x => x == null);
            if (count > attackTarget.Count)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            if(CanAttack) SpawnAttackTile();
            yield return new WaitForSeconds(unit.GetStatus(statusType.attackSpeed));
        }
    }
    void SpawnAttackTile()
    {
        for (int i = 0; i < attackTarget.Count; i++)
        {
            GameObject clone = Instantiate(attackPrefap, spawnPoint.position, Quaternion.identity);
            float damage = unit.GetAttackDamage();
            clone.GetComponent<Projectile>().Setup(attackTarget[i], damage, isPlayer);
        }
    }
}
