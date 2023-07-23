using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPopup : Popup
{
    [SerializeField] GameObject Equip;
    [SerializeField] GameObject Enforce;
    static WeaponPopup instance;
    public static WeaponPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    public void OpenPopup(bool isEnforce)
    {
        Enforce.SetActive(isEnforce);
        Equip.SetActive(!isEnforce);
        Open();
    }
    public void EquipRemove()//舌搾 背薦つつつつつつつつつつつつつつ
    {

        if (EquipmentSlot.currentSelectedSlot.Weapon != null)
        {
            InventoryManager.instance.AddItems<Weapon>(EquipmentSlot.currentSelectedSlot.Weapon, 1);
            EquipmentManager.instance.Unit.BuffStatusWithWeapon(false, EquipmentSlot.currentSelectedSlot.Weapon);
            EquipmentManager.EquipWeapon[EquipmentSlot.currentSelectedSlot.Id] = null;
            EquipmentSlot.currentSelectedSlot.FreashSlot();
            DataManager.instance.JsonSave();

        }
        Close();
    }
}
