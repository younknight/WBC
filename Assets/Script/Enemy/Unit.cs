using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum statusType { maxHp, hp, attack, defence, criDamage, criRate}
public class Unit : MonoBehaviour
{
    [SerializeField] float maxHp = 1;
    [SerializeField] float attack = 1;//
    [SerializeField] float defence = 0;//
    [SerializeField] float criDamage = 0;//
    [SerializeField] float criRate = 0;//
    int roundIndex = 0;
    [SerializeField] float hp;
    public int RoundIndex { get => roundIndex; set => roundIndex = value; }
    public float Hp { get => hp; set => hp = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }

    // Start is called before the first frame update
    void Start()
    {
        Hp = maxHp;
    }
    public float GetStatus(statusType status)
    {
        switch (status)
        {
            case statusType.maxHp:
                return maxHp;
            case statusType.hp:
                return hp;
            case statusType.attack:
                return attack;
            case statusType.defence:
                return defence;
            case statusType.criDamage:
                return criDamage;
            case statusType.criRate:
                return criRate;
        }
        return 0;
    }
    public void SetStatus(statusType status, float value)
    {
        switch (status)
        {
            case statusType.maxHp:
                maxHp = value;
                break;
            case statusType.hp:
                hp = value;
                break;
            case statusType.attack:
                attack = value;
                break;
            case statusType.defence:
                defence = value;
                break;
            case statusType.criDamage:
                criDamage = value;
                break;
            case statusType.criRate:
                criRate = value;
                break;
        }
    }
    public void Setup(Status status)
    {
        maxHp = status.Hp;
        attack = status.Attack;
        defence = status.Defence;
        criDamage = status.CriDamage;
        criRate = status.CriRate;
        Hp = maxHp;

    }
    public void Damaged(float damage)
    {
        float totalDamage = damage - defence > 1 ? damage - defence : 1; 
        Hp -= totalDamage;
        if(Hp <= 0)
        {
            if(gameObject.tag == "Enemy")
            {
                EnemySpawner.Instance.DestroyEnemy(this, roundIndex);
                Destroy(gameObject);
            }
            if(gameObject.tag == "Player")
            {

            }
            //»ç¸Á
        }
    }
    public float GetAttackDamage()
    {
        float totalDamgae = Random.Range(0,100) < criRate ? attack : attack + criDamage;
        return totalDamgae;
    }
}
