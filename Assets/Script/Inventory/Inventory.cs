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


    private static List<itemInfo> items = new List<itemInfo>();
    private static List<chestInfo> chests = new List<chestInfo>();
    private static List<weaponInfo> weapons = new List<weaponInfo>();

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    public inventoryType InventoryType { get => inventoryType; }
    public static List<itemInfo> Items { get => items; set => items = value; }
    public static List<chestInfo> Chests { get => chests; set => chests = value; }
    public static List<weaponInfo> Weapons { get => weapons; set => weapons = value; }
    private void OnDestroy()
    {
        items = null;
        chests = null;
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
    public static bool CheckNewWeapon(Weapon weapon)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (weapon == weapons[i].weapon) return false;
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
                slots[Items[i].item.id].NewAddItemInfo<Item>(Items[i].item, Items[i].num);
            }
        }
        if (inventoryType == inventoryType.chest)
        {
            for (int i = 0; i < Chests.Count; i++)
            {
                slots[Chests[i].chest.id].isSet = true;
                slots[Chests[i].chest.id].gameObject.SetActive(true);
                if (openPopupType == popupType.recipe && Chests[i].chest.id == 1) slots[Chests[i].chest.id].gameObject.SetActive(false);
                if (openPopupType == popupType.autoCraft && (Chests[i].chest.id == 0 || Chests[i].chest.id == 1)) slots[Chests[i].chest.id].gameObject.SetActive(false);
                if (Chests[i].num <= 0 && Chests[i].chest.id == 1)
                {
                    slots[Chests[i].chest.id].gameObject.SetActive(false);
                    continue;
                }
                slots[Chests[i].chest.id].NewAddItemInfo<Chest>(Chests[i].chest, Chests[i].num);
            }
        }
        if (inventoryType == inventoryType.weapon)
        {
            for (int i = 0; i < Weapons.Count; i++)//무기 초기화
            {
                slots[Weapons[i].weapon.id].isSet = true;
                slots[Weapons[i].weapon.id].gameObject.SetActive(true);
                slots[Weapons[i].weapon.id].NewAddItemInfo<Weapon>(Weapons[i].weapon, Weapons[i].num);
            }
        }
    }
    #region 아이템 추가 및 사용
    public void AddItems<T>(T _item, int num) where T : IInformation
    {
        string type = InfoManager.GetClassName(_item);
        if (slots[_item.GetId()].isSet)
        {
            slots[_item.GetId()].NewAddItemInfo(_item, num);
            SetInfo(_item,false, 0);
            DataManager.instance.JsonSave();
            return;
        }
        slots[_item.GetId()].isSet = true;
        slots[_item.GetId()].gameObject.SetActive(true);
        slots[_item.GetId()].NewAddItemInfo(_item, num);
        SetInfo(_item, true, num);
        DataManager.instance.JsonSave();
    }
    public void DropItems<T>(T _item, int num) where T : IInformation
    {
        if (slots[_item.GetId()].isSet)
        {
            slots[_item.GetId()].NewDrop(num);
            SetInfo(_item, false, 0);
        }
        DataManager.instance.JsonSave();
    }
    void SetInfo<T>(T _item, bool isNew, int num) where T : IInformation
    {
        string type = InfoManager.GetClassName(_item);
        switch (type)
        {
            case "Item":
                Item item = InfoManager.GetCharacter<Item>(_item);
                if(isNew) Items.Add(new itemInfo(item, num));
                else Items[Items.FindIndex(x => x.item == item)] = new itemInfo(item, slots[_item.GetId()].Number);
                break;
            case "Chest":
                Chest chest = InfoManager.GetCharacter<Chest>(_item);
                if (isNew) Chests.Add(new chestInfo(chest, num));
                else Chests[Chests.FindIndex(x => x.chest == chest)] = new chestInfo(chest, slots[_item.GetId()].Number);
                break;
            case "Weapon":
                Weapon weapon = InfoManager.GetCharacter<Weapon>(_item);
                if (isNew) Weapons.Add(new weaponInfo(weapon, num, 1, 0));
                else
                {
                    weaponInfo origin = Weapons[Weapons.FindIndex(x => x.weapon == weapon)];
                    Weapons[Weapons.FindIndex(x => x.weapon == weapon)] = new weaponInfo(weapon, slots[_item.GetId()].Number, origin.level, origin.enforceGauge);
                }
                break;
        }
    }
    #endregion
}