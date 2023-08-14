using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum inventoryType
{
    chest,
    ingrediant,
    weapon
}
public class Inventory : MonoBehaviour
{

    [SerializeField] GameObject slotPrefap;
    [SerializeField] inventoryType inventoryType;
    [SerializeField] popupType openPopupType;
    [SerializeField] bool isDraggble = true;
    [SerializeField] bool isShowCount = true;

    
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
    public inventoryType InventoryType { get => inventoryType; }
    public void Setup()
    {
        int count = GameManager.GetFullItemCount(inventoryType);
        Transform cavas = GameObject.Find("Canvas").transform;
        for (int i = 0; i < count; i++)
        {
            GameObject newSlot = Instantiate(slotPrefap, new Vector2(0, 0), Quaternion.identity, cavas);
            newSlot.transform.SetParent(slotParent);
        }
        slots = slotParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Id = i;
            slots[i].IsShowCount = isShowCount;
            slots[i].PopupType = openPopupType;
            slots[i].gameObject.SetActive(false);
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
    public void FreshSlot(List<ItemInfo> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].FreshSlot(true);
        }
        for(int i= 0; i < items.Count; i++)
        {
            slots[items[i].item.id].isSet = true;
            slots[items[i].item.id].gameObject.SetActive(true);
            slots[items[i].item.id].AddItemInfo(items[i].item, items[i].num);
            if(inventoryType == inventoryType.chest)//특수상자(이상한 상자, 시작의 상자) 예외처리
            {
                if (openPopupType == popupType.recipe && items[i].item.id == 1) slots[items[i].item.id].gameObject.SetActive(false);
                if (openPopupType == popupType.autoCraft && (items[i].item.id == 0 || items[i].item.id == 1)) slots[items[i].item.id].gameObject.SetActive(false);
                if (items[i].num <= 0 && items[i].item.id == 1)
                {
                    slots[items[i].item.id].gameObject.SetActive(false);
                    continue;
                }
            }
        }
    }
    #region 아이템 추가 및 사용
    public void AddItems(List<ItemInfo> items , Item _item, int num)
    {
        if (slots[_item.id].isSet)
        {
            slots[_item.id].AddItemInfo(_item, num);
            ModifyItemInInventory(items, _item, false, 0);
            DataManager.instance.JsonSave();
            return;
        }
        slots[_item.id].isSet = true;
        slots[_item.id].gameObject.SetActive(true);
        slots[_item.id].AddItemInfo(_item, num);
        ModifyItemInInventory(items, _item, true, num);
        DataManager.instance.JsonSave();
    }
    public void DropItems(List<ItemInfo> items , Item item, int num)
    {
        if (slots[item.id].isSet)
        {
            slots[item.id].Drop(num);
            ModifyItemInInventory(items, item, false, 0);
        }
        DataManager.instance.JsonSave();
    }
    void ModifyItemInInventory(List<ItemInfo> items, Item item, bool isNew, int num)
    {
        if (isNew) items.Add(new ItemInfo(item, num));
        else items[items.FindIndex(x => x.item == item)] = new ItemInfo(item, slots[item.id].Count);
        if (item is Weapon && isNew) ItemDatabaseManager.WeaponLevels.Add(new WeaponInfo(item, 1, 0));
    }
    #endregion
}