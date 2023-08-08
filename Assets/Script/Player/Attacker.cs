using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponState { SearchTarget = 0, AttackToTarget }
public enum unitType { player, summon, enemy }
public enum attackType { minDistance, random }

public class Attacker : MonoBehaviour
{
    float attackRange = 5f;
    public static bool CanAttack = true;
    [SerializeField] AnimationContol animator;
    [SerializeField] GameObject attackPrefap;
    [SerializeField] attackType attackType;
    WeaponState weaponState = WeaponState.SearchTarget;
    Transform attackTarget = null;
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
        ChangeState(WeaponState.SearchTarget);
    }
    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }
    void TargettingWithMinDistance(float closestDisSqr,int index)
    {
      
    }
    public IEnumerator SearchTarget()
    {
        while (true)
        {
            if (isPlayer == unitType.player || isPlayer == unitType.summon)
            {
                float closestDisSqr = Mathf.Infinity;
                for (int i = 0; i < EnemySpawner.Instance.GetEnemyList().Count; i++)
                {
                    float distance = Vector3.Distance(EnemySpawner.Instance.GetEnemyList()[i].transform.position, transform.position);
                    // Debug.Log(EnemySpawner.Instance.Enemies[i][j]);
                    if (distance <= attackRange && distance <= closestDisSqr)
                    {
                        closestDisSqr = distance;
                        attackTarget = EnemySpawner.Instance.GetEnemyList()[i].transform;
                    }
                }
            }
            if(isPlayer == unitType.enemy)
            {
                if (GameObject.FindWithTag("Player") != null)
                {
                    Transform player = GameObject.FindWithTag("Player").transform;
                    float distance = Vector3.Distance(player.position, transform.position);

                    if (distance <= attackRange)
                    {
                        attackTarget = player;
                    }
                }
            }
            if (attackTarget != null && CanAttack)
            {
                ChangeState(WeaponState.AttackToTarget);
            }
            yield return null;
        }
    }
    IEnumerator AttackToTarget()
    {
        if(isPlayer == unitType.player) animator.SetAnimation("attack", true);
        while (true)
        {
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            SpawnAttackTile();
            yield return new WaitForSeconds(unit.GetStatus(statusType.attackSpeed));
        }
    }
    void SpawnAttackTile()
    {
        GameObject clone = Instantiate(attackPrefap, spawnPoint.position, Quaternion.identity);
        float damage = unit.GetAttackDamage();
        clone.GetComponent<Projectile>().Setup(attackTarget, damage, isPlayer);
    }
}
