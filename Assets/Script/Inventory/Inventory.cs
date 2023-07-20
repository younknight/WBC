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

    private static List<itemInfo> items = new List<itemInfo>();
    private static List<chestInfo> chests = new List<chestInfo>();
    private static List<weaponInfo> weapons = new List<weaponInfo>();

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    public inventoryType InventoryType { get => inventoryType; }
    public EquipmentSlot EquipmentSlot { get => equipmentSlot; set => equipmentSlot = value; }
    public static List<itemInfo> Items { get => items; set => items = value; }
    public static List<chestInfo> Chests { get => chests; set => chests = value; }
    public static List<weaponInfo> Weapons { get => weapons; set => weapons = value; }
    private void OnDestroy()
    {
        items = null;
        chests = null;
        weapons = null;
    }
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
        for (int i = 0; i < Chests.Count; i++)
        {
            if (chest == Chests[i].chest) return false;
        }
        return true;
    }
    public static bool CheckNewItem(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (item == Items[i].item) return false;
        }
        return true;
    }
    #endregion

    public void FreshSlot(bool isTrue)
    {
        for(int i = 0; i< slots.Length; i++)
        {
            slots[i].FreshSlot(true);
        }
        if (inventoryType == inventoryType.item)
        {
            for(int i=0; i < Items.Count; i++)
            {
                slots[Items[i].item.id].isSet = true;
                slots[Items[i].item.id].gameObject.SetActive(true);
                slots[Items[i].item.id].AddItem(Items[i].item, Items[i].num);
            }
        }
        if (inventoryType == inventoryType.chest)
        {
            for (int i = 0; i < Chests.Count; i++)
            {
                slots[Chests[i].chest.id].isSet = true;
                if (Chests[i].num <= 0 && Chests[i].chest.id == 1) continue;
                slots[Chests[i].chest.id].gameObject.SetActive(true);
                slots[Chests[i].chest.id].AddChest(Chests[i].chest, Chests[i].num);
            }
        }
        if (inventoryType == inventoryType.weapon)
        {
            for (int i = 0; i < Weapons.Count; i++)//무기 초기화
            {
                slots[Weapons[i].weapon.id].isSet = true;
                slots[Weapons[i].weapon.id].gameObject.SetActive(true);
                slots[Weapons[i].weapon.id].AddWeapon(Weapons[i].weapon, Weapons[i].num);
            }
        }
    }

    #region 아이템 추가 및 사용
    //item-----------------------------------------
    public void AddItem(Item _item, int num)
    {
        //Debug.Log(_item.id + "/ "+ Items.FindIndex(x => x.item == _item) + "/" + slots[_item.id].IsActive);
        if (slots[_item.id].isSet)
        {
            slots[_item.id].AddItem(_item, num); 
            Items[Items.FindIndex(x => x.item == _item)] = new itemInfo(_item, slots[_item.id].number);
            DataManager.instance.JsonSave();
            return;
        }
        slots[_item.id].isSet = true;
        slots[_item.id].gameObject.SetActive(true);
        Items.Add(new itemInfo(_item, num));
        slots[_item.id].AddItem(_item, num);
        DataManager.instance.JsonSave();
    }
    public void DropItem(Item _item, int num)
    {
        if(slots[_item.id].isSet && slots[_item.id].Item == _item)
        {
            slots[_item.id].Drop(num);
            Items[Items.FindIndex(x => x.item == _item)] = new itemInfo(_item, slots[_item.id].number);
        }
        DataManager.instance.JsonSave();
    }
    //item-----------------------------------------
    //chest----------------------------------------
    public void AddChest(Chest _chest, int num)
    {
        if (slots[_chest.id].isSet)
        {
            slots[_chest.id].AddChest(_chest, num);
            Chests[Chests.FindIndex(x => x.chest == _chest)] = new chestInfo(_chest, slots[_chest.id].number);
            DataManager.instance.JsonSave();
            return;
        }
        slots[_chest.id].isSet = true;
        slots[_chest.id].gameObject.SetActive(true);
        Chests.Add(new chestInfo(_chest, num));
        slots[_chest.id].AddChest(_chest, num);
        DataManager.instance.JsonSave();
    }
    public void DropChest(Chest _chest, int num)
    {
        if (slots[_chest.id].isSet && slots[_chest.id].Chest == _chest)
        {
            slots[_chest.id].Drop(num);
            Chests[Chests.FindIndex(x => x.chest == _chest)] = new chestInfo(_chest, slots[_chest.id].number);
        }
        DataManager.instance.JsonSave();
    }
    //chest----------------------------------------
    //weapon---------------------------------------
    public void AddWeapon(Weapon _weapon, int num)
    {
        if (slots[_weapon.id].isSet)
        {
            slots[_weapon.id].AddWeapon(_weapon, num);
            Weapons[Weapons.FindIndex(x => x.weapon == _weapon)] = new weaponInfo(_weapon, slots[_weapon.id].number);
            DataManager.instance.JsonSave();
            return;
        }
        slots[_weapon.id].gameObject.SetActive(true);
        slots[_weapon.id].isSet = true;
        Weapons.Add(new weaponInfo(_weapon, num));
        slots[_weapon.id].AddWeapon(_weapon, num);
        DataManager.instance.JsonSave();
    }
    public void DropWeapon(Weapon _weapon, int num)
    {
        if (slots[_weapon.id].isSet && slots[_weapon.id].Weapon == _weapon)
        {
            slots[_weapon.id].Drop(num);
            Weapons[Weapons.FindIndex(x => x.weapon == _weapon)] = new weaponInfo(_weapon, slots[_weapon.id].number);
        }
        DataManager.instance.JsonSave();
    }
    //weapon---------------------------------------
    #endregion
}