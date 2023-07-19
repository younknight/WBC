using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equipment : MonoBehaviour
{
    [SerializeField] EquipmentManager equipmentManager;
    Status defaultStatus = new Status();//default
    static Weapon[] weapons = new Weapon[6];

    public Status DefaultStatus { get => defaultStatus; set => defaultStatus = value; }
    public static Weapon[] Weapons { get => weapons; set => weapons = value; }
    //private void OnDestroy()
    //{
    //    weapons = null;
    //}
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        var obj = FindObjectsOfType<Equipment>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ResetStatus()
    {
        for(int i=0;i< Weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                equipmentManager.Slots[i].SetWeapon(true, weapons[i]);
                Weapon weapon = weapons[i];
                defaultStatus.PlusStatus(weapon.hp, weapon.attack, weapon.defence, weapon.criDamage, weapon.criRate);
            }
        }
    }
    public void AddWeapon(Weapon weapon, int index)
    {
        Weapons[index] = weapon;
        defaultStatus.PlusStatus(weapon.hp,weapon.attack,weapon.defence,weapon.criDamage,weapon.criRate);
    }
    public void DeleteWeapon(Weapon weapon, int index)
    {
        Weapons[index] = null;
        defaultStatus.MinusStatus(weapon.hp, weapon.attack, weapon.defence, weapon.criDamage, weapon.criRate);
    }
    public float GetDifferce(statusType statusType, float cuurentStatus)
    {
        switch (statusType)
        {
            case statusType.maxHp:
                return cuurentStatus - defaultStatus.Hp;
            case statusType.attack:
                return cuurentStatus - defaultStatus.Attack;
            case statusType.defence:
                return cuurentStatus - defaultStatus.Defence;
            case statusType.criDamage:
                return cuurentStatus - defaultStatus.CriDamage;
            case statusType.criRate:
                return cuurentStatus - defaultStatus.CriRate;
        }
        return 0;
    }
}
