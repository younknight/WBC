using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    public static Weapon[] EquipWeapon = new Weapon[6];
    [SerializeField] EquipmentSlot[] slots = new EquipmentSlot[6];//
    [SerializeField] Unit unit;

    public Unit Unit { get => unit; set => unit = value; }

    private void Start()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
        for (int i = 0; i < slots.Length; i++)
        {
            if (EquipWeapon[i] != null)
            {
                unit.BuffStatusWithWeapon(true, EquipWeapon[i]);
            }
        }
        SetEquipManager();
    }
    public void SetEquipManager()
    {
        slots = GameObject.Find("equipmentSlotsParents").GetComponentsInChildren<EquipmentSlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Id = i;
            slots[i].FreashSlot();
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (EquipWeapon[i] != null)
            {
                slots[i].SetWeapon(EquipWeapon[i]);
            }
        }
    }

}
