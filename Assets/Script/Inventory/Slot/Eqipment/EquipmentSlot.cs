using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] EquipmentManager equipmentManager;
    [SerializeField] int id;//
    [SerializeField] Image image;
    [SerializeField] weaponType weaponType;
    [SerializeField] Sprite typeAccessary;
    [SerializeField] Sprite typeWeapon;
    [field: SerializeField]  Weapon weapon;

    public Weapon Weapon { get => weapon; set => weapon = value; }
    public weaponType WeaponType { get => weaponType; set => weaponType = value; }
    public int Id { get => id; set => id = value; }
    public EquipmentManager EquipmentManager { get => equipmentManager; set => equipmentManager = value; }

    public void OpenPopup()
    {
        PopupManager.instance.Popups[popupType.weapon].SetSlot(this);
        PopupManager.instance.OpenPopup(popupType.weapon);
    }
    #region ½½·Ô ¼³Á¤
    public bool isNull()
    {
        return weapon == null;
    }
    public void DeleteWeapon()
    {
        EquipmentManager.DeleteWeapon(weapon,id);
        ClearSlot();
    }
    public void ClearSlot()
    {
        weapon = null;
        image.sprite = weaponType == weaponType.weapon ? typeAccessary : typeWeapon;
        image.color = new Color(1, 1, 1, 0.5f);
    }
    public void SetWeapon(bool isReset, Weapon weapon)
    {
        this.weapon = weapon;
        if(!isReset) EquipmentManager.AddWeapon(weapon,id);
        image.color = new Color(1, 1, 1, 1);
        image.sprite = weapon.weaponImage;
    }
    #endregion
}
