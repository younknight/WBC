using System;
using System.Collections.Generic;
using UnityEngine;
public enum weaponType { weapon, accessory }
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
    public int sellPrice;

    [Space(10f)]
    [Header("Sprite")]
    public Sprite weaponImage;

    [Space(10f)]
    [Header("status")]
    public float hp = 0;
    public float attack = 0;
    public float defence = 0;
    public float criDamage = 0;
    public float criRate = 0;

    [Space(10f)]
    [Header("skill")]
    public float coolTime = 5f;
    public List<Skill> skills;
    #region Getter
    public int GetSellPrice() { return sellPrice; }
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
    }
}
