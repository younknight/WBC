using System;
using UnityEngine;
public enum weaponType { weapon, accessory }

[System.Serializable]
public struct status
{
    public float hp;
    public float attack;
    public float attackSpeed;
    public float attackTarget;
    public float defence;
    public float criDamage;
    public float criRate;

    public status(float hp, float attack, float attackSpeed, float attackTarget, float defence, float criDamage, float criRate)
    {
        this.hp = hp;
        this.attack = attack;
        this.attackSpeed = attackSpeed;
        this.attackTarget = attackTarget;
        this.defence = defence;
        this.criDamage = criDamage;
        this.criRate = criRate;
    }
}

[CreateAssetMenu]
public class Weapon : Item
{
    public weaponType weaponType;
    [Space(10f)]
    [Header("status")]
    public status status = new status(0, 0, 0, 0, 0, 0, 0);
    public status levelUpStatus = new status(0, 0, 0, 0, 0, 0, 0);
    [Space(10f)]
    [Header("skill")]
    public Skill skills;
    public float GetStatus(statusType statusType, int level)
    {
        level -= 1;
        switch (statusType)
        {
            case statusType.maxHp:
                return status.hp + (levelUpStatus.hp * level);
            case statusType.attack:
                return status.attack + (levelUpStatus.attack * level);
            case statusType.defence:
                return status.defence + (levelUpStatus.defence * level);
            case statusType.criDamage:
                return status.criDamage + (levelUpStatus.criDamage * level);
            case statusType.criRate:
                return status.criRate + (levelUpStatus.criRate * level);
            case statusType.attackSpeed:
                return status.attackSpeed + (levelUpStatus.attackSpeed * level);
            case statusType.attackTarget:
                return status.attackTarget + (levelUpStatus.attackTarget * level);
        }
        return -1000;
    }
}
