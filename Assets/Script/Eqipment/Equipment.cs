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
}
