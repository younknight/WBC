using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotActiver : MonoBehaviour
{
    Slot slot;
    InventoryManager inventoryManager;
    private void Awake()
    {
        slot = GetComponent<Slot>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }
    public void ActivateSlot()
    {
        if (slot.PopupType == popupType.explain) ShowInfo();
        if (slot.PopupType == popupType.recipe) ShowRecipe();
        if (slot.PopupType == popupType.weapon) SetEquipment();
        if (slot.PopupType == popupType.enforce) SetEnforceTarget();
        if (slot.PopupType == popupType.autoCraft) SetAutoCraft();
    }
    #region 十茎 適遣 戚坤闘
    void SetAutoCraft()
    {
        AutoCrafter.Instance.Slot.SetChest((Chest)slot.Item);
        ChestPopup.Instance.CloseStart();
    }
    void SetEquipment()//舌搾びびびびびびびびびびびびびびびびびびびびび
    {
        if (slot.Count > 0 && ((Weapon)slot.Item).weaponType == EquipmentSlot.currentSelectedSlot.WeaponType)
        {
            inventoryManager.DropItems(slot.Item, 1);
            if (EquipmentSlot.currentSelectedSlot.Weapon != null)
            {
                inventoryManager.AddItems(EquipmentSlot.currentSelectedSlot.Weapon, 1);
            }
            EquipmentSlot.currentSelectedSlot.SetWeapon((Weapon)slot.Item);
            EquipmentManager.EquipWeapon[EquipmentSlot.currentSelectedSlot.Id] = (Weapon)slot.Item;
        }
        EquipmentSlot.currentSelectedSlot.FreashSlot();
        WeaponPopup.Instance.CloseStart();
        DataManager.instance.JsonSave();
        EquipmentManager.instance.Initalize();
    }
    void SetEnforceTarget()
    {
        if (slot.Count > 0)
        {
            EnforceManager.Instance.SetWeapon((Weapon)slot.Item);
            WeaponPopup.Instance.CloseStart();
        }
    }
    void ShowInfo()
    {
        ExplainPopup.Instance.Open();
        ExplainPopup.Instance.SetExplain(slot.Item);
    }
    void ShowRecipe()
    {
        if ((Chest)slot.Item != null)
        {
            ChestPopup.Instance.Close();
            RecipePopup.Instance.SetRecipe((Chest)slot.Item);
            RecipePopup.Instance.Open();
        }
    }
    #endregion
}
