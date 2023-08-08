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
public class Weapon : ScriptableObject, IInformation
{
    [Header("Information")]
    public int id;
    public string weaponName;
    public weaponType weaponType;
    [Multiline(5)]
    public string weapomExplain;
    public string ranking;

    [Space(10f)]
    [Header("Sprite")]
    public Sprite weaponImage;

    [Space(10f)]
    [Header("status")]
    public status status = new status(0, 0, 0, 0, 0, 0, 0);
    public status levelUpStatus = new status(0, 0, 0, 0, 0, 0, 0);
    [Space(10f)]
    [Header("skill")]
    public float coolTime = 5f;
    public ISkill skills;
    public BuffSkill buffSkill;
    public SummonSkill summonSkill;
    public NonTargetSkill nonTargetSkill;
    #region Getter
    public int GetId() { return id; }
    public string GetName() { return weaponName; }
    public string GetExplain() { return weapomExplain; }
    public string GetRanking() { return ranking; }
    public Sprite GetSprite() { return weaponImage; }
    #endregion
    private void OnValidate()
    {
        string[] nameValue = name.Split('.');
        weaponName = nameValue[1];
        id = Convert.ToInt32(nameValue[0]);
        if (buffSkill != null) skills = buffSkill;
        if (summonSkill != null) skills = summonSkill;
        if (nonTargetSkill != null) skills = nonTargetSkill;
    }
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
            case statusType.attackTarget:
                return status.attackTarget + (levelUpStatus.attackTarget * level);
            case statusType.attackSpeed:
                return status.attackSpeed + (levelUpStatus.attackSpeed * level);
        }
        return -1000;
    }
}
