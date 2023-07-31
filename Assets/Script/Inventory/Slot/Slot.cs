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

    public void ActivateSlot()
    {
        if (PopupType == popupType.explain) ShowInfo();
        else if (PopupType == popupType.recipe) ShowRecipe();
        else if (PopupType == popupType.weapon) SetEquipment();
        else if (PopupType == popupType.enforce) SetEnforceTarget();
        else if (PopupType == popupType.autoCraft) SetAutoCraft();
    }
    #region 슬롯 클릭 이벤트
    void SetAutoCraft()
    {
        AutoCrafter.Instance.Slot.SetChest((Chest)itemInformation);
        ChestPopup.Instance.CloseStart();
    }
    void SetEquipment()//장비ㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣ
    {
        if (number > 0 && ((Weapon)itemInformation).weaponType == EquipmentSlot.currentSelectedSlot.WeaponType)
        {
            InventoryManager.instance.DropItems<Weapon>((Weapon)itemInformation, 1);
            if(EquipmentSlot.currentSelectedSlot.Weapon != null)
            {
                InventoryManager.instance.AddItems<Weapon>(EquipmentSlot.currentSelectedSlot.Weapon, 1);
            }
            EquipmentSlot.currentSelectedSlot.SetWeapon((Weapon)itemInformation);
            EquipmentManager.EquipWeapon[EquipmentSlot.currentSelectedSlot.Id] = (Weapon)itemInformation;
        }
        EquipmentSlot.currentSelectedSlot.FreashSlot();
        WeaponPopup.Instance.CloseStart();
        DataManager.instance.JsonSave();
        EquipmentManager.instance.Initalize();
    }
    void SetEnforceTarget()
    {
        if (number > 0)
        {
            EnforceManager.Instance.SetWeapon((Weapon)itemInformation);
            WeaponPopup.Instance.CloseStart();
        }
    }
    void ShowInfo()
    {
        ExplainPopup.Instance.SetExplain(itemInformation);
        ExplainPopup.Instance.Open();
    }
    void ShowRecipe()
    {
        if ((Chest)itemInformation != null)
        {
            RecipePopup.Instance.SetRecipe((Chest)itemInformation);
            RecipePopup.Instance.Open();
        }
    }
    #endregion
    #region 슬롯 세팅
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
        //시작 아이템
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