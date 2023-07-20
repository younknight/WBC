using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] int id;//
    [SerializeField] Weapon weapon;//
    [Header("기본 이미지")]
    [SerializeField] Image weaponImage;
    [SerializeField] weaponType weaponType;
    [SerializeField] Sprite[] defaultImage;//0 w, 1 a
    public static EquipmentSlot currentSelectedSlot;
    public Weapon Weapon { get => weapon; set => weapon = value; }
    public weaponType WeaponType { get => weaponType; set => weaponType = value; }
    public int Id { get => id; set => id = value; }

    public void OpenPopup()
    {
        PopupManager.instance.OpenWeaponPopup();
        currentSelectedSlot = this;
    }
    public void FreashSlot()
    {
        weapon = null;
        weaponImage.sprite = defaultImage[(int)weaponType];
        weaponImage.color = new Color(1, 1, 1, 0.5f);
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weaponImage.sprite = weapon.weaponImage;
        weaponImage.color = new Color(1, 1, 1, 1);
    }
    private void OnDestroy()
    {
        currentSelectedSlot = null;
    }
}
