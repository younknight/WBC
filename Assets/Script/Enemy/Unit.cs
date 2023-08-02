using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum statusType { maxHp, hp, attack, defence, criDamage, criRate}
public class Unit : MonoBehaviour
{
    [SerializeField] Sprite portrait;
    [SerializeField] float maxHp = 10;
    [SerializeField] float attack = 1;//
    [SerializeField] float defence = 0;//
    [SerializeField] float criDamage = 0;//
    [SerializeField] float criRate = 0;//
    int roundIndex = 0;
    [SerializeField] float hp;
    public int RoundIndex { get => roundIndex; set => roundIndex = value; }
    public float Hp { get => hp; set => hp = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }
    public Sprite Portrait { get => portrait; set => portrait = value; }

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
    public void BuffStatusWithWeapon(bool isBuff, Weapon weapon)
    {
        BuffStatus(isBuff, statusType.maxHp, weapon.hp);
        BuffStatus(isBuff, statusType.attack, weapon.attack);
        BuffStatus(isBuff, statusType.defence, weapon.defence);
        BuffStatus(isBuff, statusType.criDamage, weapon.criDamage);
        BuffStatus(isBuff, statusType.criRate, weapon.criRate);
    }
    public void BuffStatus(bool isBuff, statusType status, float value)
    {
        switch (status)
        {
            case statusType.maxHp:
                maxHp += isBuff ? value : -value;
                break;
            case statusType.attack:
                attack += isBuff ? value : -value;
                break;
            case statusType.defence:
                defence += isBuff ? value : -value;
                break;
            case statusType.criDamage:
                criDamage += isBuff ? value : -value;
                break;
            case statusType.criRate:
                criRate += isBuff ? value : -value;
                break;
        }
    }
    public void SetDefult()
    {
        maxHp = 10;
        attack = 1;
        defence = 0;
        criDamage = 0;
        criRate = 0;
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
        float percent = Random.Range(0, 100);
        bool isCri = percent < criRate;
    //    Debug.Log(isCri + " / "+ percent + " / "+ criRate);
        float totalDamgae = isCri ? attack : attack + criDamage;
        return totalDamgae;
    }
}
