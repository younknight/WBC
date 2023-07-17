using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum inventoryType
{
    chest,
    item,
    weapon
}
public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject slotPrefap;
    [SerializeField] inventoryType inventoryType;
    [SerializeField] bool isDraggble = true;
    [SerializeField] popupType openPopupType;
    [SerializeField] bool isShowCount = true;

    [SerializeField] EquipmentSlot equipmentSlot;//

    public static List<itemInfo> items = new List<itemInfo>();
    public static List<chestInfo> chests = new List<chestInfo>();
    public static List<weaponInfo> weapons = new List<weaponInfo>();

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    public inventoryType InventoryType { get => inventoryType; }
    public EquipmentSlot EquipmentSlot { get => equipmentSlot; set => equipmentSlot = value; }

    private void Awake()
    {
        int count = GameManager.GetCount(inventoryType);
        Transform cavas = GameObject.Find("Canvas").transform;
        for(int i = 0; i < count; i++)
        {
            GameObject newSlot = Instantiate(slotPrefap, new Vector2(0,0), Quaternion.identity, cavas);
            newSlot.transform.SetParent(slotParent);
        }
        slots = slotParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Inventory = this;
            slots[i].Id = i;
            slots[i].IsShowCount = isShowCount;
            slots[i].PopupType = openPopupType;
        }
    }
    void Start()
    {
        if (!isDraggble)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                Destroy(slots[i].transform.GetChild(0).GetComponent<Drag>());
            }
        }
    }
    #region 새로운아이템 확인
    public static bool CheckNewChest(Chest chest)
    {
        for (int i = 0; i < chests.Count; i++)
        {
            if (chest == chests[i].chest) return false;
        }
        return true;
    }
    public static bool CheckNewItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (item == items[i].item) return false;
        }
        return true;
    }
    #endregion

    public void FreshSlot()
    {
        for(int i = 0; i< slots.Length; i++)
        {
            slots[i].FreshSlot(true);
        }
        if (inventoryType == inventoryType.item)
        {
            for(int i=0; i < items.Count; i++)
            {
                slots[items[i].item.id].gameObject.SetActive(true);
                slots[items[i].item.id].AddItem(items[i].item, items[i].num);
            }
        }
        if (inventoryType == inventoryType.chest)
        {
            for (int i = 0; i < chests.Count; i++)
            {
                if(chests[i].num <= 0 && chests[i].chest.id == 0) continue;
                slots[chests[i].chest.id].gameObject.SetActive(true);
                slots[chests[i].chest.id].AddChest(chests[i].chest, chests[i].num);
            }
        }
        if (inventoryType == inventoryType.weapon)
        {
            for (int i = 0; i < weapons.Count; i++)//무기 초기화
            {
                slots[weapons[i].weapon.id].gameObject.SetActive(true);
                slots[weapons[i].weapon.id].AddWeapon(weapons[i].weapon, weapons[i].num);
            }
        }
    }

    #region 아이템 추가 및 사용
    //item-----------------------------------------
    public void AddItem(Item _item, int num)
    {
        if (slots[_item.id].IsActive)
        {
            slots[_item.id].AddItem(_item, num); 
            items[items.FindIndex(x => x.item == _item)] = new itemInfo(_item, slots[_item.id].number);
            DataManager.instance.JsonSave();
            return;
        }
        slots[_item.id].gameObject.SetActive(true);
        items.Add(new itemInfo(_item, num));
        slots[_item.id].AddItem(_item, num);
        DataManager.instance.JsonSave();
    }
    public void DropItem(Item _item, int num)
    {
        if(slots[_item.id].IsActive && slots[_item.id].Item == _item)
        {
            slots[_item.id].Drop(num);
            items[items.FindIndex(x => x.item == _item)] = new itemInfo(_item, slots[_item.id].number);
        }
        DataManager.instance.JsonSave();
    }
    //item-----------------------------------------
    //chest----------------------------------------
    public void AddChest(Chest _chest, int num)
    {
        if (slots[_chest.id].IsActive)
        {
            slots[_chest.id].AddChest(_chest, num);
            chests[chests.FindIndex(x => x.chest == _chest)] = new chestInfo(_chest, slots[_chest.id].number);
            DataManager.instance.JsonSave();
            return;
        }
        slots[_chest.id].gameObject.SetActive(true);
        chests.Add(new chestInfo(_chest, num));
        slots[_chest.id].AddChest(_chest, num);
        DataManager.instance.JsonSave();
    }
    public void DropChest(Chest _chest, int num)
    {
        if (slots[_chest.id].IsActive && slots[_chest.id].Chest == _chest)
        {
            slots[_chest.id].Drop(num);
            chests[chests.FindIndex(x => x.chest == _chest)] = new chestInfo(_chest, slots[_chest.id].number);
        }
        DataManager.instance.JsonSave();
    }
    //chest----------------------------------------
    //weapon---------------------------------------
    public void AddWeapon(Weapon _weapon, int num)
    {
        if (slots[_weapon.id].IsActive)
        {
            slots[_weapon.id].AddWeapon(_weapon, num);
            weapons[weapons.FindIndex(x => x.weapon == _weapon)] = new weaponInfo(_weapon, slots[_weapon.id].number);
            DataManager.instance.JsonSave();
            return;
        }
        slots[_weapon.id].gameObject.SetActive(true);
        weapons.Add(new weaponInfo(_weapon, num));
        slots[_weapon.id].AddWeapon(_weapon, num);
        DataManager.instance.JsonSave();
    }
    public void DropWeapon(Weapon _weapon, int num)
    {
        if (slots[_weapon.id].IsActive && slots[_weapon.id].Weapon == _weapon)
        {
            slots[_weapon.id].Drop(num);
            weapons[weapons.FindIndex(x => x.weapon == _weapon)] = new weaponInfo(_weapon, slots[_weapon.id].number);
        }
        DataManager.instance.JsonSave();
    }
    //weapon---------------------------------------
    #endregion
}