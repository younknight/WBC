using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    [SerializeField] Equipment equipment;//
    [SerializeField] Transform slotParent;
    [SerializeField] EquipmentSlot[] slots;

    public Equipment Equipment { get => equipment; set => equipment = value; }
    public EquipmentSlot[] Slots { get => slots; set => slots = value; }

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<EquipmentSlot>();
        for(int i=0;i< slots.Length; i++)
        {
            slots[i].Id = i;
        }
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        FreshSlot();
    }
    public void AddWeapon(Weapon weapon,int id)
    {
        equipment.AddWeapon(weapon, id);
    }
    public void DeleteWeapon(Weapon weapon,int id)
    {
        equipment.DeleteWeapon(weapon,id);
    }
    public Status GetStatus()
    {
        return equipment.DefaultStatus;
    }
    public void FreshSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }
}
