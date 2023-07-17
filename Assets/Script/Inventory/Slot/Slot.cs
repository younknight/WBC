using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    [SerializeField] int id;//
    [SerializeField] popupType popupType;
    [SerializeField] bool isShowCount = true;
    [SerializeField] Image image;
    [SerializeField] Image numberFrame;
    [SerializeField] TextMeshProUGUI numberText;

    bool isActive = false;
    public int number = 0;//
    [SerializeField] private Item _item;//
    [SerializeField] private Chest _chest;//
    [SerializeField] private Weapon _weapon;//
    Inventory inventory;
    public Chest Chest { get => _chest; }
    public Item Item { get => _item; }
    public Weapon Weapon { get => _weapon; }
    public int Id { get => id; set => id = value; }
    public popupType PopupType { get => popupType; set => popupType = value; }
    public bool IsShowCount { get => isShowCount; set => isShowCount = value; }
    public Inventory Inventory { get => inventory; set => inventory = value; }
    public bool IsActive { get => isActive; set => isActive = value; }

    #region 팝업
    public void ShowPopup()
    {
        if (PopupType == popupType.explain) ShowInfo();
        else if (PopupType == popupType.recipe) ShowRecipe();
        else if (PopupType == popupType.weapon) SetEquipment();
    }
    void SetEquipment()//장비ㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣ
    {
        if(number > 0 && _weapon.weaponType == inventory.EquipmentSlot.WeaponType)
        {
            InventoryManager.instance.DropWeapon(_weapon, 1);
            if (!inventory.EquipmentSlot.isNull())
            {
                InventoryManager.instance.AddWeapon(inventory.EquipmentSlot.Weapon, 1);
                inventory.EquipmentSlot.DeleteWeapon();
            }
            inventory.EquipmentSlot.SetWeapon(false, _weapon);
        }
        PopupManager.instance.CloesPopup(popupType.weapon);
        DataManager.instance.JsonSave();
    }
    void ShowInfo()
    {
        string name = "";
        string explain = "";
        Sprite sprite = null;
        string ranking = "None";
        int id = 0;
        if (_item != null)
        {
            name = _item.itemName;
            explain = _item.itemExplain;
            sprite = _item.itemImage;
            ranking = _item.ranking;
            id = _item.id;
        }
        if (_chest != null)
        {
            name = _chest.chestName + " 상자";
            explain = _chest.chestExplain;
            sprite = _chest.chetImage;
            ranking = _chest.ranking;
            id = _chest.id;
        }
        if (_weapon != null)
        {
            name = _weapon.weaponName;
            explain = _weapon.weapomExplain;
            sprite = _weapon.weaponImage;
            ranking = _weapon.ranking;
            id = _weapon.id;
        }
        PopupManager.instance.OpenExplainPopup(name, explain, sprite, id, ranking);
    }
    void ShowRecipe()
    {
        if(_chest != null) PopupManager.instance.OpenRecipePopup(_chest);
    }
    #endregion
    #region 슬롯 세팅
    public void FreshSlot(bool isFresh)
    {
        _item = null;
        _chest = null;
        _weapon = null;
        number = 0;
        gameObject.SetActive(!isFresh);
    }
    public void Drop(int num)
    {
        number -= num;
        numberText.text = number.ToString();
        //시작 아이템
        if(number <= 0 && _chest != null)
        {
            if(_chest.id == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void AddItem(Item item, int num)
    {
        isActive = true;
        if (!IsShowCount) numberFrame.gameObject.SetActive(false);
        _item = item;
        number += num;
        numberText.text = number.ToString();
        image.sprite = item.itemImage;
        numberFrame.color = new Color(1, 1, 1, 1);
        image.color = new Color(1, 1, 1, 1);
    }
    public void AddChest(Chest chest, int num)
    {
        isActive = true;
        if (!IsShowCount) numberFrame.gameObject.SetActive(false);
        _chest = chest;
        number += num;
        numberText.text = number.ToString();
        image.sprite = chest.chetImage;
        numberFrame.color = new Color(1, 1, 1, 1);
        image.color = new Color(1, 1, 1, 1);
    }
    public void AddWeapon(Weapon weapon, int num)
    {
        isActive = true;
        _weapon = weapon;
        number += num;
        numberText.text = number.ToString();
        image.sprite = weapon.weaponImage;
        numberFrame.color = new Color(1, 1, 1, 1);
        image.color = new Color(1, 1, 1, 1);
    }
    #endregion
}