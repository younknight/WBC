using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPopup : Popup
{
    #region ½Ì±ÛÅæ
    static WeaponPopup instance;
    public static WeaponPopup Instance { get => instance; set => instance = value; }
    private void OnDestroy() { Instance = null; }
    void Awake() { if (Instance == null) Instance = this; }
    #endregion
    [SerializeField] GameObject Equip;
    [SerializeField] GameObject Enforce;
    public void OpenPopup(bool isEnforce)
    {
        Enforce.SetActive(isEnforce);
        Equip.SetActive(!isEnforce);
        Open();
    }
    public void EquipRemove()//Àåºñ ÇØÁ¦¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä¤Ä
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
