using System;
using System.Collections.Generic;
using UnityEngine;
public enum weaponType { weapon, accessory }
[CreateAssetMenu]
public class Weapon : ScriptableObject
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
    public float hp = 0;
    public float attack = 0;
    public float defence = 0;
    public float criDamage = 0;
    public float criRate = 0;

    private void OnValidate()
    {
        string[] idNum = name.Split('.');
        weaponName = idNum[1];
        id = Convert.ToInt32(idNum[0]);
    }
}
