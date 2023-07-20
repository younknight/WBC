using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    public bool isSet = false;
    [SerializeField] int id;//
    [SerializeField] popupType popupType;
    [SerializeField] bool isShowCount = true;
    [SerializeField] Image image;
    [SerializeField] Image numberFrame;
    [SerializeField] TextMeshProUGUI numberText;

    bool isActive = false;
    int number = 0;

    [SerializeField] IInformation itemInformation;
    Inventory inventory;
    public int Id { get => id; set => id = value; }
    public popupType PopupType { get => popupType; set => popupType = value; }
    public bool IsShowCount { get => isShowCount; set => isShowCount = value; }
    public Inventory Inventory { get => inventory; set => inventory = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    public IInformation ItemInformation { get => itemInformation; set => itemInformation = value; }
    public int Number { get => number; set => number = value; }

    #region で機
    public void ShowPopup()
    {
        if (PopupType == popupType.explain) ShowInfo();
        else if (PopupType == popupType.recipe) ShowRecipe();
        else if (PopupType == popupType.weapon) SetEquipment();
    }
    void SetEquipment()//濰綠太太太太太太太太太太太太太太太太太太太太太
    {
        if(number > 0 && ((Weapon)itemInformation).weaponType == inventory.EquipmentSlot.WeaponType)
        {
            InventoryManager.instance.DropItems<Weapon>((Weapon)itemInformation, 1);
            if (!inventory.EquipmentSlot.isNull())
            {
                InventoryManager.instance.AddItems<Weapon>(inventory.EquipmentSlot.Weapon, 1);
                inventory.EquipmentSlot.DeleteWeapon();
            }
            inventory.EquipmentSlot.SetWeapon(false, (Weapon)itemInformation);
        }
        PopupManager.instance.CloesPopup(popupType.weapon);
        DataManager.instance.JsonSave();
    }
    void ShowInfo()
    {
        string name = itemInformation.GetName();
        string explain = itemInformation.GetExplain();
        Sprite sprite = itemInformation.GetSprite();
        string ranking = itemInformation.GetRanking();
        int id = itemInformation.GetId();
        PopupManager.instance.OpenExplainPopup(name, explain, sprite, id, ranking);
    }
    void ShowRecipe()
    {
        if((Chest)itemInformation != null) PopupManager.instance.OpenRecipePopup((Chest)itemInformation);
    }
    #endregion
    #region 蝸煜 撮た
    public void FreshSlot(bool isFresh)
    {
        isSet = false;
        number = 0;
        gameObject.SetActive(!isFresh);
    }
    public void NewAddItemInfo<T>(T item, int num) where T : IInformation
    {
        isActive = true;
        if (!IsShowCount) numberFrame.gameObject.SetActive(false);
        itemInformation = item;
        number += num;
        numberText.text = number.ToString();
        image.sprite = item.GetSprite();
        numberFrame.color = new Color(1, 1, 1, 1);
        image.color = new Color(1, 1, 1, 1);
    }
    public void NewDrop(int num)
    {
        number -= num;
        numberText.text = number.ToString();
        //衛濛 嬴檜蠱
        if (number <= 0 && itemInformation != null)
        {
            if (itemInformation.GetId() == 1 && InfoManager.GetClassName(itemInformation) == "Chest")
            {
                gameObject.SetActive(false);
            }
        }
    }
    #endregion
}